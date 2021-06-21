using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class MyFlowers
    {
        public int Id { get; set; }
        public Flower Flower { get; set; }
        public User User { get; set; }
        public List<IrrigationDates> IrrigationDates { get; set; }
    }
}
