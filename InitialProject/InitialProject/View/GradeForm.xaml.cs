using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
using System.Xml.Linq;
using TravelAgency.Forms;
using TravelAgency.Model;
using TravelAgency.Repository;

namespace TravelAgency.View
{
    /// <summary>
    /// Interaction logic for GradeForm.xaml
    /// </summary>
    public partial class GradeForm : Window
    {
        GuestGrade NewGrade = new GuestGrade();

        private readonly GradeGuest1Repository _repository;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }



        public GradeForm()
        {
            InitializeComponent();
            Title = "Create new comment";
            DataContext = this;
            _repository = new GradeGuest1Repository();
        }

            private void GusetLoaded(object sender, RoutedEventArgs e)
        {
            Guest1 guest1 = new Guest1();
            //popuni combobox sa id-ma od gostiju kojima je rezervacija istekla pre najvise 5 dana
            //privremeno resenje:
            GuestsCB.Items.Add("1");
            GuestsCB.Items.Add("2");
            GuestsCB.Items.Add("3");

        }

        private void Fill(object sender, RoutedEventArgs e)
        {
            CB1.Items.Add("1");
            CB1.Items.Add("2");
            CB1.Items.Add("3");
            CB1.Items.Add("4");
            CB1.Items.Add("5");
        }

        private void Fill1(object sender, RoutedEventArgs e)
        {
            CB2.Items.Add("1");
            CB2.Items.Add("2");
            CB2.Items.Add("3");
            CB2.Items.Add("4");
            CB2.Items.Add("5");
        }

        private void SaveGrade_Click(object sender, RoutedEventArgs e)
        {

            GuestGrade newGrade = new GuestGrade(
                Convert.ToInt32(GuestsCB.Text),
                Convert.ToInt32(CB1.Text),
                Convert.ToInt32(CB2.Text),
                CommentText.Text);
            _repository.Save(newGrade);

            CommentText.Clear();
        }
    }
}
