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
    public static class Bank
    {
        public static BE.BankAccount bank;
    }

    /// <summary>
    /// Interaction logic for BankAccountWin.xaml
    /// </summary>
    public partial class BankAccountWin :Window
    {
        BL.IBL bl;
        
        public event EventHandler chooseBank;

        public BankAccountWin()
        {
            InitializeComponent();
            bl = BL.FactoryBl.GetInstance();
           

        }


     private void choose_click(object sender, RoutedEventArgs e)
        {
            try
            {
                Bank.bank = (BE.BankAccount)(branchComboBox.SelectedItem);
                Bank.bank.accountNumber = int.Parse(accountNumTextBox.Text);
                if (chooseBank != null)
                    chooseBank(this, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void list_bank_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bankComboBox.ItemsSource = bl.groupingByCitiesAndBranches();
                bankComboBox.DisplayMemberPath = "Key";
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}


