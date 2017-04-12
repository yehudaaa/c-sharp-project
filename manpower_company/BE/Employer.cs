using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    /// <summary>
    /// Gender = gender of the CEO
    /// AmountOfWorkers = the amount of workers at the company
    /// </summary>
    public class Employer
    {

        // ------------------------------------------------------------------//
        // Fields Declaration - Properties

        public uint CompanyNumber { get; set; } // the ID of Company or ID of manager 
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public GENDER Gender { get; set; } // the gender of manager 
        public DateTime EstablishmentDate { get; set; }
        public bool IfPrivate { get; set; }
        public string EmployerPhoneNumber { get; set ; }
        public string EmployerAddress { get; set; }
        public string CompanyAddress { get; set; }
        public string CompanyName { get; set; }
        public FIELD_NAME Field { get; set; }
        public int AmountOfWorkers { get; set; }

        // ------------------------------------------------------------------//
        //ctor
        public Employer() {; }
        public Employer(uint compNum, bool ifPriv, string fName, string lName, GENDER g,string compName,
            string empPhone, string empAddress, string compAddress, FIELD_NAME f, DateTime establish, int amountWorkers)
        {
            CompanyNumber = compNum;
            IfPrivate = ifPriv;
            FirstName = fName;
            LastName = lName;
            Gender = g;
            CompanyName = compName;
            EmployerPhoneNumber = empPhone;
            EmployerAddress = empAddress;
            CompanyAddress = compAddress;
            Field = f;
            EstablishmentDate = establish;
            AmountOfWorkers = amountWorkers;
        }

        //copy constractor
        public Employer(Employer t)
        {
            CompanyNumber = t.CompanyNumber;
            IfPrivate = t.IfPrivate;
            FirstName = t.FirstName;
            LastName = t.LastName;
            Gender = t.Gender;
            CompanyName = t.CompanyName;
            EmployerPhoneNumber = t.EmployerPhoneNumber;
            EmployerAddress = t.EmployerAddress;
            CompanyAddress = t.CompanyAddress;
            Field = t.Field;
            EstablishmentDate = t.EstablishmentDate;
            AmountOfWorkers = t.AmountOfWorkers;
        }
        // ------------------------------------------------------------------//

        // Function
        public override string ToString()
        {
            string str = "Employee:\n";
            str += "Name - " + FirstName + " " + LastName + "\n";
            str += "Number - " + EmployerPhoneNumber + "\n";
            str += "Address - " + EmployerAddress;

            return str;
        }
        // ------------------------------------------------------------------//
    }
}
