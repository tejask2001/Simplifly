﻿using Simplifly.Models;
using Simplifly.Models.DTOs;

namespace Simplifly.Mappers
{
    public class RegisterToAdmin
    {
        Admin admin;
        public RegisterToAdmin(RegisterAdminUserDTO register)
        {
            admin = new Admin();
            admin.Name = register.Name;
            admin.Email = register.Email;
            admin.Position = register.Position;
            admin.ContactNumber = register.ContactNumber;
            admin.Address = register.Address;
            admin.Username = register.Username;
        }
        public Admin GetAdmin()
        {

            return admin;
        }
    }
}