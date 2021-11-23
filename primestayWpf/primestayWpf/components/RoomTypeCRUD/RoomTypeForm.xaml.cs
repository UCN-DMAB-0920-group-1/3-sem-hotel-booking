using Model;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace WinApp.Components.RoomTypeCRUD
{
    /// <summary>
    /// Interaction logic for RoomTypeForm.xaml
    /// </summary>
    public partial class RoomTypeForm : Window
    {
        public RoomTypeForm()
        {
            InitializeComponent();

        }
        public RoomTypeForm(RoomType roomType)
        {
            InitializeComponent();
            Id.Text = roomType.Id.ToString();
            Type.Text = roomType.Type;
            Description.Text = roomType.Description;
            Beds.Text = roomType.Beds.ToString();
            Rating.Value = roomType.Rating ?? 0d;
            HotelHref.Text = roomType.HotelHref;
            Active.IsChecked = roomType.Active;
        }

        private void Button_Click_Cancel(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void Button_Click_Ok(object sender, RoutedEventArgs e)
        {
            if (validateForm())
            {
                DialogResult = true;
                Close();
            }
        }

        private void Name_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {

            if (Type.Text.Length > 5)
            {
                Type.Background = Brushes.Green;
            }
            else
            {
                Type.Background = Brushes.White;
            }
        }
        private bool validateForm()
        {
            var errors = new List<string>();


            if (string.IsNullOrWhiteSpace(Type.Text))
            {
                errors.Add("Please enter a type");
            }

            if (string.IsNullOrWhiteSpace(Description.Text))
            {
                errors.Add("Please enter a description");
            }

            if (string.IsNullOrWhiteSpace(Beds.Text))
            {
                errors.Add("Please enter an amount of beds");
            }

            if (Rating.Value < 1)
            {
                errors.Add("Please enter more than 0 stars");
            }
            if (string.IsNullOrWhiteSpace(HotelHref.Text))
            {
                errors.Add("Please enter a hotelHref");
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
