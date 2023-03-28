using Office_Seat_Book_Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace Office_Seat_Book_DLL.Repost
{
    public class SeatRepost:ISeatRepost
    {
        Office_DB_Context _dbContext;//default private

        public SeatRepost(Office_DB_Context dbContext)
        {
            _dbContext = dbContext;
        }
        public void AddSeat(Seat seat)
        {
            _dbContext.seat.Add(seat);
            _dbContext.SaveChanges();
        }

        public void DeleteSeat(int seatId)
        {
            var seat = _dbContext.seat.Find(seatId);
            _dbContext.seat.Remove(seat);
            _dbContext.SaveChanges();
        }

        public Seat GetSeatById(int seatId)
        {
            return _dbContext.seat.Find(seatId);
        }

        public IEnumerable<Seat> GetSeats()
        {
            return _dbContext.seat.ToList();
        }


        public void UpdateSeat(Seat seat)
        {

            _dbContext.Entry(seat).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _dbContext.SaveChanges();
        }
    }
}
