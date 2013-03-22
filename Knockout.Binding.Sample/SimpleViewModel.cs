﻿using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using KnckoutBindingGenerater;

namespace CEF_Demo
{
    public class SimpleViewModel : IBindableToJs, INotifyPropertyChanged
    {
        private string m_SimpleString;
        private readonly ObservableCollectionEx<string> m_ObservableCollection = new ObservableCollectionEx<string>(Enumerable.Range(0, 10).Select(i => Convert.ToString(i)));

        public SimpleViewModel()
        {
            SimpleString = "A simeple string";
        }

        public string SimpleString
        {
            get { return m_SimpleString; }
            set
            {
                m_SimpleString = value;
                OnPropertyChanged("SimpleString");
            }
        }

        public int SimpleInt32
        {
            get { return 42; }
        }

        public ObservableCollectionEx<string> StringCollection
        {
            get { return m_ObservableCollection; }
        }

        public void ClickMethod()
        {
            SimpleString = Guid.NewGuid().ToString();

            StringCollection.Add(Guid.NewGuid().ToString());
        }

        public string Name
        {
            get { return "simpleViewModelInstance"; }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}