using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class IrrigationHistory
    {
        public int Id { get; set; }

        public MyFlowers MyFlower { get; set; }

        public User User { get; set; }

        public DateTime IrrigationDate { get; set; }

        public bool IsCompleted { get; set; }
    }
}
