using System;
using System.ComponentModel.DataAnnotations;

namespace DnD.Models.Entities
{
    public class Character
    {
        [Key]
        public int Id { get; set; }
        [Required, MaxLength(50)]
        public string Name { get; set; }
        public int Strength { get; set; }
        public int Dexterity { get; set; }
        public int Constitution { get; set; }
        public int Intelligence { get; set; }
        public int Wisdom { get; set; }
        public int Charisma { get; set; }

        public virtual User User { get; set; }
        public int UserId { get; set; }
    }
}
