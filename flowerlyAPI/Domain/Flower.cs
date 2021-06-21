using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class Flower
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Species { get; set; }
        public string Nationality { get; set; }
        public string ImageUrl { get; set; }
        public int IrrigationPerWeek { get; set; }
        public WaterAmountEnum WaterAmount { get; set; }
    }
}
