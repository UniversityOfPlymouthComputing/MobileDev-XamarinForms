using System;
using System.Collections.Generic;
using System.Text;

namespace DepartmentOfTransport
{

    class Driver
    {
        public string Name { get; set; }
        public RoadVehicle PrimaryModeOfTransport { get; set; }

        public Driver(string Name, RoadVehicle PrimaryModeOfTransport = null)
        {
            this.Name = Name;
            if (PrimaryModeOfTransport != null)
            {
                this.PrimaryModeOfTransport = PrimaryModeOfTransport;
            }
        }

        public string Description
        {
            get
            {
                if (PrimaryModeOfTransport == null)
                {
                    return Name + ", has no primary vehicle";
                }
                else
                {
                    return Name + ": Primary mode of transport is " + PrimaryModeOfTransport.Description;
                }
            }
        }
    }
}
