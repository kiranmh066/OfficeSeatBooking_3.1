using AutoFixture;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Office_Seat_Book_BLL.Services;
using Office_Seat_Book_DLL.Repost;
using Office_Seat_Book_Entity;
using OfficeAPI.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeAPITests.Controller.Tests
{
    [TestClass()]
    public class BookingControllerTests
    {
        BookingController bookingController;
        Fixture _fixture;
        Mock<IBookingRepost> moq;
        public BookingControllerTests()
        {
            _fixture = new Fixture();
            moq = new Mock<IBookingRepost>();
        }

        [TestMethod()]
        public async Task GetBookingsTest()
        {
            var bookinglist = _fixture.CreateMany<Booking>(3).ToList();
            moq.Setup(x => x.GetBookings()).Returns(bookinglist);
            bookingController = new BookingController(new BookingService(moq.Object));
            var result = bookingController.GetBookings();
            Assert.AreEqual(result.Count(), 3);
        }
        [TestMethod()]
        public async Task GetBookingsNegativeTest()
        {
            List<Booking> bookinglist = null;
            moq.Setup(x => x.GetBookings()).Returns(bookinglist);
            bookingController = new BookingController(new BookingService(moq.Object));
            Assert.IsNull(bookingController.GetBookings());
        }
        [TestMethod()]
        public void DeleteBookingTest()
        {
            var booking = _fixture.Create<Booking>();
            moq.Setup(x => x.DeleteBooking(1));
            bookingController = new BookingController(new BookingService(moq.Object));
            var result = bookingController.DeleteBooking(1);
            var Obj = result as ObjectResult;
            Assert.AreEqual(Obj.StatusCode, 200);
        }

        [TestMethod()]
        public void UpdateBookingTest()
        {
            var booking = _fixture.Create<Booking>();
            moq.Setup(x => x.UpdateBooking(booking));
            bookingController = new BookingController(new BookingService(moq.Object));
            var result = bookingController.UpdateBooking(booking);
            var Obj = result as ObjectResult;
            Assert.AreEqual(200, Obj.StatusCode);
        }

        [TestMethod()]
        public void GetBookingByIdTest()
        {
            var booking = _fixture.Create<Booking>();
            moq.Setup(x => x.GetBookingById(1)).Returns(booking);
            bookingController = new BookingController(new BookingService(moq.Object));
            Assert.AreEqual(bookingController.GetBookingById(1), booking);
        }
        [TestMethod()]
        public void GetBookingById_ExistingIdPassed_ReturnsRightItem()
        {
            // Arrange
            var booking = _fixture.Create<Booking>();
            moq.Setup(x => x.GetBookingById(10)).Returns(booking);
            bookingController = new BookingController(new BookingService(moq.Object));
            // Act
            var okResult = bookingController.GetBookingById(1);
            // Assert
            Assert.AreEqual(okResult, null);
        }
        [TestMethod()]
        public async Task AddBookingTest()
        {
            var booking = _fixture.Create<Booking>();
            moq.Setup(x => x.AddBooking(booking));
            bookingController = new BookingController(new BookingService(moq.Object));
            var result = bookingController.AddBooking(booking);
            /*var Obj = result as ObjectResult;
            Assert.AreEqual(200, Obj.StatusCode);*/
        }
        [TestMethod()]
        public void DeleteBooking_ThrowsException_IfIdNotFound()
        {
            var booking = _fixture.Create<Booking>();
            moq.Setup(x => x.DeleteBooking(It.IsAny<int>())).
                 Throws(new Exception());
            bookingController = new BookingController(new BookingService(moq.Object));
            var result = bookingController.DeleteBooking(1);
            var Obj = result as ObjectResult;
            Assert.AreEqual(Obj.StatusCode, 400);
        }
        [TestMethod()]
        public void UpdateBooking_ThrowsException_IfIdNotFound()
        {
            var booking = _fixture.Create<Booking>();
            moq.Setup(x => x.UpdateBooking(It.IsAny<Booking>())).
                 Throws(new Exception());
            bookingController = new BookingController(new BookingService(moq.Object));
            var result = bookingController.UpdateBooking(booking);
            var Obj = result as ObjectResult;
            Assert.AreEqual(Obj.StatusCode, 400);
        }
        [TestMethod()]
        public async Task AddBookingNegativeTest()
        {
            var booking = _fixture.Create<Booking>();
            moq.Setup(x => x.AddBooking(It.IsAny<Booking>())).
                 Throws(new Exception());
            bookingController = new BookingController(new BookingService(moq.Object));
            var result = bookingController.AddBooking(booking);
           /* var Obj = result as ObjectResult;
            Assert.AreEqual(Obj.StatusCode, 200);*/
        }
    }
}
