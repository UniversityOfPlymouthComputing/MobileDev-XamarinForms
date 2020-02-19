using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace uoplib.mvvm
{
    public class MVVMBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void Update<T>(ref T property, T withValue, [CallerMemberName]string propertyName = "") where T : IComparable
        {
            if (withValue.Equals(property)) return;
            property = withValue;
            OnPropertyChanged(propertyName);
        }
        protected virtual void OnPropertyChanged([CallerMemberName]string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
