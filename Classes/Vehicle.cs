using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ST10083941_PROG6221_Task_3.Classes
{
    class Vehicle : Expenses
    {
        //Properties of the vehicle.
        public string ModelMake { get; set; }
        public double PurchasePrice { get; set; }
        public double TotalDeposit { get; set; }
        public double InterestRate { get; set; }
        public double InsurancePremium { get; set; }

        public Vehicle(string name) : base(name)
        {

        }

        //Sets the vehicles properties using the data from frmMain.
        public void SetProperties(string modelMake, double purchasePrice, double totalDeposit, double interestRate, double insurancePremium)
        {
            ModelMake = modelMake;
            PurchasePrice = purchasePrice;
            TotalDeposit = totalDeposit;
            InterestRate = interestRate;
            InsurancePremium = insurancePremium;
        }

        //Calculates the monthly repayment for a vehicle loan.
        public double CalculateRepayment()
        {
            double loanAmount = PurchasePrice - TotalDeposit;
            double totalAmount = loanAmount + (loanAmount * InterestRate);
            double monthlyCost = totalAmount / 60;
            double totalExpenses = monthlyCost + InsurancePremium;
            return totalExpenses;
        }


    }
}
