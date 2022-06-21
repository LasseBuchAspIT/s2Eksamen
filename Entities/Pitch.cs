namespace Entities
{
    public class Pitch
    {
        public List<Booking> bookings { get; private set; }

        public int id { get; set; }
        public int number { get; set; }
        public Pitch(int id, int number)
        {
            this.id = id;
            this.number = number;
            bookings = new List<Booking>();
        }



        public void addBooking(Booking booking)
        {
            bookings.Add(booking);
        }

        public bool IsBooked(DateTime intervalStart, DateTime intervalEnd)
        {
            foreach(Booking b in bookings)
            {
                if(intervalStart > b.Start && intervalStart < b.End || intervalEnd > b.Start && intervalEnd < b.End) 
                {
                    return true;
                }

            }
            return false;
        }


        public override string ToString()
        {
            return "Plads: " + number.ToString();
        }

    }
}