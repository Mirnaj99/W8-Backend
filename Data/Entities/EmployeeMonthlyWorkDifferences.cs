using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace W8_Backend.Data.Entities
{
    public class EmployeeMonthlyWorkDifferences
    {

       
        public int Month { get; set; }
       
        public int Year { get; set; }
       
        public string Emp_number { get; set; }

        public decimal Total_minus_month { get; set; }

        public string CompanyName { get; set; }

        public string CodeNumber { get; set; }
      
        public string CodeDesc { get; set; }

        public bool IsJob { get; set; }

        public bool IsSynced { get; set; }

        public decimal Working_hours { get; set; }

        public decimal Percent_working { get; set; }

        public decimal Percent_of_month { get; set; }

        public decimal CostPerMonth { get; set; }

    }
}

