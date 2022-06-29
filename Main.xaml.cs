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
                
                case 3:
                    CollapseExpensePanels();
                    gridSavings.Visibility = Visibility.Visible;
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
            gridSavings.Visibility = Visibility.Collapsed;
        }

        //Submit button for the vehicle expense.
        private void btnSubmitVehicle_Click(object sender, RoutedEventArgs e)
        {
            List<NumericUpDown> lstVehicleNUD = new List<NumericUpDown>();
            lstVehicleNUD.Add(nudVehicleDeposit);
            lstVehicleNUD.Add(nudVehicleInsurancePremium);
            lstVehicleNUD.Add(nudVehicleInterestRate);
            lstVehicleNUD.Add(nudVehiclePrice);

            List<TextBlock> lstVehicleTB = new List<TextBlock>();
            lstVehicleTB.Add(tbVehicleDeposit);
            lstVehicleTB.Add(tbVehicleInsurancePremium);
            lstVehicleTB.Add(tbVehicleInterestRate);
            lstVehicleTB.Add(tbVehiclePrice);
            bool bValidate = true;
            
            for (int i = 0; i < lstVehicleNUD.Count; i++)
            {

                if (lstVehicleNUD[i].Value == null)
                {
                    ValidateNumericUpDown(lstVehicleNUD[i], lstVehicleTB[i]);
                    bValidate = false;
                }
            }

            if (bValidate == true)
            {
                MessageBox.Show("Expense added.");
                ClearNUD(lstVehicleNUD, lstVehicleTB);
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
            List<NumericUpDown> lstMonthlyExpensesNUD = new List<NumericUpDown> { nudGroceries, nudUtilities, nudTravel, nudPhoneExpenses, nudOther };
            List<TextBlock> lstMonthlyExpensesTB = new List<TextBlock> { tbGroceries, tbUtilities, tbTravelCosts, tbPhoneExpenses, tbOtherExpenses };

            bool bValidate = true;

            for (int i = 0; i < lstMonthlyExpensesNUD.Count; i++)
            {
                if (lstMonthlyExpensesNUD[i].Value == null)
                {
                    ValidateNumericUpDown(lstMonthlyExpensesNUD[i], lstMonthlyExpensesTB[i]);
                    bValidate = false;
                }
            }

            if (bValidate == true)
            {
                MessageBox.Show("Expense added.");
                ClearNUD(lstMonthlyExpensesNUD, lstMonthlyExpensesTB);
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

        //Savings submit button. Validates each input field and displays corresponding error.
        private void btnSavings_Click(object sender, RoutedEventArgs e)
        {
            List<NumericUpDown> lstSavingsNUD = new List<NumericUpDown> { nudSavingsAmount, nudSavingsInterestRate };
            List<TextBlock> lstSavingsTB = new List<TextBlock> { tbSavingsAmount, tbSavingsInterestRate };
            bool bValid = true;

            for (int i = 0; i < lstSavingsNUD.Count; i++)
            {
                if (lstSavingsNUD[i].Value == null)
                {
                    ValidateNumericUpDown(lstSavingsNUD[i], lstSavingsTB[i]);
                    bValid = false;
                }
            }

            if (dateSavings.SelectedDate == null)
            {
                bValid = false;
                MaterialDesignThemes.Wpf.HintAssist.SetHelperText(dateSavings, "You cannot leave the date empty!");
                tbSavingsDateToSave.Foreground = Brushes.Red;
            }

            if (txbSavingsReason.Text.Length <= 0)
            {
                bValid = false;
                MaterialDesignThemes.Wpf.HintAssist.SetHelperText(txbSavingsReason, "You cannot leave the reason empty!");
                tbSavingsReason.Foreground = Brushes.Red;
            }

            if (bValid == true)
            {
                MessageBox.Show("Expense added.");
            }
        }

        //Value on change for the Savings tab to either clear or show error regarding input.
        private void nudSavingsAmount_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
        {
            ValidateValueChangedNUD(nudSavingsAmount, tbSavingsAmount);
        }

        private void nudSavingsInterestRate_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
        {
            ValidateValueChangedNUD(nudSavingsInterestRate, tbSavingsInterestRate);
        }

        private void dateSavings_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            MaterialDesignThemes.Wpf.HintAssist.SetHelperText(dateSavings, null);
            tbSavingsDateToSave.Foreground = dateSavings.SelectedDate != null ? Brushes.Green : Brushes.Red; 
        }

        private void txbSavingsReason_TextChanged(object sender, TextChangedEventArgs e)
        {
            MaterialDesignThemes.Wpf.HintAssist.SetHelperText(txbSavingsReason, null);
            tbSavingsReason.Foreground = tbSavingsReason.Text != null ? Brushes.Green : Brushes.Red;
        }


        //Toggle button if user wants to rent.
        private void tgRent_Click(object sender, RoutedEventArgs e)
        {
            if (tgRent.IsChecked == true)
            {
                tgLoan.IsEnabled = false;
                pnlRenting.Visibility = Visibility.Visible;
            }
            else
            {
                tgLoan.IsEnabled = true;
                pnlRenting.Visibility = Visibility.Collapsed;
            }
        }

        //Toggle button if user wants to take out a loan
        private void tgLoan_Click(object sender, RoutedEventArgs e)
        {
            if (tgLoan.IsChecked == true)
            {
                tgRent.IsEnabled = false;
                pnlLoan.Visibility = Visibility.Visible;

            }
            else
            {
                tgRent.IsEnabled = true;
                pnlLoan.Visibility = Visibility.Collapsed;
            }
        }

        //Submit button for renting.
        private void btnRent_Click(object sender, RoutedEventArgs e)
        {
            bool bValid = true;
            if (nudRent.Value == null)
            {
                ValidateNumericUpDown(nudRent, tbRenting);
                bValid = false;
            }

            if (bValid == true)
            {
                MessageBox.Show("Expense added.");
                tgRent.IsEnabled = false;
                tgLoan.IsEnabled = false;
            }
        }

        //Update the colors of the textbox and helper text for renting fields.
        private void nudRent_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
        {
            ValidateValueChangedNUD(nudRent, tbRenting);
        }
    }
}
