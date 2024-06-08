
using DOTNETAPI.Data;
using DOTNETAPI.Dtos;
using DOTNETAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace DOTNETAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    DataContextDapper _dapper;

    public UserController(IConfiguration config)
    {
        _dapper = new DataContextDapper(config);
    }

    [HttpGet("TestConnection")]
    public DateTime TestConnection()
    {
        return _dapper.LoadDataSingle<DateTime>("SELECT GETDATE()");
    }


    [HttpGet("GetUsers")]

    public IEnumerable<User> GetUsers()
    {
        string sql = @"SELECT * FROM TutorialAppSchema.Users";
        IEnumerable<User> users = _dapper.LoadData<User>(sql);
        return users;

    }

    [HttpGet("GetSingleUser/{userId}")]
    public User GetSingleUser(int userId)
    {

        string sql = @"SELECT * FROM TutorialAppSchema.Users WHERE Users.userId =" + userId.ToString();
        User user = _dapper.LoadDataSingle<User>(sql);
        return user;

    }

    [HttpPut("EditUser")]

    public IActionResult EditUser(User user)
    {
        string sql = @"UPDATE TutorialAppSchema.Users
                SET [FirstName] = '" + user.Firstname + 
                    "',[LastName] = '" + user.Lastname + 
                    "',[Email] = '" + user.Email +
                    "',[Gender] = '" + user.Gender + 
                    "',[Active] = '" + user.Active +
                    "' WHERE UserId = " + user.UserId;

                    // Console.WriteLine(sql);

        if (_dapper.ExecuteSql(sql))
        {
        return Ok();
        } 

        throw new Exception("Fail to update user");
    }

    [HttpPost("AddUser")]

    public IActionResult AddUser(UserToAddDto user)
    {
        string sql = @"INSERT TutorialAppSchema.Users(
                [FirstName],
                [LastName],
                [Email],
                [Gender], 
                [Active]
            ) VALUES (" +
                "'" + user.Firstname +
                "', '" + user.Lastname +
                "', '" + user.Email +
                "', '" + user.Gender +
                "', '" + user.Active +
            "')";

        Console.WriteLine(sql);

        if (_dapper.ExecuteSql(sql))
        {
            return Ok();
        }

        throw new Exception("Fail to add user");

    }

    [HttpDelete("DeleteUser/{userId}")]

    public IActionResult DeleteUser(int userId)
    {

        string sql = @"DELETE FROM TutorialAppSchema.Users WHERE Users.userId =" + userId.ToString();

        Console.WriteLine(sql);

        if (_dapper.ExecuteSql(sql))
        {
            return Ok();
        }

        throw new Exception("Fail to Delete user");


    }

    [HttpGet("GetAllUserSalary")]

    public IEnumerable<UserSalary> GetAllUserSalary()
    {
        string sql = @"SELECT * FROM TutorialAppSchema.UserSalary";
        IEnumerable<UserSalary> usersalary = _dapper.LoadData<UserSalary>(sql);
        return usersalary;

    }

    [HttpGet("GetUserSalary/{userId}")]

    public UserSalary GetUserSalary(int userId)
    {

        string sql = @"SELECT * FROM TutorialAppSchema.UserSalary WHERE UserSalary.userId =" + userId.ToString();
        UserSalary usersalary = _dapper.LoadDataSingle<UserSalary>(sql);
        return usersalary;

    }


    [HttpPut("EditUserSalary")]

    public IActionResult EditUserSalary(UserSalary usersalary)
    {
        string sql = @"UPDATE TutorialAppSchema.UserJobInfo
                SET [Salary] = '" + usersalary.Salary +
                    "' WHERE UserId = " + usersalary.UserId;

        // Console.WriteLine(sql);

        if (_dapper.ExecuteSql(sql))
        {
            return Ok();
        }

        throw new Exception("Fail to update UserSalary");
    }

    [HttpPost("AddUserSalary")]

    public IActionResult AddJobInfo(UserSalary usersalary)
    {
        string sql = @"INSERT TutorialAppSchema.UserSalary(
                [UserId],
                [Salary]
            ) VALUES (" + usersalary.UserId
                + ", '" + usersalary.Salary +
            "')";

        Console.WriteLine(sql);

        if (_dapper.ExecuteSql(sql))
        {
            return Ok(usersalary);
        }

        throw new Exception("Fail to add user UserSalary");

    }

    [HttpDelete("DeleteUserSalary/{userId}")]

    public IActionResult DeleteUserSalary(int userId)
    {

        string sql = @"DELETE FROM TutorialAppSchema.UserSalary WHERE UserSalary.userId =" + userId.ToString();

        Console.WriteLine(sql);

        if (_dapper.ExecuteSql(sql))
        {
            return Ok();
        }

        throw new Exception("Fail to Delete UserSalary");


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
