using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Base;

namespace AddressBook.Models
{
    public enum Gender
    {
        Male,
        Female
    }

    public class Person : ObservableObject
    {

        #region Property: FirstName

        string _FirstName;

        public string FirstName
        {
            get { return _FirstName; }
            set { SetProperty(ref _FirstName, value); }
        }

        #endregion

        #region Property: LastName

        string _LastName;

        public string LastName
        {
            get { return _LastName; }
            set { SetProperty(ref _LastName, value); }
        }

        #endregion

        #region Property: BirthDate

        DateTime _BirthDate;

        public DateTime BirthDate
        {
            get { return _BirthDate; }
            set
            {
                SetProperty(ref _BirthDate, value);

                var now = DateTime.Today;

                var age = now.Year - BirthDate.Year;

                if (now.Month < BirthDate.Month || (now.Month == BirthDate.Month && now.Day < BirthDate.Day))
                    age--;

                Age = age;
            }
        }

        #endregion

        #region Property: Address

        Address _Address;

        public Address Address
        {
            get { return _Address; }
            set { SetProperty(ref _Address, value); }
        }

        #endregion

        #region Property: Gender

        Gender _Gender;

        public Gender Gender
        {
            get { return _Gender; }
            set { SetProperty(ref _Gender, value); }
        }

        #endregion

        public int Age { get; private set; }

        public Person()
        {
            
        }

        public Person(XElement xml)
        {
            FirstName = xml.GetAttribute<string>(nameof(FirstName));
            LastName = xml.GetAttribute<string>(nameof(LastName));
            var dateStr = xml.GetAttribute<string>(nameof(BirthDate));
            if (dateStr.IsNotEmpty())
                BirthDate = DateTime.Parse(dateStr);

            var genderStr = xml.GetAttribute<string>(nameof(Gender));
            if (genderStr.IsNotEmpty())
                Gender = (Gender)Enum.Parse(typeof(Gender), genderStr);
        }

        public XElement ToXml()
        {
            var xml = new XElement("Person");

            xml.AddAttribute(nameof(FirstName), FirstName);
            xml.AddAttribute(nameof(LastName), LastName);
            xml.AddAttribute(nameof(BirthDate), BirthDate.ToShortDateString());
            xml.AddAttribute(nameof(Gender), Gender);
            xml.Add(Address?.ToXml());

            return xml;
        }

        public Person Clone() => new Person
        {
            FirstName = FirstName,
            LastName = LastName,
            BirthDate = BirthDate,
            Gender = Gender,
            Address = Address.Clone()
        };
    }
}
