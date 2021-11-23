using Model;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace primestayWpf.HotelCRUD
{
    /// <summary>
    /// Interaction logic for HotelForm.xaml
    /// </summary>
    public partial class HotelForm : Window
    {
        public readonly string HotelHref = string.Empty;
        public HotelForm()
        {
            InitializeComponent();

        }
        public HotelForm(Hotel hotel)
        {
            InitializeComponent();
            Name.Text = hotel.Name;
            Description.Text = hotel.Description;
            StaffedHours.Text = hotel.StaffedHours;
            Stars.Value = hotel.Stars ?? 0d;
            LocationHref.Text = hotel.LocationHref;
            HotelHref = hotel.Href;
            Active.IsChecked = hotel.Active;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (validateForm())
            {
                DialogResult = true;
                Close();
            }
        }

        private void Name_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {

            if (Name.Text.Length > 5)
            {
                Name.Background = Brushes.Green;
            }
            else
            {
                Name.Background = Brushes.White;
            }
        }
        private bool validateForm()
        {
            var errors = new List<string>();


            if (string.IsNullOrWhiteSpace(Name.Text))
            {
                errors.Add("Please enter a name");
            }

            if (string.IsNullOrWhiteSpace(Description.Text))
            {
                errors.Add("Please enter a description");
            }

            if (string.IsNullOrWhiteSpace(StaffedHours.Text))
            {
                errors.Add("Please a time when the hotel is staffed");
            }

            if (string.IsNullOrWhiteSpace(LocationHref.Text))
            {
                errors.Add("Please a valid location");
            }

            if (Stars.Value < 1)
            {
                errors.Add("Please enter more than 0 stars");
            }

            var any = errors.Any();
            if (any)
            {
                string error = string.Join(",\n", errors);
                MessageBox.Show(error, "Empty Fields");
            }

            return !any;
        }
    }
}
