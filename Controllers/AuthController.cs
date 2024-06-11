using System.Data;
using System.Security.Cryptography;
using System.Text;
using DOTNETAPI.Data;
using DOTNETAPI.Dtos;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.ObjectPool;

namespace DOTNETAPI.Controllers
{
    public class AuthController : ControllerBase
    {
        private readonly DataContextDapper _dapper;

        private readonly IConfiguration _config;
        public AuthController(IConfiguration config){
                  _dapper = new DataContextDapper(config);
                  _config = config;
        }

        [HttpPost("Register")]

        public ActionResult Register(UserForRegistrationDto userForRegistration)
        {
            if (userForRegistration.Password == userForRegistration.PasswordConfirm)
            {

                string sqlCheckIfUserExist = @"SELECT Email FROM TutorialAppSchema.Auth WHERE EMAIL = '" + userForRegistration.Email + "'";

                IEnumerable<string> existingUsers = _dapper.LoadData<string>(sqlCheckIfUserExist);
                if (existingUsers.Count() == 0)
                {

                    byte[] passwordSalt = new byte[128 / 8];
                    using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
                    {

                        rng.GetNonZeroBytes(passwordSalt);

                    }

                    string passwordSaltPlusString = _config.GetSection("AppSettings:PasswordKey").Value + 
                    Convert.ToBase64String(passwordSalt);

                    byte[] passwordHash = GetPasswordHash(userForRegistration.Password, passwordSalt);

                    string sqlAddAuth = @"INSERT INTO TutorialAppSchema.Auth (
                                            [EMail],
                                            [PasswordHash],
                                            [PasswordSalt]
                                        ) VALUES ('" + userForRegistration.Email + "', @PasswordHash, @PasswordSalt)";
                    List<SqlParameter> sqlParameters = new List<SqlParameter>();

                    SqlParameter passwordSaltParameter = new SqlParameter("@PasswordSalt", SqlDbType.VarBinary);
                    passwordSaltParameter.Value = passwordSalt;

                    SqlParameter passwordHashParameter = new SqlParameter("@PasswordHash", SqlDbType.VarBinary);
                    passwordHashParameter.Value = passwordHash;

                    sqlParameters.Add(passwordSaltParameter);
                    sqlParameters.Add(passwordHashParameter);

                    if(_dapper.ExecuteSqlWithParameters(sqlAddAuth, sqlParameters))
                    {
                        string sqlAddUser = @"INSERT TutorialAppSchema.Users(
                                            [FirstName],
                                            [LastName],
                                             [Email],
                                            [Gender], 
                                            [Active]
                                        ) VALUES (" +
                                    "'" + userForRegistration.Firstname +
                                    "', '" + userForRegistration.Lastname +
                                    "', '" + userForRegistration.Email +
                                    "', '" + userForRegistration.Gender +
                                "', 1)";
                        if (_dapper.ExecuteSql(sqlAddUser)){
                            return Ok();
                        }
                        throw new Exception("Failed to Add user!");
                    }
                    throw new Exception("Failed to register user!");
                }
                throw new Exception("User with this email already exists!");
            }
            throw new Exception("Passwords Do Not Match!");
        }

        [HttpPost("Login")]

        public ActionResult Login(UserForLoginDto userForLogin)
        {
            string sqlForHashAndSalt = @"SELECT [PasswordHash],
                                        [PasswordSalt] FROM TutorialAppSchema.Auth WHERE EMAIL = '"+ userForLogin.Email + "'";

            UserForLoginConfirmDto userForLoginConfirm = _dapper
                .LoadDataSingle<UserForLoginConfirmDto>(sqlForHashAndSalt);

            byte[] passwordHash = GetPasswordHash(userForLogin.Password, userForLoginConfirm.PasswordSalt);

            for (int i = 0; i < passwordHash.Length; i++){
                if (passwordHash[i] != userForLoginConfirm.PasswordHash[i])
                {
                    return StatusCode(401,"Password was incorrect!");
                }
            }


            return Ok();
        }

        private byte[] GetPasswordHash(string password, byte[] passwordSalt)
        {
            string passwordSaltPlusString = _config.GetSection("AppSettings:PasswordKey").Value +
                Convert.ToBase64String(passwordSalt);

                return KeyDerivation.Pbkdf2(
                password: password,
                salt: Encoding.ASCII.GetBytes(passwordSaltPlusString),
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8
            );
        }



    }

}