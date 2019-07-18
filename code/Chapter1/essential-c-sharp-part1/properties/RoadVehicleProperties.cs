using System;
using System.Collections.Generic;
using System.Text;

namespace DepartmentOfTransport
{
    partial class RoadVehicle
    {
        public static string ProjectVersion = "1.0"; // static member variable
        public int EngineSerialNumber { get; }
        public int NumberOfWheels { get; }
        public int CarriageCapacity { get; }
        private string _description;

        public string Description
        {
            get
            {
                if (_description == null)
                {
                    _description = string.Format("Road Vehicle. Wheels: {0:d}, Capacity: {1:d} people, serialNo: {2:d}", NumberOfWheels, CarriageCapacity, EngineSerialNumber);
                }
                return _description;
            }
        }
    }
}
