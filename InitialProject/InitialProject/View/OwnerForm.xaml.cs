using InitialProject.Model;
using InitialProject.Repository;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using TravelAgency.Model;
using TravelAgency.Repository;

namespace InitialProject.Forms
{
    /// <summary>
    /// Interaction logic for CommentForm.xaml
    /// </summary>
    public partial class OwnerForm : Window
    {

        public User LoggedInUser { get; set; }

        public Owner SelectedComment { get; set; }

        private readonly OwnerRepository _repository;

        private string _text;
        public string Text
        {
            get => _text;
            set
            {
                if (value != _text)
                {
                    _text = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }        

        public OwnerForm(User user)
        {
            InitializeComponent();
            Title = "Create new comment";
            DataContext = this;
            LoggedInUser = user;
            _repository = new OwnerRepository();
        }

        

        

    }
}
