using AutoMapper;
using DOTNETAPI.Data;
using DOTNETAPI.Dtos;
using DOTNETAPI.Models;
using Microsoft.AspNetCore.Mvc;


namespace DOTNETAPI.Controllers;
    [ApiController]
    [Route("[controller]")]
    public class UserSalaryEFController : ControllerBase
{
        DataContextEF _entityFrameWork;
        IMapper _mapper;
    public UserSalaryEFController(IConfiguration config)
    {
        _entityFrameWork = new DataContextEF(config);

        _mapper = new Mapper(new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<UserToAddDto, User>();
        }));
    }


    [HttpGet("GetAllUserSalary")]

        public IEnumerable<UserSalary> GetAllUsersSalary()
        {
            IEnumerable<UserSalary> usersalary = _entityFrameWork.UserSalary.ToList<UserSalary>();
        return usersalary;

        }

        [HttpGet("GetUserSalary/{userId}")]

        public UserSalary GetUserSalary(int userId)
        {

            UserSalary? usersalary = _entityFrameWork.UserSalary
            .Where(u => u.UserId == userId)
            .FirstOrDefault<UserSalary>();

            if (usersalary != null)
            {
                return usersalary;
            }

            throw new Exception("Fail to get UserSalary");


        }


        [HttpPut("EditUserSalary")]

        public IActionResult EditUserSalary(UserSalary usersalary)
        {

            UserSalary? usersalaryDb = _entityFrameWork.UserSalary
            .Where(u => u.UserId == usersalary.UserId)
            .FirstOrDefault<UserSalary>();

            if (usersalaryDb != null)
            {
                usersalaryDb.Salary = usersalary.Salary;

            if(_entityFrameWork.SaveChanges() > 0)
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

            _entityFrameWork.Add(usersalaryoDb);
            if (_entityFrameWork.SaveChanges() > 0)
            {
                return Ok();
            }

            throw new Exception("Fail to add user salary");

        }

        [HttpDelete("DeleteUserSalary/{userId}")]

        public IActionResult DeleteUserJobInfor(int userId)
        {

            UserSalary? usersalaryoDb = _entityFrameWork.UserSalary
            .Where(u => u.UserId == userId)
            .FirstOrDefault<UserSalary>();

                if (usersalaryoDb != null)
                {
                    _entityFrameWork.UserSalary.Remove(usersalaryoDb);

                    if (_entityFrameWork.SaveChanges() > 0)
                    {
                        return Ok();
                    }

                    throw new Exception("Fail to update user salary");

                }


            throw new Exception("Fail to get user salary");

        }




    }
