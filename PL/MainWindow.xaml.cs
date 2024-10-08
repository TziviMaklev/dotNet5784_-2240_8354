﻿using BlApi;
using BO;
using BlImplementation;
using PL.Engineer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void btnEngineer_Click(object sender, RoutedEventArgs e)
        { new EngineerListWindow().Show(); }

        private void init_db(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show
                ("Do you really want to initialize the database?", "Agreement", MessageBoxButton.YesNo)==MessageBoxResult.Yes)
            {
                IBl blInstance = new Bl();
                blInstance.InitializeDB();
            };
        }
        private void rst_db(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show
                ("Do you really want to initialize the database?", "Agreement", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                IBl blInstance = new Bl();
                blInstance.ResetDB();
            };
        }
    }
}
