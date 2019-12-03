namespace TGSAutoTest.Entities
{
    public class Album
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Artist { get; set; }
        public int Year { get; set; }
        public string Genres { get; set; }
        public string SubGenres { get; set; }

        public Album() { }

        public Album(int id, string name, string artist, int year, string genres, string subGenres)
        {
            this.Id = id;
            this.Name = name;
            this.Artist = artist;
            this.Year = year;
            this.Genres = genres;
            this.SubGenres = subGenres;
        }

        public Album(string name, string artist, int year, string genres, string subGenres)
        {
            this.Name = name;
            this.Artist = artist;
            this.Year = year;
            this.Genres = genres;
            this.SubGenres = subGenres;
        }
    }
}
