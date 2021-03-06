﻿using System;
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

        public void UpdateIPAddress(string username, string ip)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(conString))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand("UPDATE Users SET IP = @IP WHERE Username = @Username", connection);

                    command.Parameters.Add("@Username", SqlDbType.NVarChar, 50);
                    command.Parameters["@Username"].Value = username;

                    command.Parameters.Add("@IP", SqlDbType.NVarChar, 50);
                    command.Parameters["@IP"].Value = ip;

                    command.ExecuteNonQuery();

                    command = new SqlCommand("UPDATE Friends SET FriendIP = @IP WHERE FriendUsername = @Username", connection);

                    command.Parameters.Add("@Username", SqlDbType.NVarChar, 50);
                    command.Parameters["@Username"].Value = username;

                    command.Parameters.Add("@IP", SqlDbType.NVarChar, 50);
                    command.Parameters["@IP"].Value = ip;

                    command.ExecuteNonQuery();

                    command = new SqlCommand("UPDATE Friends SET UserIP = @IP WHERE Username = @Username", connection);

                    command.Parameters.Add("@Username", SqlDbType.NVarChar, 50);
                    command.Parameters["@Username"].Value = username;

                    command.Parameters.Add("@IP", SqlDbType.NVarChar, 50);
                    command.Parameters["@IP"].Value = ip;

                    command.ExecuteNonQuery();

                    connection.Close();
                }
            }
            catch (Exception e)
            {

            }
        }


        public bool AddUser(User user)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(conString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("INSERT INTO Users (Username, Password, Name, Email, Address, Phone, DateOfBirth, IP) VALUES "
                        + "(@Username, @Password, @Name, @Email, @Address, @Phone, @DateOfBirth, @IP)", connection);

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

                    command.Parameters.Add("@IP", SqlDbType.NVarChar, 50);
                    command.Parameters["@IP"].Value = user.IP;

                    command.ExecuteNonQuery();
                    connection.Close();

                    return true;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool AddFriend(User user, User friend)
        {
            bool success = true;
            bool alreadyFriends = false;
            List<String> value = getFriendsList(user.Username);
            foreach(var name in value)
            {
                string[] tokens = name.Split(' ');
                if (tokens[0].Equals(friend.Username))
                    alreadyFriends = true;
            }
            if (!alreadyFriends)
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection(conString))
                    {
                        connection.Open();
                        SqlCommand command = new SqlCommand("INSERT INTO Friends (Username, FriendUsername, FriendIP, UserIP) VALUES "
                            + "(@Username, @FriendUsername, @FriendIP, @UserIP)", connection);

                        command.Parameters.Add("@Username", SqlDbType.NVarChar, 50);
                        command.Parameters["@Username"].Value = user.Username;

                        command.Parameters.Add("@FriendUsername", SqlDbType.NVarChar, 50);
                        command.Parameters["@FriendUsername"].Value = friend.Username;

                        command.Parameters.Add("@FriendIP", SqlDbType.NVarChar, 50);
                        command.Parameters["@FriendIP"].Value = friend.IP;

                        command.Parameters.Add("@UserIP", SqlDbType.NVarChar, 50);
                        command.Parameters["@UserIP"].Value = user.IP;

                        command.ExecuteNonQuery();
                        connection.Close();
                    }
                }
                catch (Exception e)
                {

                }
            }
            else
            {
                success = false;
            }
            return success;
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

        public List<PendingRequest> GetPendingFriendRequests()
        {
            List<PendingRequest> values = new List<PendingRequest>();
            try
            {
                using (SqlConnection connection = new SqlConnection(conString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("SELECT Username, UserIP, FriendUsername, FriendIP FROM Friends WHERE Accepted = 0", connection);

                    SqlDataReader reader = command.ExecuteReader();

                    try
                    {
                        while (reader.Read())
                        {
                            PendingRequest temp = new PendingRequest();
                            temp.Username = reader[0].ToString();
                            temp.UserIP = reader[1].ToString();
                            temp.FriendUsername = reader[2].ToString();
                            temp.FriendIP = reader[3].ToString();
                            temp.FriendIP = temp.FriendIP.Trim('\\');
                            values.Add(temp);
                        }
                        return values;
                    }
                    catch(Exception e)
                    {

                    }
                }
                return values;
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
                    SqlCommand command = new SqlCommand("SELECT FriendUsername, FriendIP FROM Friends WHERE Username = @Username AND Accepted = 1", connection);

                    command.Parameters.Add("@Username", SqlDbType.NVarChar, 50);
                    command.Parameters["@Username"].Value = username;

                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        try
                        {
                            while (reader.Read())
                            {
                                values.Add(reader.GetString(0) + " " + reader.GetString(1));
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

        public Boolean login(string username, string password)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(conString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("SELECT Username, Password FROM Users WHERE Username = @Username AND Password = @Password", connection);

                    command.Parameters.Add("@Username", SqlDbType.NVarChar, 50);
                    command.Parameters["@Username"].Value = username;

                    command.Parameters.Add("@Password", SqlDbType.NVarChar, 50);
                    command.Parameters["@Password"].Value = password;

                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        return true;
                    }
                }
                return false;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public void reject(User user, User friend)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(conString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("DELETE FROM Friends WHERE Username = @FriendUsername AND FriendUsername = @Username", connection);

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
        }

        public bool setToAccept(User user, User friend)
        {
            bool success = true;
            try
            {
                using (SqlConnection connection = new SqlConnection(conString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("UPDATE Friends SET Accepted = 1 WHERE Username = @FriendUsername AND FriendUsername = @Username", connection);

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

            success = AddFriend(user, friend);

            try
            {
                using (SqlConnection connection = new SqlConnection(conString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("UPDATE Friends SET Accepted = 1 WHERE Username = @Username AND FriendUsername = @FriendUsername", connection);

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
            return success;
        }
    }
}
