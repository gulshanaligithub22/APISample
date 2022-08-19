using DataAccessLayer.Entity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Repository
{
     interface IUserRepository
    {
        Task<ActionResult<List<User>>> GetAllUsers();

        Task<ActionResult<User>> GetUserByID(int UserID);

    }
}
