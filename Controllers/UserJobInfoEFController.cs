using AutoMapper;
using DOTNETAPI.Data;
using DOTNETAPI.Dtos;
using DOTNETAPI.Models;
using Microsoft.AspNetCore.Mvc;


namespace DOTNETAPI.Controllers;
    [ApiController]
    [Route("[controller]")]
    public class UserJobInfoEFController : ControllerBase
{
        DataContextEF _entityFrameWork;
        IMapper _mapper;
    public UserJobInfoEFController(IConfiguration config)
    {
        _entityFrameWork = new DataContextEF(config);

        _mapper = new Mapper(new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<UserToAddDto, User>();
        }));
    }


    [HttpGet("GetAllUserJobInfo")]

        public IEnumerable<UserJobInfo> GetAllUsersJobInfo()
        {
            IEnumerable<UserJobInfo> usersalljobinfo = _entityFrameWork.UserJobInfo.ToList<UserJobInfo>();
        return usersalljobinfo;

        }

        [HttpGet("GetUserJobInfo/{userId}")]

        public UserJobInfo GetUserJobInfo(int userId)
        {

            UserJobInfo? userJobInfo = _entityFrameWork.UserJobInfo
            .Where(u => u.UserId == userId)
            .FirstOrDefault<UserJobInfo>();

            if (userJobInfo != null)
            {
                return userJobInfo;
            }

            throw new Exception("Fail to get userjobinfo");


        }


        [HttpPut("EditUserJobInfo")]

        public IActionResult EditUser(UserJobInfo userjobinfo)
        {

            UserJobInfo? userJobInfoDb = _entityFrameWork.UserJobInfo
            .Where(u => u.UserId == userjobinfo.UserId)
            .FirstOrDefault<UserJobInfo>();

            if (userJobInfoDb != null)
            {
                userJobInfoDb.JobTitle = userjobinfo.JobTitle;
                userJobInfoDb.Department = userjobinfo.Department;

            if(_entityFrameWork.SaveChanges() > 0)
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

            _entityFrameWork.Add(userJobInfoDb);
            if (_entityFrameWork.SaveChanges() > 0)
            {
                return Ok();
            }

            throw new Exception("Fail to add user job info");

        }

        [HttpDelete("DeleteUserJobInfo/{userId}")]

        public IActionResult DeleteUserJobInfor(int userId)
        {

            UserJobInfo? userJobInfoDb = _entityFrameWork.UserJobInfo
            .Where(u => u.UserId == userId)
            .FirstOrDefault<UserJobInfo>();

                if (userJobInfoDb != null)
                {
                    _entityFrameWork.UserJobInfo.Remove(userJobInfoDb);

                    if (_entityFrameWork.SaveChanges() > 0)
                    {
                        return Ok();
                    }

                    throw new Exception("Fail to update userjobinfo");

                }


            throw new Exception("Fail to get userjobinfo");

        }




    }
