using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Specialization
    {
        public static int i = 0; // static run number
        // ------------------------------------------------------------------//
        // Fields Declaration - Properties

        public uint SpecializationNumber { get; set; }
        public FIELD_NAME Field { get; set; }
        public string SpecializationName { get; set; }
        public float MinHourWage { get; set; }
        public float MaxHourWage { get; set; }

        // ------------------------------------------------------------------//
        // Ctor and default ctor

        public Specialization() {; }
        public Specialization(FIELD_NAME f, string spName, float min, float max, int sNum = -1)
        {
            if (sNum == -1)
            {
                SpecializationNumber = (uint)f * 10000000 + (uint)(++i * 100000); // the first digit of number represent the FIELD_NAME,
                                                                                  // and the second one represent the Specialization uniqe code
            }
            else
                SpecializationNumber = (uint)sNum;
            Field = f;
            SpecializationName = spName;
            MinHourWage = min;
            MaxHourWage = max;
        }

        //copy constractor
        public Specialization(Specialization t)
        {
            SpecializationNumber = t.SpecializationNumber;
            Field =t.Field;
            SpecializationName = t.SpecializationName;
            MinHourWage = t.MinHourWage;
            MaxHourWage = t.MaxHourWage;
        }
        // Function
        public override string ToString()
        {
            string str = "Specialization:\n";
            str += "Field - " + Field.ToString() + "\n";
            str += "Name - " + SpecializationName + "\n";
            str += "Number - " + SpecializationNumber.ToString() + "\n";
            str += string.Format("Wage range per hour {0} - {1}", MinHourWage, MaxHourWage);

            return str;
        }
        // ------------------------------------------------------------------//
    }
}
