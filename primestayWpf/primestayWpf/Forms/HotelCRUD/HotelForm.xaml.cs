using PrimestayWpf.Model;
using System.Windows;

namespace primestayWpf.HotelCRUD
{
    /// <summary>
    /// Interaction logic for HotelForm.xaml
    /// </summary>
    public partial class HotelForm : Window
    {
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
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }
    }
}
