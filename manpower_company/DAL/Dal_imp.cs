using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using DS;

namespace DAL
{
    internal class Dal_imp : IDAL
    {
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
        {   // check if Specialization is already exist by comparing field and specialization name
            if (DataSource.SpecializationList.Exists(result => result.Field == t.Field && result.SpecializationName == t.SpecializationName))
                throw new Exception("The Specialization is already exist");
            DataSource.SpecializationList.Add(t);
        }

        public void deleteContract(Contract t)
        {
            DataSource.ContractList.Remove(t);
        }
        public void deleteEmployee(Employee t)
        {
            DataSource.ContractList.RemoveAll(item => item.EmployeeNumber == t.ID); // delete all contract that with this employee
            DataSource.EmployeeList.Remove(t);
        }
        public void deleteEmployer(Employer t)
        {
            DataSource.ContractList.RemoveAll(item => item.EmployerNumber == t.CompanyNumber); // delete all contract that with this employer
            DataSource.EmployerList.Remove(t);
        }
        public void deleteSpecialization(Specialization t)
        {
            DataSource.SpecializationList.Remove(t);
        }

        public IEnumerable<BankAccount> getBankAccountsList()
        {
            List<BankAccount> list = new List<BankAccount>();
            list.Add(new BankAccount(1, "Poalim", 698, "Yafo 121", "Jerusalem", 725687));
            list.Add(new BankAccount(2, "Leumi", 456, "Bialik 65", "Yafo", 165789));
            list.Add(new BankAccount(3, "Diskont", 782, "Harav Maimon 121", "Haifa", 195634));
            list.Add(new BankAccount(4, "Mizrachi", 717, "Jabotinski 13", "Yafo", 284930));
            list.Add(new BankAccount(1, "Poalim", 624, "Gaza 17", "Tel-Aviv", 508493));

            return list;
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
            if (func != null)
            {
                return DataSource.SpecializationList.Where(func);
            }
            else
            {
                return DataSource.SpecializationList;
            }
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
            tmp.ContractSigned = t.ContractSigned;
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
            Specialization tmp = DataSource.SpecializationList.Find(item => item.SpecializationName == t.SpecializationName && item.Field == t.Field);
            // update optional changes at values
            tmp.MinHourWage = t.MinHourWage;
            tmp.MaxHourWage = t.MaxHourWage;
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
            List<Specialization> list = (List<Specialization>)getSpecializationsList();
            int i = list.FindIndex(t => t.SpecializationNumber == num);
            if (i == -1)
                return null;
            return DataSource.SpecializationList[i];
        }

        /// <summary>
        /// get the all valid contracts at the moment t
        /// </summary>
        /// <returns>return IEnumerable that contain the contract</returns>
        public IEnumerable<Contract> getValidContracts(DateTime t)
        {
            return DataSource.ContractList.Where(result => t >= result.BeginEmployment && t <= result.FinishEmployment);
        }

        public void SaveListsToFiles()
        {
            ;
        }
    }
}
