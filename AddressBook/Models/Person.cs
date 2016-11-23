using System;
using System.Xml.Linq;
using System.ComponentModel.DataAnnotations;
using AddressBook.Properties;
using Base;
using Base.WPF.Validation;

namespace AddressBook.Models
{
    public enum Gender
    {
        Male,
        Female
    }

    public class Person : ValidationObject
    {
        #region Property: FirstName

        string _FirstName;

        [Required(AllowEmptyStrings = false)] [MinLength(3)]
        public string FirstName
        {
            get { return _FirstName; }
            set
            {
                SetProperty(ref _FirstName, value);
                RaisePropertyChanged(nameof(FullName));
            }
        }

        #endregion

        #region Property: LastName

        string _LastName;


        [Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "Person_LastName_User_Name_is_required_")]
        [MinLength(3, ErrorMessage = "More than 2 characters")]
        [StringLength(50, ErrorMessage = "No more than 50 characters")]
        public string LastName
        {
            get { return _LastName; }
            set
            {
                SetProperty(ref _LastName, value);
                RaisePropertyChanged(nameof(FullName));
            }
        }

        #endregion

        #region Property: BirthDate

        DateTime? _BirthDate;
        [DateOfBirth(MinAge = 18, MaxAge = 120, ErrorMessage = "Invalid Age")]
        [Required]
        public DateTime? BirthDate
        {
            get { return _BirthDate; }
            set
            {
                SetProperty(ref _BirthDate, value);

                if (_BirthDate == null)
                {
                    Age = 0;
                }
                else
                {
                    var now = DateTime.Today;
                    var bday = _BirthDate.Value;
                    var age = now.Year - bday.Year;

                    if (now.Month < bday.Month || (now.Month == bday.Month && now.Day < bday.Day))
                        age--;

                    Age = age;
                }
                RaisePropertyChanged(nameof(Age));
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


        #region Property: Id

        string _Id;

        [Required(ErrorMessage = "This field is required.")]
        [RegularExpression(@"^\d\d{3,15}$", ErrorMessage = "Please enter up to 15 digits for Id")]

        public string Id
        {
            get { return _Id; }
            set { SetProperty(ref _Id, value); }
        }

        #endregion



        #region Property: Email

        string _Email;

        [Required(ErrorMessage = "Email Adress is required.")]
        [EmailAddress]
        public string Email
        {
            get { return _Email; }
            set { SetProperty(ref _Email, value); }
        }

        #endregion


        public string FullName => $"{FirstName} {LastName}";

        public int Age { get; private set; }

        public Person()
        {
            Address = new Address();
            RaisePropertyChanged();
        }

        public Person(XElement xml)
        {
            FirstName = xml.GetAttribute<string>(nameof(FirstName));
            LastName = xml.GetAttribute<string>(nameof(LastName));
            Email = xml.GetAttribute<string>(nameof(Email));
            Id = xml.GetAttribute<string>(nameof(Id));

            var dateStr = xml.GetAttribute<string>(nameof(BirthDate));
            if (dateStr.IsNotEmpty())
                BirthDate = DateTime.Parse(dateStr);

            var genderStr = xml.GetAttribute<string>(nameof(Gender));
            if (genderStr.IsNotEmpty())
                Gender = (Gender)Enum.Parse(typeof(Gender), genderStr);

            Address = new Address(xml.Element("Address"));
        }

        public XElement ToXml()
        {
            var xml = new XElement("Person");

            xml.AddAttribute(nameof(FirstName), FirstName);
            xml.AddAttribute(nameof(LastName), LastName);
            xml.AddAttribute(nameof(Email), Email);
            xml.AddAttribute(nameof(Id), Id);

            if (BirthDate != null)
                xml.AddAttribute(nameof(BirthDate), BirthDate?.ToShortDateString());

            xml.AddAttribute(nameof(Gender), Gender);
            xml.Add(Address?.ToXml());

            return xml;
        }

        public void Copy(Person src)
        {
            FirstName = src.FirstName;
            LastName = src.LastName;
            BirthDate = src.BirthDate;
            Gender = src.Gender;
            Email = src.Email;
            Id = src.Id;
            Address = src.Address.Clone();
        }

        public Person Clone() => new Person
        {
            FirstName = FirstName,
            LastName = LastName,
            BirthDate = BirthDate,
            Gender = Gender,
            Id = Id,
            Email = Email,
            Address = Address.Clone()
        };
    }
}
