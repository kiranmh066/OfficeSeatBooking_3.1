using Office_Seat_Book_DLL.Repost;
using Office_Seat_Book_Entity;
using System.Collections.Generic;

namespace Office_Seat_Book_BLL.Services
{
    public class ParkingService
    {
        IParkingRepost _parkingRepost;
        public ParkingService(IParkingRepost floorRepost)
        {
            _parkingRepost = floorRepost;
        }

        //Add Appointment
        public void AddParking(Parking Parking)
        {
            _parkingRepost.AddParking(Parking);
        }

        //Delete Appointment

        public void DeleteParking(int ParkingID)
        {
            _parkingRepost.DeleteParking(ParkingID);
        }

        //Update Appointment

        public void UpdateParking(Parking Parking)
        {
            _parkingRepost.UpdateParking(Parking);
        }

        //Get getAppointments

        public IEnumerable<Parking> GetParking()
        {
            return _parkingRepost.GetParkings();
        }
        public Parking GetByParkingId(int ParkingID)
        {
            return _parkingRepost.GetParkingById(ParkingID);
        }
    }
}
