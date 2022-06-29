using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ST10083941_PROG6221_Task_3.Classes
{
    class HomeLoan : Expenses
    {
        //Properties of the HomeLoan class for calculation.
        public double PropertyPrice { get; private set; }
        public double TotalDeposit { get; private set; }
        public double InterestRate { get; private set; }
        public int MonthsToRepay { get; private set; }
        public double Income { get; set; }

        public HomeLoan(string name) : base(name)
        {

        }

        //Sets the properties using values from frmMain
        public void SetProperties(double propertyPrice, double totalDeposit, double interestRate, int monthsToRepay, double income)
        {
            PropertyPrice = propertyPrice;
            TotalDeposit = totalDeposit;
            InterestRate = interestRate;
            MonthsToRepay = monthsToRepay;
            Income = income;
        }

        //Delegate to keep track of the monthy home loan cost.
        public delegate void MonthlyPayment(double monthlyPayment);

        //Calculates the monthly cost of the home loan.
        public double CalculateCost(MonthlyPayment monthlyPayment)
        {
            double loanAmount = PropertyPrice - TotalDeposit;
            double interest = InterestRate / 100;
            double monthlyCost = loanAmount * (interest / 12) * Math.Pow(1 + (interest / 12), MonthsToRepay) / (Math.Pow(1 + (interest / 12), MonthsToRepay) - 1);
            monthlyPayment(Math.Round(monthlyCost, 2));
            return Math.Round(monthlyCost, 2);
        }
    }
}
