using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DnD.Models.Repository;
using DnD.Models.Entities;

namespace DnD.CoreApi.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        UserRepository userRepo = new UserRepository();
        // GET api/user/5
        [HttpGet("{id}")]
        public User Get(int id)
        {
            return userRepo.GetUser(id);
        }
    }
}
