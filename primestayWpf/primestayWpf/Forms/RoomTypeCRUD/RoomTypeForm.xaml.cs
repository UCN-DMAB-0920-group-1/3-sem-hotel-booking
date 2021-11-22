using PrimestayWpf.Model;
using System.Windows;
using System.Windows.Media;

namespace primestayWpf.RoomTypeCRUD
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
            DialogResult = true;
            Close();
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
    }
}
