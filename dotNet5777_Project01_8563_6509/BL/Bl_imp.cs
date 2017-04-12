using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using BE;
using DAL;

namespace BL
{
    internal class Bl_imp : IBL
    {
        public Bl_imp()
        {
            dal = FactoryDal.GetInstance();
           //init();
        }

        IDAL dal; // create appearance of Dal_imp to provide access to the functions

        public event EventHandler<BL_EventArgs> addedElement; // event that applyed when added some elements

        public void addContract(Contract t)
        {
            // before adding to Contract list. calculate the net hour wage
            t.NetHourWage = netHourWageCalculation(t.GrossHourWage, t.EmployeeNumber, t.EmployerNumber);
            // validate information
            if (findSpecializationByNumber(t.SpecializationNumber).MinHourWage > t.NetHourWage)
                throw new Exception("Net Hour Wage is less than the min hour wage for this Specialization");
            if ((DateTime.Now - findEmployerByNumber(t.EmployerNumber).EstablishmentDate).TotalDays < 365 && t.ContractSigned)
                throw new Exception("The contract is not good, because the company exist less than a year");

            dal.addContract(t);
            if (addedElement != null)
            {
                addedElement(this, new BL_EventArgs("Contract", getSumOfContractsByCondition()));
            }
        }
        public void addEmployee(Employee t)
        {
            
            if (t.ID < 100000000 || t.ID >= 999999999)
                throw new Exception("the employee's ID is invalid");
            if (t.Age < 18)
                throw new Exception("The employee is minor ");
            if ((!(t.Email.EndsWith(".com") || t.Email.EndsWith(".co.il") || t.Email.EndsWith(".ac.il"))) && (!(t.Email.Contains("@"))))
                throw new Exception("The email address is invalid");
            if (!(t.FirstName.All(Char.IsLetter)) || t.FirstName.Length<2)
                throw new Exception("You have to insert a real first name");
            if (!(t.LastName.All(Char.IsLetter)) || t.FirstName.Length < 2)
                throw new Exception("You have to insert a last name");
            if ( t.Address.Length < 2)
                throw new Exception("You have to insert a real address");
            if( t.BAccount.accountNumber==-1  ||t.BAccount.bankAddress == null || t.BAccount.bankName == null || t.BAccount.branchCity == null || t.BAccount.branchNumber ==-1)
                throw new Exception("You didn't insert bank account");
            dal.addEmployee(t);

            if (addedElement != null)
            {
                addedElement(this, new BL_EventArgs("Employee", getSumOfEmployeesByCondition()));
            }

        }
        public void addEmployer(Employer t)
        {
            if (!(t.LastName.All(Char.IsLetter))|| (t.LastName.Length) < 2)
                throw new Exception("You have to insert real last name");
            if (!(t.FirstName.All(Char.IsLetter)) || (t.FirstName.Length) < 2)
                throw new Exception("You have to insert real first name");
            if ((t.CompanyAddress.Length) < 2)
                throw new Exception("You have to insert a company address");
            if ((t.CompanyName.Length) < 2)
                throw new Exception("You have to insert a real company name");
            if ((t.EmployerAddress.Length) < 2)
                throw new Exception("You have to insert a real employer address");
            if (t.EstablishmentDate> DateTime.Now)
                throw new Exception("The company cannot establish at the future!");
            dal.addEmployer(t);
            if (addedElement != null)
            {
                addedElement(this, new BL_EventArgs("Employer", getSumOfEmployersByCondition()));
            }

        }
        public void addSpecialization(Specialization t)
        {
            if (!t.SpecializationName.All(Char.IsLetter) && t.SpecializationName.Length < 2)
                throw new Exception("This not real name");
            if (t.MinHourWage < 25)
                throw new Exception("The Wage is under the minimum wage of the law");
            if (t.MinHourWage > t.MaxHourWage)
                throw new Exception("The min hour wage is under max hour wage");
            dal.addSpecialization(t);
        }

