using Office_Seat_Book_DLL.Repost;
using Office_Seat_Book_Entity;
using System;
using System.Collections.Generic;

namespace Office_Seat_Book_BLL.Services
{
    public class BookingService
    {
        IBookingRepost _bookingRepost;
        public BookingService(IBookingRepost bookingRepost)
        {
            _bookingRepost = bookingRepost;
        }

        //Add Appointment
        public int AddBooking(Booking booking)
        {
            return _bookingRepost.AddBooking(booking);
        }

        //Delete Appointment

        public void DeleteBooking(int bookingID)
        {
            _bookingRepost.DeleteBooking(bookingID);
        }

        //Update Appointment

        public void UpdateBooking(Booking booking)
        {
            _bookingRepost.UpdateBooking(booking);
        }

        //Get getAppointments

        public IEnumerable<Booking> GetBookings()
        {
            return _bookingRepost.GetBookings();
        }
        public Booking GetBookingById(int bookingID)
        {
            return _bookingRepost.GetBookingById(bookingID);
        }


        public Booking GetBookingByEmpId(int EmpId)
        {
            return _bookingRepost.GetBookingByEmpId(EmpId);
        }


        public IEnumerable<Booking> GetBookingsByDate(DateTime date1)
        {
            return _bookingRepost.GetBookingsByDate(date1);

        }
    }
}
