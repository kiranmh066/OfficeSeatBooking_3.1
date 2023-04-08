using Office_Seat_Book_Entity;
using System.Collections.Generic;

namespace Office_Seat_Book_DLL.Repost
{
    public interface IParkingRepost
    {
        void AddParking(Parking parking);
        void UpdateParking(Parking parking);

        void DeleteParking(int parkingId);

        Parking GetParkingById(int parkingId);

        IEnumerable<Parking> GetParkings();
    }
}
