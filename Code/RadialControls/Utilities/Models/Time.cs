using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace RadialControls.Utilities
{
    public class Time : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public enum Meridian { AM, PM }

        private int _hours, _minutes;
        private Meridian _period;

        #region Properties

        public int Hours
        {
            get { return _hours; }

            set
            {
                if ((0 > value) || (value > 11)) return;
                _hours = value; OnPropertyChanged();
            }
        }

        public int Minutes
        {
            get { return _minutes; }

            set
            {
                if ((0 > value) || (value > 59)) return;
                _minutes = value; OnPropertyChanged();
            }
        }

        public Meridian Period
        {
            get { return _period; }
            set { _period = value; OnPropertyChanged(); }
        }

        #endregion

        #region Event Handlers

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(
                    this, new PropertyChangedEventArgs(propertyName)
                );
            }
        }

        #endregion
    }
}
