using System.ComponentModel.DataAnnotations;

namespace Simplifly.Models
{
    public class User:IEquatable<User>
    {
        [Key]
        public int UserId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; }= string.Empty;

        public string Password {  get; set; } = string.Empty;
        public string? Phone { get; set; }
        public string? Gender { get; set; }

        //Navigation
        public List<Booking>? Bookings { get; set; }

        public User()
        {
            UserId = 0;
        }
        public User( string name, string email, string? phone,string? gender,string password)
        {

 
            Name = name;
            Email = email;
            Phone = phone;
            Gender = gender;
            Password = password;
        }

        public User(int userId, string name, string email, string? phone,string? gender,string password)
        {
            UserId = userId;
            Name = name;
            Email = email;
            Phone = phone;
            Gender = gender;
            Password= password;
        }

        public bool Equals(User? other)
        {
            var User = other ?? new User();
            return this.UserId.Equals(User.UserId) && this.Email.Equals(User.Email);
        }
    }
}
