using NUnit.Framework;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using TGSAutoTest.Entities;

namespace TGSAutoTest.Test.Data.DataBaseConector
{
    public class AlbumConection
    {
        private static readonly string Conexion = TestContext.Parameters["conectionBBDD"];

        private Album ReadAlbumByName(string albumName)
        {
            Album album = new Album();
            using (SqlConnection connection = new SqlConnection(Conexion))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = CommandType.Text;
                    command.CommandText = "select * from dbo.AlbumList a where a.Album=@AlbumName";
                    command.Parameters.AddWithValue("@AlbumName", albumName);

                    connection.Open();
                    SqlDataReader myReader = command.ExecuteReader();
                    while (myReader.Read())
                    {
                        album.Id = Convert.ToInt32(myReader["Id"]);
                        album.Name = myReader["Album"].ToString();
                        album.Year = Convert.ToInt32(myReader["Year"]);
                        album.Artist = myReader["Artist"].ToString();
                        album.Genres = myReader["Genre"].ToString();
                        album.SubGenres = myReader["Subgenre"].ToString();
                    }
                }
            }
            return album;
        }

        public bool CheckAlbumName(Album album)
        {
            Thread.Sleep(500);
            bool exist = false;
            Console.WriteLine("Album Name: {0}, en la base de datos: {1}", album.Name, ReadAlbumByName(album.Name));
            if (ReadAlbumByName(album.Name).Name == album.Name)
            {
                exist = true;
            }
            return exist;
        }

        public int GetAlbumId(Album album)
        {
            Thread.Sleep(500);
            return ReadAlbumByName(album.Name).Id;
        }
    }
}