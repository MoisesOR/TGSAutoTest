using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TGSAutoTest.Entities;

namespace TGSAutoTest.Test.Data.DataBaseConector
{
    public class GroupConnection
    {
        private static readonly string Conexion = TestContext.Parameters["conectionBBDD"];

        private Group ReadGroupByName(string groupName)
        {
            Group group = new Group();
            using (SqlConnection connection = new SqlConnection(Conexion))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = CommandType.Text;
                    command.CommandText = "select * from dbo.GroupList a where a.[Group]=@GroupName";
                    command.Parameters.AddWithValue("@GroupName", groupName);

                    connection.Open();
                    SqlDataReader myReader = command.ExecuteReader();
                    while (myReader.Read())
                    {
                        group.Id = Convert.ToInt32(myReader["Id"]);
                        group.Name = myReader["Group"].ToString();
                        group.StartYear = Convert.ToInt32(myReader["StartYear"]);
                        group.EndYear = Convert.ToInt32(myReader["EndYear"]);
                        group.Country = myReader["Country"].ToString();
                        group.City = myReader["City"].ToString();
                        group.URLWiki = myReader["UrlWiki"].ToString();
                        group.Description = myReader["Description"].ToString();
                    }
                }
            }
            return group;
        }

        public bool CheckGroupName(Group group)
        {
            Thread.Sleep(500);
            bool exist = false;
            Console.WriteLine("Group Name: {0}, en la base de datos: {1}", group.Name, ReadGroupByName(group.Name));
            if (ReadGroupByName(group.Name).Name == group.Name)
            {
                exist = true;
            }
            return exist;
        }

        public int GetGroupId(Group group)
        {
            Thread.Sleep(500);
            return ReadGroupByName(group.Name).Id;
        }
    }
}
