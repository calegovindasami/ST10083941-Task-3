using MahApps.Metro.Controls;
using MaterialDesignThemes.Wpf;
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
using LiveCharts;
using LiveCharts.Wpf;
using System.Windows.Shapes;

namespace ST10083941_PROG6221_Task_3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        //CREATE FUNCTIONS TO ADD DIALOG OPEN/CLOSE
        //CREATE FUNCTIONS TO CLEAR AND RESET DIALOG CONTENT
        //RENAME ALL COMPONENTS CORRECTLY
        

        public class Expense
        {

            public Expense(string name, double cost)
            {
                Name = name;
                Cost = cost;
            }

            public string Name { get; set; }
            public double Cost { get; set; }

        }

        public class PhoneBill : Expense
        {
            public PhoneBill(string name, double cost) : base(name, cost)
            {

            }
        }

        List<Expense> expenses;
        public MainWindow()
        {
            InitializeComponent();


        }

        private void btnHousing_Click(object sender, RoutedEventArgs e)
        {
            dlogHome.ShowDialog(dlogHome.DialogContent);
        }

        private void tgRent_Click(object sender, RoutedEventArgs e)
        {
            if (tgRent.IsChecked == true)
            {
                tgLoan.IsEnabled = false;
                pnlRenting.Visibility = Visibility.Visible;
                pnlHousing.Height = 300;
            }
            else
            {
                tgLoan.IsEnabled = true;
                pnlRenting.Visibility = Visibility.Collapsed;
                pnlHousing.Height = 150;
            }
        }

        private void tgLoan_Click(object sender, RoutedEventArgs e)
        {
            if (tgLoan.IsChecked == true)
            {
                tgRent.IsEnabled = false;
                pnlLoan.Visibility = Visibility.Visible;
                pnlHousing.Height = 480;
            }
            else
            {
                tgRent.IsEnabled = true;
                pnlLoan.Visibility = Visibility.Collapsed;
                pnlHousing.Height = 150;
            }
        }

        private void btnRent_Click(object sender, RoutedEventArgs e)
        {
            expenses.Add(new PhoneBill("Rent", (double)nudRent.Value));
            lvExpenses.Items.Refresh();
            pcExpenses.Series.Add(new PieSeries { Title = "Rent", StrokeThickness = 1, Values = new ChartValues<double> { (double)nudRent.Value} });
            ResetHousing();
        }

        public void ResetHousing()
        {
            tgRent.IsChecked = false;
            tgLoan.IsChecked = false;
            tgRent.IsEnabled = true;
            tgLoan.IsEnabled = true;
            pnlRenting.Visibility = Visibility.Collapsed;
            pnlLoan.Visibility = Visibility.Collapsed;
            pnlHousing.Height = 150;
            dlogHome.IsOpen = false;
        }

        private void btnLoan_Click(object sender, RoutedEventArgs e)
        {
            ResetHousing();
        }


        private void btnVehicle_Click(object sender, RoutedEventArgs e)
        {
            dlogVehicle.ShowDialog(dlogVehicle.DialogContent);
        }

        private void btnSubmitVehicle_Click(object sender, RoutedEventArgs e)
        {
            dlogVehicle.IsOpen = false;
        }

        private void btnSaveMonthlyExpenses_Click(object sender, RoutedEventArgs e)
        {
            dlogMonthlyExpenses.IsOpen = false;
        }

        private void btnMonthly_Click(object sender, RoutedEventArgs e)
        {
            dlogMonthlyExpenses.ShowDialog(dlogMonthlyExpenses.DialogContent);
        }

        private void btnSubmitSavings_Click(object sender, RoutedEventArgs e)
        {
            dlogSaving.IsOpen = false;
        }

        private void btnSaving_Click(object sender, RoutedEventArgs e)
        {
            dlogSaving.ShowDialog(dlogSaving.DialogContent);
        }

        private void btnExitRent_Click(object sender, RoutedEventArgs e)
        {

        }

    }
}
