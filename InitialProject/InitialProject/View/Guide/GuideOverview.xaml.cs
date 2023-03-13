﻿using System;
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
using TravelAgency.Forms;
using TravelAgency.Model;
using TravelAgency.Repository;

namespace TravelAgency.View
{
    /// <summary>
    /// Interaction logic for GuideOverview.xaml
    /// </summary>
    public partial class GuideOverview : Window
    {
        public User LoggedInUser { get; set; }

        public GuideOverview(User user)
        {
            InitializeComponent();
            DataContext = this;
            LoggedInUser = user;
        }

        public GuideOverview()
        {
        }
        private void OpenGuideForm(object sender, RoutedEventArgs e)
        {
            GuideForm createGuideForm = new GuideForm();
            createGuideForm.Show();
        }
    }
}
