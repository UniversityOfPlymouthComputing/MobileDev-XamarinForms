using System;
using System.Collections.Generic;
using System.Text;

namespace bmi_estimate
{
    public class BmiModel
    {
        private BodyParameter Weight = new BodyParameter(min: 20.0, max: 200.0, "Weight", "Kg");
        private BodyParameter Height = new BodyParameter(min: 0.5, max: 3.0, "Height", "m");

        public double? BmiValue
        {
            get
            {
                if ((Weight != null) && (Height != null))
                {
                    return Weight / (Height * Height);
                }
                else
                {
                    return null;
                }
            }
        }

        public static implicit operator double?(BmiModel m)
        {
            return m.BmiValue;
        }

        public bool SetWeightAsString(string strWeight, out string ErrorString)
        {
            return Weight.SetValueFromString(strWeight, out ErrorString);
        }
        public bool SetHeightAsString(string strHeight, out string ErrorString)
        {
            return Height.SetValueFromString(strHeight, out ErrorString);
        }

    }
}
