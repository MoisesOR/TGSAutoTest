using NUnit.Framework;
using System.Data;
using System.Data.SqlClient;

namespace TGSAutoTest.Test.Data.DataBaseConector
{
    public class UserConnection
    {
        private static readonly string Conexion = TestContext.Parameters["conectionBBDD"];

        private string ReadByName(string userName)
        {
            string user = "";
            using (SqlConnection connection = new SqlConnection(Conexion))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = CommandType.Text;
                    command.CommandText = "select * from dbo.UserList a where a.[User]=@userName";
                    command.Parameters.AddWithValue("@userName", userName);

                    connection.Open();
                    SqlDataReader myReader = command.ExecuteReader();
                    while (myReader.Read())
                    {
                        user = myReader["User"].ToString();
                    }
                }
            }
            return user;
        }

        private int DeleteByName(string userName)
        {
            using (SqlConnection connection = new SqlConnection(Conexion))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = CommandType.Text;
                    command.CommandText = @"DELETE from dbo.UserList 
                                            WHERE [User] = @UserName";
                    command.Parameters.AddWithValue("@UserName", userName);

                    connection.Open();
                    int recordsAffected = command.ExecuteNonQuery();
                    return recordsAffected;
                }
            }
        }

        public bool CheckUser(string userName)
        {
            bool exist = false;
            if (ReadByName(userName) == userName)
            {
                exist = true;
            }
            return exist;
        }

        public bool CheckIfUserDeleted(string userName)
        {
            bool deleted = false;
            if (DeleteByName(userName) == 1)
            {
                deleted = true;
            }
            return deleted;
        }
    }
}
