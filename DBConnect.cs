using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Collections;
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



        //============FOR RATINGS=================
        public double AverageRating(int MID) //If no value found -1.0 
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
                    if((dataReader["avg"]+"").Length >= 1)
                        output = double.Parse(dataReader["avg"]+"");//FIXED TOSTRING
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

        public int getUserRating(int MID, int UID)
        {
            int rating = -1;
            String query = "SELECT * FROM Ratings WHERE MID='" + MID + "' AND UID ='" + UID + "'";
            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader datareader = cmd.ExecuteReader();

                while (datareader.Read())
                {
                    rating = (int)double.Parse(datareader["value"]+"");
                }

                this.CloseConnection();
            }

                return rating;
        }

        public void addRating(int MID, int UID, int value)
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

        //============END RATINGS=================

        //============FOR WATCHLIST=================

        public void addToWatchList(int MID, int UID)
        {
            int count = 0;

            string queryS = "SELECT * FROM WatchList WHERE MID='" + MID + "' AND UID='" + UID + "'";

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

                string query = "INSERT INTO `WatchList`(`UID`, `MID`) VALUES ("+UID+","+MID+")";
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

        public List<Movie> selectMoviesFromWatchList(int UID)
        {
            List<Movie> list = new List<Movie>();

            string query = "SELECT MID FROM WatchList WHERE UID ='" + UID + "'";

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

        public void removeFromWatchList(int MID, int UID)
        {
            string query = "DELETE FROM `WatchList`WHERE MID='" + MID + "' AND UID='" + UID + "' ";
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

        public Boolean inWatchList(int MID, int UID)
        {
            int count = 0;

            string queryS = "SELECT * FROM WatchList WHERE MID='" + MID + "' AND UID='" + UID + "'";

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
            return count > 0;
        }

        //===========End FOR WATCHLIST============


        public List<Movie> selectRecomendations(int UID)
        {
            //Build Recomendations

            string query = "SELECT avg(R.value) as avg,F.GID as GeID FROM `FitsIn` F,Ratings R WHERE R.UID =" + UID + " AND F.MID = R.MID GROUP BY F.GID";


            List<Movie> list = new List<Movie>();
            //Open connection
            if (this.OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();
                //Read the data and store them in the list

                double value1 = -1;
                int GID1 = -1;
                double value2 = -1;
                int GID2 = -1;



                while (dataReader.Read())
                {
                    if (double.Parse(dataReader["avg"] + "") > value1)
                    {
                        value1 = double.Parse(dataReader["avg"] + "");
                        GID1 = int.Parse(dataReader["GeID"] + "");
                    }
                    else if (double.Parse(dataReader["avg"] + "") > value2)
                    {
                        value2 = double.Parse(dataReader["avg"] + "");
                        GID2 = int.Parse(dataReader["GeID"] + "");
                    }

                    //genreScores.Add(double.Parse(dataReader["avg"] + ""), int.Parse(dataReader["GeID"] + ""));
                }


                dataReader.Close();


                //Pick top n Genre
                const int n = 2;
                int curGID;
                for (int i = 0; i < n; i++)
                {
                    curGID = -1;
                    if (i == 0)
                    {
                        curGID =GID1;
                    }
                    else if (i == 1)
                    {
                        curGID = GID2;
                    }




                    if (curGID != -1)
                    {
                        query = "SELECT MID FROM `FitsIn` WHERE GID = " + curGID + " ORDER BY RAND() LIMIT 5";

                        //Open connection

                        //Create Command
                        cmd = new MySqlCommand(query, connection);
                        //Create a data reader and Execute the command
                        dataReader = cmd.ExecuteReader();
                        //Read the data and store them in the list
                        while (dataReader.Read())
                        {
                            DBConnect temp = new DBConnect();
                            //Console.Out.WriteLine(dataReader["MID"]);
                            list.AddRange(temp.SelectMovie("SELECT * FROM Movies WHERE MID='" + dataReader["MID"] + "'"));
                        }
                        dataReader.Close();
                    }

                }
            }
            return list;

        }

        public List<Movie> selectRandomMovies(int n)
        {
            //Build Recomendations

            
            List<Movie> list = new List<Movie>();
            //Open connection
            if (this.OpenConnection() == true)
            {
                
                String query = "SELECT MID FROM `Movies` ORDER BY RAND() LIMIT "+n;


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
                dataReader.Close();
                    
            }
            return list;

        }

        public List<Movie> SelectMovieByGenre(string genre)
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

        public List<Movie> SelectMovieByActor(string actor)
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
