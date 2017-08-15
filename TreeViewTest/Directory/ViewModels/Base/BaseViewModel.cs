﻿using PropertyChanged;
using System.ComponentModel;

namespace TreeViewTest
{
    [AddINotifyPropertyChangedInterface]
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = (sender,e) => { };
    }
}
