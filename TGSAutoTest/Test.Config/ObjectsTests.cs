using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TGSAutoTest.Test.Data.Loaders;
using TGSAutoTest.Test.Data.Resources.Models;

namespace TGSAutoTest.Test.Config
{
    public class ObjectsTests
    {
        readonly LoadUserConfiguration loadUserConfiguration = new LoadUserConfiguration();

        public User UserType(string userType)
        {
            User user = loadUserConfiguration.GetUser(userType);
            if (user.UserName == null)
            {
                user.UserName = "";
            }
            if (user.Password == null)
            {
                user.Password = "";
            }
            return user;
        }
    }
}
