using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapAppMauiClient.Models
{
    public class CapApp : INotifyPropertyChanged
    {
        int _id;
        public int Id 
        { 
            get => _id;
            set
            {
                if (_id == value)
                    return;

                _id = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Id)));    
            }
        }

        string _capappname;
        public string CapName 
        { 
            get => _capappname;
            set
            {
                if (_capappname == value)
                    return;

                _capappname = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CapName)));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
