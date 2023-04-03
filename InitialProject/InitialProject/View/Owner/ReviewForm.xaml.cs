using Microsoft.Graph.Models;
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
using TravelAgency.Model;
using TravelAgency.Repository;
using User = TravelAgency.Model.User;

namespace TravelAgency.View.Owner
{
    /// <summary>
    /// Interaction logic for ReviewForm.xaml
    /// </summary>
    public partial class ReviewForm : Window
    {
        GradeGuest1Repository gradeGuest1Repository;
        OwnerRepository ownerRepository;
        private User LogedOwner { get; set; }
        public ReviewForm(User user)
        {
            InitializeComponent();
            gradeGuest1Repository = new GradeGuest1Repository();
            ownerRepository = new OwnerRepository();
            LogedOwner = user;
        }

        public void ShowReviews()
        {
            //logika izlistaj ocene od onih koje si ti ocenio
            

            //ako su ocenjeni znaci da je u rezervacijama graded status (fun za uzimanje svih gostova1 iz rezervacije i distinctujem)
            //izlistava iz OwnerRating.csv (fun koja poredi usernamove od fun iznad sa usernamovima u OwnerOwerRating i izlistava ih u Grid)
        }

        private void Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
