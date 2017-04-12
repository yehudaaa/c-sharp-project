using BE;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    /// <summary>
    /// Add functions for each class
    /// Delete functions for each class
    /// Update functions for each class
    /// Create List for each class
    /// </summary>
    public interface IBL
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

        IEnumerable<Specialization> getSpecializationsList(Func<Specialization, bool> func = null);
        IEnumerable<Employee> getEmployeesList(Func<Employee, bool> func = null);
        IEnumerable<Employer> getEmployersList(Func<Employer, bool> func = null);
        IEnumerable<Contract> getContractsList(Func<Contract, bool> func = null);

        IEnumerable<BankAccount> getBankAccountsList();

        /// <summary>
        /// Find or search functions, by given a uniqe code of object
        /// </summary>
        /// <param name="num">the uniqe code</param>
        /// <returns>return appearance of object, could be returned null if not exist</returns>
        Contract findContractByNumber(int num);
        Employee findEmployeeByNumber(int num);
        Employer findEmployerByNumber(int num);
        Specialization findSpecializationByNumber(int num);

        /// <summary>
        /// get the sum of contracts by boolian function
        /// </summary>
        /// <param name="func">delegate that will point on the function that we will get </param>
        /// <returns>the sum of the contracts that the function provide</returns>
        int getSumOfContractsByCondition(Func<Contract, bool> func = null);
        /// <summary>
        /// get Sum Of Contracts By given Condition
        /// </summary>
        /// <param name="func">the result of this boolian function will decide if the object will get into the IEnumerable </param>
        /// <returns></returns>
        int getSumOfEmployeesByCondition(Func<Employee, bool> func = null);
        /// <summary>
        /// get the employee that earn the biggest salary
        /// </summary>
        /// <returns>return employee object </returns>
        Employee biggestSalary();
        
        /// <summary>
        /// This function grouping all bank accounts by bank name, and city
        /// </summary>
        /// <returns>return IEnumerable of the banks names that contain inside grouping of all the branch banks by cities </returns>
        IEnumerable<IGrouping<string, IGrouping<string, BankAccount>>> groupingByCitiesAndBranches();
        IEnumerable<IGrouping<int, Contract>> groupingContractsBySpecialization(bool sort = false);
        IEnumerable<IGrouping<LIVING_AREA, Contract>> groupingContractsByLivingArea(bool sort = false);
        IEnumerable<IGrouping<string, float>> groupingProfitsByMonth(bool sort = false);
        /// <summary>
        /// grouping employees by age to five classes
        /// </summary>
        /// <param name="sort">boolian parameter thet say if the user want to sort the grouping</param>
        /// <returns></returns>
        IEnumerable<IGrouping<string, Employee>> groupingEmployeesByAge(bool sort = false);

        /// <summary>
        /// get the average net hour wage by condition at the function
        /// </summary>
        /// <param name="func">the condition at the func will decide which values get to the IEnumerable</param>
        /// <returns>the avergae</returns>
        float getAverageHourPayWageByCondition(Func<Contract, bool> func = null);
        /// <summary>
        /// check if to Employee have a valid contract 
        /// </summary>
        /// <param name="t">the employee</param>
        /// <returns>if to Employee have a valid contract return true, else return false </returns>
        bool IfEmployeeStillWorking(Employee t);

        /// <summary>
        /// this function call to dal for save the static lists in xml files
        /// </summary>
        void SaveListsToFiles();

        event EventHandler<BL_EventArgs> addedElement;
    }
}
