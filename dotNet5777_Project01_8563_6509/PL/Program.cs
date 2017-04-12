using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using BL;

namespace PL
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("HI");
                IBL bl = FactoryBl.GetInstance(); // create appearance of Bl_imp to provide access to the functions
                int externalSwitch, internalSwitch = 0;
                //----------------------------------------//
                //bl.addSpecialization(new Specialization((FIELD_NAME)1, "DBA", 50, 80));
                //bl.addSpecialization(new Specialization((FIELD_NAME)2, "Computer communication", 55, 90));
                //bl.addSpecialization(new Specialization((FIELD_NAME)3, "Haker", 30, 70));
                //bl.addSpecialization(new Specialization((FIELD_NAME)6, "Meatzev Grafi", 26, 50));

                //bl.addEmployee(new Employee(308358563, "Ami", "Shnitz", true, new DateTime(1992, 5, 7), "0545241434", "Bet-El", (GENDER)0, (EDUCATION)2, true,
                //    new BankAccount(1, "Poalim", 698, "Yafo 121", "Jerusalem", 725687, 10001), 10100000, 3.5f,"amichaysh@gmail.com"));
                //bl.addEmployee(new Employee(345629563, "Yehuda", "Avramov", false, new DateTime(1987, 4, 18), "0526217809", "Petach-Tikva", (GENDER)0, (EDUCATION)3, true,
                //    new BankAccount(2, "Leumi", 456, "Bialik 65", "Yafo", 165789, 200), 20200000, 2f, "yehudaaa@gmail.com"));
                //bl.addEmployee(new Employee(208452330, "Achia", "Grinshtein",true, new DateTime(1967, 5, 10), "0536985485", "Jerusalem", (GENDER)0, (EDUCATION)1,false,
                //    new BankAccount(2, "Discont", 129, "Agripas 13", "Bat-Yam", 1589630, 32000), 30300000,5.5f, "Achia@gmail.com"));
                //bl.addEmployee(new Employee(203079578, "Elia", "Mashash", false, new DateTime(1975, 11, 3), "0523148756", "Maalot", (GENDER)0, (EDUCATION)0, true,
                //    new BankAccount(2, "Tfchot", 433, "Shachal 12", "Kiriat Yam",956321, -12), 60400000, 3.0f,"elia@gmail.com"));

                //bl.addEmployer(new Employer(305216508, true, "Dankner", "Dana", (GENDER)1, "Dankner Mobile", "053-6172390", "Tel-Aviv", "Givaataim",
                //    (FIELD_NAME)2, new DateTime(1982, 3, 1), 20));
                //bl.addEmployer(new Employer(203945716, false, "Zvulun", "Nir", 0, "Audio Codes", "02-9841563", "Nesher", "Haifa",
                //    (FIELD_NAME)1, new DateTime(1990, 7, 17), 150));
                //bl.addEmployer(new Employer(320569841,false, "Rabinovitch", "Haim", 0, "Haim programs", "052-9734673", "Dafna", "Kiryat Shmona",
                //    (FIELD_NAME)3, new DateTime(1972, 6, 27), 150));
                //bl.addEmployer(new Employer(985632101, true, "Amsalem", "Dudo", 0, "Cisco", "03-9346758", "Meitar", "Beer-Sheva",
                //    (FIELD_NAME)6, new DateTime(1989, 1, 5), 300));

                //bl.addContract(new Contract(203945716, 308358563, true, true, 70, new DateTime(2010, 12, 1), new DateTime(2017, 12, 1), 180, (FIELD_NAME)1, (LIVING_AREA)3, 10100000));
                //bl.addContract(new Contract(305216508, 345629563, false, false, 75, new DateTime(2016, 8, 1), new DateTime(2020, 6, 4), 100, (FIELD_NAME)2, (LIVING_AREA)2, 20200000));
                //bl.addContract(new Contract(320569841, 208452330, true, true, 60, new DateTime(1993, 3, 14), new DateTime(2009, 2, 15), 200, (FIELD_NAME)3, (LIVING_AREA)0, 30300000));
                //bl.addContract(new Contract(985632101, 203079578, true, true, 57, new DateTime(2016, 4, 5), new DateTime(2025, 12, 6), 150, (FIELD_NAME)6, (LIVING_AREA)1, 60400000));
                ////----------------------------------------//
                //Specializations variables:

                FIELD_NAME f = 0;
                string spName = null;
                int spNum = 0;
                int min = 0;
                int max = 0;

                //----------------------------------------//
                //Employees variables:

                uint id = 0;
                string lName = null;
                string fName = null;
                bool married = false;
                DateTime birth = new DateTime(1945, 1, 1);
                string phone = null;
                string address = null;
                int g = 0;
                int e = 0;
                bool service = false;
                uint specNum = 0;
                float experience = 0;
                string email="";
                //all that is fields of BankAccount bank:

                int bankNumber = 0;
                string bankName = null;
                int branchNumber = 0;
                string branchCity = null;
                string bankAddress = null;
                int accountNumber = 0;
                float balance = 0;

                //----------------------------------------//
                //Employers variables:

                uint compNum = 0;
                bool ifPriv = false;
                string compName;
                string empPhone;
                string empAddress;
                string compAddress;
                DateTime establish = new DateTime(1945 / 1 / 1);
                int amountWorkers = 0;

                //----------------------------------------//
                //Contracts variables:

                int ContractNumber = 0;
                int _EmployerNumber = 0;
                int _EmployeeNumber = 0;
                bool _Interview = false;
                bool _ContractSigned = false;
                float _GrossHourWage = 0;
                DateTime _BeginEmployment = new DateTime(1945 / 1 / 1);
                DateTime _FinishEmployment = new DateTime(1945 / 1 / 1);
                int _ContractHours = 0;
                LIVING_AREA area = 0;
                int _SpecializationNumber = 0;
                
                //----------------------------------------//
                do
                {

                    Console.WriteLine(@"Welcome to the Men Power system!
For doing functions on bank accounts press 1
For doing functions on specializations press 2
For doing functions on employees press 3
For doing functions on employers press 4
For doing functions on contracts press 5
For doing special function press 6
For getting out press 0");
                    externalSwitch = Convert.ToInt32(Console.ReadLine());
                    switch (externalSwitch)
                    {

                        //----------------------------------------//
                        //Bank accounts

                        case 1:
                            do
                            {

                                Console.WriteLine(@"Welcome to the bank acount system!
For getting bank accounts list press 1
For getting out press 0");
                                internalSwitch = Convert.ToInt32(Console.ReadLine());
                                switch (internalSwitch)
                                {
                                    case 1:
                                        Console.WriteLine("Here the specializations list:\n");
                                        IEnumerable<BankAccount> list = bl.getBankAccountsList();
                                        foreach (BankAccount item in list)
                                        {
                                            Console.WriteLine("{0}", item.ToString());
                                        }
                                        break;

                                    case 0:
                                        Console.WriteLine("Goodbye\n");
                                        break;
                                }
                            } while (internalSwitch != 0);
                            break;

                        //----------------------------------------//
                        //Specializations

                        case 2:

                            do
                            {
                                Console.WriteLine(@"Welcome to the specializations system!
For adding specialization press 1
For delete specialization press 2
For update specialization press 3
For finding specialization and print its data by number press 4
For getting specializations list press 5
For getting out press 0");
                                internalSwitch = Convert.ToInt32(Console.ReadLine());
                                switch (internalSwitch)
                                {
                                    case 1:
                                        Console.WriteLine("Write the specialization data:\n field, specialization name, min hour wage,max hour wage\n");
                                        f = (FIELD_NAME)Convert.ToInt32(Console.ReadLine());
                                        spName = Console.ReadLine();
                                        min = Convert.ToInt32(Console.ReadLine());
                                        max = Convert.ToInt32(Console.ReadLine());
                                        Specialization tmpSpecialization = new Specialization((FIELD_NAME)f, spName, min, max);
                                        try
                                        {
                                            bl.addSpecialization(tmpSpecialization);
                                        }
                                        catch (Exception ex)
                                        {
                                            Console.WriteLine(ex.Message);
                                        }
                                        break;

                                    case 2:
                                        Console.WriteLine("Write the specialization number to delete:\n");
                                        spNum = Convert.ToInt32(Console.ReadLine());
                                        try
                                        {
                                            Specialization tmpSpecialization2 = bl.findSpecializationByNumber(spNum);
                                            try
                                            {
                                                bl.deleteSpecialization(tmpSpecialization2);
                                            }
                                            catch (Exception ex)
                                            {
                                                Console.WriteLine(ex.Message);
                                            }
                                        }

                                        catch (Exception ex)
                                        {
                                            Console.WriteLine(ex.Message);
                                        }

                                        break;

                                    case 3:
                                        Console.WriteLine("Write the specialization number that you want to update:\n");
                                        int t = int.Parse(Console.ReadLine());
                                        Specialization tmp = bl.findSpecializationByNumber(t);
                                        if (tmp == null)
                                        {
                                            Console.WriteLine("the specialization number is not exist!\n");
                                            break;
                                        }
                                        Console.WriteLine(@"Write the specialization data to update:\n
min hour wage,max hour wage\n");
                                        min = Convert.ToInt32(Console.ReadLine());
                                        max = Convert.ToInt32(Console.ReadLine());
                                        Specialization tmpSpecialization3 = new Specialization(tmp.Field,tmp.SpecializationName, min, max);
                                        try
                                        {
                                            bl.updateSpecialization(tmpSpecialization3);
                                        }
                                        catch (Exception ex)
                                        {
                                            Console.WriteLine(ex.Message);
                                        }
                                        break;

                                    case 4:
                                        Console.WriteLine("Write the specialization number to print:\n");
                                        spNum = Convert.ToInt32(Console.ReadLine());
                                        try
                                        {
                                            Specialization tmpSpecialization4 = bl.findSpecializationByNumber(spNum);
                                            Console.WriteLine("{0}", tmpSpecialization4.ToString());
                                        }
                                        catch (Exception ex)
                                        {
                                            Console.WriteLine(ex.Message);
                                        }
                                        break;

                                    case 5:
                                        Console.WriteLine("Here the specializations list:\n");
                                        try
                                        {
                                            IEnumerable<Specialization> list = bl.getSpecializationsList();
                                            foreach (Specialization item in list)
                                            {
                                                Console.WriteLine(item.ToString());
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            Console.WriteLine(ex.Message);
                                        }
                                        break;

                                    case 0:
                                        Console.WriteLine("Goodbye\n");
                                        break;
                                }
                            } while (internalSwitch != 0);
                            break;

                        //----------------------------------------//
                        //Employees

                        case 3:
                            do
                            {

                                Console.WriteLine(@"Welcome to the employees system!
For adding employee press 1
For delete employee press 2
For update employee press 3
For finding an employee and print his data by id press 4
For getting sum of employees press 5
For getting employees list press 6
For getting out press 0
");
                                internalSwitch = Convert.ToInt32(Console.ReadLine());
                                switch (internalSwitch)
                                {
                                    case 1:
                                        Console.WriteLine("Write the employee data:\nid, last name, first name, married, date of birth, phone number,\n address, gender, education, army service,specialization number, years of experince, email.\nFor the bank account:\n bank number, bank name, branch number, branch city, bank address, account number, balance");
                                        id = Convert.ToUInt32(Console.ReadLine());
                                        lName = Console.ReadLine();
                                        fName = Console.ReadLine();
                                        married = Convert.ToBoolean(Console.ReadLine());
                                        birth = Convert.ToDateTime(Console.ReadLine());
                                        phone = Console.ReadLine();
                                        address = Console.ReadLine();
                                        g = Convert.ToInt32(Console.ReadLine());
                                        e = Convert.ToInt32(Console.ReadLine());
                                        service = Convert.ToBoolean(Console.ReadLine());
                                        specNum = Convert.ToUInt32(Console.ReadLine());
                                        experience = float.Parse(Console.ReadLine());
                                        email = Console.ReadLine();
                                        //all that is fields of BankAccount bank:
                                        bankNumber = Convert.ToInt32(Console.ReadLine());
                                        bankName = Console.ReadLine();
                                        branchNumber = Convert.ToInt32(Console.ReadLine());
                                        branchCity = Console.ReadLine();
                                        bankAddress = Console.ReadLine();
                                        accountNumber = Convert.ToInt32(Console.ReadLine());
                                        balance = float.Parse(Console.ReadLine());

                                        BankAccount bank = new BankAccount(bankNumber, bankName, branchNumber,
                                            branchCity, bankAddress, accountNumber);

                                        Employee tmpEmployee = new Employee(id, lName, fName, married, (DateTime)birth, phone,
                                        address, (GENDER)g, (EDUCATION)e, service, bank, specNum, experience,email);
                                        try
                                        {
                                            bl.addEmployee(tmpEmployee);
                                        }
                                        catch (Exception ex)
                                        {
                                            Console.WriteLine(ex.Message);
                                        }
                                        break;

                                    case 2:
                                        Console.WriteLine("Write the Employee id to delete:\n");
                                        id = Convert.ToUInt32(Console.ReadLine());
                                        Employee tmpEmployee2 = new Employee();
                                        try
                                        {
                                            tmpEmployee2 = bl.findEmployeeByNumber((int)id);
                                            try
                                            {
                                                bl.deleteEmployee(tmpEmployee2);
                                            }
                                            catch (Exception ex)
                                            {
                                                Console.WriteLine(ex.Message);
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            Console.WriteLine(ex.Message);
                                        }

                                        break;

                                    case 3:
                                        Console.WriteLine("Write the Employee number that you want to update:\n");
                                        int t = int.Parse(Console.ReadLine());
                                        Employee tmp = bl.findEmployeeByNumber(t);
                                        if (tmp == null)
                                        {
                                            Console.WriteLine("the Employee number is not exist!\n");
                                            break;
                                        }
                                        
                                        Console.WriteLine("Write the Employee data to update:\n last name, married, phone number, address, education, army service,specialization number, years of experince.\n For the bank account:\n bank number, bank name, branch number, branch city, bank address, account number, balance,email");

                                        lName= Console.ReadLine();
                                        married = Convert.ToBoolean(Console.ReadLine());
                                        phone = Console.ReadLine();
                                        address = Console.ReadLine();
                                        e = Convert.ToInt32(Console.ReadLine());
                                        service = Convert.ToBoolean(Console.ReadLine());
                                        specNum = Convert.ToUInt32(Console.ReadLine());
                                        experience = float.Parse(Console.ReadLine());
                                        email = Console.ReadLine();
                                        //all that is fields of BankAccount bank:

                                        bankNumber = Convert.ToInt32(Console.ReadLine());
                                        bankName = Console.ReadLine();
                                        branchNumber = Convert.ToInt32(Console.ReadLine());
                                        branchCity = Console.ReadLine();
                                        bankAddress = Console.ReadLine();
                                        accountNumber = Convert.ToInt32(Console.ReadLine());
                                        balance = float.Parse(Console.ReadLine());

                                        BankAccount bank3 = new BankAccount(bankNumber, bankName, branchNumber,
                                            branchCity, bankAddress, accountNumber);

                                        tmp.BAccount = bank3;

                                        Employee tmpEmployee3 = new Employee(tmp.ID,lName,tmp.FirstName, married,tmp.BirthDate, phone, address,tmp.Gender, (EDUCATION)e, service, tmp.BAccount, specNum, experience,email);
                                        
                                        try
                                        {
                                            bl.updateEmployee(tmp);
                                        }
                                        catch (Exception ex)
                                        {
                                            Console.WriteLine(ex.Message);
                                        }
                                        break;

                                    case 4:
                                        Console.WriteLine("Write the Employee id to print:\n");
                                        id = Convert.ToUInt32(Console.ReadLine());
                                        
                                        try
                                        {
                                            Employee tmpEmployee4 = bl.findEmployeeByNumber((int)id);
                                            Console.WriteLine("{0}", tmpEmployee4.ToString());
                                        }
                                        catch (Exception ex)
                                        {
                                            Console.WriteLine(ex.Message);
                                        }

                                        break;

                                    case 5:
                                        int sumOfEmployees = bl.getSumOfEmployeesByCondition();
                                        Console.WriteLine("The sum of the employees is: {0} \n", sumOfEmployees);
                                        break;

                                    case 6:

                                        Console.WriteLine("Here the employees list:\n");
                                        IEnumerable<Employee> list = bl.getEmployeesList();
                                        foreach (Employee item in list)
                                        {
                                            Console.WriteLine("{0}", item.ToString());
                                        }
                                        break;


                                    case 0:
                                        Console.WriteLine("Goodbye\n");
                                        break;
                                }
                            } while (internalSwitch != 0);
                            break;

                        //----------------------------------------//
                        //Employers

                        case 4:

                            do
                            {
                                Console.WriteLine(@"Welcome to the employers system!
For adding employer press 1
For delete employer press 2
For update employer press 3
For finding an employer and print his data by his number press 4
For getting employer list press 5
For getting out press 0
");
                                internalSwitch = Convert.ToInt32(Console.ReadLine());
                                switch (internalSwitch)
                                {
                                    case 1:
                                        Console.WriteLine("Write the employer data:\n company number , if private, first name, last name, gender, company name, employer phone,\n employer address, company address, field, date of establish, amount of workers");

                                        compNum = Convert.ToUInt32(Console.ReadLine()); ;
                                        ifPriv = Convert.ToBoolean(Console.ReadLine());
                                        fName = Console.ReadLine();
                                        lName = Console.ReadLine();
                                        g = Convert.ToInt32(Console.ReadLine());
                                        compName = Console.ReadLine();
                                        empPhone = Console.ReadLine();
                                        empAddress = Console.ReadLine();
                                        compAddress = Console.ReadLine();
                                        f = (FIELD_NAME)Convert.ToInt32(Console.ReadLine());
                                        establish = Convert.ToDateTime(Console.ReadLine());
                                        amountWorkers = Convert.ToInt32(Console.ReadLine());

                                        Employer tmpEmployer = new Employer(compNum, ifPriv, fName, lName, (GENDER)g, compName,
                                        empPhone, empAddress, compAddress, f, establish, amountWorkers);
                                        bl.addEmployer(tmpEmployer);

                                        break;

                                    case 2:
                                        Console.WriteLine("Write the company number to delete:\n");
                                        compNum = Convert.ToUInt32(Console.ReadLine());
                                        try
                                        {
                                            Employer tmpEmployer2 = bl.findEmployerByNumber((int)compNum);
                                            try
                                            {
                                                bl.deleteEmployer(tmpEmployer2);
                                            }
                                            catch (Exception ex)
                                            {
                                                Console.WriteLine(ex.Message);
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            Console.WriteLine(ex.Message);
                                        }
                                        break;

                                    case 3:
                                        Console.WriteLine("Write the Employer number that you want to update:\n");
                                        int t = int.Parse(Console.ReadLine());
                                        Employer tmp = bl.findEmployerByNumber(t);
                                        if (tmp == null)
                                        {
                                            Console.WriteLine("the Employer number is not exist!\n");
                                            break;
                                        }
                                        
                                        Console.WriteLine(@"Write the employer data to update:\n lName, if private, company name, employer phone,\n
                                            employer address, company address, field, amount of workers");
                                        ifPriv = Convert.ToBoolean(Console.ReadLine());
                                        lName = Console.ReadLine();
                                        compName = Console.ReadLine();
                                        empPhone = Console.ReadLine();
                                        empAddress = Console.ReadLine();
                                        compAddress = Console.ReadLine();
                                        f = (FIELD_NAME)Convert.ToInt32(Console.ReadLine());
                                        amountWorkers = Convert.ToInt32(Console.ReadLine());
                                        Employer tmpEmployer3 = new Employer(tmp.CompanyNumber, ifPriv,tmp.FirstName, lName,tmp.Gender, compName, empPhone, empAddress, compAddress,(FIELD_NAME)f,tmp.EstablishmentDate, amountWorkers);
                                        try
                                        {
                                            bl.updateEmployer(tmp);
                                        }
                                        catch (Exception ex)
                                        {
                                            Console.WriteLine(ex.Message);
                                        }
                                        break;
                                        

                                    case 4:
                                        Console.WriteLine("Write the employer number to print:\n");
                                        compNum = Convert.ToUInt32(Console.ReadLine());
                                        try
                                        {
                                            Employer tmpEmployer4 = bl.findEmployerByNumber((int)compNum);

                                            Console.WriteLine("{0}", tmpEmployer4.ToString());
                                        }
                                        catch (Exception ex)
                                        {
                                            Console.WriteLine(ex.Message);
                                        }

                                        break;

                                    case 5:

                                        Console.WriteLine("Here the employers list:\n");
                                        IEnumerable<Employer> list = bl.getEmployersList();
                                        foreach (Employer item in list)
                                        {
                                            Console.WriteLine("{0}", item.ToString());
                                        }

                                        break;

                                    case 0:
                                        Console.WriteLine("Goodbye\n");
                                        break;
                                }
                            } while (internalSwitch != 0);
                            break;

                        //----------------------------------------//
                        //Contracts

                        case 5:

                            do
                            {
                                Console.WriteLine(@"Welcome to the contracts system!
For adding contract press 1
For delete contract press 2
For update contract press 3
For finding contract by number and print it press 4
For getting sum of contracts press 5
For getting contract list press 6
For getting average hour pay wage press 7
For getting out press 0
");
                                internalSwitch = Convert.ToInt32(Console.ReadLine());
                                switch (internalSwitch)
                                {
                                    case 1:
                                        Console.WriteLine("Write the contract data:\n employer number, employee number, interview, contract signed, gross hour wage\n, begin employment, finish employment, contract hours, field, area, specialization number\n");

                                        _EmployerNumber = Convert.ToInt32(Console.ReadLine());
                                        _EmployeeNumber = Convert.ToInt32(Console.ReadLine());
                                        _Interview = Convert.ToBoolean(Console.ReadLine());
                                        _ContractSigned = Convert.ToBoolean(Console.ReadLine());
                                        _GrossHourWage = float.Parse(Console.ReadLine());
                                        _BeginEmployment = Convert.ToDateTime(Console.ReadLine());
                                        _FinishEmployment = Convert.ToDateTime(Console.ReadLine());
                                        _ContractHours = Convert.ToInt32(Console.ReadLine());
                                        f = (FIELD_NAME)Convert.ToInt32(Console.ReadLine());
                                        area = (LIVING_AREA)Convert.ToInt32(Console.ReadLine());
                                        _SpecializationNumber = Convert.ToInt32(Console.ReadLine());

                                        Contract tmpContract = new Contract(_EmployerNumber, _EmployeeNumber, _Interview, _ContractSigned,
                                        _GrossHourWage, _BeginEmployment, _FinishEmployment, _ContractHours, (FIELD_NAME)f,
                                        (LIVING_AREA)area, _SpecializationNumber);
                                        bl.addContract(tmpContract);

                                        break;

                                    case 2:
                                        Console.WriteLine("Write the contract number to delete:\n");
                                        ContractNumber = Convert.ToInt32(Console.ReadLine());
                                        try
                                        {
                                            Contract tmpContract2 = bl.findContractByNumber(ContractNumber);
                                            try
                                            {
                                                bl.deleteContract(tmpContract2);
                                            }
                                            catch (Exception ex)
                                            {
                                                Console.WriteLine(ex.Message);
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            Console.WriteLine(ex.Message);
                                        }

                                        break;

                                    case 3:
                                        Console.WriteLine("Write the Contract number that you want to update:\n");
                                        int t = int.Parse(Console.ReadLine());
                                        Contract tmp = bl.findContractByNumber(t);
                                        if (tmp == null)
                                        {
                                            Console.WriteLine("the Contract number is not exist!\n");
                                            break;
                                        }
                                        Console.WriteLine("Write the contract data to update:\n interview, contract signed, gross hour wage, contract hours, field, area, specialization number\n");

                                        _Interview = Convert.ToBoolean(Console.ReadLine());
                                        _ContractSigned = Convert.ToBoolean(Console.ReadLine());
                                        _GrossHourWage = float.Parse(Console.ReadLine());
                                        _BeginEmployment = Convert.ToDateTime(Console.ReadLine());
                                        _FinishEmployment = Convert.ToDateTime(Console.ReadLine());
                                        _ContractHours = Convert.ToInt32(Console.ReadLine());
                                        f = (FIELD_NAME)Convert.ToInt32(Console.ReadLine());
                                        area = (LIVING_AREA) Convert.ToInt32(Console.ReadLine());
                                        Contract tmpContract3 = new Contract(tmp.EmployerNumber,tmp.EmployeeNumber, _Interview, _ContractSigned, _GrossHourWage, _BeginEmployment, _FinishEmployment, _ContractHours,f,area,tmp.SpecializationNumber);

                                        try
                                        {
                                            bl.updateContract(tmp);
                                        }
                                        catch (Exception ex)
                                        {
                                            Console.WriteLine(ex.Message);
                                        }

                                        break;

                                    case 4:
                                        Console.WriteLine("Write the contract number to print:\n");
                                        ContractNumber = Convert.ToInt32(Console.ReadLine());
                                        try
                                        {
                                            Contract tmpContract4 = bl.findContractByNumber(ContractNumber);
                                            Console.WriteLine("{0}", tmpContract4.ToString());
                                        }
                                        catch (Exception ex)
                                        {
                                            Console.WriteLine(ex.Message);
                                        }

                                        break;

                                    case 5:
                                        int sumOfContracts = bl.getSumOfContractsByCondition();
                                        Console.WriteLine("The sum of the contracts is: {0} \n", sumOfContracts);

                                        break;

                                    case 6:

                                        Console.WriteLine("Here the contracts list:\n");
                                        IEnumerable<Contract> list = bl.getContractsList();
                                        foreach (Contract item in list)
                                        {
                                            Console.WriteLine("{0}", item.ToString());
                                        }

                                        break;

                                    case 0:
                                        Console.WriteLine("Goodbye\n");
                                        break;
                                }
                            } while (internalSwitch != 0);
                            break;

                        //----------------------------------------//
                        //Special functions

                        case 6:
                            do
                            {
                                Console.WriteLine(@"Welcome to the special functions system!
For getting contracts by specialization press 1
For getting contracts by living area press 2
For getting profits by monthes press 3
For getting the worker who earn the biggest salary press 4
For getting the workers by age groups press 5
For getting average hour pay wage press 6
For getting out press 0
");
                                internalSwitch = Convert.ToInt32(Console.ReadLine());
                                switch (internalSwitch)
                                {
                                    case 1:
                                        IEnumerable<IGrouping<int, Contract>> result = bl.groupingContractsBySpecialization();
                                        foreach (var item in result)
                                        {
                                            Console.WriteLine(string.Format("the contracts of this specialization number - {0} ", item.Key));
                                            foreach (var c in item)
                                            {
                                                Console.WriteLine(c.ToString());
                                            }
                                        }
                                        break;
                                    case 2:
                                        IEnumerable<IGrouping<LIVING_AREA, Contract>> result1 = bl.groupingContractsByLivingArea();
                                        foreach (var item in result1)
                                        {
                                            Console.WriteLine(string.Format("the contracts of this living area - {0} ", item.Key));
                                            foreach (var c in item)
                                            {
                                                Console.WriteLine(c.ToString());
                                            }
                                        }
                                        break;
                                    case 3:
                                        IEnumerable<IGrouping<string, float>> result2 = bl.groupingProfitsByMonth();
                                        foreach (var item in result2)
                                        {
                                            Console.WriteLine(string.Format("the profit of {0} ", item.Key));
                                            foreach (var c in item)
                                            {
                                                Console.WriteLine(c);
                                            }
                                        }
                                        break;

                                    case 4:
                                        Employee emp = bl.biggestSalary();
                                        Console.WriteLine(emp.ToString());
                                        break;

                                    case 5:
                                        IEnumerable<IGrouping<string, Employee>> result3 = bl.groupingEmployeesByAge();
                                        foreach (var item in result3)
                                        {
                                            Console.WriteLine(string.Format("this is the employees between {0} ", item.Key));
                                            foreach (var c in item)
                                            {
                                                Console.WriteLine(c.ToString());
                                            }
                                        }
                                        break;

                                    case 6:

                                        float averageNetHourWage = bl.getAverageHourPayWageByCondition();
                                        Console.WriteLine("The average hour wage of the contracts is: {0} \n", averageNetHourWage);

                                        break;
                                    case 0:
                                        Console.WriteLine("Goodbye\n");
                                        break;
                                }

                            } while (internalSwitch != 0);
                            break;
                        case 0:
                            break;

                    }
                    Console.WriteLine("Goodbye\n");
                } while (externalSwitch != 0);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }

}
