using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace UnitTest
{
    public class ObjectCreation
    {
        [Fact]
        public void CanCreateBooker()
        {
            Booker booker;

            booker = new(1, "testName", "test@Mail.dk");

            Assert.Contains("@", booker.Mail);
        }

        [Fact]
        public void CanCreateBooking()
        {
            Booking booking;

            booking = new(1, new DateTime(1, 1, 1), new DateTime(1, 1, 1), null, 1);

            Assert.NotNull(booking);
        }

    }
}
