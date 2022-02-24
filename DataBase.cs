using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;
using MySql.Data.MySqlClient;
using Bcrypt;

namespace MFServer
{
    class DataBase
    {
        public static bool DatabaseСonnection = false;
        public static MySqlConnection Connection;
        public String Host { set; get; }
        public String Username { set; get; }
        public String Password { set; get; }
        public String Databank { set; get; }

        public DataBase()
        {
            this.Host = Settings._settings.Host;
            this.Username = Settings._settings.Username;
            this.Password = Settings._settings.Password;
            this.Databank = Settings._settings.Databank;
        }

        public static String GetConnectString() 
        {
            DataBase sql = new DataBase();
            return $"SERVER={sql.Host}; DATABASE={sql.Databank}; UID = {sql.Username}; PASSWORD= {sql.Password}";

        }


        public static void InitConnection() 
        {
            
            Connection = new MySqlConnection(GetConnectString());
            try
            {
                Connection.Open();
                DatabaseСonnection = true;
                NAPI.Util.ConsoleOutput("[MYSQL] -> Связь установлена!");
                
            }
            catch (Exception e) 
            {
                DatabaseСonnection = false;
                NAPI.Util.ConsoleOutput("[MYSQL] -> Ошбика подключения к базе данных");
                NAPI.Util.ConsoleOutput("[MYSQL] ->" + e.ToString());
                NAPI.Task.Run(() => {  Environment.Exit(0); }, delayTime: 5000);
            }


        }
        //account

        public static bool AccountAlreadyExists(string name)
        {
            MySqlCommand command = Connection.CreateCommand();
            command.CommandText = "SELECT * FROM accounts WHERE name =@name LIMIT 1";
            command.Parameters.AddWithValue("@name", name);
            using (MySqlDataReader reader = command.ExecuteReader()) 
            {
                if (reader.HasRows) 
                {
                    return true;
                }
            }
            return false;
        }
        public static void NewAccountRegistred(Account account, string password) 
        {
            string saltedpw = BCrypt.HashPassword(password, BCrypt.GenerateSalt()); // переменная с зашифроваанным паролем от BCrypt 

            try
            {
                MySqlCommand command = Connection.CreateCommand();
                command.CommandText = "INSERT INTO accounts (password, name, adminlvl, cash) VALUES (@password, @name, @adminlvl, @cash)";
                command.Parameters.AddWithValue("@password", saltedpw); 
                command.Parameters.AddWithValue("@name", account.Name);
                command.Parameters.AddWithValue("@adminlvl", account.Adminlvl);
                command.Parameters.AddWithValue("@cash", account.Cash);

                command.ExecuteNonQuery();
                account.ID = (int)command.LastInsertedId;

            }
            catch(Exception e) 
            {
                NAPI.Util.ConsoleOutput($"[NewAccountRegistred] -> " + e.ToString());
            }
        }

        public static void AccountLoad(Account account) 
        {
            MySqlCommand command = Connection.CreateCommand();
            command.CommandText = "SELECT * FROM accounts WHERE name=@name LIMIT 1";

            command.Parameters.AddWithValue("@name", account.Name);

            using (MySqlDataReader reader = command.ExecuteReader())
            {
                if (reader.HasRows) 
                {
                    reader.Read();
                    account.ID = reader.GetInt16("id");
                    account.Adminlvl = reader.GetInt16("adminlvl");
                    account.Cash = reader.GetInt32("cash");

                }
            }
        }

        public static void AccountSave(Account account) 
        {
            MySqlCommand command = Connection.CreateCommand();
            command.CommandText = "UPDATE accounts SET adminlvl = @adminlvl, cash=@cash WHERE id=@id";
            command.Parameters.AddWithValue("@adminlvl", account.Adminlvl);
            command.Parameters.AddWithValue("@cash", account.Cash);
            command.Parameters.AddWithValue("id", account.ID);
        }
        public static bool PasswordCheck(string name, string passwordinput) 
        {
            string password = "";
            MySqlCommand command = Connection.CreateCommand();
            command.CommandText = "SELECT password FROM accounts WHERE name=@name LIMIT 1";
            command.Parameters.AddWithValue("@name", name);

            using (MySqlDataReader reader = command.ExecuteReader()) 
            {
                if (reader.HasRows) 
                {
                    reader.Read();
                    password = reader.GetString("password");
                }
            }

            if (CheckPassword(passwordinput, password))
            {
                return true;
            } 

            return false;
        }
    }   
}
