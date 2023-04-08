using Office_Seat_Book_DLL.Repost;
using Office_Seat_Book_Entity;
using System.Collections.Generic;

namespace Office_Seat_Book_BLL.Services
{
    public class SeatService
    {
        ISeatRepost _SeatRepost;
        public SeatService(ISeatRepost seatRepost)
        {
            _SeatRepost = seatRepost;
        }

        //Add Appointment
        public void AddSeat(Seat seat)
        {
            _SeatRepost.AddSeat(seat);
        }

        //Delete Appointment


        public void DeleteSeat(int seatID)
        {
            _SeatRepost.DeleteSeat(seatID);
        }

        //Update Appointment

        public void UpdateSeat(Seat Seat)
        {
            _SeatRepost.UpdateSeat(Seat);
        }

        //Get getAppointments

        public IEnumerable<Seat> GetSeat()
        {
            return _SeatRepost.GetSeats();
        }
        public Seat GetBySeatId(int SeatID)
        {
            return _SeatRepost.GetSeatById(SeatID);
        }
        public IEnumerable<Seat> GetSeatsByFloorId(int floorId)
        {
            return _SeatRepost.GetSeatsByFloorId(floorId);
        }
    }
}
