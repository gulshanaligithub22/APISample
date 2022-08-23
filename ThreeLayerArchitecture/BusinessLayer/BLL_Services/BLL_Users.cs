using AutoMapper;
using BusinessLayer.Model;
using DataAccessLayer;
using DataAccessLayer.Entity;
using DataAccessLayer.ViewClass;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class BLL_Users: ControllerBase
    {
        private readonly  DAL_Users _context;
        private readonly IMapper _mapper;
        public BLL_Users()
        {
            _context = new DAL_Users();
            var config = new MapperConfiguration(c => c.CreateMap<User, UserModel>().ReverseMap());
            _mapper = new Mapper(config);
        }

        #region Users
        public async Task<List<UserModel>> GetAllUsers()
        {
            List<User> users = await _context.GetUsers();
            List<UserModel> view = _mapper.Map<List<User>, List<UserModel>>(users);
            return view;
        }

        public async Task<ActionResult<UserModel>> GetUserByID(int id)
        {
            var user = await _context.GetUserByID(id);
            var UserModel = _mapper.Map<User, UserModel>(user.Value);
            return UserModel;
        }

        public async Task PostUser(UserModel usermodel)
        {
            User User = _mapper.Map<UserModel, User>(usermodel);
           await _context.CreateUser(User);
        }

        public async Task<ActionResult<bool>> updateUser(UserModel userModel)
        {
            User user = _mapper.Map<UserModel, User>(userModel);
         var result =    await _context.UpdateUser(user);
            return result;
        }

        public async Task<ActionResult<bool>> DelUser(int id)
        {
          bool result =  await _context.DeleteUser(id);
            return result;

        }
        public async Task<ActionResult<List<UserModel>>> GetAllp()
        {
            List<UserView_POCO> users = await _context.GetAllbyProc();
            List<UserModel> userModels = _mapper.Map<List<UserView_POCO>, List<UserModel>>(users);
            return userModels;
        }

        public async Task<ActionResult<object>> GetAllDynamicColumn()
        {
          var res = await _context.GetAllDynamicColumn();
            return res;
        }
        #endregion
    }
}
