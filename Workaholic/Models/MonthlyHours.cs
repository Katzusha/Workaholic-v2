using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workaholic.Models
{
    internal class MonthlyHours
    {
        public int Id { get; set; }
        public int StampType { get; set; }
        public double Duration { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public string Username { get; set; }
        public int WorkStampId { get; set; }
    }
}
