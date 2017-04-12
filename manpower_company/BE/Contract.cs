using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Contract: IComparable
    {
        public static int j = 0; // static run number

        // ------------------------------------------------------------------//
        // Fields Declaration - Properties
        public int ContractNumber { get; set; }
        public int EmployerNumber { get; set; }
        public int EmployeeNumber { get; set; }
        public bool Interview { get; set; }
        public bool ContractSigned { get; set; }
        public float NetHourWage { get; set; }
        public float GrossHourWage { get; set; }
        public DateTime BeginEmployment { get; set; }
        public DateTime FinishEmployment { get; set; }
        public int ContractHours { get; set; }
        public FIELD_NAME Field { get; set; }
        public LIVING_AREA LivingArea { get; set; }
        public int SpecializationNumber { get; set; }

        // ------------------------------------------------------------------//
        // ctor
        public Contract() {; }

        public Contract(int _EmployerNumber, int _EmployeeNumber, bool _Interview, bool _ContractSigned,
           float _GrossHourWage, DateTime _BeginEmployment, DateTime _FinishEmployment, int _ContractHours, FIELD_NAME f,
           LIVING_AREA area, int _SpecializationNumber)
        {
            ContractNumber = _SpecializationNumber + (++j);
            EmployerNumber = _EmployerNumber;
            EmployeeNumber = _EmployeeNumber;
            Interview = _Interview;
            ContractSigned = _ContractSigned;
            NetHourWage = -1; // this value is calculated at the BL layer
            GrossHourWage = _GrossHourWage;
            BeginEmployment = _BeginEmployment;
            FinishEmployment = _FinishEmployment;
            ContractHours = _ContractHours;
            Field = f;
            SpecializationNumber = _SpecializationNumber;
            LivingArea = area;
        }

        // copy constractor
        public Contract(Contract t)
        {
            ContractNumber = t.ContractNumber;
            EmployerNumber = t.EmployerNumber;
            EmployeeNumber = t.EmployeeNumber;
            Interview = t.Interview;
            ContractSigned = t.ContractSigned;
            NetHourWage = t.NetHourWage;
            GrossHourWage = t.GrossHourWage;
            BeginEmployment = t.BeginEmployment;
            FinishEmployment = t.FinishEmployment;
            ContractHours = t.ContractHours;
            Field = t.Field;
            SpecializationNumber = t.SpecializationNumber;
            LivingArea = t.LivingArea;
        }
        // ------------------------------------------------------------------//
        // Function

        public override string ToString()
        {
            string str = "Contract:\n";
            str += string.Format("This contract is between - {0} And {1}\n" ,EmployerNumber,EmployeeNumber);
            str += string.Format("Date of begin employment - {0}\n Date of final employment - {1}\n",
                BeginEmployment, FinishEmployment);

            return str;
        }

        public int CompareTo(object obj)
        {
            Contract tmp = obj as Contract;
            if (tmp != null)
            {
                return NetHourWage.CompareTo(tmp.NetHourWage);
            }
            return -1;
        }

    }
}
