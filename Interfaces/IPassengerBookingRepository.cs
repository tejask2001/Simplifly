﻿using Simplifly.Models;

namespace Simplifly.Interfaces
{
    public interface IPassengerBookingRepository
    {

        Task AddPassengerBookingAsync(PassengerBooking passengerBooking);
        Task<IEnumerable<PassengerBooking>> GetPassengerBookingsAsync(int bookingId);
        Task RemovePassengerBookingAsync(int passengerBookingId);
    }
}
