using DOTNETAPI.Data;
// using DOTNETAPI.Dtos;
using DOTNETAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace DOTNETAPI.Controllers;
[ApiController]
[Route("[controller]")]
public class UserSalaryController : ControllerBase
{
    DataContextDapper _dapper;
    public UserSalaryController(IConfiguration config)
    {
        _dapper = new DataContextDapper(config);
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




}
