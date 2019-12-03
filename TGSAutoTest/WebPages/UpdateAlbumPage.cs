using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Threading;
using TGSAutoTest.Entities;
using TGSAutoTest.SetUp;
using TGSAutoTest.WebPages.Base;

namespace TGSAutoTest.WebPages
{
    public class UpdateAlbumPage : Page
    {
        private IWebElement IdDropDown
        {
            get { return WebDriver.FindElementById("updateAlbum"); }
        }
        private IWebElement AlbumNameInput
        {
            get { return WebDriver.FindElementById("albumName"); }
        }
        private IWebElement AlbumArtistInput
        {
            get { return WebDriver.FindElementById("albumArtist"); }
        }
        private IWebElement AlbumYearInput
        {
            get { return WebDriver.FindElementById("albumYear"); }
        }
        private IWebElement GenresInput
        {
            get { return WebDriver.FindElementById("genres"); }
        }
        private IWebElement SubGenresInput
        {
            get { return WebDriver.FindElementById("subGenres"); }
        }
        private IWebElement UpdateButton
        {
            get { return WebDriver.FindElementByCssSelector("button.btn.btn-primary"); }
        }
        private IWebElement ErrorDiv
        {
            get { return WebDriver.FindElementById("textCreationAlbum"); }
        }
        
        public UpdateAlbumPage(ISetUpWebDriver setUpWebDriver) : base(setUpWebDriver)
        {
        }

        public bool IsError()
        {
            bool isShow = false;
            if (ErrorDiv.Text == "Error")
            {
                isShow = true;
            }
            return isShow;
        }

        public UpdateAlbumPage SelectId(int albumId)
        {
            new SelectElement(IdDropDown).SelectByText(albumId.ToString());
            return this;
        }

        public UpdateAlbumPage UpdateAlbum(Album album)
        {
            Thread.Sleep(1000);
            AlbumNameInput.Clear();
            AlbumNameInput.SendKeys(album.Name);
            Thread.Sleep(1000);
            AlbumArtistInput.Clear();
            AlbumArtistInput.SendKeys(album.Artist);
            Thread.Sleep(1000);
            AlbumYearInput.Clear();
            AlbumYearInput.SendKeys(album.Year.ToString());
            Thread.Sleep(1000);
            GenresInput.Clear();
            GenresInput.SendKeys(album.Genres);
            Thread.Sleep(1000);
            SubGenresInput.Clear();
            SubGenresInput.SendKeys(album.SubGenres);

            return this;
        }

        public UpdateAlbumPage Continue()
        {
            UpdateButton.Click();
            return this;
        }
    }
}
