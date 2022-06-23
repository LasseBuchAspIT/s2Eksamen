namespace Entities
{
    public class Booking
    {

        private int id;
        private DateTime start;
        private DateTime end;
        private Booker booker;
        private int pitchId;

        public Booking(int id, DateTime start, DateTime end, Booker bookingBooker, int pitchId)
        {
            Id = id;
            Start = start;
            End = end;
            BookingBooker = bookingBooker;
            PitchId = pitchId;
        }

        public int Id { get => id; set => id = value; }
        public DateTime Start { get => start; set => start = value; }
        public DateTime End { get => end; set => end = value; }
        public Booker BookingBooker
        {
            get => booker;
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("Booker må ikke være null!");
                }
                else
                {
                    booker = value;
                }
            }
        }

        public int PitchId { get => pitchId; set => pitchId = value; }
    }
}
