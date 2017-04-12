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
    /// Interaction logic for EmployeeWin.xaml
    /// </summary>
    public partial class EmployeeWin : UserControl
    {
        BE.Employee employee;
        BE.BankAccount bankAccount;
        BL.IBL bl;
        BankAccountWin win;

        public EmployeeWin()
        {
            InitializeComponent();
            this.genderComboBox.ItemsSource = Enum.GetValues(typeof(BE.GENDER));
            this.educationTextBox.ItemsSource = Enum.GetValues(typeof(BE.EDUCATION));

            bl = BL.FactoryBl.GetInstance();
            this.list_comboBox.ItemsSource = bl.getEmployeesList();
            this.list_comboBox.DisplayMemberPath = "FirstName";
            this.list_comboBox.SelectedValuePath = "ID";

            this.specializationNumberComboBox.ItemsSource = bl.getSpecializationsList();
            this.specializationNumberComboBox.DisplayMemberPath = "SpecializationName";
            this.specializationNumberComboBox.SelectedValuePath = "SpecializationNumber";

        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

            // Do not load your data at design time.
            // if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
            // {
            // 	//Load your data here and assign the result to the CollectionViewSource.
            // 	System.Windows.Data.CollectionViewSource myCollectionViewSource = (System.Windows.Data.CollectionViewSource)this.Resources["Resource Key for CollectionViewSource"];
            // 	myCollectionViewSource.Source = your data
            // }
            // Do not load your data at design time.
            // if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
            // {
            // 	//Load your data here and assign the result to the CollectionViewSource.
            // 	System.Windows.Data.CollectionViewSource myCollectionViewSource = (System.Windows.Data.CollectionViewSource)this.Resources["Resource Key for CollectionViewSource"];
            // 	myCollectionViewSource.Source = your data
            // }
            // Do not load your data at design time.
            // if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
            // {
            // 	//Load your data here and assign the result to the CollectionViewSource.
            // 	System.Windows.Data.CollectionViewSource myCollectionViewSource = (System.Windows.Data.CollectionViewSource)this.Resources["Resource Key for CollectionViewSource"];
            // 	myCollectionViewSource.Source = your data
            // }
        }

        private void list_comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is ComboBox && ((ComboBox)sender).SelectedIndex > -1)
            {
                employee = new BE.Employee((BE.Employee)(list_comboBox.SelectedItem));
                this.DataContext = null;
                this.DataContext = employee;
            }
        }

        private void add_click(object sender, RoutedEventArgs e)
        {
            float tmp;
            try
            {
                employee = new BE.Employee(
                    uint.Parse(iDTextBox.Text),
                    lastNameTextBox.Text,
                    firstNameTextBox.Text,
                    (bool)marriedCheckBox.IsChecked,
                    DateTime.Parse(birthDateDatePicker.Text),
                    phoneNumberTextBox.Text,
                    addressTextBox.Text,
                    (BE.GENDER)(genderComboBox.SelectedIndex + 1),
                    (BE.EDUCATION)educationTextBox.SelectedItem,
                    (bool)(armyServiceCheckBox.IsChecked),
                    bankAccount,
                    uint.Parse(specializationNumberComboBox.SelectedValue.ToString()),
                    float.TryParse(yearsExperienceTextBox.Text,out tmp)? tmp : 0,
                    emailTextBox.Text);
                bl.addEmployee(employee);

                MessageBox.Show("the employee added successfully..", "", MessageBoxButton.OK, MessageBoxImage.Information);


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
                        bl.deleteEmployee((BE.Employee)DataContext);
                        DataContext = null;
                        this.list_comboBox.ItemsSource = null;
                        this.list_comboBox.ItemsSource = bl.getEmployeesList();
                        MessageBox.Show("the employee deleted successfully..", "", MessageBoxButton.OK, MessageBoxImage.Information);
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
                bl.updateEmployee((BE.Employee)DataContext);
                this.list_comboBox.ItemsSource = null;
                this.list_comboBox.ItemsSource = bl.getEmployeesList();
                MessageBox.Show("the employee updated successfully..", "", MessageBoxButton.OK, MessageBoxImage.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void addBankAccount_Click(object sender, RoutedEventArgs e)
        {
            win = new BankAccountWin();
            win.chooseBank += Win_chooseBank;
            win.Show();
        }

        private void Win_chooseBank(object sender, EventArgs e)
        {
            bankAccount = Bank.bank;
            win.Close();
            this.bankAccountDetail.Content = bankAccount.ToString();
        }

        private void TextBox_Error(object sender, ValidationErrorEventArgs e)
        {
            if (e.Action == ValidationErrorEventAction.Added)
                (sender as Control).ToolTip = e.Error.ErrorContent.ToString();
            else
                (sender as Control).ToolTip = "the value is correct";
        }

        private void add_employee_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                list_comboBox.SelectedIndex = -1;
            }
            catch (Exception)
            {

            }

            DataContext = null;
        }

        private void delete_employee_Checked(object sender, RoutedEventArgs e)
        {
            DataContext = null;
           
            this.list_comboBox.ItemsSource = null;
            this.list_comboBox.ItemsSource = bl.getEmployeesList();
        }

        private void update_employee_Checked(object sender, RoutedEventArgs e)
        {
            DataContext = null;
            this.list_comboBox.ItemsSource = null;
            this.list_comboBox.ItemsSource = bl.getEmployeesList();
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

