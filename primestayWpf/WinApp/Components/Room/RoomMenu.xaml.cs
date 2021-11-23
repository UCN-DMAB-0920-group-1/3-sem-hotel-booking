using DataAccessLayer;
using DataAccessLayer.DTO;
using Model;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using WinApp.src.auth;

namespace WinApp.Components.RoomViews
{
    /// <summary>
    /// Interaction logic for RoomMenu.xaml
    /// </summary>
    public partial class RoomMenu : Window
    {
        private readonly IDao<RoomDto> dao;
        private ObservableCollection<Room> RoomList { get; set; } = new ObservableCollection<Room>();

        public RoomMenu(IDao<RoomDto> _dao, RoomType roomType)
        {
            InitializeComponent();
            RoomListView.ItemsSource = RoomList;
            this.dao = _dao;
            UpdateList(roomType.Id ?? 0);
        }

        private void Add(object sender, RoutedEventArgs e)
        {

        }

        private void Edit(object sender, RoutedEventArgs e)
        {
            if (RoomListView.SelectedItem is not Room room)
            {
                MessageBox.Show("Please select a Hotel to edit", "ERROR");
            }

        }

        private void Delete(object sender, RoutedEventArgs e)
        {

        }
        private void UpdateList(int roomTypeId = 0)
        {
            RoomList.Clear();
            var room = new Room() { RoomTypeId = roomTypeId != 0 ? roomTypeId : null };
            dao.ReadAll(room.Map(), Auth.AccessToken).ToList().ForEach(room => RoomList.Add(room.Map()));
        }
    }
}
