using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
namespace MovieOrganizer
{
    class DBConnect
    {
        private MySqlConnection connection;
        private string server;
        private string database;
        private string uid;
        private string password;
        //Constructor
        public DBConnect()
        {
            Initialize();
        }
        //Initialize values
        private void Initialize()
        {
            server = "174.127.110.143";
            database = "kccofor9_Movie";
            uid = "kccofor9_m";
            password = "1a2b3c4d";
            string connectionString;
            connectionString = "SERVER=" + server + ";" + "DATABASE=" +
            database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";
            connection = new MySqlConnection(connectionString);
        }
        //open connection to database
        private bool OpenConnection()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                //When handling errors, you can your application's response based 
                //on the error number.
                //The two most common error numbers when connecting are as follows:
                //0: Cannot connect to server.
                //1045: Invalid user name and/or password.
                switch (ex.Number)
                {
                    case 0:
                        MessageBox.Show("Cannot connect to server.  Contact administrator");
                        break;
                    case 1045:
                        MessageBox.Show("Invalid username/password, please try again");
                        break;
                }
                return false;
            }
        }
        //Close connection
        private bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
        //Insert statement
        public void Insert(String cmdString)
        {
            string query = cmdString;
            //open connection
            if (this.OpenConnection() == true)
            {
                //create command and assign the query and connection from the constructor
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Execute command
                cmd.ExecuteNonQuery();
                //close connection
                this.CloseConnection();
            }
        }
        //Update statement
        public void Update()
        {
        }
        //Delete statement
        public void Delete(String cmdString)
        {
            string query = cmdString;
            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                this.CloseConnection();
            }
        }
        //Select statement
        public List<string>[] SelectUsers(String cmdString)
        {
            string query = cmdString;
            //Create a list to store the result
            List<string>[] list = new List<string>[3];
            list[0] = new List<string>();
            list[1] = new List<string>();
            list[2] = new List<string>();
            //Open connection
            if (this.OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();
                //Read the data and store them in the list
                while (dataReader.Read())
                {
                    list[0].Add(dataReader["UID"] + "");
                    list[1].Add(dataReader["name"] + "");
                    list[2].Add(dataReader["pass"] + "");
                }
                //close Data Reader
                dataReader.Close();
                //close Connection
                this.CloseConnection();
                //return list to be displayed
                return list;
            }
            else
            {
                return list;
            }
        }
        //Select statement
        public List<Genre> SelectGenres(String cmdString)
        {
            string query = cmdString;
            //Create a list to store the result
            //List<string>[] list = new List<string>[3];
            List<Genre> list = new List<Genre>();
            // list[0] = new List<string>();
            //list[1] = new List<string>();
            //list[2] = new List<string>();
            //list[2] = new List<string>();
            //list[2] = new List<string>();
            //Open connection
            if (this.OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list
                while (dataReader.Read())
                {
                    list.Add(new Genre(Int32.Parse(dataReader["GID"] + ""), dataReader["name"] + ""));
                }
                //close Data Reader
                dataReader.Close();
                //close Connection
                this.CloseConnection();
                //return list to be displayed
                return list;
            }
            else
            {
                return list;
            }
        }
        //Select statement
        public List<Actor> SelectActor(String cmdString)
        {
            string query = cmdString;
            List<Actor> list = new List<Actor>();
            //Open connection
            if (this.OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();
                //Read the data and store them in the list
                while (dataReader.Read())
                {
                    list.Add(new Actor(Int32.Parse(dataReader["AID"] + ""), dataReader["name"] + ""));
                }
                //close Data Reader
                dataReader.Close();
                //close Connection
                this.CloseConnection();
                //return list to be displayed
                return list;
            }
            else
            {
                return list;
            }
        }
        //Select statement
        //  public Movie(int MID,String title,String length,String director,String year,List<Actor> Actors, List<Genre> Genres)
        public List<Movie> SelectMovie(String cmdString)
        {
            string query = cmdString;
            List<Movie> list = new List<Movie>();
            //Open connection
            if (this.OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();
                //Read the data and store them in the list
                DBConnect tempA = new DBConnect();
                DBConnect tempB = new DBConnect();
                while (dataReader.Read())
                {
                    List<Actor> actors = tempA.SelectActor("SELECT * FROM WasIn,Actors WHERE WasIn.AID=Actors.AID AND WasIn.MID=" + dataReader["MID"]);
                    List<Genre> genres = tempB.SelectGenres("SELECT * FROM FitsIn,Genres WHERE Genres.GID=FitsIn.GID AND FitsIn.MID=" + dataReader["MID"]);
                    list.Add(new Movie(Int32.Parse(dataReader["MID"] + ""), dataReader["title"] + "", dataReader["length"] + "", dataReader["director"] + "", dataReader["year"] + "", actors, genres));
                }
                //close Data Reader
                dataReader.Close();
                //close Connection
                this.CloseConnection();
                //return list to be displayed
                return list;
            }
            else
            {
                return list;
            }
        }

        //If no value found -1.0 
        public double AverageRating(int MID)
        {
            string query = "SELECT avg(value) AS avg FROM Ratings WHERE MID='" + MID + "'";
            double output = -1.0;
            //Open connection
            if (this.OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list

                while (dataReader.Read())
                {
                    output = double.Parse(dataReader["avg"].ToString());
                }

                //close Data Reader
                dataReader.Close();

                //close Connection
                this.CloseConnection();

                //return list to be displayed
                return output;
            }
            else
            {
                return output;
            }
        }


        public void addRating(int MID, int UID,int value)
        {
            int count = 0;

            string queryS = "SELECT * FROM Ratings WHERE MID='" + MID + "' AND UID='" + UID + "'";
            
            //Open connection
            if (this.OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(queryS, connection);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list

                while (dataReader.Read())
                {
                    count++;
                }

                //close Data Reader
                dataReader.Close();

                //close Connection
                this.CloseConnection();

            }
            

            //======================END SELECT AND DECIDE ON ACTION=====================

            if (count == 0)
            {

                string query = "INSERT INTO Ratings VALUES('" + UID + "','" + MID + "','" + value + "')";
                //open connection
                if (this.OpenConnection() == true)
                {
                    //create command and assign the query and connection from the constructor
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    //Execute command
                    cmd.ExecuteNonQuery();
                    //close connection
                    this.CloseConnection();
                }
            }
            else
            {
                string query = "UPDATE Ratings SET value='" + value + "' WHERE MID='" + MID + "' AND UID='" + UID + "'";

                //open connection
                if (this.OpenConnection() == true)
                {
                    //create command and assign the query and connection from the constructor
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    //Execute command
                    cmd.ExecuteNonQuery();
                    //close connection
                    this.CloseConnection();
                }
            }
        }

        /*
        //Count statement
        public int Count()
        {
            return -999;
        }
        //Backup
        public void Backup()
        {
        }
        //Restore
        public void Restore()
        {
        }
         */

        internal List<Movie> SelectMovieByGenre(string genre)
        {
            List<Movie> list = new List<Movie>();

            string query = "SELECT * FROM FitsIn,Genres WHERE Genres.GID=FitsIn.GID AND name LIKE '%" + genre + "%'";
                    
            //Open connection
            if (this.OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();
                //Read the data and store them in the list
                while (dataReader.Read())
                {
                    DBConnect temp = new DBConnect();
                    //Console.Out.WriteLine(dataReader["MID"]);
                    list.AddRange( temp.SelectMovie("SELECT * FROM Movies WHERE MID='"+dataReader["MID"]+"'"));
                }
      
            }
            return list;
            
        }

        internal List<Movie> SelectMovieByActor(string actor)
        {
            List<Movie> list = new List<Movie>();

            string query = "SELECT * FROM WasIn,Actors WHERE Actors.AID=WasIn.AID AND Actors.name LIKE '%" + actor + "%'";

            //Open connection
            if (this.OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();
                //Read the data and store them in the list
                while (dataReader.Read())
                {
                    DBConnect temp = new DBConnect();
                    //Console.Out.WriteLine(dataReader["MID"]);
                    list.AddRange(temp.SelectMovie("SELECT * FROM Movies WHERE MID='" + dataReader["MID"] + "'"));
                }

            }
            return list;

        }
    }
}