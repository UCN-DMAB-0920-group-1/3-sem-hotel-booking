using DataAccessLayer;
using DataAccessLayer.DTO;
using Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace WinApp.Components.BookingView

{
    /// <summary>
    /// Interaction logic for BookingMenu.xaml
    /// </summary>
    public partial class BookingMenu : Window
    {
        internal enum Type
        {
            hotel,
            roomType,
            customer,

        }
        private readonly Type type;
        private readonly IDao<BookingDto> dao;
        private readonly Customer? customer;
        private readonly IEnumerable<RoomType>? hotelRooms;
        private readonly RoomType? roomType;
        private ObservableCollection<Booking> BookingList { get; set; } = new ObservableCollection<Booking>();

        public BookingMenu(IDao<BookingDto> _dao, Customer? customer = null, IEnumerable<RoomType>? hotelRooms = null, RoomType? roomType = null)
        {
            InitializeComponent();
            dao = _dao;
            if (customer is not null)
            {
                type = Type.customer;
                this.customer = customer;
            }
            if (hotelRooms is not null)
            {
                type = Type.hotel;
                this.hotelRooms = hotelRooms;
            }
            if (roomType is not null)
            {
                type = Type.roomType;
                this.roomType = roomType;
            }
            BookingListView.ItemsSource = BookingList;
            UpdateList();
        }

        private void UpdateList()
        {
            BookingList.Clear();
            Booking search = new();

            switch (type)
            {
                case Type.customer:
                    CustomerBookingSeach(search);
                    break;
                case Type.hotel:
                    HotelBookingsearch(search);
                    break;
                case Type.roomType:
                    RoomTypeBookingSearch(search);
                    break;
                default:
                    break;
            }


        }

        private void Add(object sender, RoutedEventArgs e)
        {

        }

        private void Edit(object sender, RoutedEventArgs e)
        {

        }

        private void Delete(object sender, RoutedEventArgs e)
        {

        }
        #region dao-searches
        private void CustomerBookingSeach(Booking search)
        {
            search.CustomerId = customer!.Id;
            dao.ReadAll(search.Map()).ToList().ForEach(booking => BookingList.Add(booking.Map()));
        }

        private void RoomTypeBookingSearch(Booking search)
        {
            search.RoomTypeId = roomType!.Id;
            dao.ReadAll(search.Map()).ToList().ForEach(booking => BookingList.Add(booking.Map()));
        }

        private void HotelBookingsearch(Booking search)
        {
            hotelRooms!.ToList().ForEach(roomType =>
            {
                search.RoomTypeId = roomType.Id;
                dao.ReadAll(search.Map()).ToList().ForEach(booking => BookingList.Add(booking.Map()));
            });
        }
        #endregion
    }
}
