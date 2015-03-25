using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectYear4.Models
{
    public class PersonalExpense
    {
        public int PersonalExpenseId { get; set; }
        public double SocialAmount { get; set; }
        public double GymMembershipAmount { get; set; }
        public double SportsFeeAmount { get; set; }
        public double HolidayAmount { get; set; }
        public double SavingsAmount { get; set; }
        public double LoanRepaymentAmount { get; set; }
        public double PersonalExpenseOther { get; set; }

        public int BudgetId { get; set; }
        public virtual Budget Budget { get; set; }
    }
}