        public void deleteContract(Contract t)
        {
            if (findContractByNumber(t.ContractNumber) == null)
                throw new Exception("The contract is not exist at contracts list");
            dal.deleteContract(t);
            if (addedElement != null)
            {
                addedElement(this, new BL_EventArgs("Contract", getSumOfContractsByCondition()));
            }

        }
        public void deleteEmployee(Employee t)
        {
            if (findEmployeeByNumber((int)t.ID) == null)
                throw new Exception("The employee is not exist at employees list");
            dal.deleteEmployee(t);
            if (addedElement != null)
            {
                addedElement(this, new BL_EventArgs("Employee", getSumOfEmployeesByCondition()));
            }

        }
        public void deleteEmployer(Employer t)
        {
            if (findEmployerByNumber((int)t.CompanyNumber) == null)
                throw new Exception("The employer is not exist at employers list");
            dal.deleteEmployer(t);
            if (addedElement != null)
            {
                addedElement(this, new BL_EventArgs("Employer", getSumOfEmployersByCondition()));
                // if we delete employer, we delete all of his contracts
                addedElement(this, new BL_EventArgs("Contract", getSumOfContractsByCondition()));
            }
        }
        public void deleteSpecialization(Specialization t)
        {
            if (findSpecializationByNumber((int)t.SpecializationNumber) == null)
                throw new Exception("The specialization is not exist at specializations list");

            dal.deleteSpecialization(t);
        }

        public IEnumerable<BankAccount> getBankAccountsList()
        {
            return dal.getBankAccountsList();
        }

        public IEnumerable<Contract> getContractsList(Func<Contract, bool> func = null)
        {
            return dal.getContractsList(func);
        }
        public IEnumerable<Employee> getEmployeesList(Func<Employee, bool> func = null)
        {
            return dal.getEmployeesList(func);
        }
        public IEnumerable<Employer> getEmployersList(Func<Employer, bool> func = null)
        {
            return dal.getEmployersList(func);
        }
        public IEnumerable<Specialization> getSpecializationsList(Func<Specialization, bool> func = null)
        {
            return dal.getSpecializationsList(func);
        }

        public void updateContract(Contract t)
        {
            if (findContractByNumber(t.ContractNumber) == null)
                throw new Exception("The contract is not exist at contracts list");
            if (findSpecializationByNumber(t.SpecializationNumber).MinHourWage > t.NetHourWage)
                throw new Exception("Net Hour Wage is less than the min hour wage for this Specialization");
            if ((DateTime.Now - findEmployerByNumber(t.EmployerNumber).EstablishmentDate).TotalDays < 365 && t.ContractSigned)
                throw new Exception("The contract is not good, because the company exist less than a year");
            dal.updateContract(t);
        }
        public void updateEmployee(Employee t)
        {
            if (findEmployeeByNumber((int)t.ID) == null)
                throw new Exception("The employee is not exist at employees list");
            if (t.Age < 18)
                throw new Exception("The employee minor");
            dal.updateEmployee(t);
        }
        public void updateEmployer(Employer t)
        {
            if (findEmployerByNumber((int)t.CompanyNumber) == null)
                throw new Exception("The employer is not exist at employers list");
            dal.updateEmployer(t);
        }
        public void updateSpecialization(Specialization t)
        {
            if (findSpecializationByNumber((int)t.SpecializationNumber) == null)
                throw new Exception("The specialization is not exist at specializations list");
            if (t.MinHourWage < 25)
                throw new Exception("The Wage is under the minimum wage of the law");
            dal.updateSpecialization(t);
        }

        // additional functions----------------------------------------------//
        // Find functions
        public Contract findContractByNumber(int num)
        {
            return dal.findContractByNumber(num);
        }
        public Employee findEmployeeByNumber(int num)
        {
            return dal.findEmployeeByNumber(num);
        }
        public Employer findEmployerByNumber(int num)
        {
            return dal.findEmployerByNumber(num);
        }
        public Specialization findSpecializationByNumber(int num)
        {
            return dal.findSpecializationByNumber(num);
        }


        /// <summary>
        /// function that calculate the net hour wage after we remove the commission from the gross hour wage
        /// </summary>
        /// <param name="grossWage">gross hour wage</param>
        /// <param name="employeeId">employee Id</param>
        /// <param name="employerNum">employer Number</param>
        /// <returns>net Hour Wage</returns>
        float netHourWageCalculation(float grossWage, int employeeId, int employerNum)
        {
            int amountContractsEmployee = getSumOfContractsByCondition(t => t.EmployeeNumber == employeeId);
            int amountContractsEmployer = getSumOfContractsByCondition(t => t.EmployerNumber == employerNum);
            float commission = 10 - (amountContractsEmployer / (float)4) + amountContractsEmployee;

            if (commission >= 1)
                return ((grossWage / 100) * (100 - commission));
            return (grossWage / 100) * (99);
        }

