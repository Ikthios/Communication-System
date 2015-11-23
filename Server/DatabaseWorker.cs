using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class DatabaseWorker
    {
        public DatabaseWorker()
        {
            string startupPath = Environment.CurrentDirectory;
            startupPath += @"\UserDatabase.mdf;";
            string conString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=";
            conString += startupPath;
            conString += "Integrated Security=True;Connect Timeout=30";
            this.conString = conString;
        }

        //Was using this earlier, so keep it.  Now it doesn't matter what computer run on.
        //public string conString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Erick\\Source\\Repos\\Communication-System\\Server\\UserDatabase.mdf;Integrated Security=True;Connect Timeout=30";
        public string conString;

        public void AddUser(User user)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(conString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("INSERT INTO Users (Username, Password, Name, Email, Address, Phone, DateOfBirth) VALUES "
                        + "(@Username, @Password, @Name, @Email, @Address, @Phone, @DateOfBirth)", connection);

                    command.Parameters.Add("@Username", SqlDbType.NVarChar, 50);
                    command.Parameters["@Username"].Value = user.Username;

                    command.Parameters.Add("@Password", SqlDbType.NVarChar, 50);
                    command.Parameters["@Password"].Value = user.Password;

                    command.Parameters.Add("@Name", SqlDbType.NVarChar, 50);
                    command.Parameters["@Name"].Value = user.Name;

                    command.Parameters.Add("@Email", SqlDbType.NVarChar, 50);
                    command.Parameters["@Email"].Value = user.Email;

                    command.Parameters.Add("@Address", SqlDbType.NVarChar, 50);
                    command.Parameters["@Address"].Value = user.Address;

                    command.Parameters.Add("@Phone", SqlDbType.NVarChar, 50);
                    command.Parameters["@Phone"].Value = user.Phone;

                    command.Parameters.Add("@DateOfBirth", SqlDbType.DateTime);
                    command.Parameters["@DateOfBirth"].Value = user.DateOfBirth;

                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch (Exception e)
            {

            }
        }

        public void AddFriend(User user, User friend)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(conString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("INSERT INTO Friends (Username, FriendUsername) VALUES "
                        + "(@Username, @FriendUsername)", connection);

                    command.Parameters.Add("@Username", SqlDbType.NVarChar, 50);
                    command.Parameters["@Username"].Value = user.Username;

                    command.Parameters.Add("@FriendUsername", SqlDbType.NVarChar, 50);
                    command.Parameters["@FriendUsername"].Value = friend.Username;

                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch (Exception e)
            {

            }
            try
            {
                using (SqlConnection connection = new SqlConnection(conString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("INSERT INTO Friends (Username, FriendUsername) VALUES "
                        + "(@Username, @FriendUsername)", connection);

                    command.Parameters.Add("@Username", SqlDbType.NVarChar, 50);
                    command.Parameters["@Username"].Value = friend.Username;

                    command.Parameters.Add("@FriendUsername", SqlDbType.NVarChar, 50);
                    command.Parameters["@FriendUsername"].Value = user.Username;

                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch (Exception e)
            {

            }
        }

        public void ClearFriendsTable()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(conString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("TRUNCATE TABLE Friends", connection);

                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch (Exception e)
            {

            }
        }


        public void ClearUsersTable()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(conString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("TRUNCATE TABLE Users", connection);

                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch (Exception e)
            {

            }
        }

        public List<string> getUsernameInfo(string username)
        {
            List<string> values = new List<string>();
            try
            {
                using (SqlConnection connection = new SqlConnection(conString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("SELECT * FROM Users WHERE Username = @Username", connection);

                    command.Parameters.Add("@Username", SqlDbType.NVarChar, 50);
                    command.Parameters["@Username"].Value = username;

                    SqlDataReader reader = command.ExecuteReader();
                    int numberOfColumns = getNumberOfColumnsUsers();
                    try
                    {
                        while (reader.Read())
                        {
                            for (int i = 0; i < numberOfColumns; i++)
                            {
                                values.Add(reader[i].ToString());
                            }
                        }
                    }
                    finally
                    {
                        // Always call Close when done reading.
                        reader.Close();

                    }
                    return values;
                    connection.Close();
                }
            }
            catch (Exception e)
            {
                return values;
            }
        }

        public int getNumberOfColumnsUsers()
        {
            int columns = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(conString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("SELECT Count(*) FROM information_schema.columns WHERE TABLE_NAME = 'Users'", connection);

                    SqlDataReader reader = command.ExecuteReader();
                    try
                    {
                        while (reader.Read())
                        {
                            columns = Int32.Parse(reader[0].ToString());
                        }
                    }
                    finally
                    {
                        // Always call Close when done reading.
                        reader.Close();
                    }
                }
                return columns;
            }
            
            catch (Exception e)
            {
                Console.WriteLine(e);
                return columns;
            }
        }

        public int getNumberOfColumnsFriends()
        {
            int columns = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(conString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("SELECT Count(*) FROM information_schema.columns WHERE TABLE_NAME = 'Friends'", connection);

                    SqlDataReader reader = command.ExecuteReader();
                    try
                    {
                        while (reader.Read())
                        {
                            columns = Int32.Parse(reader[0].ToString());
                        }
                    }
                    finally
                    {
                        // Always call Close when done reading.
                        reader.Close();
                    }
                }
                return columns;
            }

            catch (Exception e)
            {
                Console.WriteLine(e);
                return columns;
            }
        }

        public List<string> getFriendsList(string username)
        {
            List<string> values = new List<string>();
            try
            {
                using (SqlConnection connection = new SqlConnection(conString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("SELECT FriendUsername FROM Friends WHERE Username = @Username", connection);

                    command.Parameters.Add("@Username", SqlDbType.NVarChar, 50);
                    command.Parameters["@Username"].Value = username;

                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        try
                        {
                            while (reader.Read())
                            {
                                values.Add(reader.GetString(0));
                            }
                        }
                        finally
                        {
                            // Always call Close when done reading.
                            reader.Close();
                        }
                    }
                }
                return values;
            }
            catch (Exception e)
            {
                return values;
            }
        }
    }
}
