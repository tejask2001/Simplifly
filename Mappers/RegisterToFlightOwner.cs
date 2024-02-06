﻿using Simplifly.Models.DTOs;
using Simplifly.Models;

namespace Simplifly.Mappers
{
    public class RegisterToFlightOwner
    {
        FlightOwner flightowner;
        public RegisterToFlightOwner(RegisterFlightOwnerUserDTO register)
        {
            flightowner = new FlightOwner();
            flightowner.Name = register.Name;
            flightowner.Email = register.Email;
            flightowner.CompanyName = register.CompanyName;
            flightowner.ContactNumber = register.ContactNumber;
            flightowner.Address = register.Address;
            flightowner.BusinessRegistrationNumber = register.BusinessRegistrationNumber;
            flightowner.Username = register.Username;
        }
        public FlightOwner GetFlightOwner()
        {

            return flightowner;
        }
    }
}
