using System;
using System.Collections.Generic;
using System.Globalization;
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
    /// Interaction logic for ContractWin.xaml
    /// </summary>
    public partial class ContractWin : UserControl
    {
        BE.Contract contract;
        BL.IBL bl;

        public ContractWin()
        {
            InitializeComponent();
            this.fieldComboBox.ItemsSource = Enum.GetValues(typeof(BE.FIELD_NAME));
            this.livingAreaComboBox1.ItemsSource = Enum.GetValues(typeof(BE.LIVING_AREA));

            bl = BL.FactoryBl.GetInstance();
            this.list_comboBox.ItemsSource = bl.getContractsList();
            this.list_comboBox.DisplayMemberPath = "ContractNumber";
            this.list_comboBox.SelectedValuePath = "ContractNumber";

            this.specializationNumberComboBox.ItemsSource = bl.getSpecializationsList();
            this.specializationNumberComboBox.DisplayMemberPath = "SpecializationName";
            this.specializationNumberComboBox.SelectedValuePath = "SpecializationNumber";

            this.employerNumberCombobox1.ItemsSource = bl.getEmployersList();
            this.employerNumberCombobox1.DisplayMemberPath = "FirstName";
            this.employerNumberCombobox1.SelectedValuePath = "CompanyNumber";

            this.employeeNumberCombobox1.ItemsSource = bl.getEmployeesList();
            this.employeeNumberCombobox1.DisplayMemberPath = "FirstName";
            this.employeeNumberCombobox1.SelectedValuePath = "ID";
        }

       
        private void list_comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is ComboBox && ((ComboBox)sender).SelectedIndex > -1)
            {
                contract = new BE.Contract((BE.Contract)list_comboBox.SelectedItem);
                this.DataContext = contract;
                // update the values in the folowing ComboBox
                employeeNumberCombobox1.ItemsSource = new List<BE.Employee>() { bl.findEmployeeByNumber(contract.EmployeeNumber)};
                employeeNumberCombobox1.SelectedIndex++;
                employerNumberCombobox1.ItemsSource = new List<BE.Employer>() { bl.findEmployerByNumber(contract.EmployerNumber) };
                employerNumberCombobox1.SelectedIndex++;
                specializationNumberComboBox.ItemsSource = new List<BE.Specialization>() { bl.findSpecializationByNumber(contract.SpecializationNumber) };
                specializationNumberComboBox.SelectedIndex++;
            }
        }

        private void add_click(object sender, RoutedEventArgs e)
        {
            try
            {
                contract = new BE.Contract(
                    int.Parse(employerNumberCombobox1.SelectedValue.ToString()),
                    int.Parse(employeeNumberCombobox1.SelectedValue.ToString()),
                    (bool)interviewCheckBox1.IsChecked,
                    (bool)contractSignedCheckBox1.IsChecked,
                    float.Parse(grossHourWageTextBox1.Text),
                    DateTime.Parse(beginEmploymentDatePicker1.Text),
                    DateTime.Parse(finishEmploymentDatePicker1.Text),
                    int.Parse(contractHoursTextBox1.Text),
                    (BE.FIELD_NAME)fieldComboBox.SelectedItem,
                    (BE.LIVING_AREA)livingAreaComboBox1.SelectedItem,
                    int.Parse(specializationNumberComboBox.SelectedValue.ToString())
                    );

                bl.addContract(contract);
                
                MessageBox.Show("the contract added successfully..", "", MessageBoxButton.OK, MessageBoxImage.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void delete_click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure?..", "Validate Action", MessageBoxButton.YesNo, MessageBoxImage.Question);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    try
                    {
                        bl.deleteContract((BE.Contract)DataContext);
                        DataContext = null;
                        this.list_comboBox.ItemsSource = null;
                        this.list_comboBox.ItemsSource = bl.getContractsList();
                        MessageBox.Show("the contract deleted successfully..", "", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    break;
                case MessageBoxResult.No:
                    break;
                default:
                    break;
            }
        }

        private void update_click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.updateContract((BE.Contract)DataContext);
                this.list_comboBox.ItemsSource = null;
                this.list_comboBox.ItemsSource = bl.getContractsList();
                MessageBox.Show("the contract updated successfully..", "", MessageBoxButton.OK, MessageBoxImage.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void TextBox_Error(object sender, ValidationErrorEventArgs e)
        {
            if (e.Action == ValidationErrorEventAction.Added)
                (sender as Control).ToolTip = e.Error.ErrorContent.ToString();
            else
                (sender as Control).ToolTip = "the value is correct";
        }

        private void add_contract_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                this.specializationNumberComboBox.ItemsSource = null;
                this.employerNumberCombobox1.ItemsSource = null;
                this.employeeNumberCombobox1.ItemsSource = null;

                this.specializationNumberComboBox.ItemsSource = bl.getSpecializationsList();
                this.employerNumberCombobox1.ItemsSource = bl.getEmployersList();
                this.employeeNumberCombobox1.ItemsSource = bl.getEmployeesList();

                list_comboBox.SelectedIndex = -1;
            }
            catch (Exception)
            {
                ;
            }
            DataContext = null;
        }

        private void delete_contract_Checked(object sender, RoutedEventArgs e)
        {
            this.list_comboBox.ItemsSource = null;
            this.list_comboBox.ItemsSource = bl.getContractsList();
            DataContext = null;
        }

        private void update_contract_Checked(object sender, RoutedEventArgs e)
        {
            DataContext = null;
            this.employerNumberCombobox1.ItemsSource = null;
            this.employeeNumberCombobox1.ItemsSource = null;
            this.list_comboBox.ItemsSource = null;
            this.list_comboBox.ItemsSource = bl.getContractsList();

            this.specializationNumberComboBox.ItemsSource = null;
            this.specializationNumberComboBox.ItemsSource = bl.getSpecializationsList();
           
        }

        private void txtNum_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox t = sender as TextBox;

            if (t == null)
                return;

            float val;
            if (!float.TryParse(t.Text, out val))
                t.Text = "";
        }
    }
}
