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
using System.Text.RegularExpressions;

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for SpecializationWin.xaml
    /// </summary>
    public partial class SpecializationWin : UserControl
    {
        BE.Specialization specialization;
        BL.IBL bl;


        public SpecializationWin()
        {
            InitializeComponent();
            this.fieldComboBox.ItemsSource = Enum.GetValues(typeof(BE.FIELD_NAME));

            bl = BL.FactoryBl.GetInstance();

            specialization = new BE.Specialization();
            this.DataContext = specialization;

            this.list_comboBox.ItemsSource = null;
            this.list_comboBox.ItemsSource = bl.getSpecializationsList();
            this.list_comboBox.DisplayMemberPath = "SpecializationName";
            this.list_comboBox.SelectedValuePath = "SpecializationNumber";
        }
         

        private void list_comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is ComboBox && ((ComboBox)sender).SelectedIndex > -1)
            {
                specialization = new BE.Specialization((BE.Specialization)(list_comboBox.SelectedItem));
                this.DataContext = specialization;
            }
        }

        private void add_click(object sender, RoutedEventArgs e)
        {
            try
            {
                specialization = new BE.Specialization((BE.FIELD_NAME)(fieldComboBox.SelectedIndex + 1), specializationNameTextBox.Text, float.Parse(minHourWageTextBox.Text), float.Parse(maxHourWageTextBox.Text));
                bl.addSpecialization(specialization);
                MessageBox.Show("the specialization added successfully..", "", MessageBoxButton.OK, MessageBoxImage.Information);
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
                        bl.deleteSpecialization((BE.Specialization)DataContext);
                        DataContext = null;
                        this.list_comboBox.ItemsSource = null;
                        this.list_comboBox.ItemsSource = bl.getSpecializationsList();
                        MessageBox.Show("the specialization deleted successfully..", "", MessageBoxButton.OK, MessageBoxImage.Information);
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
                bl.updateSpecialization((BE.Specialization)DataContext);
                this.list_comboBox.ItemsSource = null;
                this.list_comboBox.ItemsSource = bl.getSpecializationsList();
                MessageBox.Show("the specialization updated successfully..", "", MessageBoxButton.OK, MessageBoxImage.Information);

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

        private void add_specialization_Checked(object sender, RoutedEventArgs e)
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

        private void delete_specialization_Checked(object sender, RoutedEventArgs e)
        {
            this.list_comboBox.ItemsSource = null;
            this.list_comboBox.ItemsSource = bl.getSpecializationsList();
            DataContext = null;
        }

        private void update_specialization_Checked(object sender, RoutedEventArgs e)
        {
            this.list_comboBox.ItemsSource = null;
            this.list_comboBox.ItemsSource = bl.getSpecializationsList();
            DataContext = null;
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
