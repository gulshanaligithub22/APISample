using AutoMapper;
using BusinessLayer.Model;
using BusinessLayer.Repository;
using DataAccessLayer.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

using System.Threading.Tasks;

namespace BusinessLayer
{
    public class UserBLL:  IUserRepository
    {
        private readonly UserDbContext _DbContext;
        private Mapper _mapper;
        public UserBLL()
        {
            _DbContext = new UserDbContext();
            var config = new MapperConfiguration(c => c.CreateMap<User, UserModel>().ReverseMap());
            //_mapper = new Mapper
        }
       
        public async Task<ActionResult<List<User>>> GetAllUsers()
        {
            var result =  await _DbContext.Users.ToListAsync();
            return result;
        }

        public async Task<ActionResult<User>> GetUserByID(int UserID)
        {
            var result = await _DbContext.Users.Where(x => x.UserID == UserID).FirstOrDefaultAsync();
            return result;
        }
    }
}
