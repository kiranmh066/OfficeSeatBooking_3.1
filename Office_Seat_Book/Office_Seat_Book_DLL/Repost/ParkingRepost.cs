using Office_Seat_Book_Entity;
using System.Collections.Generic;
using System.Linq;

namespace Office_Seat_Book_DLL.Repost
{
    public class ParkingRepost : IParkingRepost
    {
        Office_DB_Context _dbContext;//default private

        public ParkingRepost(Office_DB_Context dbContext)
        {
            _dbContext = dbContext;
        }
        public void AddParking(Parking parking)
        {
            _dbContext.parking.Add(parking);
            _dbContext.SaveChanges();
        }

        public void DeleteParking(int parkingId)
        {
            var parking = _dbContext.parking.Find(parkingId);
            _dbContext.parking.Remove(parking);
            _dbContext.SaveChanges();
        }

        public Parking GetParkingById(int parkingId)
        {
            return _dbContext.parking.Find(parkingId);
        }

        public IEnumerable<Parking> GetParkings()
        {
            return _dbContext.parking.ToList();
        }


        public void UpdateParking(Parking parking)
        {

            _dbContext.Entry(parking).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _dbContext.SaveChanges();
        }
    }
}
