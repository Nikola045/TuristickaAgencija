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
using TravelAgency.Repository;
using TravelAgency.Services;

namespace TravelAgency.View.Guide
{
    /// <summary>
    /// Interaction logic for GuideTourRequestStatistic.xaml
    /// </summary>
    public partial class GuideTourRequestStatistic : Window
    {
        private readonly TourService tourService;
        public GuideTourRequestStatistic()
        {
            InitializeComponent();
            tourService = new TourService();
            string mostWantedLanguage = tourService.FindMostWantedLanguage();
            WantedLanguage.Text = mostWantedLanguage;
            string mostWantedCity = tourService.FindMostWantedCity();
            WantedCity.Text = mostWantedCity;
            string mostWantedCountry = tourService.FindMostWantedCountry();
            WantedCountry.Text = mostWantedCountry;
        }

        private void FillComboBox(object sender, RoutedEventArgs e)
        {
            CB.Items.Add("2023");
            CB.Items.Add("2022");
            CB.Items.Add("2021");
            CB.Items.Add("2020");
            CB.Items.Add("2019");
        }

        private void RBY_Checked(object sender, RoutedEventArgs e)
        {
            Year.IsEnabled = true;
            CB.IsEnabled = false;
            Month.IsEnabled = false;
            
        }

        private void RBM_Checked(object sender, RoutedEventArgs e)
        {
            Month.IsEnabled=true;
            CB.IsEnabled = true;
            Year.IsEnabled = false;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
                int numberOf = 0;
                if (string.IsNullOrEmpty(tbCity.Text) && string.IsNullOrEmpty(tbCountry.Text))
                {
                    if (string.IsNullOrEmpty(tbLanguage.Text))
                    {
                        MessageBox.Show("Statistics not found");
                    }
                    else
                    {
                        numberOf = tourService.StatisticByLanguage(tbLanguage.Text);
                        Result.Text = numberOf.ToString();
                    }
                }
                else
                {
                    numberOf = tourService.StatisticByLocation(tbCity.Text, tbCountry.Text);
                    Result.Text = numberOf.ToString();
                }
           
        }

        private void Wanted_Click(object sender, RoutedEventArgs e)
        {
            GuideForm createGuideForm = new GuideForm();  
            createGuideForm.Show();
            createGuideForm.txtCity.Text = tourService.FindMostWantedCity();
            createGuideForm.txtCountry.Text = tourService.FindMostWantedCountry();
            createGuideForm.txtLangueg.Text = tourService.FindMostWantedLanguage();
            createGuideForm.txtCity.IsEnabled = false;
            createGuideForm.txtCountry.IsEnabled = false;
            createGuideForm.txtLangueg.IsEnabled = false;
        }
    }
}
