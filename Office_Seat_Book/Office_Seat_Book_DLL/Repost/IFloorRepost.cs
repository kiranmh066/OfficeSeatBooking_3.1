using Office_Seat_Book_Entity;
using System.Collections.Generic;

namespace Office_Seat_Book_DLL.Repost
{
    public interface IFloorRepost
    {
        void AddFloor(Floor floor);
        void UpdateFloor(Floor floor);

        void DeleteFloor(int FloorId);

        Floor GetFloorById(int floorId);

        IEnumerable<Floor> GetFloors();
    }
}
