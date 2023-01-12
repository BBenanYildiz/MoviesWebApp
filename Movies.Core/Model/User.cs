﻿using System.ComponentModel.DataAnnotations;

namespace Movies.Core.Model
{
    public class User:BaseEntity
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Mail { get; set; }
        public string Password { get; set; }
    }
}
