using Office_Seat_Book_DLL.Repost;
using Office_Seat_Book_Entity;
using System;
using System.Collections.Generic;

namespace Office_Seat_Book_BLL.Services
{
    public class FloorService
    {
        IFloorRepost _floorRepost;
        public FloorService(IFloorRepost floorRepost)
        {
            _floorRepost = floorRepost;
        }

        //Add Appointment
        public void AddFloor(Floor floor)
        {
            _floorRepost.AddFloor(floor);
        }

        //Delete Appointment

        public void DeleteFloor(int floorID)
        {
            _floorRepost.DeleteFloor(floorID);
        }

        //Update Appointment

        public void UpdateFloor(Floor floor)
        {
            _floorRepost.UpdateFloor(floor);
        }

        //Get getAppointments

        public IEnumerable<Floor> GetFloor()
        {
            return _floorRepost.GetFloors();
        }
        public Floor GetByFloorId(int EmployeeID)
        {
            return _floorRepost.GetFloorById(EmployeeID);
        }
    }
}
