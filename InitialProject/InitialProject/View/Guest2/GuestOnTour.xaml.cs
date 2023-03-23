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

namespace TravelAgency.View.Guest2
{
    /// <summary>
    /// Interaction logic for GuestOnTour.xaml
    /// </summary>
    public partial class GuestOnTour : Window
    {
        Model.User LogedUser = new Model.User();
        public GuestOnTour(Model.User logedUser)
        {
            InitializeComponent();
            LogedUser = logedUser;
        }
    }
}
