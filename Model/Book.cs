using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAppLibraryf.Model
{
    public class Book : INotifyPropertyChanged
    {
        internal bool IsEditMode;

        public string Title { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }


        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
