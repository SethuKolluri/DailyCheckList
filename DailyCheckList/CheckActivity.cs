using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace DailyCheckList
{
    public class CheckActivity : INotifyPropertyChanged
    {
        public CheckActivity(string _Name, bool _isChecked)
        {
            Name = _Name; IsChecked = _isChecked;
        }

        public string Name { get; set; }
        public bool IsChecked { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
