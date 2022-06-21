using Entities;
using System.Data.SqlClient;

namespace DataAccess
{
    public class Repository
    {
        const string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Camping;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public Repository()
        {
            SqlConnection connection = new(connectionString);
            connection.Open();
            connection.Close();
        }

        public List<Booker> GetallBookers()
        {
            List<Booker> returnList = new();


            SqlConnection connection = new(connectionString);
            connection.Open();
            string sql = "SELECT * FROM Bookers";
            SqlCommand command = new(sql, connection);
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                int id = (int)reader[0];
                string name = (string)reader[1];
                string mail = (string)reader[2];

                Booker booker = new(id, name, mail);
                returnList.Add(booker);
            }
            connection.Close();

            return returnList;
        }

        public List<Booking> GetAllBookings()
        {
            List<Booking> bookings = new List<Booking>();

            SqlConnection connection = new(connectionString);
            connection.Open();
            string sql = "SELECT * FROM Bookings";
            SqlCommand command = new(sql, connection);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                int id = (int)reader[0];
                DateTime start = (DateTime)reader[1];
                DateTime end = (DateTime)reader[2];
                int pitchId = (int)reader[3];
                Booker booker = null;

                foreach (Booker b in GetallBookers())
                {
                    if(b.Id == (int)reader[4])
                    {
                        booker = b;
                        break;
                    }
                }

                Booking booking = new(id, start, end, booker, pitchId);
                bookings.Add(booking);
            }

            return bookings;
        }

        public List<Pitch> GetAllPitches()
        {
            List<Pitch> pitches = new();

            SqlConnection connection = new(connectionString);
            connection.Open();
            string sql = "SELECT * FROM Pitches";
            SqlCommand command = new(sql, connection);
            SqlDataReader reader = command.ExecuteReader();

            List<Booking> tempBookingList = GetAllBookings();

            while (reader.Read())
            {
                int id = (int)reader[0];
                int number = (int)reader[1];
                Pitch pitch = new(id, number);
                foreach(Booking b in tempBookingList)
                {
                    if(b.PitchId == id)
                    {
                        pitch.addBooking(b);
                    }
                }
                pitches.Add(pitch);
            }

            return pitches;
        }

        public List<Pitch> GetAllAvaliablePitches(DateTime intervalStart, DateTime intervalEnd)
        {
            List<Pitch> pitches = new();

            foreach(Pitch pitch in GetAllPitches())
            {
                if (!pitch.IsBooked(intervalStart, intervalEnd))
                {
                    pitches.Add(pitch);
                }
            }

            return pitches;
        }

        public void AddNewBooker(Booker booker)
        {
            SqlConnection connection = new(connectionString);
            connection.Open();
            string sql = $"INSERT INTO Bookers(Name, Mail) VALUES('{booker.Name}', '{booker.Mail}')";
            SqlCommand command = new(sql, connection);
            command.ExecuteNonQuery();
            connection.Close();
        }

        public void AddNewBookingWithNewBooker(Booking booking, Booker booker, Pitch pitch)
        {
            int id = 1;

            SqlConnection connection = new(connectionString);
            connection.Open();
            string sql = $"INSERT INTO Bookers(Name, Mail) VALUES('{booker.Name}', '{booker.Mail}')";
            SqlCommand command1 = new(sql, connection);
            command1.ExecuteNonQuery();

            sql = $"SELECT MAX(BookerId) from Bookers";
            command1 = new(sql, connection);
            SqlDataReader dataReader = command1.ExecuteReader();
            while (dataReader.Read())
            {
                id = (int)dataReader[0];
            }
            dataReader.Close();

            sql = $"INSERT INTO Bookings(BookingStart, BookingEnd, PitchId, BookerId) VALUES('{booking.Start.ToString("yyyy-MM-dd")}', '{booking.End.ToString("yyyy-MM-dd")}', {pitch.id}, {id})";
            command1 = new(sql, connection);
            command1.ExecuteNonQuery();

            connection.Close();
        }

        public void EditBooker(Booker booker)
        {
            SqlConnection connection = new(connectionString);
            connection.Open();
            string sql = $"UPDATE Bookers SET Name = '{booker.Name}', Mail = '{booker.Mail}' WHERE BookerId = {booker.Id}";
            SqlCommand command = new(sql, connection);
            command.ExecuteNonQuery();
            connection.Close();
        }

        public void EditBooking(Booking booking)
        {
            SqlConnection connection = new(connectionString);
            connection.Open();
            string sql = $"UPDATE Bookings Set BookingStart = '{booking.Start.ToString("yyyy-MM-dd")}', BookingEnd = '{booking.End.ToString("yyyy-MM-dd")}', PitchId = {booking.PitchId}";
            SqlCommand command = new(sql, connection);
            command.ExecuteNonQuery();
            connection.Close();
        }

        public void DeleteBooking(Booking booking)
        {
            SqlConnection connection = new(connectionString);
            connection.Open();
            string sql = $"DELETE FROM Bookings WHERE BookingsId = '{booking.Id}'";
            SqlCommand command = new(sql, connection);
            command.ExecuteNonQuery();

            sql = $"DELETE FROM Bookers WHERE BookerId = '{booking.BookingBooker.Id}'";
            command = new(sql, connection);
            command.ExecuteNonQuery();
            connection.Close();
        }


    }
}