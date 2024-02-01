using System.ComponentModel.DataAnnotations;

namespace Simplifly.Models
{
    public class User:IEquatable<User>
    {
        [Key]
        public int UserId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; }= string.Empty;
        public string? Phone { get; set; }
        public string? Gender { get; set; }

        //Navigation
        public List<Booking>? Bookings { get; set; }

        public User()
        {
        }
        public User( string name, string email, string? phone,string? gender)
        {
 
            Name = name;
            Email = email;
            Phone = phone;
            Gender = gender;
        }

        public User(int userId, string name, string email, string? phone,string? gender)
        {
            UserId = userId;
            Name = name;
            Email = email;
            Phone = phone;
            Gender = gender;
        }

        public bool Equals(User? other)
        {
            var User = other ?? new User();
            return this.UserId.Equals(User.UserId) && this.Email.Equals(User.Email);
        }
    }
}
