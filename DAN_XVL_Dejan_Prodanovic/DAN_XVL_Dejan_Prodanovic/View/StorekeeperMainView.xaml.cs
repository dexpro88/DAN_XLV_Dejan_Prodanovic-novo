﻿using DAN_XVL_Dejan_Prodanovic.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DAN_XVL_Dejan_Prodanovic.View
{
    /// <summary>
    /// Interaction logic for StorekeeperMainView.xaml
    /// </summary>
    public partial class StorekeeperMainView : Window
    {
        public StorekeeperMainView()
        {
            InitializeComponent();
            DataContext = new StorekeeperMainViewModel(this);
        }
    }
}
