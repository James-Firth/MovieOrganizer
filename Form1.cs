﻿using System;
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
    public partial class LoginForm : Form
    {
        RegisterForm reg;
        bool userValid;
        bool passValid;
        public LoginForm()
        {
            InitializeComponent();
            txtboxUserName.Select();
            userValid = false;
            passValid = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            DBConnect connector = new DBConnect();
            List<String>[] temp = connector.Select("SELECT * FROM Users");
            if (temp[1].Equals(txtboxUserName.Text))
            {
                userValid = true;
            }
            else
            {
                userValid = false;
            }
            if (temp[2].Equals(txtboxPassword.Text))
            {
                passValid = true;
            }
            else
            {
                passValid = false;
            }
          
            if (userValid && passValid) //txtboxUserName.Text.Equals("username") && txtboxPassword.Text.Equals("Password");
            {
                HomeForm main = new HomeForm(this);
                main.Show();
                this.Hide();
                txtboxPassword.Text = "";
                txtboxUserName.Text = "";
            }
            else
            {
                if (txtboxPassword.Text.Equals("") || txtboxUserName.Text.Equals(""))
                {
                    lblErrorMsg.Text = "Please enter information into the empty field(s)";
                }
                else
                {
                    lblErrorMsg.Text = "Please fix Errors before continuing";
                }

            }
        }

        private void txtboxUserName_Validating(object sender, CancelEventArgs e)
        {
            if (!txtboxUserName.Text.Equals(""))//everythin's cool
            {
                errUserName.Clear();
                lblErrorMsg.Text = "";
                userValid = true;
            }
            else
            {
                errUserName.SetError(txtboxUserName, "No Blanks allowed!");
                lblErrorMsg.Text = "No blanks allowed!";
                userValid = false;
            }
        }

        private void txtboxPassword_Validating(object sender, CancelEventArgs e)
        {
            if (!txtboxPassword.Text.Equals(""))//everythin's cool
            {
                errPassword.Clear();
                lblErrorMsg.Text = "";
                passValid = true;
            }
            else
            {
                errUserName.SetError(txtboxPassword, "No Blanks allowed!");
                lblErrorMsg.Text = "No blanks allowed!";
                passValid = false;
            }
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            this.Hide();
            if (reg == null)
            {
                reg = new RegisterForm(this);
                reg.Show();
            }
            else
            {
                reg.Show();
            }

        }

        private void txtboxPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (Char)System.ConsoleKey.Enter)
            {
                txtboxPassword_Validating(this, null);
                btnLogin_Click(this, null);
            }
        }

    }

    /*
     * New class for dealing with DB connections
   */
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
        public List<string>[] Select(String cmdString)
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
    }
}
