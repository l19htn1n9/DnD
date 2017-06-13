using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DnD.Models.DatabaseContext;
using DnD.Models.Entities;

namespace DnD.CoreApi.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private DndEntities _db = null;
        // GET api/user/5
        [HttpGet("{id}")]
        public User Get(int id)
        {
            if (_db == null)
                _db = new DndEntities();
            return _db.Users.Where(x => x.Id == id).FirstOrDefault();
        }
    }
}
