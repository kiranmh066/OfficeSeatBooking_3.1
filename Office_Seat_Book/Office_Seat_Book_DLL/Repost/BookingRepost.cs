using Microsoft.EntityFrameworkCore;
using Office_Seat_Book_Entity;
using System.Collections.Generic;
using System.Linq;

namespace Office_Seat_Book_DLL.Repost
{
    public class BookingRepost : IBookingRepost
    {

        Office_DB_Context _dbContext;//default private

        public BookingRepost(Office_DB_Context dbContext)
        {
            _dbContext = dbContext;
        }
        public int AddBooking(Booking booking)
        {
            _dbContext.booking.Add(booking);
            _dbContext.SaveChanges();
            List<Booking> list = new List<Booking>();
            list = _dbContext.booking.ToList();
            var booking1 = (from list1 in list
                               select list1).Last();
            return booking1.BookingID;
        }

        public void DeleteBooking(int bookingId)
        {
            var booking = _dbContext.booking.Find(bookingId);
            _dbContext.booking.Remove(booking);
            _dbContext.SaveChanges();
        }

        public Booking GetBookingById(int bookingId)
        {
            return _dbContext.booking.Find(bookingId);
        }

        public IEnumerable<Booking> GetBookings()
        {
            return _dbContext.booking.ToList();
        }


        public void UpdateBooking(Booking booking)
        {

            _dbContext.Entry(booking).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _dbContext.SaveChanges();
        }
        public Booking GetBookingByEmpId(int empId)
        {
            List<Booking> bookings = _dbContext.booking.Include(obj => obj.employee).ToList();
            Booking booking = new Booking();
            foreach(var item in bookings)
            {   if(item.EmployeeID == empId)
                booking = item;
            }
            return booking;
        }
    }
}
