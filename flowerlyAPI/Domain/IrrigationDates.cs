using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class IrrigationDates
    {
        public int Id { get; set; }
        public int DayNumber { get; set; }
        public string DayName { get; set; }
        public MyFlowers MyFlowers { get; set; }
        public string ScheduledJobId { get; set; }
        public bool IsCompleted { get; set; }
        public List<IrrigationHistory> History { get; set; }
    }
}
