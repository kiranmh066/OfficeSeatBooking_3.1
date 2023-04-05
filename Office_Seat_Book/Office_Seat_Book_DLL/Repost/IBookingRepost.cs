using Office_Seat_Book_Entity;
using System;
using System.Collections.Generic;

namespace Office_Seat_Book_DLL.Repost
{
    public interface IBookingRepost
    {
        void UpdateBooking(Booking booking);

        void DeleteBooking(int bookingId);

        Booking GetBookingById(int bookingId);

        IEnumerable<Booking> GetBookings();

        int AddBooking(Booking booking);

        Booking GetBookingByEmpId(int EmpId);
        IEnumerable<Booking> GetBookingsByDate(DateTime date1);


    }
}
