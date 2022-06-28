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
            ValidateNumericUpDown(VehicleNud, VehicleTb);


        }

        //Checks Numeric Up Down to see if it's empty. If it is a helper message is then displayed telling the user they need to add an input to it.
        public void ValidateNumericUpDown(List<NumericUpDown> nud, List<TextBlock> tb)
        {
            for (int i = 0; i < nud.Count; i++)
            {
                if (nud[i].Value == null)
                {
                    MaterialDesignThemes.Wpf.HintAssist.SetHelperText(nud[i], "You cannot leave the input above empty!");
                    tb[i].Foreground = Brushes.Red;
                }
            }
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
    }
}
