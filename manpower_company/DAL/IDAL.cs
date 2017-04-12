using BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    /// <summary>
    /// Add functions for each class
    /// Delete functions for each class
    /// Update functions for each class
    /// Create List for each class
    /// </summary>
    public interface IDAL
    {
        void addSpecialization(Specialization t);
        void deleteSpecialization(Specialization t);
        void updateSpecialization(Specialization t);

        void addEmployee(Employee t);
        void deleteEmployee(Employee t);
        void updateEmployee(Employee t);

        void addEmployer(Employer t);
        void deleteEmployer(Employer t);
        void updateEmployer(Employer t);

        void addContract(Contract t);
        void deleteContract(Contract t);
        void updateContract(Contract t);

        IEnumerable<Specialization> getSpecializationsList(Func<Specialization,bool> func = null);
        IEnumerable<Employee> getEmployeesList(Func<Employee, bool> func = null);
        IEnumerable<Employer> getEmployersList(Func<Employer, bool> func = null);
        IEnumerable<Contract> getContractsList(Func<Contract, bool> func = null);
        IEnumerable<BankAccount> getBankAccountsList();


        Contract findContractByNumber(int num);
        Employer findEmployerByNumber(int num);
        Employee findEmployeeByNumber(int num);
        Specialization findSpecializationByNumber(int num);

        /// <summary>
        /// get the all valid contracts at the moment t
        /// </summary>
        /// <returns>return IEnumerable that contain the contract</returns>
        IEnumerable<Contract> getValidContracts(DateTime t);

        void SaveListsToFiles();
    }
}
