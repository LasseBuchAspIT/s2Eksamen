using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Booker
    {
        public List<Booking> bookings;

        private int id;
        private string name;
        private string mail;

        public Booker(int id, string name, string mail)
        {
            this.Id = id;
            this.Name = name;
            this.Mail = mail;
        }

        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public string Mail { get => mail; set => mail = value; }
    }
}
