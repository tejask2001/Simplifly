﻿using System.Diagnostics.CodeAnalysis;

namespace Simplifly.Models.DTO_s
{
    [ExcludeFromCodeCoverage]
    public class UpdateFlightOwnerDTO
    {
        public int OwnerId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string CompanyName { get; set; } = string.Empty;
        public string ContactNumber { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string BusinessRegistrationNumber { get; set; } = string.Empty;
        public string Username { get; set; }
    }
}
