using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectYear4.Models
{
    public enum RentType { Rent, Mortgage };
    public class HouseholdExpense
    {
        public int HouseholdExpenseId { get; set; }
        
        public RentType RentType { get; set; }
        
        public double RentAmount { get; set; }
        
        public double GroceryAmount { get; set; }
        
        public double ClothingAmount { get; set; }
        
        public double EducationFeesAmount { get; set; }
        
        public double SchoolBooksAmount { get; set; }
        
        public double MedicalBillAmount { get; set; }
        
        public double HouseholdOtherAmount { get; set; }

        public int BudgetId { get; set; }
        public virtual Budget Budget { get; set; }
    }
}