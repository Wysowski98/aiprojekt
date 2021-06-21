using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Dtos
{
    public class MyFlowersUpdateCommand
    {
        public int FlowerId { get; set; }
        public string UserId { get; set; }

        public IrrigationDates Dates { get; set; }
    }
}
