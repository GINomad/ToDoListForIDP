using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ToDoList.Repositories;

namespace ToDoList.Controllers
{
    [System.Web.Http.Route("api/user")]
    public class UserApiController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
      
        public UserApiController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [Route("api/user/getusers/{taskId}")]
        public IHttpActionResult GetUsers(int taskId)
        {
            var users = _unitOfWork.Users.GetNonAssignedUsers(taskId);

            return Ok(users);
        }

        [HttpGet]
        [Route("api/user/getStatistic")]
        public IHttpActionResult GetStatistic()
        {
            var result = _unitOfWork.Users.GetUserStatistic();
            return Ok(result);
        }
    }
}