        /// <summary>
        /// get Sum Of Contracts By given Condition
        /// </summary>
        /// <param name="func">the result of this boolian function will decide if the object will get into the IEnumerable </param>
        /// <returns></returns>
        public int getSumOfContractsByCondition(Func<Contract, bool> func = null)
        {
            IEnumerable<Contract> list = getContractsList(func);
            int i = 0;
            foreach (var item in list)
            {
                i++;
            }
            return i;
        }
        public int getSumOfEmployeesByCondition(Func<Employee, bool> func = null)
        {
            IEnumerable<Employee> list = getEmployeesList(func);
            int i = 0;
            foreach (var item in list)
            {
                i++;
            }
            return i;
        }
        public int getSumOfEmployersByCondition(Func<Employer, bool> func = null)
        {
            IEnumerable<Employer> list = getEmployersList(func);
            int i = 0;
            foreach (var item in list)
            {
                i++;
            }
            return i;
        }

        public float getAverageHourPayWageByCondition(Func<Contract, bool> func = null)
        {
            IEnumerable<Contract> list = getContractsList(func);
            return list.Average(t => t.NetHourWage);
        }

        // Grouping functions
        
        public IEnumerable<IGrouping<string, IGrouping<string, BankAccount>>> groupingByCitiesAndBranches()
        {
            var allBanksGroup = from bank in getBankAccountsList().Distinct()
                                orderby bank.bankName, bank.branchCity, bank.bankNumber
                                group bank by bank.bankName into g // first query
                                from newGroup in (from city in g
                                                  group city by city.branchCity) // second query
                                group newGroup by g.Key; // merging at the two queries
            return allBanksGroup;
        }

        public IEnumerable<IGrouping<int, Contract>> groupingContractsBySpecialization(bool sort = false)
        {
            List<Contract> list = (List<Contract>)getContractsList();
            if (sort)
            {
                var result = from item in list
                             orderby item.SpecializationNumber
                             group item by (item.SpecializationNumber) / 1000000;
                return result;
            }
            else
            {
                var result = from item in list
                             group item by (item.SpecializationNumber) / 1000000;
                return result;
            }
        }
        public IEnumerable<IGrouping<LIVING_AREA, Contract>> groupingContractsByLivingArea(bool sort = false)
        {
            List<Contract> list = (List<Contract>)getContractsList();
            if (sort)
            {
                var result = from item in list
                             orderby item.LivingArea
                             group item by (item.LivingArea);
                return result;
            }
            else
            {
                var result = from item in list
                             group item by (item.LivingArea);
                return result;
            }
        }
        public IEnumerable<IGrouping<string, float>> groupingProfitsByMonth(bool sort = false)
        {
            IEnumerable<Contract> list = getContractsList();
            int j = 0;
            string[] array = new string[12];
            for (int i = 12; i > 0; i--, j++)
            {
                array[j] = monthConvert(DateTime.Now.Month - i) + "\n" + DateTime.UtcNow.AddMonths(-i).Year.ToString();
            }

            if (sort)
            {
                var result = from month in array
                             let date = DateTime.UtcNow.AddMonths(-j--)
                             let profit = profitOfMonth(date)
                             orderby profit
                             group profit by month;
                return result;
            }
            else
            {
                var result = from month in array
                             let date = DateTime.UtcNow.AddMonths(-j--)
                             let profit = profitOfMonth(date)
                             group profit by month;
                return result;
            }
        }
        public IEnumerable<IGrouping<string, Employee>> groupingEmployeesByAge(bool sort = false)
        {
            List<Employee> list = (List<Employee>)getEmployeesList();

            if (sort)
            {
                var result = from item in list
                             orderby item.LastName
                             group item by divideAges(item);
                return result;
            }
            else
            {
                var result = from item in list
                             group item by divideAges(item);
                return result;
            }
        }


