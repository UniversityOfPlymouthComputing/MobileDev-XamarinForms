using System;
using System.Collections.Generic;
using System.Text;

namespace bmi_estimate
{
    public class BodyParameter
    {
        private double? _value;
        private double _min;
        private double _max;
        private string _nameString;
        private string _unitString;
        public BodyParameter(double min, double max, string ParameterName, string Units)
        {
            _min = min;
            _max = max;
            _nameString = ParameterName;
            _unitString = Units;
        }

        public double? Value {

            get
            {
                return _value;
            }
                
            set
            {
                if ((value >= _min) && (value <= _max))
                {
                    _value = value;
                } else
                {
                    _value = null;
                }
            }
        }

        public static implicit operator double?(BodyParameter d)
        {
            return d.Value;
        }

        public bool SetValueFromString(string StringValue, out string ErrorString)
        {
            if (double.TryParse(StringValue, out double NewValue))
            {
                Value = NewValue;
                if (Value == null)
                {
                    ErrorString = string.Format(_nameString + " must be between {0:f1} and {1:f1}", _min, _max) + _unitString;
                    return false;
                }
                else
                {
                    ErrorString = "";
                    return true;
                }
            }
            else
            {
                _value = null;
                ErrorString = "Please enter a numerical value";
                return false;
            }
        }
    }
}
