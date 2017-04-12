using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for Data.xaml
    /// </summary>
    public partial class Data : UserControl
    {
        int min1, min2, max;
        bool army, married, ifPrivate, contractSigned;
        
        BL.IBL bl;

        public Data()
        {
            InitializeComponent();
            this.list_comboBox.ItemsSource = new List<string>() { "Specialization", "Employee", "Employer", "Contract" };
            bl = BL.FactoryBl.GetInstance();
            this.Gender.ItemsSource = Enum.GetValues(typeof(BE.GENDER));
            this.Field.ItemsSource = Enum.GetValues(typeof(BE.FIELD_NAME));

            ContractsOfCompany.ItemsSource = bl.getEmployersList();
            ContractsOfCompany.DisplayMemberPath = "CompanyName";
            
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                switch (list_comboBox.SelectedIndex)
                {
                    case 0:
                        if (MinHourWage.Text != "")
                            min1 = int.Parse(MinHourWage.Text);
                        else
                            min1 = 0;
                        if (MaxHourWage.Text != "")
                            max = int.Parse(MaxHourWage.Text);
                        else
                            max = int.MaxValue;

                        specializationDataGrid.ItemsSource = bl.getSpecializationsList(item => item.MinHourWage >= min1 && item.MaxHourWage <= max);
                        break;
                    case 1:
                        if (MinAge.Text != "")
                            min1 = int.Parse(MinAge.Text);
                        else
                            min1 = 0;
                        if (MaxAge.Text != "")
                            max = int.Parse(MaxAge.Text);
                        else
                            max = int.MaxValue;
                        if (YearsExperience.Text != "")
                            min2 = int.Parse(YearsExperience.Text);
                        else
                            min2 = 0;


                        if (ArmyService.Text != "" && Married.Text != "" && Gender.SelectedIndex == -1)
                        {
                            if (ArmyService.Text.ToUpper() == "Y")
                                army = true;
                            else
                                army = false;
                            if (Married.Text.ToUpper() == "Y")
                                married = true;
                            else
                                married = false;
                            employeeDataGrid.ItemsSource = bl.getEmployeesList(item => item.Age >= min1 && item.Age <= max && item.YearsExperience>=min2 && item.ArmyService == army && item.Married == married);
                            break;
                        }

                        if (ArmyService.Text != "" && Married.Text != "" && Gender.SelectedIndex != -1)
                        {
                            if (ArmyService.Text.ToUpper() == "Y")
                                army = true;
                            else
                                army = false;
                            if (Married.Text.ToUpper() == "Y")
                                married = true;
                            else
                                married = false;
                            employeeDataGrid.ItemsSource = bl.getEmployeesList(item => item.Age >= min1 && item.Age <= max && item.YearsExperience >= min2 && item.ArmyService == army && item.Married == married && item.Gender == (BE.GENDER)Gender.SelectedIndex);
                            break;
                        }

                        if (ArmyService.Text != "" && Married.Text == "" && Gender.SelectedIndex == -1)
                        {
                            if (ArmyService.Text.ToUpper() == "Y")
                                army = true;
                            else
                                army = false;
                            
                            employeeDataGrid.ItemsSource = bl.getEmployeesList(item => item.Age >= min1 && item.Age <= max && item.YearsExperience >= min2 && item.ArmyService == army);
                            break;
                        }

                        if (ArmyService.Text != "" && Married.Text == "" && Gender.SelectedIndex != -1)
                        {
                            if (ArmyService.Text.ToUpper() == "Y")
                                army = true;
                            else
                                army = false;

                            employeeDataGrid.ItemsSource = bl.getEmployeesList(item => item.Age >= min1 && item.Age <= max && item.YearsExperience >= min2 && item.ArmyService == army && item.Gender == (BE.GENDER)Gender.SelectedIndex);
                            break;
                        }


                        if (ArmyService.Text == "" && Married.Text != "" && Gender.SelectedIndex == -1)
                        {
                            if (Married.Text.ToUpper() == "Y")
                                married = true;
                            else
                                married = false;
                            employeeDataGrid.ItemsSource = bl.getEmployeesList(item => item.Age >= min1 && item.Age <= max && item.YearsExperience >= min2 && item.Married == married);
                            break;
                        }

                        if (ArmyService.Text == "" && Married.Text != "" && Gender.SelectedIndex != -1)
                        {
                            if (Married.Text.ToUpper() == "Y")
                                married = true;
                            else
                                married = false;
                            employeeDataGrid.ItemsSource = bl.getEmployeesList(item => item.Age >= min1 && item.Age <= max && item.YearsExperience >= min2 && item.Married == married && item.Gender == (BE.GENDER)Gender.SelectedIndex);
                            break;
                        }

                        if (ArmyService.Text == "" && Married.Text == "" && Gender.SelectedIndex != -1)
                        {
                            employeeDataGrid.ItemsSource = bl.getEmployeesList(item => item.Age >= min1 && item.Age <= max && item.YearsExperience >= min2 && item.Gender == (BE.GENDER)Gender.SelectedIndex);
                            break;
                        }

                        employeeDataGrid.ItemsSource = bl.getEmployeesList(item => item.Age >= min1 && item.Age <= max && item.YearsExperience >= min2);
                        break;

                    case 2:
                        if (AmountOfWorkers.Text != "")
                            min1 = int.Parse(AmountOfWorkers.Text);
                        else
                            min1 = 0;

                        if (IfPrivate.Text != "" && Field.SelectedIndex != -1)
                        {
                            if (IfPrivate.Text.ToUpper() == "Y")
                                ifPrivate = true;
                            else
                                ifPrivate = false;

                            employerDataGrid.ItemsSource = bl.getEmployersList(item => item.AmountOfWorkers >= min1 && item.IfPrivate == ifPrivate && item.Field == (BE.FIELD_NAME)(Field.SelectedIndex+1));
                            break;
                        }
                        if (IfPrivate.Text == "" && Field.SelectedIndex != -1)
                        {
                            employerDataGrid.ItemsSource = bl.getEmployersList(item => item.AmountOfWorkers >= min1 && item.Field == (BE.FIELD_NAME)(Field.SelectedIndex+1));
                            break;
                        }
                        if (IfPrivate.Text != "" && Field.SelectedIndex == -1)
                        {
                            if (IfPrivate.Text.ToUpper() == "Y")
                                ifPrivate = true;
                            else
                                ifPrivate = false;

                            employerDataGrid.ItemsSource = bl.getEmployersList(item => item.AmountOfWorkers >= min1 && item.IfPrivate == ifPrivate);
                            break;
                        }
                        if (IfPrivate.Text == "" && Field.SelectedIndex == -1)
                        {
                            employerDataGrid.ItemsSource = bl.getEmployersList(item => item.AmountOfWorkers >= min1);
                            break;
                        }
                        employerDataGrid.ItemsSource = bl.getEmployersList();
                        break;

                    case 3:
                        if (MinNetHourWage.Text != "")
                            min1 = int.Parse(MinNetHourWage.Text);
                        else
                            min1 = 0;

                        if (ContractSigned.Text != ""  && ContractsThatStillValid.IsChecked != false && ContractsOfCompany.SelectedIndex != -1)
                        {
                            if (ContractSigned.Text.ToUpper() == "Y")
                                contractSigned = true;
                            else
                                contractSigned = false;
                            contractDataGrid.ItemsSource = bl.getContractsList(item => item.NetHourWage >= min1 && item.ContractSigned == contractSigned && (item.BeginEmployment <= DateTime.Now && item.FinishEmployment >= DateTime.Now) && item.EmployerNumber == ((BE.Employer)ContractsOfCompany.SelectedItem).CompanyNumber);
                            break;
                        }

                        if (ContractSigned.Text != "" && ContractsThatStillValid.IsChecked != false && ContractsOfCompany.SelectedIndex == -1)
                        {
                            if (ContractSigned.Text.ToUpper() == "Y")
                                contractSigned = true;
                            else
                                contractSigned = false;
                            contractDataGrid.ItemsSource = bl.getContractsList(item => item.NetHourWage >= min1 && (item.BeginEmployment <= DateTime.Now && item.FinishEmployment >= DateTime.Now) && item.ContractSigned == contractSigned);
                            break;
                        }

                        if (ContractSigned.Text != "" && ContractsThatStillValid.IsChecked == false && ContractsOfCompany.SelectedIndex != -1)
                        {
                            if (ContractSigned.Text.ToUpper() == "Y")
                                contractSigned = true;
                            else
                                contractSigned = false;
                            contractDataGrid.ItemsSource = bl.getContractsList(item => item.NetHourWage >= min1 && item.ContractSigned == contractSigned && item.EmployerNumber == ((BE.Employer)ContractsOfCompany.SelectedItem).CompanyNumber);
                            break;
                        }

                        if (ContractSigned.Text != "" && ContractsThatStillValid.IsChecked == false && ContractsOfCompany.SelectedIndex == -1)
                        {
                            if (ContractSigned.Text.ToUpper() == "Y")
                                contractSigned = true;
                            else
                                contractSigned = false;
                            contractDataGrid.ItemsSource = bl.getContractsList(item => item.NetHourWage >= min1 && item.ContractSigned == contractSigned);
                            break;
                        }

                        if (ContractSigned.Text == "" && ContractsThatStillValid.IsChecked != false && ContractsOfCompany.SelectedIndex != -1)
                        {
                            contractDataGrid.ItemsSource = bl.getContractsList(item => item.NetHourWage >= min1 && (item.BeginEmployment <= DateTime.Now && item.FinishEmployment >= DateTime.Now) && item.EmployerNumber == ((BE.Employer)ContractsOfCompany.SelectedItem).CompanyNumber);
                            break;
                        }

                        if (ContractSigned.Text == "" && ContractsThatStillValid.IsChecked != false && ContractsOfCompany.SelectedIndex == -1)
                        {
                            contractDataGrid.ItemsSource = bl.getContractsList(item => item.NetHourWage >= min1 && item.BeginEmployment <= DateTime.Now && item.FinishEmployment >= DateTime.Now);
                            break;
                        }

                        if (ContractSigned.Text == "" && ContractsThatStillValid.IsChecked == false && ContractsOfCompany.SelectedIndex != -1)
                        {
                            contractDataGrid.ItemsSource = bl.getContractsList(item => item.NetHourWage >= min1 && item.EmployerNumber == ((BE.Employer)ContractsOfCompany.SelectedItem).CompanyNumber);
                            break;
                        }

                        contractDataGrid.ItemsSource = bl.getContractsList(item => item.NetHourWage >= min1);
                        break;                      
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void clear_Click(object sender, RoutedEventArgs e)
        {
            switch (list_comboBox.SelectedIndex)
            {
                case 0:
                    MinHourWage.Text = "";
                    MaxHourWage.Text = "";
                    specializationDataGrid.ItemsSource = bl.getSpecializationsList();
                    break;
                case 1:
                    MinAge.Text = "";
                    MaxAge.Text = "";
                    ArmyService.Text = "";
                    Married.Text = "";
                    YearsExperience.Text = "";
                    employeeDataGrid.ItemsSource = bl.getEmployeesList();
                    break;
                case 2:
                    IfPrivate.Text = "";
                    AmountOfWorkers.Text = "";
                    Field.SelectedIndex = -1;
                    employerDataGrid.ItemsSource = bl.getEmployersList();
                    break;
                case 3:
                    ContractsOfCompany.Text = "";
                    ContractSigned.Text = "";
                    MinNetHourWage.Text = "";
                    ContractsThatStillValid.IsChecked = false;
                    contractDataGrid.ItemsSource = bl.getContractsList();
                    break;
                default:
                    break;
            }
        }
        private void list_comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is ComboBox && ((ComboBox)sender).SelectedIndex > -1)
            {
                switch (((ComboBox)sender).SelectedIndex)
                {
                    case 0:
                        specializationDataGrid.ItemsSource = bl.getSpecializationsList();
                        break;
                    case 1:
                        employeeDataGrid.ItemsSource = bl.getEmployeesList();
                        break;
                    case 2:
                        employerDataGrid.ItemsSource = bl.getEmployersList();
                        break;
                    case 3:
                        contractDataGrid.ItemsSource = bl.getContractsList();
                        break;
                    default:
                        break;
                }
            }
        }

    }
}
