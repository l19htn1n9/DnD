using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DnD.Models.Entities
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required, MaxLength(20)]
        public string Username { get; set; }
        [Required, MaxLength(100)]
        public string Password { get; set; }

        public virtual ICollection<Character> Characters { get; set; }
    }
}
