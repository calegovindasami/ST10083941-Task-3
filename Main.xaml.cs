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
using LiveCharts;
using LiveCharts.Wpf;
using ST10083941_PROG6221_Task_3.Classes;
using System.ComponentModel;

namespace ST10083941_PROG6221_Task_3
{
    /// <summary>
    /// Interaction logic for Main.xaml
    /// </summary>
    public partial class Main : MetroWindow
    {
        //TEMP INCOME
        public double Balance { get; set; }
        public double ExpenseTotal { get; set; }
        public double Income { get; set; }



        //List of each expense object
        List<Expenses> expense = new List<Expenses>();
        //Formatter for the Y-Axis of the line chart.
        public Func<double, string> YFormatter { get; set; }

        //Formatter for the X-Axis of the line chart.
        public string[] Years { get; set; }
        //Collection to bind to the line graph.
        public SeriesCollection SeriesCollection { get; set; }

        public Main()
        {
            InitializeComponent();
            cmbExpenses.SelectedIndex = 0;
            Years = GenerateYears();

            //Sets datepicker for savings to the first day of the following month. Prevents user from trying to save within the same month.
            DateTime firstDayNextMonth = DateTime.Now.AddDays(-DateTime.Now.Day + 1).AddMonths(1);
            dateSavings.DisplayDateStart = firstDayNextMonth;

            tbDisplayIncome.DataContext = this;

            //SeriesCollection to store Income, Expenses and Account Balance to display on a Line Series.
            SeriesCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Income",
                    Values = new ChartValues<double> {}
                },

                new LineSeries
                {
                    Title = "Expenses",
                    Values = new ChartValues<double> {}
                },

                new LineSeries
                {
                    Title = "Balance",
                    Values = new ChartValues<double> {}
                }
            };

            //Formats the Y-Axis to currency.
            YFormatter = value => value.ToString("C2");
            lvExpenses.ItemsSource = expense;
            DataContext = this;

