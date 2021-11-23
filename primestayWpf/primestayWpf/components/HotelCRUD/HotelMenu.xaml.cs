using DataAccessLayer;
using DataAccessLayer.DTO;
using Model;
using primestayWpf.HotelCRUD;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using WinApp.src.auth;

namespace primestayWpf
{
    /// <summary>
    /// Interaction logic for HotelsWindow.xaml
    /// </summary>
    public partial class HotelMenu : Window
    {
        private readonly IDao<HotelDto> dao;
        private ObservableCollection<Hotel> HotelList { get; set; } = new ObservableCollection<Hotel>();

        public HotelMenu(IDao<HotelDto> _dao)
        {
            InitializeComponent();
            dao = _dao;
            HotelListView.ItemsSource = HotelList;
            UpdateList();
        }


        private void Edit(object sender, RoutedEventArgs e)
        {
            if (HotelListView.SelectedItem is not Hotel oldHotel)
            {
                MessageBox.Show("Please select a Hotel to edit", "ERROR");
            }
            else
            {
                var form = oldHotel is null ? new HotelForm() : new HotelForm(oldHotel);
                var yesNo = form.ShowDialog();


                if (yesNo ?? false)
                {

                    Hotel hotel = new()
                    {
                        Href = form.HotelHref,
                        Name = form.Name.Text,
                        Description = form.Description.Text,
                        LocationHref = form.LocationHref.Text,
                        StaffedHours = form.StaffedHours.Text,
                        Stars = (int)form.Stars.Value,
                        Active = form.Active.IsChecked,
                    };
                    var res = dao.Update(hotel.Map(), Auth.AccessToken);
                    UpdateList();
                    if (res > 0) MessageBox.Show($"Hotel {hotel.Name} was updated");
                    else MessageBox.Show($"Could not update {hotel.Name}, contact admin");
                }
            }

        }
        private void Delete(object sender, RoutedEventArgs e)
        {
            if (HotelListView.SelectedItem is not Hotel hotel)
            {
                MessageBox.Show("Please select a Hotel to delete", "ERROR");
            }
            else
            {

                string text = $"Are you sure you would like to delete {hotel?.Name ?? "this hotel"}?";
                if (MessageBox.Show(text, "Delete", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    var res = dao.Delete(hotel.Map(), Auth.AccessToken);
                    UpdateList();
                    if (res > 0) MessageBox.Show($"Hotel {hotel!.Name} was deleted");
                    else MessageBox.Show($"Could not delete {hotel!.Name}, contact admin");
                }
            }
        }

        private void Add(object sender, RoutedEventArgs e)
        {
            var form = new HotelForm();
            var yesNo = form.ShowDialog();
            if (yesNo ?? false)
            {

                Hotel hotel = new()
                {
                    Name = form.Name.Text,
                    Description = form.Description.Text,
                    LocationHref = form.LocationHref.Text,
                    StaffedHours = form.StaffedHours.Text,
                    Stars = (int)form.Stars.Value,
                };
                var newHotelHref = dao.Create(hotel.Map(), Auth.AccessToken);
                if (newHotelHref is null) MessageBox.Show("could not create Hotel");
                else
                {
                    MessageBox.Show($"Hotel: {hotel.Name} was succesfully created");
                    UpdateList();
                }

            }

        }

        private void UpdateList()
        {
            var hotels = dao.ReadAll(new HotelDto(), Auth.AccessToken).Select(x => x.Map());
            HotelList.Clear();
            hotels.ToList().ForEach(x => HotelList.Add(x));
        }


    }
}
