namespace Entities
{
    public class Pitch
    {
        public List<Booking> bookings;

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

    }
}