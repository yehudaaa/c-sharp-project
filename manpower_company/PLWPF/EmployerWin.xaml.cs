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
    /// Interaction logic for EmployerWin.xaml
    /// </summary>
    public partial class EmployerWin : UserControl
    {
        BE.Employer employer;
        BL.IBL bl;

        public EmployerWin()
        {
            InitializeComponent();
            this.genderComboBox.ItemsSource = Enum.GetValues(typeof(BE.GENDER));
            this.fieldComboBox.ItemsSource = Enum.GetValues(typeof(BE.FIELD_NAME));

            bl = BL.FactoryBl.GetInstance();
            this.list_comboBox.ItemsSource = bl.getEmployersList();
            this.list_comboBox.DisplayMemberPath = "CompanyName";
            this.list_comboBox.SelectedValuePath = "CompanyNumber";

        }

        private void list_comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is ComboBox && ((ComboBox)sender).SelectedIndex > -1)
            {
                employer = new BE.Employer((BE.Employer)(list_comboBox.SelectedItem));
                this.DataContext = employer;
            }
        }

        private void add_click(object sender, RoutedEventArgs e)
        {
            try
            {
                employer = new BE.Employer(
                    uint.Parse(companyNumberTextBox.Text),
                    (bool)ifPrivateCheckBox.IsChecked,
                    firstNameTextBox.Text,
                    lastNameTextBox.Text,
                    (BE.GENDER)(genderComboBox.SelectedIndex),
                    companyNameTextBox.Text,
                    employerPhoneNumberTextBox.Text,
                    employerAddressTextBox.Text,
                    companyAddressTextBox.Text,
                    (BE.FIELD_NAME)fieldComboBox.SelectedItem,
                    DateTime.Parse(establishmentDateDatePicker.Text),
                    int.Parse(amountOfWorkersTextBox.Text) );

                bl.addEmployer(employer);
                
                MessageBox.Show("the employer added successfully..", "", MessageBoxButton.OK, MessageBoxImage.Information);
               
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
                        bl.deleteEmployer((BE.Employer)DataContext);
                        DataContext = null;
                        this.list_comboBox.ItemsSource = null;
                        this.list_comboBox.ItemsSource = bl.getEmployersList();
                        MessageBox.Show("the employer deleted successfully..", "", MessageBoxButton.OK, MessageBoxImage.Information);
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
                bl.updateEmployer((BE.Employer)DataContext);
                this.list_comboBox.ItemsSource = null;
                this.list_comboBox.ItemsSource = bl.getEmployersList();
                MessageBox.Show("the employer updated successfully..", "", MessageBoxButton.OK, MessageBoxImage.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void TextBox_Error(object sender, ValidationErrorEventArgs e)
        {
            if (e.Action == ValidationErrorEventAction.Added)
                (sender as Control).ToolTip = e.Error.ErrorContent.ToString();
            else
                (sender as Control).ToolTip = "the value is correct";
        }

        private void add_employer_Checked(object sender, RoutedEventArgs e)
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

        private void delete_employer_Checked(object sender, RoutedEventArgs e)
        {
            DataContext = null;
            this.list_comboBox.ItemsSource = null;
            this.list_comboBox.ItemsSource = bl.getEmployersList();
        }

        private void update_employer_Checked(object sender, RoutedEventArgs e)
        {
            DataContext = null;
            this.list_comboBox.ItemsSource = null;
            this.list_comboBox.ItemsSource = bl.getEmployersList();
            
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
