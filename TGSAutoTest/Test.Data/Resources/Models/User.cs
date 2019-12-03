using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TGSAutoTest.Test.Data.Resources.Models
{
    [XmlRoot("User")]
    public class User
    {
        [XmlElement(ElementName = "UserType")]
        public string UserType { get; set; }

        [XmlElement(ElementName = "UserName")]
        public string UserName { get; set; }

        [XmlElement(ElementName = "Password")]
        public string Password { get; set; }

        [XmlElement(ElementName = "NewUser")]
        public string NewUser { get; set; }

        [XmlElement(ElementName = "NewPassword")]
        public string NewPassword { get; set; }

        [XmlElement(ElementName = "NewPassword2")]
        public string NewPassword2 { get; set; }

    }
}
