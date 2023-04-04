using Microsoft.EntityFrameworkCore;
using Office_Seat_Book_Entity;
using System;
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

        public Booking GetBookingByEmpId(int EmpId)
        {
            List<Booking> bookings = new List<Booking>();
            List<Booking> bookings1 = new List<Booking>();

            bookings = _dbContext.booking.Include(obj=>obj.employee).ToList();
            foreach(var item in bookings)
            {
                if(item.EmployeeID==EmpId)
                {
                    bookings1.Add(item);
                }
            }
            return bookings1.Last();
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

        public IEnumerable<Booking> GetBookingsByDate(DateTime date1)
        {
            List<Booking> booking = _dbContext.booking.Include(obj=>obj.employee).ToList();
            List<Booking>booking1= new List<Booking>();

            foreach(var item in booking)
            {
                if(item.From_Date.Date==item.To_Date.Date && item.From_Date.Date==date1)
                {
                    booking1.Add(item);
                    continue;
                }
                else if((item.From_Date.Date != item.To_Date.Date)&&(item.From_Date.Date>=date1&& date1<= item.To_Date.Date))
                {
                    booking1.Add(item);
                }
            }
            return booking1;
        }

       
    }
}
