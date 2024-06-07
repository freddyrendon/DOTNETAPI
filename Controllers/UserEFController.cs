
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
    DataContextEF _entityFrameWork;
    IMapper _mapper;

    public UserEFController(IConfiguration config)
    {
        _entityFrameWork = new DataContextEF(config);

        _mapper = new Mapper(new MapperConfiguration(cfg => {
            cfg.CreateMap<UserToAddDto, User>();
        }));
    }


    [HttpGet("GetUsers")]

    public IEnumerable<User> GetUsers()
    {
        IEnumerable<User> users = _entityFrameWork.Users.ToList<User>();
        return users;
    }

    [HttpGet("GetSingleUser/{userId}")]
    public User GetSingleUser(int userId)
    {

        User? user = _entityFrameWork.Users
        .Where( u => u.UserId == userId)
        .FirstOrDefault<User>();

        if (user != null)
        {
        return user;
        }

        throw new Exception("Fail to get User");
    }

    [HttpPut("EditUser")]

    public IActionResult EditUser(User user)
    {


        User? userDb = _entityFrameWork.Users
            .Where(u => u.UserId == user.UserId)
            .FirstOrDefault<User>();

        if (userDb != null)
        {
            userDb.Active = user.Active;
            userDb.Firstname = user.Firstname;
            userDb.Lastname = user.Lastname;
            userDb.Email = user.Email;
            userDb.Gender = user.Gender;
            if (_entityFrameWork.SaveChanges() > 0)
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
        
            _entityFrameWork.Add(userDb);
            if (_entityFrameWork.SaveChanges() > 0)
            {
                return Ok();
            }

            throw new Exception("Fail to Add User");

    }

    [HttpDelete("DeleteUser/{userId}")]

    public IActionResult DeleteUser(int userId)
    {

        User? userDb = _entityFrameWork.Users
            .Where(u => u.UserId == userId)
            .FirstOrDefault<User>();

            if (userDb != null)
            {
                _entityFrameWork.Users.Remove(userDb);

                if (_entityFrameWork.SaveChanges() > 0)
                {
                    return Ok();
                }

                throw new Exception("Fail to Delete User");

            }

            throw new Exception("Fail to Get User");

    }


}
