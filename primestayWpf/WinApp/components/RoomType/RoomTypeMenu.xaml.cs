using DataAccessLayer;
using DataAccessLayer.DAO;
using DataAccessLayer.DTO;
using Model;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using WinApp.Components.BookingView;
using WinApp.Components.RoomView;
using WinApp.src.auth;

namespace WinApp.Components.RoomTypeView
{
    /// <summary>
    /// Interaction logic for RoomTypeWindow.xaml
    /// </summary>
    public partial class RoomTypeMenu : Window
    {
        private readonly IDao<RoomTypeDto> dao;
        private readonly IDao<BookingDto> bookingDao;
        private ObservableCollection<RoomType> roomTypeList { get; set; } = new ObservableCollection<RoomType>();

        public RoomTypeMenu(IDao<RoomTypeDto> _dao, IDao<BookingDto> _bookingDao)
        {
            InitializeComponent();
            dao = _dao;
            bookingDao = _bookingDao;
            RoomTypeListView.ItemsSource = roomTypeList;
            UpdateList();
        }


        private void Edit(object sender, RoutedEventArgs e)
        {
            var oldRoomType = RoomTypeListView.SelectedItem as RoomType;
            if (oldRoomType is null) MessageBox.Show("Please select a RoomType to edit", "ERROR");
            else
            {
                var form = oldRoomType is null ? new RoomTypeForm() : new RoomTypeForm(oldRoomType);
                var yesNo = form.ShowDialog();

                if (yesNo ?? false)
                {

                    RoomType roomType = new()
                    {
                        Id = int.Parse(form.Id.Text),
                        Type = form.Type.Text,
                        Description = form.Description.Text,
                        Rating = (int)form.Rating.Value,
                        Beds = int.Parse(form.Beds.Text),
                        HotelId = int.Parse(form.HotelID.Text),
                        Active = form.Active.IsChecked,
                    };
                    var res = dao.Update(roomType.Map());
                    UpdateList();
                    if (res > 0) MessageBox.Show($"RoomType: {roomType.Type} was updated");
                    else MessageBox.Show($"Could not update {roomType.Type}, contact admin");

                }
            }

        }
        private void Delete(object sender, RoutedEventArgs e)
        {
            var roomType = RoomTypeListView.SelectedItem as RoomType;
            string text = $"Are you sure you would like to delete {roomType?.Type ?? "this RoomType"}?";
            if (MessageBox.Show(text, "Delete", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                var res = dao.Delete(roomType.Map());
                UpdateList();
                if (res > 0) MessageBox.Show($"RoomType: {roomType!.Type} was deleted");
                else MessageBox.Show($"Could not delete {roomType!.Type}, contact admin");
            }
        }

        private void Add(object sender, RoutedEventArgs e)
        {
            var form = new RoomTypeForm();
            var yesNo = form.ShowDialog();
            if (yesNo ?? false)
            {


                RoomType roomType = new()
                {
                    Id = int.Parse(form.Id.Text),
                    Type = form.Type.Text,
                    Description = form.Description.Text,
                    Rating = (int)form.Rating.Value,
                    Beds = int.Parse(form.Beds.Text),
                    HotelId = int.Parse(form.HotelID.Text),
                    Active = form.Active.IsChecked,
                };
                var newHotelHref = dao.Create(roomType.Map());
                if (newHotelHref is null) MessageBox.Show("could not create RoomType");
                else
                {
                    MessageBox.Show($"RoomType: {roomType.Type} was succesfully created");
                    UpdateList();
                }

            }

        }

        private void UpdateList()
        {
            try
            {
                var RoomTypes = dao.ReadAll(new RoomTypeDto()).Select(x => x.Map());
                roomTypeList.Clear();
                RoomTypes.ToList().ForEach(x => roomTypeList.Add(x));
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                Debug.WriteLine("No resources returned from API");
                MessageBox.Show("No resources returned from API, check connection", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Rooms(object sender, RoutedEventArgs e)
        {
            if (RoomTypeListView.SelectedItem is not RoomType roomType)
            {
                MessageBox.Show("Please select a HotelType to view its rooms", "ERROR");
            }
            else
            {
                var dao = DaoFactory.Create<RoomDto>(RestDataContext.GetInstance(), Auth.AccessToken);
                new RoomMenu(dao, roomType).ShowDialog();
            }
        }
        private void RoomBookings(object sender, RoutedEventArgs e)
        {
            if (RoomTypeListView.SelectedItem is not RoomType roomType)
            {
                MessageBox.Show("Please select a Hotel to view bookings", "ERROR");
            }
            else
            {
                new BookingMenu(bookingDao, roomType: roomType).ShowDialog();
            }

        }
    }
}
