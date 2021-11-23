using Model;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace WinApp.Components.RoomViews
{
    /// <summary>
    /// Interaction logic for RoomForm.xaml
    /// </summary>
    public partial class RoomForm : Window
    {
        public RoomForm(Room? room = null)
        {
            InitializeComponent();
            if (room is not null)
            {
                Id.Text = room.Id.ToString();
                RoomType.Text = room.RoomTypeId.ToString();
                Notes.Text = room.Notes;
                RoomNumber.Text = room.RoomNumber;
                Active.IsChecked = room.Active;
            }
        }

        private void Button_Click_Ok(object sender, RoutedEventArgs e)
        {
            if (validateForm())
            {
                DialogResult = true;
                Close();
            }

        }

        private void Button_Click_Cancel(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private bool validateForm()
        {
            var errors = new List<string>();


            if (string.IsNullOrWhiteSpace(RoomType.Text) || !int.TryParse(RoomType.Text, out _))
            {
                errors.Add("Please enter a valid room type");
            }
            if (string.IsNullOrWhiteSpace(RoomNumber.Text) || !int.TryParse(RoomNumber.Text, out _))
            {
                errors.Add("Please enter a valid room number");
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
