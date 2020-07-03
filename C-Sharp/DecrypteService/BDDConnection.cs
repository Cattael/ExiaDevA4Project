using DecrypteServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.ServiceModel.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DecrypteService
{
    public class BDDConnection
    {
        static private SqlConnection connection;
        private static BDDConnection instance;
        public static BDDConnection GetInstance
        {
            get
            {
                if (instance == null)
                    instance = new BDDConnection();
                return instance;
            }
        }

        private BDDConnection() { }

        public void OpenConnection(string costring)
        {
            connection = new SqlConnection(costring);
            try
            {
                connection.Open();
            }
            catch (SqlException sqlex)
            {
                connection.Close();
                MessageBox.Show(sqlex.Message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public bool CheckTokenApp(string token)
        {
            bool output = false; 
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = "Select ID FROM AppTokens WHERE Token='" + token + "';";
            SqlDataReader dr = cmd.ExecuteReader();
            dr.Read();
            if (dr.FieldCount > 0)
                output = true; 
            dr.Close();
            cmd.Dispose();
            return output;
        }
        public bool CheckTokenUser(string token)
        {
            bool output = false; 
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = "Select * FROM Accounts WHERE Token='" + token+ "';";
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                output = true; 
            }
            dr.Close();
            cmd.Dispose();
            return output; 
        }

        public string getMailFromRequest(string token)
        {
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = "Select email FROM Requests WHERE Token='" + token+ "';";
            SqlDataReader dr = cmd.ExecuteReader();
            dr.Read();
            string mail = dr.GetValue(0).ToString();
            dr.Close();
            cmd.Dispose();
            return mail;
        }

        public int getUserID(string username)
        {
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = "Select ID FROM Accounts WHERE Username='" + username + "';";
            SqlDataReader dr = cmd.ExecuteReader();
            dr.Read();
            int id = (int)dr.GetValue(0);
            dr.Close();
            cmd.Dispose();
            return id; 
        }

        public void UpdateUserToken(int id, string token)
        {
            ExeSqlQuery("UPDATE Accounts SET Token ='" + token + "', Last=GETDATE() WHERE id=" + id + ";");
        }

        public User getUserInfo(string username)
        {
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = "Select ID, Username, Token, Last, email  FROM Accounts WHERE Username='" + username + "';";
            SqlDataReader dr = cmd.ExecuteReader();
            dr.Read();
            User user = new User() { 
                ID = (int)dr.GetValue(0), 
                Username = dr.GetValue(1).ToString(), 
                Token = dr.GetValue(2).ToString(), 
                Last = DateTime.Parse(dr.GetValue(3).ToString()),
                email = dr.GetValue(4).ToString()
            };
            dr.Close();
            cmd.Dispose();
            return user; 
        }

        public User getUserInfoByToken(string token)
        {
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = "Select ID, Username, Token, Last, Email  FROM Accounts WHERE Token='" + token + "';";
            SqlDataReader dr = cmd.ExecuteReader();
            dr.Read();
            User user = new User() { 
                ID = (int)dr.GetValue(0), 
                Username = dr.GetValue(1).ToString(), 
                Token = dr.GetValue(2).ToString(), 
                Last = DateTime.Parse(dr.GetValue(3).ToString()), 
                email = dr.GetValue(4).ToString() };
            dr.Close();
            cmd.Dispose();
            return user;
        }

        public bool UserPasswordExist(string username, string password)
        {
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = "Select ID FROM Accounts WHERE Username='" + username + "' AND Password='" + password + "';";
            SqlDataReader dr = cmd.ExecuteReader();
            bool output = dr.Read(); 
            cmd.Dispose();
            dr.Close(); 
            return output;
        }

        public string[] getTokenRequestFromEmail(string mail)
        {
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = "Select Token FROM Requests WHERE email='" + mail + "';";
            SqlDataReader dr = cmd.ExecuteReader();
            List<string> output = new List<string>(); 
            while (dr.Read())
            {
                output.Add(dr.GetValue(0).ToString());
            }
            dr.Close();
            return output.ToArray();
        }

        public int getPourcentRequest(string token)
        {
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = "Select Pourcent FROM Requests WHERE Token='" + token + "';";
            SqlDataReader dr = cmd.ExecuteReader();
            dr.Read();
            int output = (int)dr.GetValue(0);
            dr.Close();
            return output;
        }

        public void UpdateProgress(string tokenRequest, int value)
        {
            if (100 < value)
                value = 100;
            if (97 < value)
                value = 100; 
            ExeSqlQuery("Update Requests Set pourcent =" + value.ToString() + " where Token='" + tokenRequest + "';");
        }

        public void RegisterRequest(string token, string email)
        {
            ExeSqlQuery("INSERT INTO Requests VALUES('" + token + "', '"+ email +"', 0)"); 
        }

        public void AddApp(string name, DateTime expDate)
        {
            ExeSqlQuery("Insert Into AppTokens Values('" + TokenGen.GetInstance.GenerateToken(15) + "','" + name +"' , GETDATE(),'" + expDate.ToString() + "')");
        }

        public void AddAccount(string username, string password, string email)
        {
            ExeSqlQuery("Insert Into Accounts Values('" + username + "','" + password + "', GETDATE(), NULL, '" + email + "')");
        }

        public void ResetTokenUser()
        {
            ExeSqlQuery("Update Accounts Set Token=NULL");
            Console.WriteLine("All Token user reset.");
        }

        public void ResetTokenApp(string name)
        {
            string token = TokenGen.GetInstance.GenerateToken(15);
            ExeSqlQuery("Update AppTokens Set Token='" + token + "' WHERE AppName=" + name + ";");
            Console.WriteLine(name + " token's has been changed to " + token);
        }
        private void ExeSqlQuery(string command)
        {
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = command; 
            cmd.ExecuteNonQuery();
            cmd.Dispose();
        }

        public void CloseConnectino()
        {
            connection.Close();
        }
    }
}
