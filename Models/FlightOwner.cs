using System.ComponentModel.DataAnnotations;

namespace Simplifly.Models
{
    public class FlightOwner:IEquatable<FlightOwner>
    {
        [Key]
        public int OwnerId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        // Additional Fields for Flight Owner
        public string CompanyName { get; set; } = string.Empty;
        public string ContactNumber { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string BusinessRegistrationNumber { get; set; } = string.Empty;


        // Navigation Properties
        public List<Flight>? OwnedFlights { get; set; }

        public FlightOwner()
        {
            OwnerId = 0;
        }
        public FlightOwner(int ownerId, string name, string email, string companyName, string contactNumber, string address, string businessRegistrationNumber, List<Flight> ownedFlights,string password)
        {
            OwnerId = ownerId;
            Name = name;
            Email = email;
            CompanyName = companyName;
            ContactNumber = contactNumber;
            Address = address;
            BusinessRegistrationNumber = businessRegistrationNumber;
            OwnedFlights = ownedFlights;
            Password = password;
        }
        public FlightOwner( string name, string email, string companyName, string contactNumber, string address, string businessRegistrationNumber, List<Flight> ownedFlights,string password)
        {
            Name = name;
            Email = email;
            CompanyName = companyName;
            ContactNumber = contactNumber;
            Address = address;
            BusinessRegistrationNumber = businessRegistrationNumber;
            OwnedFlights = ownedFlights;
            Password = password;
        }
       

        public bool Equals(FlightOwner? other)
        {
            var Owner = other ?? new FlightOwner();
            return OwnerId.Equals(Owner.OwnerId) && BusinessRegistrationNumber.Equals(Owner.BusinessRegistrationNumber);
        }
    }
}
