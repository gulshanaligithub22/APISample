using BusinessLayer;
using BusinessLayer.Model;
using DataAccessLayer.Entity;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private  UserBLL _userBLL;
        //private Mapper _mapper;
        public UserController()
        {
            _userBLL = new UserBLL();
            //var config = new MapperConfiguration(c => c.CreateMap<User, UserModel>().ReverseMap());
            //_mapper = new Mapper(config);
        }
        [HttpGet]
        [Route("GetUsers")]
        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<User>>>  GetUsers()
        {
            var result = await _userBLL.GetAllUsers();
            return result.Value == null ? NotFound() : Ok(result.Value);

        }
        [HttpGet]
        [Route("GetUsersByID")]
        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<User>> UserByID( int UserID)
        {
            var result = await _userBLL.GetUserByID(UserID);
            return result.Value == null ? NotFound() : Ok(result.Value);
        }

    }
}
