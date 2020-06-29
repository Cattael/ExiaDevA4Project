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
    public static class BDDConnection
    {
        static private SqlConnection connection;

        public static void OpenConnection(string costring)
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
        public static bool CheckTokenApp(string token)
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
        public static bool CheckTokenUser(string token)
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

        public static int getUserID(string username)
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

        public static void UpdateUserToken(int id, string token)
        {
            ExeSqlQuery("UPDATE Accounts SET Token ='" + token + "', Last=GETDATE() WHERE id=" + id + ";");
        }

        public static User getUserInfo(string username)
        {
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = "Select ID, Username, Token, Last  FROM Accounts WHERE Username='" + username + "';";
            SqlDataReader dr = cmd.ExecuteReader();
            dr.Read();
            User user = new User() { ID = (int)dr.GetValue(0), Username = dr.GetValue(1).ToString(), Token = dr.GetValue(2).ToString(), Last = DateTime.Parse(dr.GetValue(3).ToString()), };
            dr.Close();
            cmd.Dispose();
            return user; 
        }

        public static User getUserInfoByToken(string token)
        {
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = "Select ID, Username, Token, Last  FROM Accounts WHERE Token='" + token + "';";
            SqlDataReader dr = cmd.ExecuteReader();
            dr.Read();
            User user = new User() { ID = (int)dr.GetValue(0), Username = dr.GetValue(1).ToString(), Token = dr.GetValue(2).ToString(), Last = DateTime.Parse(dr.GetValue(3).ToString()), };
            dr.Close();
            cmd.Dispose();
            return user;
        }

        public static bool UserPasswordExist(string username, string password)
        {
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = "Select ID FROM Accounts WHERE Username='" + username + "' AND Password='" + password + "';";
            SqlDataReader dr = cmd.ExecuteReader();
            bool output = dr.Read(); 
            cmd.Dispose();
            dr.Close(); 
            return output;
        }

        public static void RegisterRequest(string token, string email)
        {
            ExeSqlQuery("INSERT INTO Requests VALUES('"); 
        }

        public static void AddApp(string name, DateTime expDate)
        {
            ExeSqlQuery("Insert Into AppTokens Values('" + TokenGen.GetInstance.GenerateToken(15) + "','" + name +"' , GETDATE(),'" + expDate.ToString() + "')");
        }

        public static void AddAccount(string username, string password, string email)
        {
            ExeSqlQuery("Insert Into Accounts Values('" + username + "','" + password + "', GETDATE(), NULL, '" + email + "')");
        }

        public static void ResetTokenUser()
        {
            ExeSqlQuery("Update Accounts Set Token=NULL");
            Console.WriteLine("All Token user reset.");
        }

        public static void ResetTokenApp(string name)
        {
            string token = TokenGen.GetInstance.GenerateToken(15);
            ExeSqlQuery("Update AppTokens Set Token='" + token + "' WHERE AppName=" + name + ";");
            Console.WriteLine(name + " token's has been changed to " + token);
        }
        private static void ExeSqlQuery(string command)
        {
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = command; 
            cmd.ExecuteNonQuery();
            cmd.Dispose();
        }

        public static void CloseConnectino()
        {
            connection.Close();
        }
    }
}
