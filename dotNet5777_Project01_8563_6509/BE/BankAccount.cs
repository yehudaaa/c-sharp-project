using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    
    public struct BankAccount
    {
        public int bankNumber { get; set; }
        public string bankName { get; set; }
        public int branchNumber { get; set; }
        public string branchCity { get; set; }
        public string bankAddress { get; set; }
        public int accountNumber { get; set; }
      
        public BankAccount(int _bankNumber, string _bankName, int _branchNumber,string _bankAddress,string _branchCity,
            int _accountNumber = 0 )
        {
            bankNumber = _bankNumber;
            bankName = _bankName;
            branchNumber = _branchNumber;
            bankAddress = _bankAddress;
            branchCity = _branchCity;
            accountNumber = _accountNumber;
            
        }

        public BankAccount(BankAccount t)
        {
            bankNumber = t.bankNumber;
            bankName = t.bankName;
            branchNumber = t.branchNumber;
            bankAddress = t.bankAddress;
            branchCity = t.branchCity;
            accountNumber = t.accountNumber;
        }
        public override string ToString()
        {
            string str = "The Chosen Bank Account:\n";
            str += bankName.ToString() + "\n";
            str += branchNumber.ToString() + "\n";
            //str += "bankAddress - " + bankAddress.ToString() + "\n";
            //str += "branchCity - " + branchCity.ToString() + "\n";
            str += accountNumber.ToString() + "\n";
            //str += "balance - " + balance.ToString() + "\n";
            return str;
        }
    }
    
    // ------------------------------------------------------------------//
    // Enums Definition:
    public enum FIELD_NAME { DataBase=1, Communication, InformationSecurity,
        ServerSideProgramming, MobileProgramming, UserInterfaceDesigh }
    public enum EDUCATION { Diploma, BA, MA, PHD, Student }
    public enum GENDER { Male , Female}
    public enum LIVING_AREA { North, South, Merkaz, Jerusalem }
}