        /// <summary>
        /// this function give the employee number by his group age
        /// </summary>
        /// <param name="t">Employee</param>
        /// <returns>the number that represent the group age</returns>
        string divideAges(Employee t)
        {
            if (t.Age <= 25)
                return "18 - 25";
            if (t.Age <= 35)
                return "26 - 35";
            if (t.Age <= 45)
                return "36 - 45";
            if (t.Age <= 55)
                return "46 - 55";
            return "55 - 67";
        }
        string monthConvert(int m)
        {
            if (m > 0)
            {
                if (m == 1) return "January";
                if (m == 2) return "February";
                if (m == 3) return "March";
                if (m == 4) return "April";
                if (m == 5) return "May";
                if (m == 6) return "June";
                if (m == 7) return "July";
                if (m == 8) return "August";
                if (m == 9) return "September";
                if (m == 10) return "October";
                if (m == 11) return "November";
                if (m == 12) return "December";
            }
            else
            {
                return monthConvert(12 + m);
            }
            return "";
        }
        public Employee biggestSalary()
        {
            IEnumerable<Contract> list = getContractsList();
            Contract tmp = list.Max();
            return findEmployeeByNumber(tmp.EmployeeNumber);
        }
        float profitOfMonth(DateTime t)
        {
            IEnumerable<Contract> list = dal.getValidContracts(t);
            return list.Sum(item => item.GrossHourWage - item.NetHourWage);
        }

        public bool IfEmployeeStillWorking(Employee t)
        {
            List<Contract> list = getContractsList(item => item.EmployeeNumber == t.ID).ToList();
            foreach (Contract c in list)
            {
                if (c.BeginEmployment >= DateTime.Now && c.FinishEmployment < DateTime.Now)
                    return true;
            }
            return false;
        }

