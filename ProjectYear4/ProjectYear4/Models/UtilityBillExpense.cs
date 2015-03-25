using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectYear4.Models
{
    public class UtilityBillExpense
    {
        public int UtilityBillExpenseId { get; set; }
        
        public double Electricity { get; set; }
        
        public double Gas { get; set; }
        
        public double RefuseCollection { get; set; }
        
        public double IrishWater { get; set; }

        // ADDED AFTER DB SET UP - RE-RUN MIGRATIONS
        //public double TVLicenseAmount { get; set; }
        //public double PhoneBillAmount { get; set; }
        //public double BroadbandAmount { get; set; }
        //public double UtilityBillExpenseAmout { get; set; }

        public int BudgetId { get; set; }
        public virtual Budget Budget { get; set; }
    }
}