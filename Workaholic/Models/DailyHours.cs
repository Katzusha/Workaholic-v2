using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workaholic.Models
{
    internal class DailyHours
    {
        public int Id { get; set; }
        public int StampType { get; set; }
        public double Start { get; set; }
        public double End { get; set; }
        public double Duration { get; set; }
        public DateTime Date { get; set; }
        public string Username { get; set; }
    }
}
