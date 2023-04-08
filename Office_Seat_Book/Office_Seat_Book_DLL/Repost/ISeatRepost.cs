using Office_Seat_Book_Entity;
using System.Collections.Generic;

namespace Office_Seat_Book_DLL.Repost
{
    public interface ISeatRepost
    {
        void AddSeat(Seat seat);
        void UpdateSeat(Seat seat);

        void DeleteSeat(int seatId);

        Seat GetSeatById(int seatId);

        IEnumerable<Seat> GetSeats();

        IEnumerable<Seat> GetSeatsByFloorId(int floorId);
    }
}
