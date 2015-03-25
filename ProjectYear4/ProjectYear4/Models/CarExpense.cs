// using ProjectYear4.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjectYear4.Models
{
    public enum FuelType { Petrol, Diesel };
    public enum NctTest { [Display(Name = "NCT Full")] Full, [Display(Name = "NCT Retest")] Retest };
    public class CarExpense
    {
        public int CarExpenseId { get; set; }

        public double CarTax { get; set; }
        
        public double CarInsurance { get; set; }
        
        public double Maintenance { get; set; }
        
        public FuelType FuelType { get; set; }

        // ADDED AFTER DB SET UP - RE-RUN MIGRATIONS
        // public double FuelAmount { get; set; }
        public NctTest NctTest { get; set; }

        // ADDED AFTER DB SET UP - RE-RUN MIGRATIONS
        // public double NctAmount { get; set; }
        public int BudgetId { get; set; }
        
        public virtual Budget Budget { get; set; }
    }
}