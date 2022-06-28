using MahApps.Metro.Controls;
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
using System.Windows.Shapes;
using MaterialDesignThemes;
using MaterialDesignColors;

namespace ST10083941_PROG6221_Task_3
{
    /// <summary>
    /// Interaction logic for Main.xaml
    /// </summary>
    public partial class Main : MetroWindow
    {
        public Main()
        {
            InitializeComponent();
        }

        //Changes the form based on what the user selects from the combo box in the expense tab.
        private void cmbExpenses_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int selectedIndex = cmbExpenses.SelectedIndex;
            switch (selectedIndex)
            {
                case 0:
                    CollapseExpensePanels();
                    pnlMonthlyExpenses.Visibility = Visibility.Visible;
                    break;
                case 2:
                    CollapseExpensePanels();
                    pnlVehicle.Visibility = Visibility.Visible;
                    break;
                default:
                    break;
            }
        }

        //Collapses the expense panels in the expense tab.
        public void CollapseExpensePanels()
        {
            pnlMonthlyExpenses.Visibility = Visibility.Collapsed;
            pnlVehicle.Visibility = Visibility.Collapsed;
        }

        //Submit button for the vehicle expense.
        private void btnSubmitVehicle_Click(object sender, RoutedEventArgs e)
        {
            List<NumericUpDown> VehicleNud = new List<NumericUpDown>();
            VehicleNud.Add(nudVehicleDeposit);
            VehicleNud.Add(nudVehicleInsurancePremium);
            VehicleNud.Add(nudVehicleInterestRate);
            VehicleNud.Add(nudVehiclePrice);

            List<TextBlock> VehicleTb = new List<TextBlock>();
            VehicleTb.Add(tbVehicleDeposit);
            VehicleTb.Add(tbVehicleInsurancePremium);
            VehicleTb.Add(tbVehicleInterestRate);
            VehicleTb.Add(tbVehiclePrice);
            bool bValidate = true;
            
            for (int i = 0; i < VehicleNud.Count; i++)
            {

                if (VehicleNud[i].Value == null)
                {
                    ValidateNumericUpDown(VehicleNud[i], VehicleTb[i]);
                    bValidate = false;
                }
            }

            if (bValidate == true)
            {
                MessageBox.Show("Expense added.");
                ClearNUD(VehicleNud, VehicleTb);
            }


        }


        //Clears the NumericUpDown once the expense is correctly submitted.
        public void ClearNUD(List<NumericUpDown> nud, List<TextBlock> tb)
        {
            for (int i = 0; i < nud.Count; i++)
            {
                nud[i].Value = null;
                tb[i].Foreground = Brushes.Black;
            }
        }

        //Checks Numeric Up Down to see if it's empty. If it is a helper message is then displayed telling the user they need to add an input to it.
        public void ValidateNumericUpDown(NumericUpDown nud, TextBlock tb)
        {

            MaterialDesignThemes.Wpf.HintAssist.SetHelperText(nud, "You cannot leave the input above empty!");
            tb.Foreground = Brushes.Red;

        }

        //Clears helper text if Numeric Up Down value is changed.
        public void ValidateValueChangedNUD(NumericUpDown nud, TextBlock tb)
        {
            MaterialDesignThemes.Wpf.HintAssist.SetHelperText(nud, null);
            tb.Foreground = nud.Value > 0 ? Brushes.Green : Brushes.Red;
        }


        private void nudVehiclePrice_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
        {
            ValidateValueChangedNUD(nudVehiclePrice, tbVehiclePrice);
        }

        private void nudVehicleDeposit_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
        {
            ValidateValueChangedNUD(nudVehicleDeposit, tbVehicleDeposit);
        }

        private void nudVehicleInterestRate_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
        {
            ValidateValueChangedNUD(nudVehicleInterestRate, tbVehicleInterestRate);
        }

        private void nudVehicleInsurancePremium_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
        {
            ValidateValueChangedNUD(nudVehicleInsurancePremium, tbVehicleInsurancePremium);
        }

        //Submit button for monthly expenses which validate the input before accepting it.
        private void btnMonthlyExpenses_Click(object sender, RoutedEventArgs e)
        {
            List<NumericUpDown> MonthlyExpensesNUD = new List<NumericUpDown> { nudGroceries, nudUtilities, nudTravel, nudPhoneExpenses, nudOther };
            List<TextBlock> MonthlyExpensesTb = new List<TextBlock> { tbGroceries, tbUtilities, tbTravelCosts, tbPhoneExpenses, tbOtherExpenses };

            bool bValidate = true;

            for (int i = 0; i < MonthlyExpensesNUD.Count; i++)
            {
                if (MonthlyExpensesNUD[i].Value == null)
                {
                    ValidateNumericUpDown(MonthlyExpensesNUD[i], MonthlyExpensesTb[i]);
                    bValidate = false;
                }
            }

            if (bValidate == true)
            {
                MessageBox.Show("Expense added.");
                ClearNUD(MonthlyExpensesNUD, MonthlyExpensesTb);
            }
        }

        //Value change events for the Monthly Expense controls to allow validation on value change.
        private void nudGroceries_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
        {
            ValidateValueChangedNUD(nudGroceries, tbGroceries);
        }

        private void nudUtilities_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
        {
            ValidateValueChangedNUD(nudUtilities, tbUtilities);
        }

        private void nudTravel_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
        {
            ValidateValueChangedNUD(nudTravel, tbTravelCosts);
        }

        private void nudPhoneExpenses_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
        {
            ValidateValueChangedNUD(nudPhoneExpenses, tbPhoneExpenses);
        }

        private void nudOther_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
        {
            ValidateValueChangedNUD(nudOther, tbOtherExpenses);
        }
    }
}
