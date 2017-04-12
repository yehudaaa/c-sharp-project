using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Employee :IComparable
    {
        // ------------------------------------------------------------------//
        // Fields Declaration - Properties
        public uint ID { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public DateTime BirthDate { get; set; }
        public int Age { get; set; }
        public GENDER Gender { get; set; }
        public bool Married { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public EDUCATION Education { get; set; }
        public bool ArmyService { get; set; }
        public BankAccount BAccount { get; set; }
        public uint SpecializationNumber { get; set; }
        public float YearsExperience { get; set; }// the years of experience that the worker has
        public string Email { get; set; }
        // ------------------------------------------------------------------//
        //ctor
        public Employee() {; }
        public Employee(uint id, string lName, string fName, bool married, DateTime birth, string phone,
            string address, GENDER g, EDUCATION e, bool service, BankAccount bank, uint specNum, float experience, string email)
        {
            ID = id;
            LastName = lName;
            FirstName = fName;
            BirthDate = birth;
            Married = married;
            BirthDate = birth;
            PhoneNumber = phone.ToString();
            Address = address;
            Education = e;
            Gender = g;
            ArmyService = service;
            BAccount = bank;
            SpecializationNumber = specNum;
            YearsExperience = experience;
            Age = (DateTime.Now - birth).Days / 365;
            Email = email;
        }

        //copy constractor
        public Employee(Employee t)
        {
            ID = t.ID;
            LastName = t.LastName;
            FirstName = t.FirstName;
            BirthDate = t.BirthDate;
            Married = t.Married;
            BirthDate = t.BirthDate;
            PhoneNumber = t.PhoneNumber;
            Address = t.Address;
            Education = t.Education;
            Gender = t.Gender;
            ArmyService = t.ArmyService;
            BAccount = t.BAccount;
            SpecializationNumber = t.SpecializationNumber;
            YearsExperience = t.YearsExperience;
            Age = t.Age;
            Email = t.Email;
        }
        // ------------------------------------------------------------------//

        // Function
        public override string ToString()
        {
            string str = "Employee:\n ";
            str += "Name - " + FirstName + " " + LastName + "\n";
            str += "Number - " + PhoneNumber + "\n";
            str += "Address - " + Address;

            return str;
        }

        public int CompareTo(object obj)
        {
            Employee tmp = obj as Employee;
            if (tmp != null)
            {
                return Age.CompareTo(tmp.Age);
            }
            return -1;
        }
        // ------------------------------------------------------------------//
    }
}
