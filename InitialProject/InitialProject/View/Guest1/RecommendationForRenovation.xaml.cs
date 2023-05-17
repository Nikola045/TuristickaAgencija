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

namespace TravelAgency.View.Guest1
{
    /// <summary>
    /// Interaction logic for RecommendationForRenovation.xaml
    /// </summary>
    public partial class RecommendationForRenovation : Window
    {
       // private readonly RecomodationRepositroy recomodatioRepsitory;
        public RecommendationForRenovation()
        {
            InitializeComponent();
            //recomodationRepositoyr = new(InjectorService.CreateInstance<IStorage<Recomodaiton>>());
        }

        private object selectedHotel;

        public void HotelChoice(object izbor)
        {
            selectedHotel = izbor;
        }

        private void Rate(object sender, RoutedEventArgs e)
        {
            //novi recomodaiotn
            //recomodaitonRepository.Save(recomodation);
        }
    }
}
