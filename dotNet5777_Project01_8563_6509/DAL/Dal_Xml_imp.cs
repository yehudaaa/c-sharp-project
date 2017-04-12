using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using BE;
using DS;
using System.Xml.Linq;
using System.Threading;
using System.IO;
using System.Xml.Serialization;

namespace DAL
{
    public class XmlSpecialization
    {
        public XElement SpecializationRoot;
        const string SpecializationPath = @"Specializations.xml";
        
        public XmlSpecialization()
        {
            if (!File.Exists(SpecializationPath))
                CreateFiles();
            else
                LoadData();
        }

        private void CreateFiles()
        {
            SpecializationRoot = new XElement("Specializations");
            SpecializationRoot.Save(SpecializationPath);
        }

        private void LoadData()
        {
            try
            {
                SpecializationRoot = XElement.Load(SpecializationPath);
            }
            catch
            {
                throw new Exception("File upload problem");
            }
        }
    }

    public class Config
    {
        public XElement ConfigRoot;
        const string ConfigPath = @"Config.xml";

        public Config()
        {
            if (!File.Exists(ConfigPath))
                CreateFiles();
            else
                LoadData();
        }

        private void CreateFiles()
        {
            ConfigRoot = new XElement(ConfigPath);
            ConfigRoot.Save(ConfigPath);
        }

        private void LoadData()
        {
            try
            {
                ConfigRoot = XElement.Load(ConfigPath);
            }
            catch
            {
                throw new Exception("File upload problem");
            }
        }
    }
    class Dal_Xml_imp : IDAL
    {
        XmlSpecialization xmlSpecialization;
        Config config;
        
        const string xmlLocalPath = @"BankAccounts.xml";
        bool downloadBankList = false;

        const string SpecializationPath = @"Specializations.xml";
        const string EmployeePath = @"Employees.xml";
        const string EmployerPath = @"Employers.xml";
        const string ContractsPath = @"Contracts.xml";
        public Dal_Xml_imp()
        {
            Thread t = new Thread(DownloadXmlBankAccount);
            t.Start();

            xmlSpecialization = new XmlSpecialization();
            DS.DataSource.EmployeeList = loadListEmployeesFromXml(EmployeePath);
            DS.DataSource.EmployerList = loadListEmployersFromXml(EmployerPath);
            DS.DataSource.ContractList = loadListContractsFromXml(ContractsPath);

            config = new Config();
            // config for getting the running number of specilailzation and contract from the file
            try
            {// if success that mean that xml file exist with i and j variables
                var x = (from p in config.ConfigRoot.Elements()
                         select new
                         {
                             i = int.Parse(p.Element("i").Value),
                             j = int.Parse(p.Element("j").Value)
                         }).ToList();
                // update classes by the xml data
                BE.Specialization.i = x[0].i;
                BE.Contract.j = x[0].j;
            }
            catch (Exception)
            { //xml data of i and j is empty, so we need to initalize it
                XElement i = new XElement("i", 0);
                XElement j = new XElement("j", 0);

                config.ConfigRoot.Add(new XElement("Config", i, j));
                config.ConfigRoot.Save(@"Config.xml");
            }

            
        }