            tiExpenses.IsEnabled = false;
            tiViewExpenses.IsEnabled = false;
        }

        //Updates the Line series for expense when an expense is added.
        public void UpdateLineSeries()
        {
            SeriesCollection[1].Values.Clear();
            double yearlyExpenseTotal = ExpenseTotal * 12;

            SeriesCollection[2].Values.Clear();
            double yearlyBalanceTotal = Balance * 12;

            for (int i = 0; i < 6; i++)
            {
                SeriesCollection[1].Values.Add(yearlyExpenseTotal * i);
                SeriesCollection[2].Values.Add(yearlyBalanceTotal * i);
            }

        }

        //Calculates balance and binds it to the corresponding textblock.
        public void CalculateBalance()
        {
            Balance = Income - expense.Sum(exp => exp.Cost);
            Binding bind = new Binding();
            bind.Source = Balance;
            bind.StringFormat = "{0:C2}";
            tbDisplayIncome.SetBinding(TextBlock.TextProperty, bind);
        }

        //Delegate 
        public delegate void ExpenseExceeds(double total);

        public void ExceedsPercentage(double total)
        {
            if (total > (0.75 * Income))
            {
                dlogAlert.IsOpen = true;
            }
        }

        //Calculates expense total and binds it to the textblock.
        public void CalculateExpenseTotal(ExpenseExceeds ExceedsPercentage)
        {
            ExpenseTotal = expense.Sum(exp => exp.Cost);
            ExceedsPercentage(ExpenseTotal);
            Binding bind = new Binding();
            bind.Source = ExpenseTotal;
            bind.StringFormat = "{0:C2}";
            tbDisplayExpenseTotal.SetBinding(TextBlock.TextProperty, bind);
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

                case 1:
                    CollapseExpensePanels();
                    gridHousing.Visibility = Visibility.Visible;
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

        public string[] GenerateYears()
        {
            DateTime currentYear = DateTime.Today;
            int[] Years = new[] { currentYear.Year, currentYear.AddYears(1).Year, currentYear.AddYears(2).Year, currentYear.AddYears(3).Year, currentYear.AddYears(4).Year, currentYear.AddYears(5).Year, currentYear.AddYears(6).Year };
            string[] strYears = new string[6];
            for (int i = 0; i < 6; i++)
            {
                strYears[i] = Convert.ToString(Years[i]);
            }
            return strYears;
        }

        //Collapses the expense panels in the expense tab.
        public void CollapseExpensePanels()
        {
            pnlMonthlyExpenses.Visibility = Visibility.Collapsed;
            pnlVehicle.Visibility = Visibility.Collapsed;
            gridSavings.Visibility = Visibility.Collapsed;
            gridHousing.Visibility = Visibility.Collapsed;
        }

        //Submit button for the vehicle expense which validates the fields first then adds it if validation passes.
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

            List<TextBlock> lstVehicleSubmit = new List<TextBlock> { tbSubmitVehicleDeposit, tbSubmitVehicleInsurancePremium, tbSubmitVehicleInterestRate, tbSubmitVeiclePrice };
            bool bValidate = true;
            
            for (int i = 0; i < lstVehicleNUD.Count; i++)
            {

                if (lstVehicleNUD[i].Value == null)
                {
                    ValidateNumericUpDown(lstVehicleNUD[i], lstVehicleTB[i]);
                    bValidate = false;
                }
            }

            if (txbModel.Text.Length <= 0)
            {
                MaterialDesignThemes.Wpf.HintAssist.SetHelperText(txbModel, "You cannot leave the Model of the car empty!");
                tbModelHeader.Foreground = Brushes.Red;
                bValidate = false;
            }

            if (bValidate == true)
            {
                MessageBox.Show("Expense added.");

                //Creates Vehicle object and assigns the values required to calculate monthly repayment. Then adds it to the expense List and refreshes the List View.
                Vehicle vehicle = new Vehicle("Vehicle");
                vehicle.SetProperties( txbModel.Text, (double) nudVehiclePrice.Value, (double)nudVehicleDeposit.Value, (double)nudVehicleInterestRate.Value, (double)nudVehicleInsurancePremium.Value );
                double monthlyCost = vehicle.CalculateRepayment();
                vehicle.SetCost(Math.Round(monthlyCost, 2));
                MessageBox.Show(Convert.ToString(vehicle.Cost));
                expense.Add(vehicle);
                DescendingOrder();
                CalculateBalance();
                CalculateExpenseTotal(ExceedsPercentage);
                UpdateLineSeries();

                //Displays expenses and changes visiblity of controls.
                SubmitExpenseDetails(lstVehicleNUD, lstVehicleSubmit);
                btnSubmitVehicle.Visibility = Visibility.Collapsed;
                tbSubmitVehicleMake.Text = txbModel.Text;
                tbSubmitVehicleMake.Visibility = Visibility.Visible;
                tbSubmitVehicleMake.Foreground = Brushes.Red;
                txbModel.Visibility = Visibility.Collapsed;
            }



        }
         

        //Displays expenses within textblock once the user submits the corresponding expense.
        public void SubmitExpenseDetails(List<NumericUpDown> nud, List<TextBlock> tb)
        {
            for (int i = 0; i < nud.Count; i++)
            {
                if (nud[i].StringFormat == "P")
                {
                    tb[i].Text = String.Format("{0:P}", nud[i].Value);
                }
                else if (nud[i].StringFormat == "C2")
                {
                    tb[i].Text = String.Format("{0:C2}", nud[i].Value);
                }
                else if (nud[i].StringFormat == "G")
                {
                    tb[i].Text = Convert.ToString(nud[i].Value);
                }
                tb[i].Visibility = Visibility.Visible;
                tb[i].Foreground = Brushes.Red;
                nud[i].Visibility = Visibility.Collapsed;
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

        //--------------- Clears helper text if Numeric Up Down value is changed. ------------------------------------
        public void ValidateValueChangedNUD(NumericUpDown nud, TextBlock tb)
        {
            MaterialDesignThemes.Wpf.HintAssist.SetHelperText(nud, null);
            tb.Foreground = nud.Value > 0 ? Brushes.Green : Brushes.Red;
        }

        private void txbModel_TextChanged(object sender, TextChangedEventArgs e)
        {
            MaterialDesignThemes.Wpf.HintAssist.SetHelperText(txbModel, null);
            tbModelHeader.Foreground = tbModelHeader.Text != null ? Brushes.Green : Brushes.Red;
        }

        private void nudVehiclePrice_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
        {
            ValidateValueChangedNUD(nudVehiclePrice, tbVehiclePrice);
            nudVehicleDeposit.Maximum = (double)nudVehiclePrice.Value;
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
            List<TextBlock> lstSubmitMonthlyExpenses = new List<TextBlock> { tbSubmitGroceries, tbSubmitUtilities, tbSubmitTravelCosts, tbSubmitPhoneExpenses, tbSubmitOtherExpenses };
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
                //Creates objects and assigns corresponding value to each object.
                Groceries groceries = new Groceries("Groceries");
                groceries.SetCost((double)nudGroceries.Value);

                Utility utility = new Utility("Utilites");
                utility.SetCost((double)nudUtilities.Value);

                Travel travel = new Travel("Travel");
                travel.SetCost((double)nudTravel.Value);

                PhoneBill phoneBill = new PhoneBill("Phone Bill");
                phoneBill.SetCost((double)nudPhoneExpenses.Value);

                Other other = new Other("Other");
                other.SetCost((double)nudOther.Value);

                //Adds each expense to the list if expenses.
                expense.Add(groceries);
                expense.Add(utility);
                expense.Add(travel);
                expense.Add(phoneBill);
                expense.Add(other);
                DescendingOrder();
                CalculateBalance();
                CalculateExpenseTotal(ExceedsPercentage);
                UpdateLineSeries();

                //Changes control properties to display expenses.
                btnMonthlyExpenses.Visibility = Visibility.Collapsed;
                SubmitExpenseDetails(lstMonthlyExpensesNUD, lstSubmitMonthlyExpenses);
            }
        }

        //------------- Value change events for the Monthly Expense controls to allow validation on value change. ---------------
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
            List<TextBlock> lstSubmitSavings = new List<TextBlock> { tbSubmitSavingsAmount, tbSubmitSavingsInterestRate };
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
                MaterialDesignThemes.Wpf.HintAssist.SetHelperText(txbSavingsReason, "You cannot leave the savings reason empty!");
                tbSavingsReason.Foreground = Brushes.Red;
            }

            DateTime selectedDate = dateSavings.SelectedDate.Value;
            DateTime currentDate = DateTime.Now;
            if (selectedDate < currentDate)
            {
                bValid = false;
                MaterialDesignThemes.Wpf.HintAssist.SetHelperText(dateSavings, "You cannot select a previous/current date.");
                tbSavingsDateToSave.Foreground = Brushes.Red;
            }

            if (bValid == true)
            {
                //Creates object and adds it to Expense list after calculations are done.
                Savings saving = new Savings("Savings");
                saving.SetProperties((double)nudSavingsAmount.Value, (double)nudSavingsInterestRate.Value, (DateTime)dateSavings.SelectedDate, txbSavingsReason.Text);
                saving.SetCost(Math.Round(saving.CalculateSavings(), 2));
                expense.Add(saving);
                DescendingOrder();
                CalculateBalance();
                CalculateExpenseTotal(ExceedsPercentage);
                UpdateLineSeries();

                //Changes display options to display expenses.
                SubmitExpenseDetails(lstSavingsNUD, lstSubmitSavings);
                btnSavings.Visibility = Visibility.Collapsed;
                tbSubmitSavingsReason.Text = txbSavingsReason.Text;
                txbSavingsReason.Visibility = Visibility.Collapsed;
                tbSubmitSavingsReason.Visibility = Visibility.Visible;
                tbSubmitSavingsDate.Text = dateSavings.Text;
                tbSubmitSavingsReason.Foreground = Brushes.Red;
                tbSubmitSavingsDate.Foreground = Brushes.Red;
                dateSavings.Visibility = Visibility.Collapsed;
                tbSubmitSavingsDate.Visibility = Visibility.Visible;
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
            tbSavingsDateToSave.Foreground = dateSavings.SelectedDate < DateTime.Now ? Brushes.Red : Brushes.Green;
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
                //Creates rent object and adds it to the expense list.
                Rent rent = new Rent("Rent");
                rent.SetCost((double)nudRent.Value);
                expense.Add(rent);
                DescendingOrder();
                CalculateBalance();
                CalculateExpenseTotal(ExceedsPercentage);
                UpdateLineSeries();

                //Configures control visbility to show the rent expense.
                tbSubmitRent.Text = String.Format("{0:C2}", nudRent.Value);
                nudRent.Visibility = Visibility.Collapsed;
                tbSubmitRent.Visibility = Visibility.Visible;
                tgRent.IsEnabled = false;
                btnRent.Visibility = Visibility.Collapsed;
                tgLoan.IsEnabled = false;
            }
        }

        //Update the colors of the textbox and helper text for renting fields.
        private void nudRent_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
        {
            ValidateValueChangedNUD(nudRent, tbRenting);
        }

        //Submit button for home loan which adds the expense to the expense list array and validates input.
        private void btnHomeLoan_Click(object sender, RoutedEventArgs e)
        {
            List<NumericUpDown> lstHomeLoanNUD = new List<NumericUpDown> { nudPropertyPrice, nudTotalDeposit, nudPropertyInterestRate, nudPropertyMonths};
            List<TextBlock> lstHomeLoanTB = new List<TextBlock> { tbPropertyPrice, tbTotalDeposit, tbPropertyInterestRate, tbPropertyMonths };
            List<TextBlock> lstSubmitHomeLoan = new List<TextBlock> { tbSubmitPropertyPrice, tbSubmitTotalDeposit, tbSubmitPropertyInterestRate, tbSubmitPropertyMonths};
            bool bValid = true;
            for (int i = 0; i < lstHomeLoanNUD.Count; i++)
            {
                if (lstHomeLoanNUD[i].Value == null)
                {
                    ValidateNumericUpDown(lstHomeLoanNUD[i], lstHomeLoanTB[i]);
                    bValid = false;
                }
            }

            if (bValid == true)
            {
                //Creates home loan object, calculates monthly loan payment and adds it to the expense list.
                HomeLoan homeLoan = new HomeLoan("Home Loan");
                homeLoan.SetProperties((double)nudPropertyPrice.Value, (double)nudTotalDeposit.Value, (double)nudPropertyInterestRate.Value ,(int)nudPropertyMonths.Value, Income);
                double monthlyLoanCost = homeLoan.CalculateCost(HomeLoanWarning);
                homeLoan.SetCost(Math.Round(monthlyLoanCost, 2));
                expense.Add(homeLoan);
                DescendingOrder();
                CalculateBalance();
                CalculateExpenseTotal(ExceedsPercentage);
                UpdateLineSeries();

                //Changes components visibility to display the expenses.
                SubmitExpenseDetails(lstHomeLoanNUD, lstSubmitHomeLoan);
                tgRent.IsEnabled = false;
                btnHomeLoan.Visibility = Visibility.Collapsed;
                tgLoan.IsEnabled = false;
            }
        }

        public void HomeLoanWarning(double loan)
        {
            if (loan > (Income * 0.75))
            {
                tbHomeLoanWarning.Visibility = Visibility.Visible;
            }
        }

        //------------------ Helper text and color configured based on the user input. ---------------
        private void nudPropertyPrice_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
        {
            ValidateValueChangedNUD(nudPropertyPrice, tbPropertyPrice);
            if (nudPropertyPrice.Value != null)
            {
                nudTotalDeposit.Maximum = (double)nudPropertyPrice.Value;
            }
        }

        private void nudTotalDeposit_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
        {
            ValidateValueChangedNUD(nudTotalDeposit, tbTotalDeposit);
        }

        private void nudPropertyInterestRate_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
        {
            ValidateValueChangedNUD(nudPropertyInterestRate, tbPropertyInterestRate);
        }

        private void nudPropertyMonths_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
        {
            ValidateValueChangedNUD(nudPropertyMonths, tbPropertyMonths);
        }

        private void btnAscending_Click(object sender, RoutedEventArgs e)
        {
            AscendingOrder();
        }

        //Sorts the list in ascending order and assigns it to the ListView item source.
        public void AscendingOrder()
        {
            List<Expenses> AscendingExpenses = expense.OrderBy(bill => bill.Cost).ToList();
            lvExpenses.ItemsSource = AscendingExpenses;
            lvExpenses.Items.Refresh();
        }

        //Sorts the list in descending order and assigns it to the ListView item source.
        public void DescendingOrder()
        {
            List<Expenses> DescendingExpenses = expense.OrderByDescending(bill => bill.Cost).ToList();
            lvExpenses.ItemsSource = DescendingExpenses;
            lvExpenses.Items.Refresh();
        }

        private void btnDescending_Click(object sender, RoutedEventArgs e)
        {
            DescendingOrder();
        }


        //Button to submit tax and the user initial details when application opens.
        private void btnSubmitPerson_Click(object sender, RoutedEventArgs e)
        {
            List<NumericUpDown> personNUD = new List<NumericUpDown> { nudPersonIncome, nudPersonTax };
            List<TextBlock> personNameTB = new List<TextBlock> { tbPersonIncome, tbPersonTax };
            List<TextBlock> submitPersonTB = new List<TextBlock> { tbSubmitPersonIncome, tbSubmitPersonTax };
            bool bValid = true;

            for (int i = 0; i < personNUD.Count; i++)
            {
                if (personNUD[i].Value == null)
                {
                    ValidateNumericUpDown(personNUD[i], personNameTB[i]);
                    bValid = false;
                }
            }

            if (txbPersonName.Text.Length <= 0)
            {
                bValid = false;
                MaterialDesignThemes.Wpf.HintAssist.SetHelperText(txbPersonName, "You cannot leave your name empty!");
                tbPersonName.Foreground = Brushes.Red;
            }

            if (bValid == true)
            {
                //Creates tax object and adds it to the expense list.
                Tax tax = new Tax("Tax");
                tax.SetCost(Math.Round((double)nudPersonTax.Value));
                expense.Add(tax);

                Income = (double)nudPersonIncome.Value;

                //Enables other tabs once the income and tax is added.
                tiExpenses.IsEnabled = true;
                tiViewExpenses.IsEnabled = true;

                //Configures display to show the persons details.
                SubmitExpenseDetails(personNUD, submitPersonTB);
                tbPersonName.Foreground = Brushes.Black;
                tbSubmitPersonName.Text = txbPersonName.Text;
                tbSubmitPersonName.Foreground = Brushes.Black;
                tbSubmitPersonIncome.Foreground = Brushes.Green;
                tbPersonTax.Foreground = Brushes.Red;
                txbPersonName.Visibility = Visibility.Collapsed;
                tbSubmitPersonName.Visibility = Visibility.Visible;
                btnSubmitPerson.Visibility = Visibility.Collapsed;
                DescendingOrder();
                CalculateBalance();
                CalculateExpenseTotal(ExceedsPercentage);
                UpdateLineSeries();

                //Generates line series for the income.
                SeriesCollection[0].Values.Clear();
                double yearlyIncome = Income * 12;
                for (int i = 0; i < 6; i++)
                {
                    SeriesCollection[0].Values.Add(yearlyIncome * i);
                }
            }
        }

        //---------------- Removes validation errors that display on update ------------------------- 
        private void txbPersonName_TextChanged(object sender, TextChangedEventArgs e)
        {
            MaterialDesignThemes.Wpf.HintAssist.SetHelperText(txbPersonName, null);
            tbPersonName.Foreground = tbPersonName.Text != null ? Brushes.Green : Brushes.Red;
        }

        private void nudPersonIncome_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
        {
            ValidateValueChangedNUD(nudPersonIncome, tbPersonIncome);
            if (nudPersonIncome.Value != null)
            {
                nudPersonTax.Maximum = (double)nudPersonIncome.Value;
            }
        }

        private void nudPersonTax_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
        {
            ValidateValueChangedNUD(nudPersonTax, tbPersonTax);
        }


        //Closes the dialog that displays an error when user exceeds 75% of their income.
        private void btnCloseAlert_Click(object sender, RoutedEventArgs e)
        {
            dlogAlert.IsOpen = false;
        }
    }
}
