using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ST10083941_PROG6221_Task_3.Classes
{
    class Savings : Expenses
    {
        public double SavingsAmount { get; set; }
        public double InterestRate { get; set; }
        public DateTime DateToSaveBy { get; set; }
        public string SavingsReason { get; set; }
        public Savings(string name) : base(name)
        {

        }

        public void SetProperties(double savingsAmount, double interestRate, DateTime dateToSaveBy, string savingsReason)
        {
            SavingsAmount = savingsAmount;
            InterestRate = interestRate;
            DateToSaveBy = dateToSaveBy;
            SavingsReason = savingsReason;
        }

        public double CalculateSavings()
        {
            //Months is calculated disregarding date. For example 30/06/2022 and 01/07/2022 is considered a 1 month difference. This is to simplify dates.
            DateTime currentDate = DateTime.Today;
            int numberOfMonths = ((DateToSaveBy.Year - currentDate.Year) * 12) + DateToSaveBy.Month - currentDate.Month;
            double monthlySavingsBeforeInterest = SavingsAmount / numberOfMonths;
            double monthlySavingsAfterInterest = monthlySavingsBeforeInterest - (monthlySavingsBeforeInterest * InterestRate);
            MessageBox.Show(Convert.ToString(monthlySavingsBeforeInterest));
            return monthlySavingsAfterInterest;
        }


    }
}
