using primestayWpf.Forms;
using PrimestayWpf.Model;
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
            DialogResult = true;
            Close();
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
    }
}
