﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MovieRegistrationDatabase.Models
{
    public class Movie
    {
        [Required]
        [Key]
        public int ID { get; set; }
        [Required]
        [MaxLength(30)]
        public string Title { get; set; }
        public string Genre { get; set; }
        [Range(1880, 2021)]
        public int Year { get; set; }
        public int Runtime { get; set; }
    }
}
