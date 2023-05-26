using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using TravelAgency.Domain.Model;
using TravelAgency.Domain.RepositoryInterfaces;
using TravelAgency.Repository;
using TravelAgency.Repository.HotelRepo;
using TravelAgency.Services;
using Image = TravelAgency.Domain.Model.Image;

namespace TravelAgency.View
{
    public partial class GuideForm : Window
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private readonly TourRepository tourRepository;

        private readonly CheckPointService checkPointService;
        private readonly CheckPointRepository checkPointRepository;

        private readonly ImageRepository tourImageRepository;

        public User LoggedUser { get; set; }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public GuideForm(User user)
        {
            InitializeComponent();
            Title = "Create new tour";
            DataContext = this;

            tourRepository = new(InjectorService.CreateInstance<IStorage<Tour>>());
            checkPointService = new CheckPointService();
            tourImageRepository = new(InjectorService.CreateInstance<IStorage<Image>>());
            checkPointRepository = new(InjectorService.CreateInstance<IStorage<CheckPoint>>());

        }

        private void SaveTour(object sender, RoutedEventArgs e)
        {
            string FilePath = "../../../Resources/Data/checkPoints.csv";           

            List<CheckPoint> checkPoints = new List<CheckPoint>();
            foreach (string item in ListCheckPoints.Items)
            {
                CheckPoint checkPoint = new CheckPoint();
                checkPoint.Name = item.ToString();
                checkPoint.Id = checkPointService.GetByName(checkPoint.Name).Id;
                checkPoint.Status = "Neaktivna";
                checkPoints.Add(checkPoint);
            }
            if (ListCheckPoints.Items.Count < 2)
                MessageBox.Show("Morate uneti bar dve Kljucne tacke Pocetnu i krajnju");

            for (int i = 0; i < DateList.Items.Count; i++)
            {
                Tour newTour = new Tour(
                    tourRepository.NextId(),
                    txtName.Text,
                    txtCity.Text,
                    txtCountry.Text,
                    txtDescription.Text,
                    txtLangueg.Text,
                    Convert.ToInt32(txtMaxNumberOfGuests.Text),
                    Convert.ToDateTime(DateList.Items[i]),
                    Convert.ToInt32(txtTourDuration.Text),
                    Convert.ToInt32(LoggedUser.Id),
                    checkPoints);
                Tour savedTour = tourRepository.Save(newTour);
                
            }

            foreach(string image  in ImageList.Items)
            {
                Image img = new Image();
                img.Url = image;
                tourImageRepository.Save(img);
            }


            MessageBox.Show("Uspesno uneta tura");
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void AddCheckPointToList(object sender, RoutedEventArgs e)
        {
            ListCheckPoints.Items.Add(CheckPointsCB.Text);
        }

        private void LoadCheckPoints(object sender, RoutedEventArgs e)
        {
            List<CheckPoint> checkPoints = new List<CheckPoint>();
            string FilePath = "../../../Resources/Data/checkPoints.csv";
            checkPoints = checkPointRepository.GetAll();

            for (int i = 0; i < checkPoints.Count; i++)
            {
                
                CheckPointsCB.Items.Add(checkPoints[i].Name);
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DateList.Items.Add(StartDateBox.Text);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ImageList.Items.Add(ImageTxt.Text);
        }
    }
}
