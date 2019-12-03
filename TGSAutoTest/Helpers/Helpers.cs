using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TGSAutoTest.Utils
{
    public class Helpers
    {
        const string Test = "TEST";
        static Random random = new Random();

        public static int GetRandomDay()
        {
            int randomDay = random.Next(1, 31);
            return randomDay;
        }

        public static string GetRandomString(int length)
        {
            const string letters = "abcdefghijklmnopqrstuvwxyz";
            var chars = Enumerable.Range(0, length)
                .Select(x => letters[random.Next(0, letters.Length)]);
            return new string(chars.ToArray());
        }

        public static int GetRandomYear()
        {
            int randomYear = random.Next(0, DateTime.Today.Year);
            return randomYear;
        }

        public static string GenerateName()
        {
            string generatedFirstName = Test + GetRandomString(7);
            return generatedFirstName;
        }

        public static string GenerateLastName()
        {
            string generatedLastName = Test + GetRandomString(10);
            return generatedLastName;
        }

        public static int GetRandomNumberBetween(int start, int end)
        {
            int randomNumber = random.Next(start, end);
            return randomNumber;
        }

        public static string GenerateArtist()
        {
            string artistName = "Artist" + GetRandomString(7);
            return artistName;
        }
    }
}
