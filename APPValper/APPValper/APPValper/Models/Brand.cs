﻿using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;
using SQLite;

namespace APPValper.Models
{
    public partial class Brand : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName]string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private string id;

        [PrimaryKey]
        public string Id
        {
            get { return id; }
            set
            {
                id = value;
                OnPropertyChanged();
            }
        }

        private string name;

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged();
            }
        }

        private string headquarters;

        public string Headquarters
        {
            get { return headquarters; }
            set
            {
                headquarters = value;
                OnPropertyChanged();
            }
        }

        private string founder;

        public string Founder
        {
            get { return founder; }
            set
            {
                founder = value;
                OnPropertyChanged();
            }
        }

        private bool isBusy = false;

        public bool IsBusy
        {
            get { return isBusy = false; }
            set
            {
                isBusy = value;
                OnPropertyChanged();
            }
        }

    }
}

