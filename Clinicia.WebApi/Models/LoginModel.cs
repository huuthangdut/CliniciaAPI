﻿using System.ComponentModel.DataAnnotations;

namespace Clinicia.WebApi.Models
{
    public class LoginModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        public bool IsUserLogin { get; set; } = true;
    }
}
