
using AutoMapper;
using DOTNETAPI.Data;
using DOTNETAPI.Dtos;
using DOTNETAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace DOTNETAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UserEFController : ControllerBase
{

    IUserRepository _userRepository;
    IMapper _mapper;

    public UserEFController(IConfiguration config, IUserRepository userRepository)
    {
        _userRepository = userRepository;

        _mapper = new Mapper(new MapperConfiguration(cfg => {
            cfg.CreateMap<UserToAddDto, User>();
            cfg.CreateMap<UserSalary, UserSalary>();
            cfg.CreateMap<UserJobInfo, UserJobInfo>();
        }));
    }


    [HttpGet("GetUsers")]

    public IEnumerable<User> GetUsers()
    {
        IEnumerable<User> users = _userRepository.GetUsers();
        return users;
    }

    [HttpGet("GetSingleUser/{userId}")]
    public User GetSingleUser(int userId)
    {
        return _userRepository.GetSingleUser(userId);
    }

    [HttpPut("EditUser")]

    public IActionResult EditUser(User user)
    {


        User? userDb = _userRepository.GetSingleUser(user.UserId);

        if (userDb != null)
        {
            userDb.Active = user.Active;
            userDb.Firstname = user.Firstname;
            userDb.Lastname = user.Lastname;
            userDb.Email = user.Email;
            userDb.Gender = user.Gender;
            if (_userRepository.SaveChanges())
            {
                return Ok();
            } 

            throw new Exception("Fail to Update User");

        }


        throw new Exception("Fail to Get User");
    }

    [HttpPost("AddUser")]

    public IActionResult AddUser(UserToAddDto user)
    {

        User userDb = _mapper.Map<User>(user);
        
            _userRepository.AddEntity<User>(userDb);
            if (_userRepository.SaveChanges())
            {
                return Ok();
            }

            throw new Exception("Fail to Add User");

    }

    [HttpDelete("DeleteUser/{userId}")]

    public IActionResult DeleteUser(int userId)
    {

        User? userDb = _userRepository.GetSingleUser(userId);

            if (userDb != null)
            {
                _userRepository.RemoveEntity<User>(userDb);
                if (_userRepository.SaveChanges())
                {
                    return Ok();
                }

                throw new Exception("Fail to Delete User");

            }

            throw new Exception("Fail to Get User");

    }

    [HttpGet("GetAllUserSalary")]

    public IEnumerable<UserSalary> GetAllUsersSalary()
    {
        IEnumerable<UserSalary> usersalary = _userRepository.GetAllUsersSalary();
        return usersalary;

    }

    [HttpGet("GetUserSalary/{userId}")]

    public UserSalary GetUserSalary(int userId)
    {
        return  _userRepository.GetSingleUserSalary(userId);
    }


    [HttpPut("EditUserSalary")]

    public IActionResult EditUserSalary(UserSalary usersalary)
    {

        UserSalary? usersalaryDb = _userRepository.GetSingleUserSalary(usersalary.UserId);

        if (usersalaryDb != null)
        {
            _mapper.Map(usersalaryDb, usersalary);

            if (_userRepository.SaveChanges())
            {
                return Ok();
            }

            throw new Exception("Fail to update user salary");

        }

        throw new Exception("Fail to get user");
    }

    [HttpPost("AddUserSalary")]

    public IActionResult AddUserSalary(UserSalary usersalary)
    {
        UserSalary usersalaryoDb = _mapper.Map<UserSalary>(usersalary);

        _userRepository.AddEntity<UserSalary>(usersalaryoDb);
        if (_userRepository.SaveChanges())
        {
            return Ok();
        }

        throw new Exception("Fail to add user salary");

    }

    [HttpDelete("DeleteUserSalary/{userId}")]

    public IActionResult DeleteUserSalary(int userId)
    {

        UserSalary? usersalaryoDb = _userRepository.GetSingleUserSalary(userId);

        if (usersalaryoDb != null)
        {
            _userRepository.RemoveEntity<UserSalary>(usersalaryoDb);
            if (_userRepository.SaveChanges())
            {
                return Ok();
            }

            throw new Exception("Fail to update user salary");

        }


        throw new Exception("Fail to get user salary");

    }

    [HttpGet("GetAllUserJobInfo")]

    public IEnumerable<UserJobInfo> GetAllUsersJobInfo()
    {
        IEnumerable<UserJobInfo> usersalljobinfo = _userRepository.GetAllUsersJobInfo();
        return usersalljobinfo;

    }

    [HttpGet("GetUserJobInfo/{userId}")]

    public UserJobInfo GetUserJobInfo(int userId)
    {
      return _userRepository.GetSingleUserJobInfo(userId);
    }


    [HttpPut("EditUserJobInfo")]

    public IActionResult EditUser(UserJobInfo userjobinfo)
    {

        UserJobInfo? userJobInfoDb = _userRepository.GetSingleUserJobInfo(userjobinfo.UserId);

        if (userJobInfoDb != null)
        {
            userJobInfoDb.JobTitle = userjobinfo.JobTitle;
            userJobInfoDb.Department = userjobinfo.Department;

            if (_userRepository.SaveChanges())
            {
                return Ok();
            }

            throw new Exception("Fail to update userjobinfo");

        }


        throw new Exception("Fail to get userjobinfo");
    }

    [HttpPost("AddUserJobInfo")]

    public IActionResult AddJobInfo(UserJobInfo userjobinfo)
    {
        UserJobInfo userJobInfoDb = _mapper.Map<UserJobInfo>(userjobinfo);

        _userRepository.AddEntity<UserJobInfo>(userJobInfoDb);
        if (_userRepository.SaveChanges())
        {
            return Ok();
        }

        throw new Exception("Fail to add user job info");

    }

    [HttpDelete("DeleteUserJobInfo/{userId}")]

    public IActionResult DeleteUserJobInfo(int userId)
    {

        UserJobInfo? userJobInfoDb = _userRepository.GetSingleUserJobInfo(userId);

        if (userJobInfoDb != null)
        {
            _userRepository.RemoveEntity<UserJobInfo>(userJobInfoDb);
            if (_userRepository.SaveChanges())
            {
                return Ok();
            }

            throw new Exception("Fail to update userjobinfo");

        }


        throw new Exception("Fail to get userjobinfo");

    }

}
