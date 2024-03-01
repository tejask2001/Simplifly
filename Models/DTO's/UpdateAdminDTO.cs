﻿namespace Simplifly.Models.DTO_s
{
    public class UpdateAdminDTO
    {
        public int AdminId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Position { get; set; } = string.Empty;
        public string? ContactNumber { get; set; }
        public string? Address { get; set; }
    }
}
