using Office_Seat_Book_Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace Office_Seat_Book_DLL.Repost
{
    public class FloorRepost:IFloorRepost
    {
        Office_DB_Context _dbContext;//default private

        public FloorRepost(Office_DB_Context dbContext)
        {
            _dbContext = dbContext;
        }
        public void AddFloor(Floor floor)
        {
            _dbContext.floor.Add(floor);
            _dbContext.SaveChanges();
        }

        public void DeleteFloor(int floorId)
        {
            var floor = _dbContext.floor.Find(floorId);
            _dbContext.floor.Remove(floor);
            _dbContext.SaveChanges();
        }

        public Floor GetFloorById(int floorId)
        {
            return _dbContext.floor.Find(floorId);
        }

        public IEnumerable<Floor> GetFloors()
        {
            return _dbContext.floor.ToList();
        }


        public void UpdateFloor(Floor floor)
        {

            _dbContext.Entry(floor).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _dbContext.SaveChanges();
        }
    }
}
