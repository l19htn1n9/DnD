using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DnD.Models.DatabaseContext;
using DnD.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DnD.CoreApi.Controllers
{
    [Route("api/[controller]")]
    public class CharactersController : Controller
    {
        private DndEntities _db = null;

        // GET: api/values
        [HttpGet]
        public IEnumerable<Character> Get()
        {
            if (_db == null)
                _db = new DndEntities();
            return _db.Characters.ToList();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public Character Get(int id)
		{
			if (_db == null)
				_db = new DndEntities();
            return _db.Characters.Where(x => x.Id == id).FirstOrDefault();
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]Character character)
        {
            //insert new character
            _db.Characters.Add(character);
            _db.SaveChanges();
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]Character character)
        {
            //update character
            _db.Characters.Update(character);
            _db.SaveChanges();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var character = new Character { Id = id };
            _db.Entry(character).State = EntityState.Deleted;
            _db.SaveChanges();
        }
    }
}