        void init()
        {
            addSpecialization(new Specialization((FIELD_NAME)1, "DBA", 50, 80));
            addSpecialization(new Specialization((FIELD_NAME)2, "Computer communication", 55, 90));
            addSpecialization(new Specialization((FIELD_NAME)3, "Haker", 30, 70));
            addSpecialization(new Specialization((FIELD_NAME)6, "Meatzev Grafi", 26, 50));
            addSpecialization(new Specialization((FIELD_NAME)6, "QA", 27, 60));
            addSpecialization(new Specialization((FIELD_NAME)3, "Cript Decoder", 40, 90));

            addEmployee(new Employee(308358563, "Kaplan", "Shai", true, new DateTime(1992, 5, 7), "0545241434", "Bet-El", (GENDER)0, (EDUCATION)2, true,
                new BankAccount(1, "Poalim", 698, "Yafo 121", "Jerusalem", 725687), 10100000, 3.5f, "Shai@gmail.com"));
            addEmployee(new Employee(345629563, "Davidov", "Michael", false, new DateTime(1987, 4, 18), "0526217809", "Petach-Tikva", (GENDER)0, (EDUCATION)3, true,
                new BankAccount(2, "Leumi", 456, "Bialik 65", "Yafo", 165789), 20200000, 2f, "Michael@gmail.com"));
            addEmployee(new Employee(203079578, "Mashash", "Elia", false, new DateTime(1975, 11, 3), "0523148756", "Maalot", (GENDER)0, (EDUCATION)0, true,
                new BankAccount(4, "Mizrachi", 433, "Shachal 12", "Kiriat Yam", 956321), 60400000, 25.0f, "elia@gmail.com"));
            addEmployee(new Employee(208452330, "Grinshtein", "Achia", true, new DateTime(1967, 5, 10), "0536985485", "Jerusalem", (GENDER)0, (EDUCATION)1, false,
                new BankAccount(3, "Discont", 129, "Agripas 13", "Bat-Yam", 1589630), 30300000, 5.5f, "Achia@gmail.com"));
            addEmployee(new Employee(203079580, "Aizenberg", "Benni", true, new DateTime(1984, 5, 24), "0586917523", "Bnei-Brack", (GENDER)0, (EDUCATION)0, true,
                new BankAccount(2, "Leumi", 470, "Harav-Kock 79", "Bnei-Brack", 640287), 30600000, 7.0f, "Benni@gmail.com"));
            addEmployee(new Employee(304659802, "Abramof", "Dani", true, new DateTime(1964, 2, 13), "0543648279", "Kiriat-Arba", (GENDER)0, (EDUCATION)1, true,
                new BankAccount(4, "Mizrachi", 430, "Ben-Zakay 50", "Kiriat-Arba", 957594), 20200000, 23.0f, "dani@gmail.com"));
            addEmployee(new Employee(204598765, "Meshulam", "Yonit", true, new DateTime(1990, 12, 30), "0546852976", "Givaatim", (GENDER)1, (EDUCATION)3, true,
                new BankAccount(3, "Discont", 115, "Ben-Gurion 17", "Givaatim", 345678), 60400000, 4.0f, "yonit@gmail.com"));
            addEmployee(new Employee(203648259, "Granot", "Dalya", false, new DateTime(1950, 4, 7), "0572684595", "Rishon-Lezion", (GENDER)1, (EDUCATION)2, false,
                new BankAccount(1, "Poalim", 670, "Vaitzman 3", "Rishon-Lezion", 649527), 60500000, 45.0f, "dalya@gmail.com"));
            addEmployee(new Employee(204675980, "Nagar", "Moshe", false, new DateTime(1974, 3, 14), "052468579", "Bat-Yam", (GENDER)0, (EDUCATION)1, true,
                new BankAccount(1, "Poalim", 690, "Navon 46", "Bat-Yam", 457896), 60500000, 21.0f, "moshe@gmail.com"));

            addEmployer(new Employer(305216508, true, "Dana", "Dankner", (GENDER)1, "Dankner Mobile", "053-6172390", "Tel-Aviv", "Givaataim",
                (FIELD_NAME)2, new DateTime(1982, 3, 1), 20));
            addEmployer(new Employer(203945716, false, "Nir", "Zvulun", 0, "Audio Codes", "02-9841563", "Nesher", "Haifa",
                (FIELD_NAME)1, new DateTime(1990, 7, 17), 150));
            addEmployer(new Employer(320569841, false, "Haim", "Rabinovitch", 0, "Haim programs", "052-9734673", "Dafna", "Kiryat Shmona",
                (FIELD_NAME)3, new DateTime(1972, 6, 27), 70));
            addEmployer(new Employer(985632101, true, "Dudo", "Amsalem", 0, "Cisco", "03-9346758", "Meitar", "Beer-Sheva",
                (FIELD_NAME)6, new DateTime(1989, 1, 5), 300));
            addEmployer(new Employer(304659803, false, "Liav", "Benayon", 0, "Intel", "03-93675946", "Kfar-Shmariao", "Tel-aviv",
                (FIELD_NAME)6, new DateTime(1929, 4, 17), 700));

            addContract(new Contract(203945716, 308358563, true, true, 70, new DateTime(2010, 12, 1), new DateTime(2016, 7, 1), 180, (FIELD_NAME)1, (LIVING_AREA)3, 10100000));
            addContract(new Contract(305216508, 345629563, false, false, 75, new DateTime(2016, 8, 1), new DateTime(2020, 6, 4), 100, (FIELD_NAME)2, (LIVING_AREA)2, 20200000));
            addContract(new Contract(320569841, 208452330, true, true, 60, new DateTime(1993, 3, 14), new DateTime(2016, 2, 15), 200, (FIELD_NAME)3, (LIVING_AREA)0, 30300000));
            addContract(new Contract(985632101, 203079578, true, true, 57, new DateTime(2016, 4, 5), new DateTime(2016, 2, 6), 150, (FIELD_NAME)6, (LIVING_AREA)1, 60400000));
            addContract(new Contract(305216508, 304659802, false, false, 80, new DateTime(2015, 4, 12), new DateTime(2016, 4, 23), 80, (FIELD_NAME)2, (LIVING_AREA)2, 20200000));
            addContract(new Contract(320569841, 203079580, true, false, 65, new DateTime(2016, 12, 13), new DateTime(2019, 5, 7), 180, (FIELD_NAME)3, (LIVING_AREA)0, 30600000));
            addContract(new Contract(304659803, 204598765, true, true, 40, new DateTime(2016, 9, 7), new DateTime(2016, 11, 15), 180, (FIELD_NAME)6, (LIVING_AREA)2, 60400000));
            addContract(new Contract(304659803, 203648259, false, false, 38, new DateTime(2012, 3, 14), new DateTime(2017, 2, 1), 120, (FIELD_NAME)6, (LIVING_AREA)2, 60500000));
            addContract(new Contract(304659803, 204675980, true, true, 35, new DateTime(2000, 1, 15), new DateTime(2016, 5, 16), 190, (FIELD_NAME)6, (LIVING_AREA)2, 60500000));

        }

        public void SaveListsToFiles()
        {
            dal.SaveListsToFiles();
        }

    }

    public class BL_EventArgs : EventArgs
    {
        public string elementName { get; set; }
        public int sumElements { get; set; }
        public BL_EventArgs(string t, int n)
        {
            elementName = t;
            sumElements = n;
        }
    }

}
