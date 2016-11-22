using AddressBook.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AddressBook.Services
{
    public static class PeopleRepository
    {
        public static Task<List<Person>> Load()
        {           
            return Task.Factory.StartNew(() =>
            {
                var people = new List<Person>();

                try
                { 
                    var filename = Filename;

                    if (File.Exists(filename))
                    {
                        var xml = XElement.Load(filename);

                        people.AddRange(xml.Elements("Person")
                            .Select(item => new Person(item)));
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("Failed to load People data");
                }

                return people;
            });
        }

        static string Folder => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "WPF-Demo");
        static string Filename => Path.Combine(Folder, "AddressBook.xml");


        public static Task Save(IEnumerable<Person> people)
        {
            return Task.Factory.StartNew(() =>
            {
                var folder = Folder;

                if (!Directory.Exists(folder))
                    Directory.CreateDirectory(folder);

                var xml = new XElement("AddressBook");

                foreach (var p in people)
                {
                    xml.Add(p.ToXml());
                }

                xml.Save(Filename);
            });
        }

        public static List<Person> CreateTestData()
        {
            return new List<Person>
            {
                new Person
                {
                    FirstName = "James",
                    LastName = "Bond",
                    BirthDate = new DateTime(1953, 6, 4),
                    Gender = Gender.Male,
                    Address = new Address
                    {
                        Country = "Great Britain",
                        City = "London",
                        Street = "Downing",
                        Number = "13"
                    }
                },
                new Person
                {
                    FirstName = "Sherlock",
                    LastName = "Holmes",
                    BirthDate = new DateTime(1890, 12, 11),
                    Gender = Gender.Male,
                    Address = new Address
                    {
                        Country = "Great Britain",
                        City = "London",
                        Street = "Baker",
                        Number = "212b"
                    }
                },
                new Person
                {
                    FirstName = "Bruss",
                    LastName = "Willis",
                    BirthDate = new DateTime(1960, 12, 13),
                    Gender = Gender.Male,
                    Address = new Address
                    {
                        Country = "USA",
                        City = "LA",
                        Street = "Beverly Avn.",
                        Number = "999"
                    }
                }
            };

        }
    }
}
