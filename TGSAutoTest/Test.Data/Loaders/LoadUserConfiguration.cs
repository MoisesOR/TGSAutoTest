using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using TGSAutoTest.Test.Data.Resources.Models;

namespace TGSAutoTest.Test.Data.Loaders
{
    class LoadUserConfiguration
    {
        public List<User> _configScenarioTestCases;

        public LoadUserConfiguration()
        {
            XmlRootAttribute xRoot = new XmlRootAttribute();
            xRoot.ElementName = "Users";
            XmlSerializer serializerScenarioTestCases = new XmlSerializer(typeof(List<User>), xRoot);
            using (FileStream file = File.OpenRead(TestContext.CurrentContext.TestDirectory + @"\Test.Data\Resources\XML\Users.xml"))
            {
                _configScenarioTestCases = (List<User>)serializerScenarioTestCases.Deserialize(file);
            }
        }

        public User GetUser(string userType)
        {
            User user;
            user = _configScenarioTestCases.Find(x => String.Equals(x.UserType, userType));
            return user;
        }
    }
}
