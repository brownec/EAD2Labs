using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ProjectYear4.Models
{
    public class MyDBConnection : DbContext
    {
        public DbSet<Budget> Budget { get; set; }    
        public DbSet<BudgetUser> BudgetUser { get; set; }
        public DbSet<CarExpense> CarExpense { get; set; }
        public DbSet<UtilityBillExpense> UtilityBillExpense { get; set; }

        public System.Data.Entity.DbSet<ProjectYear4.Models.HouseholdExpense> HouseholdExpenses { get; set; }

        public System.Data.Entity.DbSet<ProjectYear4.Models.PersonalExpense> PersonalExpenses { get; set; }

    }

    //protected override void OnModelCreating(ModelBuilder modelBuilder)
    //{
    //     modelBuilder.Entity<Budget>()
    //    .HasRequired(a => a.BudgetUser)
    //    .WithMany()
    //    .HasForeignKey(u => u.BudgetUserId);
    //}
}