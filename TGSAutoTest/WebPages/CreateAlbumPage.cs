using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TGSAutoTest.Entities;
using TGSAutoTest.SetUp;
using TGSAutoTest.WebPages.Base;

namespace TGSAutoTest.WebPages
{
    public class CreateAlbumPage : Page
    {
        private IList<IWebElement> AllForm
        {
            get { return WebDriver.FindElementsByCssSelector("form input"); }
        }
        private IWebElement NameInput
        {
            get { return WebDriver.FindElementById("albumNameInput"); }
        }
        private IWebElement ArtistInput
        {
            get { return WebDriver.FindElementById("albumArtistInput"); }
        }
        private IWebElement YearInput
        {
            get { return WebDriver.FindElementById("albumYearInput"); }
        }
        private IWebElement GenresInput
        {
            get { return WebDriver.FindElementById("genresInput"); }
        }
        private IWebElement SubGenresInput
        {
            get { return WebDriver.FindElementById("subGenresInput"); }
        }
        private IWebElement CreateAlbumButton
        {
            get { return WebDriver.FindElementByCssSelector("div.form-row>button.btn-primary"); }
        }
        private IWebElement ErrorDiv
        {
            get { return WebDriver.FindElementById("textCreationAlbum"); }
        }

        public CreateAlbumPage(ISetUpWebDriver setUpWebDriver) : base(setUpWebDriver)
        {
        }

        public CreateAlbumPage CrearAlbum(Album album)
        {
            foreach (var input in AllForm)
            {
                input.Clear();
            }
            Thread.Sleep(1000);
            NameInput.SendKeys(album.Name);
            Thread.Sleep(1000);
            ArtistInput.SendKeys(album.Artist);
            Thread.Sleep(1000);
            YearInput.SendKeys(album.Year.ToString());
            Thread.Sleep(1000);
            GenresInput.SendKeys(album.Genres);
            Thread.Sleep(1000);
            SubGenresInput.SendKeys(album.SubGenres);
            Thread.Sleep(1000);
            return this;
        }

        public CreateAlbumPage Continue()
        {
            CreateAlbumButton.Click();
            Thread.Sleep(1000);
            return this;
        }

        public bool CheckErrorDiv()
        {
            bool displayed = false;
            if(ErrorDiv.Text == "Error")
            {
                displayed = true;
            }
            return displayed;
        }
    }
}
