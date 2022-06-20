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

        public List<Pitch> pitches()
        {
            List<Pitch> pitches = new();



            return pitches;
        }
    }
}