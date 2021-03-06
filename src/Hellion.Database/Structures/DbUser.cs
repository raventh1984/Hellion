﻿using System.ComponentModel.DataAnnotations.Schema;

namespace Hellion.Database.Structures
{
    [Table("users")]
    public class DbUser
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("username")]
        public string Username { get; set; }

        [Column("password")]
        public string Password { get; set; }

        [Column("authority")]
        public int Authority { get; set; }

        [Column("verification")]
        public bool Verification { get; set; }
    }
}
