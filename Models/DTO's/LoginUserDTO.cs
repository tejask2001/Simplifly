﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Simplifly.Models.DTOs
{
    public class LoginUserDTO
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}