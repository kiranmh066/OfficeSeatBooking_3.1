using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Office_Seat_Book_DLL;
using Office_Seat_Book_Entity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Office_Seat_Book_MVC.Controllers
{
    public class AdminController : Controller
    {
        private IConfiguration _configuration;

        public AdminController(IConfiguration configuration)
        {
            _configuration = configuration;
        }



        Office_DB_Context db = new Office_DB_Context();

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult RegisterEmp()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> RegisterEmp(Employee employee)
        {
            ViewBag.status = "";

            using (HttpClient client = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(employee), Encoding.UTF8, "application/json");
                string endPoint = _configuration["WebApiBaseUrl"] + "Employee/AddEmployee";
                using (var response = await client.PostAsync(endPoint, content))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        ViewBag.status = "Ok";
                        ViewBag.message = "Register successfully!";
                        TempData["regcount1"] = Convert.ToInt32(TempData["regcount1"]) + 1;
                        TempData.Keep();
                        TempData["regcount2"] = TempData["regcount1"];
                        TempData.Keep();
                        TempData["register"] = "profile was added";
                        TempData.Keep();
                        TempData["TotalCount"] = Convert.ToInt32(TempData["helpcount2"]) + Convert.ToInt32(TempData["regcount2"]);
                        TempData.Keep();
                        int b = Convert.ToInt32(TempData["TotalCount"]);
                    }
                    else
                    {
                        ViewBag.status = "Error";
                        ViewBag.message = "Wrong entries!";
                    }
                }
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> ViewEmp()
        {
            IEnumerable<Employee> empresult = null;
            using (HttpClient client = new HttpClient())
            {
                // LocalHost Adress in endpoint
                string endPoint = _configuration["WebApiBaseUrl"] + "Employee/GetEmployees";
                using (var response = await client.GetAsync(endPoint))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        //It will deserilize the object in the form of JSON
                        empresult = JsonConvert.DeserializeObject<IEnumerable<Employee>>(result);
                    }
                }
            }
            return View(empresult);
        }

        public async Task<IActionResult> EditEmp(int EmpID)
        {
            //if (EmpId != 0)
            //{
            //    //We are Storing employee Id  temporary to avoid the error. Now it will show the doctor details after the update also
            //    TempData["EditEmployeeId"] = EmpId;
            //    TempData.Keep();
            //}
            Employee emp = new Employee();
            //it will fetch the Doctor Details by using DoctorID
            using (HttpClient client = new HttpClient())
            {
                string endPoint = _configuration["WebApiBaseUrl"] + "Employee/GetEmployeeById?EmployeeId=" + EmpID;/*+ Convert.ToInt32(TempData["EditDoctorId"])*/
                using (var response = await client.GetAsync(endPoint))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        //It will deserilize the object in the form of JSON
                        emp = JsonConvert.DeserializeObject<Employee>(result);
                    }
                }
            }
            //ViewBag.genderlist = GetGender();
            //ViewBag.doctorstatuslist = GetDoctorStatus();

            return View(emp);
        }



        [HttpPost]
        public async Task<IActionResult> EditEmp(Employee emp)
        {

            //it will update the doctor details after Admin Changes
            using (HttpClient client = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(emp), Encoding.UTF8, "application/json");
                string endPoint = _configuration["WebApiBaseUrl"] + "Employee/UpdateEmployee";
                using (var response = await client.PutAsync(endPoint, content))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        ViewBag.status = "Ok";
                        ViewBag.message = "Employee Details Updated Successfully!";
                        //return RedirectToAction("GetAllDoctors", "Admin");
                    }
                    else
                    {
                        ViewBag.status = "Error";
                        ViewBag.message = "Wrong Entries!";
                    }
                }
            }
            return View();
        }

        public async Task<IActionResult> DeleteEmp(int EmpId)
        {
            ViewBag.status = "";
            //it will Delete the employee Details by using employee Id
            using (HttpClient client = new HttpClient())
            {
                string endPoint = _configuration["WebApiBaseUrl"] + "Employee/DeleteEmployee?employeeId=" + EmpId;
                using (var response = await client.DeleteAsync(endPoint))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        ViewBag.status = "Ok";
                        ViewBag.message = "Details Deleted Successfully!";
                        return RedirectToAction("ViewEmp", "Admin");
                    }
                    else
                    {
                        ViewBag.status = "Error";
                        ViewBag.message = "Wrong Entries!";
                    }
                }
            }
            return View();
        }

        public IActionResult AddParking()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddParking(Parking parking)
        {
            ViewBag.status = "";
            using (HttpClient client = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(parking), Encoding.UTF8, "application/json");
                string endPoint = _configuration["WebApiBaseUrl"] + "Parking/AddParking";
                using (var response = await client.PostAsync(endPoint, content))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        ViewBag.status = "Ok";
                        ViewBag.message = "parking added";
                    }
                    else
                    {
                        ViewBag.status = "Error";
                        ViewBag.message = "wrong entities try again!";
                    }
                }
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ViewParking()
        {
            IEnumerable<Parking> parkresult = null;
            using (HttpClient client = new HttpClient())
            {
                // LocalHost Adress in endpoint
                string endPoint = _configuration["WebApiBaseUrl"] + "Parking/GetParkings";
                using (var response = await client.GetAsync(endPoint))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        //It will deserilize the object in the form of JSON
                        parkresult = JsonConvert.DeserializeObject<IEnumerable<Parking>>(result);
                    }
                }
            }
            return View(parkresult);
        }


        public async Task<IActionResult> EditParking(int parkingID)
        {
            Parking parking = new Parking();
            //it will fetch the parking Details by using ParkingID
            using (HttpClient client = new HttpClient())
            {
                string endPoint = _configuration["WebApiBaseUrl"] + "Parking/GetParkingById?parkingId=" + parkingID;/*+ Convert.ToInt32(TempData["EditDoctorId"])*/
                using (var response = await client.GetAsync(endPoint))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        //It will deserilize the object in the form of JSON
                        parking = JsonConvert.DeserializeObject<Parking>(result);
                    }
                }
            }

            return View(parking);
        }



        [HttpPost]
        public async Task<IActionResult> EditParking(Parking parking)
        {
            ViewBag.status = "";
            //Admin can update the parking details 
            using (HttpClient client = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(parking), Encoding.UTF8, "application/json");
                string endPoint = _configuration["WebApiBaseUrl"] + "Parking/UpdateParking";
                using (var response = await client.PutAsync(endPoint, content))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        ViewBag.status = "Ok";
                        ViewBag.message = "parking Details Updated Successfully!";
                    }
                    else
                    {
                        ViewBag.status = "Error";
                        ViewBag.message = "Wrong Entries!";
                    }
                }
            }
            return View();
        }
        public async Task<IActionResult> DeleteParking(int parkingId)
        {
            ViewBag.status = "";
            //it will Delete the parking Details by using parking Id
            using (HttpClient client = new HttpClient())
            {
                string endPoint = _configuration["WebApiBaseUrl"] + "Parking/DeleteParking?parkingId=" + parkingId;
                using (var response = await client.DeleteAsync(endPoint))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        ViewBag.status = "Ok";
                        ViewBag.message = "Details Deleted Successfully!";
                        return RedirectToAction("ViewEmp", "Admin");
                    }
                    else
                    {
                        ViewBag.status = "Error";
                        ViewBag.message = "Wrong Entries!";
                    }
                }
            }
            return View();
        }




        public IActionResult AddSeat()
        {


            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddSeat(Seat seat)

        {

            ViewBag.status = "";

            using (HttpClient client = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(seat), Encoding.UTF8, "application/json");
                string endPoint = _configuration["WebApiBaseUrl"] + "Seat/AddSeat";//api controller name and its function
                using (var response = await client.PostAsync(endPoint, content))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {   //dynamic viewbag we can create any variable name in run time
                        ViewBag.status = "Ok";
                        ViewBag.message = "seat Added Successfull!!";
                    }

                    else
                    {
                        ViewBag.status = "Error";
                        ViewBag.message = "Wrong Entries";

                    }
                }
            }
            return View();

        }
        public async Task<IActionResult> EditSeat(int seatID)
        {

            //List<SelectListItem> status = new List<SelectListItem>()
            //{
            //    new SelectListItem { Value = "Select", Text = "select" },
            //    new SelectListItem { Value = "true", Text = "Available" },
            //    new SelectListItem { Value ="false" , Text = "Unavailable" },
            //};
            //ViewBag.Seat = status;
            Seat seat = null;
            using (HttpClient client = new HttpClient())
            {
                string endPoint = _configuration["WebApiBaseUrl"] + "Seat/GetSeatById?seatId=" + seatID;

                using (var response = await client.GetAsync(endPoint))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {   //dynamic viewbag we can create any variable name in run time
                        var result = await response.Content.ReadAsStringAsync();
                        seat = JsonConvert.DeserializeObject<Seat>(result);
                    }
                }
            }
            return View(seat);

        }
        [HttpPost]
        public async Task<IActionResult> EditSeat(Seat seat)

        {

            ViewBag.status = "";

            using (HttpClient client = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(seat), Encoding.UTF8, "application/json");
                string endPoint = _configuration["WebApiBaseUrl"] + "Seat/UpdateSeat";//api controller name and its function



                using (var response = await client.PutAsync(endPoint, content))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {   //dynamic viewbag we can create any variable name in run time
                        ViewBag.status = "Ok";
                        ViewBag.message = "seat updated Successfully!!";
                    }

                    else
                    {
                        ViewBag.status = "Error";
                        ViewBag.message = "Wrong Entries";

                    }
                }
            }
            return View();

        }

        public async Task<IActionResult> DeleteSeat(int seatID)
        {
            ViewBag.status = "";
            //Seat seat = null;
            using (HttpClient client = new HttpClient())
            {
                //StringContent content = new StringContent(JsonConvert.SerializeObject(seatID), Encoding.UTF8, "application/json");
                string endPoint = _configuration["WebApiBaseUrl"] + "Seat/DeleteSeat?seatId=" + seatID;//api controller name and its function



                using (var response = await client.DeleteAsync(endPoint))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {   //dynamic viewbag we can create any variable name in run time
                        ViewBag.status = "Ok";
                        ViewBag.message = "seat deleted Successfully!!";
                        return RedirectToAction("GetAllSeat", "Admin");

                    }

                    else
                    {
                        ViewBag.status = "Error";
                        ViewBag.message = "Wrong Entries";

                    }
                }
            }
            return View();

        }
        [HttpGet]
        public async Task<IActionResult> GetAllSeat(Seat seat)
        {


            IEnumerable<Seat> seatresult = null;

            {
                using (HttpClient client = new HttpClient())
                {
                    string endPoint = _configuration["WebApiBaseUrl"] + "Seat/GetSeats";//api controller name and httppost name given inside httppost in moviecontroller of api

                    using (var response = await client.GetAsync(endPoint))
                    {
                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        {   //dynamic viewbag we can create any variable name in run time
                            var result = await response.Content.ReadAsStringAsync();
                            seatresult = JsonConvert.DeserializeObject<IEnumerable<Seat>>(result);
                        }
                    }
                }
            }

            return View(seatresult);

        }

        public IActionResult AddFloor()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddFloor(Floor floor)
        {
            ViewBag.status = "";

            //using grabage collection only for inbuilt classes
            using (HttpClient client = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(floor), Encoding.UTF8, "application/json");
                string endPoint = _configuration["WebApiBaseUrl"] + "Floor/AddFloor";//api controller name and its function

                using (var response = await client.PostAsync(endPoint, content))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        ViewBag.status = "Ok";
                        ViewBag.message = "Floor details saved sucessfully!!";
                    }
                    else
                    {
                        ViewBag.status = "Error";
                        ViewBag.message = "Wrong entries!";
                    }

                }
            }
            return View();

        }


        public async Task<IActionResult> EditFloor(int FloorID)
        {
            Floor floor = null;
            using (HttpClient client = new HttpClient())
            {
                string endPoint = _configuration["WebApiBaseUrl"] + "Floor/GetFloorById?FloorId=" + FloorID;
                using (var response = await client.GetAsync(endPoint))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        floor = JsonConvert.DeserializeObject<Floor>(result);
                    }
                }
            }
            return View(floor);

        }
        [HttpPost]

        public async Task<IActionResult> EditFloor(Floor Floor)
        {
            ViewBag.status = "";
            using (HttpClient client = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(Floor), Encoding.UTF8, "application/json");
                string endPoint = _configuration["WebApiBaseUrl"] + "Floor/UpdateFloor";
                using (var response = await client.PutAsync(endPoint, content))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        ViewBag.status = "Ok";
                        ViewBag.message = "Floor details updated sucessfully!!";
                    }
                    else
                    {
                        ViewBag.status = "Error";
                        ViewBag.message = "Wrong entries!";
                    }
                }
            }
            return View();

        }



        public async Task<IActionResult> Deletefloor(int FloorId)
        {
            ViewBag.status = "";
            //Seat seat = null;
            using (HttpClient client = new HttpClient())
            {
                //StringContent content = new StringContent(JsonConvert.SerializeObject(seatID), Encoding.UTF8, "application/json");
                string endPoint = _configuration["WebApiBaseUrl"] + "Floor/DeleteFloor?floorId=" + FloorId;//api controller name and its function



                using (var response = await client.DeleteAsync(endPoint))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {   //dynamic viewbag we can create any variable name in run time
                        ViewBag.status = "Ok";
                        ViewBag.message = " deleted Successfully!!";
                        return RedirectToAction("GetAllfloor", "Admin");
                    }

                    else
                    {
                        ViewBag.status = "Error";
                        ViewBag.message = "Wrong Entries";

                    }
                }
            }
            return View();

        }
        [HttpGet]
        public async Task<IActionResult> GetAllFloor(Floor floor)

        {


            IEnumerable<Floor> floorresult = null;

            {
                using (HttpClient client = new HttpClient())
                {
                    string endPoint = _configuration["WebApiBaseUrl"] + "Floor/GetFloors";//api controller name and httppost name given inside httppost in moviecontroller of api

                    using (var response = await client.GetAsync(endPoint))
                    {
                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        {   //dynamic viewbag we can create any variable name in run time
                            var result = await response.Content.ReadAsStringAsync();
                            floorresult = JsonConvert.DeserializeObject<IEnumerable<Floor>>(result);
                        }
                    }
                }
            }

            return View(floorresult);

        }
        public async Task<IActionResult> Settings()
        {
            Employee emp = null;
            using (HttpClient client = new HttpClient())
            {
                //Fetching temporary ProfileId from  tempdata

                int Id = Convert.ToInt32(TempData["empId"]);

                TempData.Keep();
                string endPoint = _configuration["WebApiBaseUrl"] + "Employee/GetEmployeeById?EmployeeId=" + Id;
                using (var response = await client.GetAsync(endPoint))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        emp = JsonConvert.DeserializeObject<Employee>(result);
                    }
                }
            }
            return View(emp);
        }



        public async Task<IActionResult> OldPassword()
        {
            Employee emp = null;
            using (HttpClient client = new HttpClient())
            {
                //Fetching temporary ProfileId from  tempdata
                int Id = Convert.ToInt32(TempData["empId"]);
                TempData.Keep();
                string endPoint = _configuration["WebApiBaseUrl"] + "Employee/GetEmployeeById?EmployeeId=" + Id;
                using (var response = await client.GetAsync(endPoint))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        emp = JsonConvert.DeserializeObject<Employee>(result);
                    }

                }

            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> OldPassword(Employee emp1)
        {
            Employee emp = new Employee();
            using (HttpClient client = new HttpClient())
            {
                //Fetching temporary ProfileId from  tempdata

                int Id = Convert.ToInt32(TempData["empId"]);

                TempData.Keep();
                string endPoint = _configuration["WebApiBaseUrl"] + "Employee/GetEmployeeById?EmployeeId=" + Id;
                using (var response = await client.GetAsync(endPoint))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        emp = JsonConvert.DeserializeObject<Employee>(result);
                    }
                }
            }
            emp1.PhoneNo = emp.PhoneNo;
            emp1.Email = emp.Email;
            emp1.Role = emp.Role; ;
            emp1.EmpID = emp.EmpID;
            emp1.Gender = emp.Gender;
            emp1.Security_Question = emp.Security_Question;
            emp1.Name = emp.Name;
            if (emp1.Password == emp.Password)
            {
                ViewBag.status = "Ok";
                ViewBag.message = "correct";

                return RedirectToAction("NewPassword", "Admin");
            }
            else
            {
                ViewBag.status = "Error";
                ViewBag.message = "Wrong Entries!";
            }
            return View(emp);
        }
        public async Task<IActionResult> NewPassword()
        {
            Employee emp = null;
            using (HttpClient client = new HttpClient())
            {
                //Fetching temporary ProfileId from  tempdata

                int Id = Convert.ToInt32(TempData["empId"]);

                TempData.Keep();
                string endPoint = _configuration["WebApiBaseUrl"] + "Employee/GetEmployeeById?EmployeeId=" + Id;
                using (var response = await client.GetAsync(endPoint))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        emp = JsonConvert.DeserializeObject<Employee>(result);
                    }

                }

            }
            return View(emp);
        }

        [HttpPost]
        public async Task<IActionResult> NewPassword(Employee emp1)
        {
            Employee emp = new Employee();
            using (HttpClient client = new HttpClient())
            {
                //Fetching temporary ProfileId from  tempdata

                int Id = Convert.ToInt32(TempData["empId"]);

                TempData.Keep();
                string endPoint = _configuration["WebApiBaseUrl"] + "Employee/GetEmployeeById?EmployeeId=" + Id;
                using (var response = await client.GetAsync(endPoint))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        emp = JsonConvert.DeserializeObject<Employee>(result);
                    }
                }
            }

            emp1.Email = emp.Email;
            emp1.Role = emp.Role; ;
            emp1.EmpID = emp.EmpID;
            emp1.Gender = emp.Gender;
            emp1.Security_Question = emp.Security_Question;
            emp1.Name = emp.Name;
            emp1.PhoneNo = emp.PhoneNo;
            using (HttpClient client = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(emp1), Encoding.UTF8, "application/json");
                string endPoint = _configuration["WebApiBaseUrl"] + "Employee/UpdateEmployee";
                using (var response = await client.PutAsync(endPoint, content))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        ViewBag.status = "Ok";
                        ViewBag.message = "Your Details Updated Successfully!";
                    }
                    else
                    {
                        ViewBag.status = "Error";
                        ViewBag.message = "Wrong Entries!";
                    }
                }
            }
            return View(emp1);
        }



    }
}