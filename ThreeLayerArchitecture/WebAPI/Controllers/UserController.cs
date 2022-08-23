using BusinessLayer;
using BusinessLayer.Model;
using DataAccessLayer.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Text.Json;

namespace WebAPI.Controllers
{
    [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly BLL_Users _userBLL;
        public UserController( )
        {
            _userBLL = new BLL_Users();
        }
        [HttpGet]
        [Route("GetUsers")]
        
        public async Task<List<UserModel>> GetAllUsers()
        {
            var result = await _userBLL.GetAllUsers();
            return result;

        }
        [HttpGet]
        [Route("GetUsersByID")]
        public async Task<ActionResult<UserModel>> GetUserByID(int UserID)
        {
            var result = await _userBLL.GetUserByID(UserID);
            return result.Value == null ? NotFound() : Ok(result.Value);
        }


        [HttpPost]
        public async Task Create([FromBody] UserModel userModel)
        {
          await _userBLL.PostUser(userModel);
        }


        [HttpPut]
        public async Task<ActionResult<bool>> updateuser(UserModel userModel)
        {
         var result =  await _userBLL.updateUser(userModel);
            return result.Value == false ? NotFound() : Ok(result.Value);
        }

        [HttpDelete]
        public async Task<ActionResult<bool>> DelUser(int id)
        {
          var result =   await _userBLL.DelUser(id);
            return result.Value == false ? NotFound() : Ok(result.Value);
        }

        [HttpGet]
        [Route("GetAllP")]
        public async Task<ActionResult<List<UserModel>>> GetAllP()
        {
            var result = await _userBLL.GetAllp();
            if (result.Value == null)
            {
                return NotFound();
            }
            else
            {
                if (result.Value.Count() == 0)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(result.Value);
                }
            }
        }

        [HttpGet]
        [Route("GetDyanamicColumn")]
        public async Task<ActionResult<object>> GetAllDynamicColumn()
        {
            var result = await _userBLL.GetAllDynamicColumn();
            return result.Value == null ? NotFound() : Ok(result.Value);
        }
    }
}
