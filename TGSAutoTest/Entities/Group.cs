namespace TGSAutoTest.Entities
{
    public class Group
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int StartYear { get; set; }
        public int EndYear { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string URLWiki { get; set; }
        public string Description { get; set; }

        public Group() { }

        public Group(int id, string name, int startYear, int endYear, string country, string city, string uRLWiki, string descripion)
        {
            this.Id = id;
            this.Name = name;
            this.StartYear = startYear;
            this.EndYear = endYear;
            this.Country = country;
            this.City = city;
            this.URLWiki = uRLWiki;
            this.Description = descripion;
        }

        public Group(string name, int startYear, int endYear, string country, string city, string uRLWiki, string descripion)
        {
            this.Name = name;
            this.StartYear = startYear;
            this.EndYear = endYear;
            this.Country = country;
            this.City = city;
            this.URLWiki = uRLWiki;
            this.Description = descripion;
        }
    }
}