        public void addContract(Contract t)
        {
            Employee tmp1 = findEmployeeByNumber(t.EmployeeNumber);
            if (tmp1 == null)
                throw new Exception("The employee is not exist at employees list");
            Employer tmp2 = findEmployerByNumber(t.EmployerNumber);
            if (tmp2 == null)
                throw new Exception("The employer is not exist at employers list");
            DataSource.ContractList.Add(t);
        }
        public void addEmployee(Employee t)
        {
            if (findEmployeeByNumber((int)t.ID) != null)
                throw new Exception("The employee is already exist");
            DataSource.EmployeeList.Add(t);
        }
        public void addEmployer(Employer t)
        {
            if (findEmployerByNumber((int)t.CompanyNumber) != null)
                throw new Exception("The employer is already exist ");
            DataSource.EmployerList.Add(t);
        }
        public void addSpecialization(Specialization t)
        {
            XElement specializationElement = (from p in xmlSpecialization.SpecializationRoot.Elements()
                                              where Convert.ToInt32(p.Element("Field").Value) == (int)(t.Field) && p.Element("SpecializationName").Value == t.SpecializationName
                                              select p).FirstOrDefault();
            if (specializationElement != null)
                throw new Exception("The Specialization is already exist");

            XElement Field = new XElement("Field", (int)t.Field);
            XElement SpecializationName = new XElement("SpecializationName", t.SpecializationName);
            XElement SpecializationNumber = new XElement("SpecializationNumber", t.SpecializationNumber);
            XElement MinHourWage = new XElement("MinHourWage", t.MinHourWage);
            XElement MaxHourWage = new XElement("MaxHourWage", t.MaxHourWage);

            xmlSpecialization.SpecializationRoot.Add(new XElement("Specialization", SpecializationNumber, Field, SpecializationName, MinHourWage, MaxHourWage));
            xmlSpecialization.SpecializationRoot.Save(SpecializationPath);
        }

