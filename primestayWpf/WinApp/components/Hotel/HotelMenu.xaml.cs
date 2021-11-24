using DataAccessLayer;
using DataAccessLayer.DTO;
using Model;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using WinApp.Components.BookingView;

namespace WinApp.Components.HotelView
{
    /// <summary>
    /// Interaction logic for HotelsWindow.xaml
    /// </summary>
    public partial class HotelMenu : Window
    {
        private readonly IDao<HotelDto> dao;
        private readonly IDao<RoomTypeDto> roomTypeDao;
        private readonly IDao<BookingDto> bookingDao;
        private ObservableCollection<Hotel> HotelList { get; set; } = new ObservableCollection<Hotel>();

        public HotelMenu(IDao<HotelDto> _dao, IDao<RoomTypeDto> _roomTypeDao, IDao<BookingDto> _bookingDao)
        {
            InitializeComponent();
            dao = _dao;
            roomTypeDao = _roomTypeDao;
            bookingDao = _bookingDao;
            HotelListView.ItemsSource = HotelList;
            UpdateList();
        }


        private void Edit(object sender, RoutedEventArgs e)
        {
            if (HotelListView.SelectedItem is not Model.Hotel oldHotel)
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
                        Id = int.Parse(form.HotelHref),
                        Name = form.Name.Text,
                        Description = form.Description.Text,
                        LocationId = int.Parse(form.LocationId.Text),
                        StaffedHours = form.StaffedHours.Text,
                        Stars = (int)form.Stars.Value,
                        Active = form.Active.IsChecked,
                    };
                    var res = dao.Update(hotel.Map());
                    UpdateList();
                    if (res > 0) MessageBox.Show($"Hotel {hotel.Name} was updated");
                    else MessageBox.Show($"Could not update {hotel.Name}, contact admin");
                }
            }

        }
        private void Delete(object sender, RoutedEventArgs e)
        {
            if (HotelListView.SelectedItem is not Model.Hotel hotel)
            {
                MessageBox.Show("Please select a Hotel to delete", "ERROR");
            }
            else
            {

                string text = $"Are you sure you would like to delete {hotel?.Name ?? "this hotel"}?";
                if (MessageBox.Show(text, "Delete", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    var res = dao.Delete(hotel.Map());
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

                Model.Hotel hotel = new()
                {
                    Name = form.Name.Text,
                    Description = form.Description.Text,
                    LocationId = int.Parse(form.LocationId.Text),
                    StaffedHours = form.StaffedHours.Text,
                    Stars = (int)form.Stars.Value,
                };
                var newHotelHref = dao.Create(hotel.Map());
                if (newHotelHref is null) MessageBox.Show("could not create Hotel");
                else
                {
                    MessageBox.Show($"Hotel: {hotel.Name} was succesfully created");
                    UpdateList();
                }

            }

        }
        private void HotelBookings(object sender, RoutedEventArgs e)
        {
            if (HotelListView.SelectedItem is not Model.Hotel hotel)
            {
                MessageBox.Show("Please select a Hotel to view bookings", "ERROR");
            }
            else
            {
                RoomType search = new() { HotelId = hotel.Id };
                var roomList = roomTypeDao.ReadAll(search.Map()).ToList().Select(roomType => roomType.Map());
                new BookingMenu(bookingDao, hotelRooms: roomList).ShowDialog();
            }

        }

        private void UpdateList()
        {
            var hotels = dao.ReadAll(new HotelDto()).Select(x => x.Map());
            HotelList.Clear();
            hotels.ToList().ForEach(x => HotelList.Add(x));
        }
    }
}
