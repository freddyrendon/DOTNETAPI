using DOTNETAPI.Data;
// using DOTNETAPI.Dtos;
using DOTNETAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace DOTNETAPI.Controllers;
    [ApiController]
    [Route("[controller]")]
    public class UserJobInfoController : ControllerBase
    {
        DataContextDapper _dapper;
        public UserJobInfoController(IConfiguration config)
        {
            _dapper = new DataContextDapper(config);
        }


        [HttpGet("GetAllUserJobInfo")]

        public IEnumerable<UserJobInfo> GetAllUsersJobInfo()
        {
            string sql = @"SELECT * FROM TutorialAppSchema.UserJobInfo";
            IEnumerable<UserJobInfo> usersalljobinfo = _dapper.LoadData<UserJobInfo>(sql);
            return usersalljobinfo;

        }

        [HttpGet("GetUserJobInfo/{userId}")]

        public UserJobInfo GetUserJobInfo(int userId)
        {

            string sql = @"SELECT * FROM TutorialAppSchema.UserJobInfo WHERE UserJobInfo.userId =" + userId.ToString();
            UserJobInfo userJobInfo = _dapper.LoadDataSingle<UserJobInfo>(sql);
            return userJobInfo;

        }


        [HttpPut("EditUserJobInfo")]

        public IActionResult EditUser(UserJobInfo userjobinfo)
        {
            string sql = @"UPDATE TutorialAppSchema.UserJobInfo
                SET [JobTitle] = '" + userjobinfo.JobTitle +
                        "',[Department] = '" + userjobinfo.Department +
                        "' WHERE UserId = " + userjobinfo.UserId;

            // Console.WriteLine(sql);

            if (_dapper.ExecuteSql(sql))
            {
                return Ok();
            }

            throw new Exception("Fail to update userjobinfo");
        }

        [HttpPost("AddUserJobInfo")]

        public IActionResult AddJobInfo(UserJobInfo userjobinfo)
        {
            string sql = @"INSERT TutorialAppSchema.UserJobInfo(
                [UserId],
                [JobTitle],
                [Department]
            ) VALUES (" + userjobinfo.UserId 
                    + ", '" + userjobinfo.JobTitle +
                    "', '" + userjobinfo.Department +
                "')";

            Console.WriteLine(sql);

            if (_dapper.ExecuteSql(sql))
            {
                return Ok(userjobinfo);
            }

            throw new Exception("Fail to add user job info");

        }

        [HttpDelete("DeleteUserJobInfo/{userId}")]

        public IActionResult DeleteUserJobInfor(int userId)
        {

            string sql = @"DELETE FROM TutorialAppSchema.UserJobInfo WHERE UserJobInfo.userId =" + userId.ToString();

            Console.WriteLine(sql);

            if (_dapper.ExecuteSql(sql))
            {
                return Ok();
            }

            throw new Exception("Fail to Delete UserJobInfo");


        }




    }