        public void deleteContract(Contract t)
        {
            t = findContractByNumber(t.ContractNumber);
            DataSource.ContractList.Remove(t);
        }
        public void deleteEmployee(Employee t)
        {
            DataSource.ContractList.RemoveAll(item => item.EmployeeNumber == t.ID); // delete all contract that with this employee
            t = findEmployeeByNumber((int)t.ID);
            DataSource.EmployeeList.Remove(t);
        }
        public void deleteEmployer(Employer t)
        {
            DataSource.ContractList.RemoveAll(item => item.EmployerNumber == t.CompanyNumber); // delete all contract that with this employer
            t = findEmployerByNumber((int)t.CompanyNumber);
            DataSource.EmployerList.Remove(t);
        }
        public void deleteSpecialization(Specialization t)
        {
            DataSource.ContractList.RemoveAll(item => item.SpecializationNumber == t.SpecializationNumber);// delete all contract that with this specialization
            XElement SpecializationElement;
            try
            {
                SpecializationElement = (from p in xmlSpecialization.SpecializationRoot.Elements()
                                         where Convert.ToInt32(p.Element("SpecializationNumber").Value) == t.SpecializationNumber
                                         select p).FirstOrDefault();
                SpecializationElement.Remove();
                xmlSpecialization.SpecializationRoot.Save(SpecializationPath);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Contract findContractByNumber(int num)
        {
            List<Contract> list = (List<Contract>)getContractsList();
            int i = list.FindIndex(t => t.ContractNumber == num);
            if (i == -1)
                return null;
            return DataSource.ContractList[i];
        }
        public Employee findEmployeeByNumber(int num)
        {
            List<Employee> list = (List<Employee>)getEmployeesList();
            int i = list.FindIndex(t => t.ID == num);
            if (i == -1)
                return null;
            return DataSource.EmployeeList[i];
        }
        public Employer findEmployerByNumber(int num)
        {
            List<Employer> list = (List<Employer>)getEmployersList();
            int i = list.FindIndex(t => t.CompanyNumber == num);
            if (i == -1)
                return null;
            return DataSource.EmployerList[i];
        }
        public Specialization findSpecializationByNumber(int num)
        {
            XElement specializationElement = (from p in xmlSpecialization.SpecializationRoot.Elements()
                                              where Convert.ToInt32(p.Element("SpecializationNumber").Value) == num
                                              select p).FirstOrDefault();
            Specialization t = new Specialization(
                (BE.FIELD_NAME)(int.Parse(specializationElement.Element("Field").Value)),
                specializationElement.Element("SpecializationName").Value,
                float.Parse(specializationElement.Element("MinHourWage").Value),
                float.Parse(specializationElement.Element("MaxHourWage").Value),
                num);
            return t;
        }

        public IEnumerable<BankAccount> getBankAccountsList()
        {
            if (downloadBankList)
            {
                XElement xml = XElement.Load(xmlLocalPath);

                foreach (var item in xml.Elements())
                {
                    yield return new BankAccount
                    {
                        bankNumber = int.Parse(item.Element("קוד_בנק").Value),
                        bankName = item.Element("שם_בנק").Value.Replace('\n', ' ').Trim(),
                        branchNumber = int.Parse(item.Element("קוד_סניף").Value),
                        bankAddress = item.Element("כתובת_ה-ATM").Value.Replace('\n', ' ').Trim(),
                        branchCity = item.Element("ישוב").Value.Replace('\n', ' ').Trim()
                    };
                }
            }
            else
                throw new Exception("The bank account list still not avilable\nplease try again later..");
        }
        public IEnumerable<Contract> getContractsList(Func<Contract, bool> func = null)
        {
            if (func != null)
            {
                return DataSource.ContractList.Where(func);
            }
            else
            {
                return DataSource.ContractList;
            }
        }
        public IEnumerable<Employee> getEmployeesList(Func<Employee, bool> func = null)
        {
            if (func != null)
            {
                return DataSource.EmployeeList.Where(func);
            }
            else
            {
                return DataSource.EmployeeList;
            }
        }
        public IEnumerable<Employer> getEmployersList(Func<Employer, bool> func = null)
        {
            if (func != null)
            {
                return DataSource.EmployerList.Where(func);
            }
            else
            {
                return DataSource.EmployerList;
            }
        }
        public IEnumerable<Specialization> getSpecializationsList(Func<Specialization, bool> func = null)
        {
            List<Specialization> specializations;
            try
            {
                specializations = (from p in xmlSpecialization.SpecializationRoot.Elements()
                                   select new Specialization()
                                   {
                                       Field = (BE.FIELD_NAME)Convert.ToInt32(p.Element("Field").Value),
                                       SpecializationName = p.Element("SpecializationName").Value,
                                       MinHourWage = float.Parse(p.Element("MinHourWage").Value),
                                       MaxHourWage = float.Parse(p.Element("MaxHourWage").Value),
                                       SpecializationNumber = uint.Parse(p.Element("SpecializationNumber").Value)
                                   }).ToList();

                if (func != null)
                {
                    return specializations.Where(func);
                }
                return specializations;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public IEnumerable<Contract> getValidContracts(DateTime t)
        {
            return DataSource.ContractList.Where(result => t >= result.BeginEmployment && t <= result.FinishEmployment);
        }

        public void updateContract(Contract t)
        {
            Contract tmp = DataSource.ContractList.Find(item => item.EmployeeNumber == t.EmployeeNumber && item.EmployerNumber == t.EmployerNumber);
            // update optional changes at values
            tmp.Interview = t.Interview;
            tmp.NetHourWage = t.NetHourWage;
            tmp.GrossHourWage = t.GrossHourWage;
            tmp.BeginEmployment = t.BeginEmployment;
            tmp.FinishEmployment = t.FinishEmployment;
            tmp.ContractHours = t.ContractHours;
            tmp.LivingArea = t.LivingArea;
        }
        public void updateEmployee(Employee t)
        {
            Employee tmp = findEmployeeByNumber((int)t.ID);
            // update optional changes at values
            tmp.Age = t.Age;
            tmp.Married = t.Married;
            tmp.PhoneNumber = t.PhoneNumber;
            tmp.Address = t.Address;
            tmp.Education = t.Education;
            tmp.ArmyService = t.ArmyService;
            tmp.BAccount = t.BAccount;
            tmp.YearsExperience = t.YearsExperience;
            tmp.LastName = t.LastName;
            tmp.Email = t.Email;
            tmp.SpecializationNumber = t.SpecializationNumber;
        }
        public void updateEmployer(Employer t)
        {
            Employer tmp = findEmployerByNumber((int)t.CompanyNumber);
            // update optional changes at values
            tmp.IfPrivate = t.IfPrivate;
            tmp.EmployerPhoneNumber = t.EmployerPhoneNumber;
            tmp.EmployerAddress = t.EmployerAddress;
            tmp.CompanyAddress = t.CompanyAddress;
            tmp.CompanyName = t.CompanyName;
            tmp.Field = t.Field;
            tmp.FirstName = t.FirstName;
            tmp.LastName = t.LastName;
            tmp.AmountOfWorkers = t.AmountOfWorkers;
        }
        public void updateSpecialization(Specialization t)
        {
            XElement specializationElement = (from p in xmlSpecialization.SpecializationRoot.Elements()
                                              where Convert.ToInt32(p.Element("SpecializationNumber").Value) == t.SpecializationNumber
                                              select p).FirstOrDefault();

            specializationElement.Element("MinHourWage").Value = t.MinHourWage.ToString();
            specializationElement.Element("MaxHourWage").Value = t.MaxHourWage.ToString();

            xmlSpecialization.SpecializationRoot.Save(SpecializationPath);
        }

        void DownloadXmlBankAccount()
        {
            WebClient wc = new WebClient();
            try
            {
                string xmlServerPath = @"http://www.boi.org.il/he/BankingSupervision/BanksAndBranchLocations/Lists/BoiBankBranchesDocs/atm.xml";
                wc.DownloadFile(xmlServerPath, xmlLocalPath);

            }
            catch (Exception)
            {
                string xmlServerPath = @"http://www.jct.ac.il/~coshri/atm.xml";
                wc.DownloadFile(xmlServerPath, xmlLocalPath);
            }
            finally
            {
                wc.Dispose();
            }
            downloadBankList = true; // successfull download
        }

        public List<Employee> loadListEmployeesFromXml(string path)
        {
            List<Employee> list;
            XmlSerializer t = new XmlSerializer(typeof(List<Employee>));
            try
            {
                FileStream f = new FileStream(path, FileMode.OpenOrCreate);
                list = (List<Employee>)t.Deserialize(f);
                return list;
            }
            catch (Exception)
            {
                //FileStream f = new FileStream(path, FileMode.Create);
                list = new List<Employee>();
                return list;
            }
        }

        public List<Employer> loadListEmployersFromXml(string path)
        {
            List<Employer> list;
            XmlSerializer t = new XmlSerializer(typeof(List<Employer>));
            try
            {
                FileStream f = new FileStream(path, FileMode.Open);
                list = (List<Employer>)t.Deserialize(f);
                return list;
            }
            catch (Exception)
            {
                //FileStream f = new FileStream(path, FileMode.Create);
                list = new List<Employer>();
                return list;
            }
        }

        public List<Contract> loadListContractsFromXml(string path)
        {
            List<Contract> list;
            XmlSerializer t = new XmlSerializer(typeof(List<Contract>));
            try
            {
                FileStream f = new FileStream(path, FileMode.Open);
                list = (List<Contract>)t.Deserialize(f);
                return list;
            }
            catch (Exception)
            {
                //FileStream f = new FileStream(path, FileMode.Create);
                list = new List<Contract>();
                return list;
            }
        }

        public void SaveListEmployeesToXml(List<Employee> list, string path)
        {
            XmlSerializer t = new XmlSerializer(typeof(List<Employee>));
            FileStream f = new FileStream(path, FileMode.Create);
            t.Serialize(f, list);
        }

        public void SaveListEmployersToXml(List<Employer> list, string path)
        {
            XmlSerializer t = new XmlSerializer(typeof(List<Employer>));
            FileStream f = new FileStream(path, FileMode.Create);
            t.Serialize(f, list);
        }

        public void SaveListContractsToXml(List<Contract> list, string path)
        {
            XmlSerializer t = new XmlSerializer(typeof(List<Contract>));
            FileStream f = new FileStream(path, FileMode.Create);
            t.Serialize(f, list);
        }

        public void SaveListsToFiles()
        {
            // functions that save the list object at the xml when the program is closing
            SaveListEmployeesToXml(DataSource.EmployeeList, EmployeePath);
            SaveListEmployersToXml(DataSource.EmployerList, EmployerPath);
            SaveListContractsToXml(DataSource.ContractList, ContractsPath);

            // update the running numbers from the classes to the xml
            XElement configElement = (from p in config.ConfigRoot.Elements()
                                              select p).FirstOrDefault(); // get the only element from the xml file

            configElement.Element("i").Value = BE.Specialization.i.ToString();
            configElement.Element("j").Value = BE.Contract.j.ToString();

            config.ConfigRoot.Save(@"Config.xml");
        }
    }
}
