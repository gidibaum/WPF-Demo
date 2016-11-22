using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Base;

namespace AddressBook.Models
{
    public class Address : ObservableObject
    {

        #region Property: Street

        string _Street;

        public string Street
        {
            get { return _Street; }
            set { SetProperty(ref _Street, value); }
        }

        #endregion

        #region Property: Number

        string _Number;

        public string Number
        {
            get { return _Number; }
            set { SetProperty(ref _Number, value); }
        }

        #endregion

        #region Property: City

        string _City;

        public string City
        {
            get { return _City; }
            set { SetProperty(ref _City, value); }
        }

        #endregion

        #region Property: Country

        string _Country;

        public string Country
        {
            get { return _Country; }
            set { SetProperty(ref _Country, value); }
        }

        #endregion

        public Address()
        {
            
        }

        public Address(XElement xml)
        {
            if (xml == null) return;
            Street = xml.GetAttribute<string>(nameof(Street));
            Number = xml.GetAttribute<string>(nameof(Number));
            City = xml.GetAttribute<string>(nameof(City));
            Country = xml.GetAttribute<string>(nameof(Country));
        }

        public XElement ToXml()
        {
            var xml = new XElement("Address");

            xml.AddAttribute(nameof(Street), Street);
            xml.AddAttribute(nameof(Number), Number);
            xml.AddAttribute(nameof(City), City);
            xml.AddAttribute(nameof(Country), Country);

            return xml;
        }

        public Address Clone() => new Address
        {
            Street = Street,
            Number = Number,
            City = City,
            Country = Country
        };
    }
}
