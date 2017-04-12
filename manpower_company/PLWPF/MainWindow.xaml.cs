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
using BE;
using BL;
using System.Threading;
using System.Windows.Threading;
using System.ComponentModel;

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IBL bl;
        
        public int sumEmployeesProperty
        {
            get { return (int)GetValue(sumEmployeesPropertyProperty); }
            set { SetValue(sumEmployeesPropertyProperty, value); }
        }

        // Using a DependencyProperty as the backing store for sumEmplyeesProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty sumEmployeesPropertyProperty =
            DependencyProperty.Register("sumEmployeesProperty", typeof(int), typeof(MainWindow), new PropertyMetadata(0));

        public int sumContractsProperty
        {
            get { return (int)GetValue(sumContractsPropertyProperty); }
            set { SetValue(sumContractsPropertyProperty, value); }
        }

        // Using a DependencyProperty as the backing store for sumContractsProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty sumContractsPropertyProperty =
            DependencyProperty.Register("sumContractsProperty", typeof(int), typeof(MainWindow), new PropertyMetadata(0));
        
        public int sumEmployersProperty
        {
            get { return (int)GetValue(sumEmployersPropertyProperty); }
            set { SetValue(sumEmployersPropertyProperty, value); }
        }

        // Using a DependencyProperty as the backing store for sumEmployersProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty sumEmployersPropertyProperty =
            DependencyProperty.Register("sumEmployersProperty", typeof(int), typeof(MainWindow), new PropertyMetadata(0));

        private readonly BackgroundWorker worker = new BackgroundWorker();
        startWin win;
        public MainWindow()
        {
            //this.Visibility = Visibility.Hidden;
            worker.DoWork += Worker_DoWork; worker.RunWorkerCompleted += Worker_RunWorkerCompleted;

            win = new startWin();
           
        }

        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            win.Close();
            this.Closed += new EventHandler(MainWindow_Closed);

            bl = FactoryBl.GetInstance();
            InitializeComponent();

            sumEmployeesProperty = bl.getSumOfEmployeesByCondition(null);
            sumEmployersProperty = bl.getEmployersList().ToList().Count;
            sumContractsProperty = bl.getSumOfContractsByCondition(null);

            sumEmloyees.DataContext = sumContracts.DataContext = sumEmloyers.DataContext = this;

            bl.addedElement += Bl_addedElement;
            this.Visibility = Visibility.Visible;
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            Thread.Sleep(4000);
        }

        private void Bl_addedElement(object sender, BL_EventArgs e)
        {
            switch (e.elementName)
            {
                case "Employee":
                    sumEmployeesProperty = e.sumElements;
                    break;
                case "Employer":
                    sumEmployersProperty = e.sumElements;
                    break;
                case "Contract":
                    sumContractsProperty = e.sumElements;
                    break;
                default:
                    break;
            }
        }

        private void MainWindow_Closed(object sender, EventArgs e)
        {
            bl.SaveListsToFiles();
            ((MainWindow)sender).Close();
        }

        private void Specialization_Click(object sender, RoutedEventArgs e)
        {
            DetailsPanel.Visibility = Visibility.Collapsed;
            content.Content = new SpecializationWin();
        }
        private void Employee_Click(object sender, RoutedEventArgs e)
        {
            DetailsPanel.Visibility = Visibility.Collapsed;
            content.Content = new EmployeeWin();
        }
        private void Employer_Click(object sender, RoutedEventArgs e)
        {
            DetailsPanel.Visibility = Visibility.Collapsed;
            content.Content = new EmployerWin();
        }
        private void Contract_Click(object sender, RoutedEventArgs e)
        {
            DetailsPanel.Visibility = Visibility.Collapsed;
            content.Content = new ContractWin();
        }

        private void home_Click(object sender, RoutedEventArgs e)
        {
            DetailsPanel.Visibility = Visibility.Visible;
            content.Content = null;
        }

        private void special_functions_Click(object sender, RoutedEventArgs e)
        {
            SpecialFunctionsWin win = new SpecialFunctionsWin();
            win.Show();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
            win.Show();

            worker.RunWorkerAsync();
        }
    }
}
