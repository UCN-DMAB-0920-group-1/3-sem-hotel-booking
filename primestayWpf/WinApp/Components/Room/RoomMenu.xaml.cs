using DataAccessLayer;
using DataAccessLayer.DTO;
using Model;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace WinApp.Components.RoomView
{
    /// <summary>
    /// Interaction logic for RoomMenu.xaml
    /// </summary>
    public partial class RoomMenu : Window
    {
        private readonly IDao<RoomDto> dao;
        private ObservableCollection<Room> RoomList { get; set; } = new ObservableCollection<Room>();
        private readonly int roomTypeID;

        public RoomMenu(IDao<RoomDto> _dao, RoomType roomType)
        {
            InitializeComponent();
            RoomListView.ItemsSource = RoomList;
            this.dao = _dao;
            roomTypeID = roomType.Id ?? 0;
            UpdateList();
        }

        private void Add(object sender, RoutedEventArgs e)
        {
            var form = new RoomForm();
            var shouldEdit = form.ShowDialog();
            var room = MakeRoomFromForm(form);

            if (shouldEdit ?? false)
                if (dao.Create(room.Map()) is not null)
                {
                    UpdateList();
                    MessageBox.Show("Room was created", "Success", MessageBoxButton.OK);
                }
                else
                    MessageBox.Show("Could not create room", "Error", MessageBoxButton.OK);


        }

        private void Edit(object sender, RoutedEventArgs e)
        {
            if (RoomListView.SelectedItem is not Room room)
            {
                MessageBox.Show("Please select a Hotel to edit", "ERROR");
            }
            else
            {
                var form = new RoomForm(room);
                var shouldEdit = form.ShowDialog();
                var newRoom = MakeRoomFromForm(form);
                if (shouldEdit ?? false)
                    if (dao.Update(newRoom.Map()) > 0)
                    {
                        UpdateList();
                        MessageBox.Show("Room was Updated");

                    }
                    else
                        MessageBox.Show("Could not Update Room");

            }

        }

        private static Room MakeRoomFromForm(RoomForm form)
        {
            if (form is null) return null;
            return new Room()
            {
                Id = int.TryParse(form.Id.Text, out int id) ? id : null,
                Notes = form.Notes.Text,
                Active = form.Active.IsChecked,
                RoomNumber = form.RoomNumber.Text,
                RoomTypeId = int.Parse(form.RoomType.Text),

            };


        }

        private void Delete(object sender, RoutedEventArgs e)
        {
            if (RoomListView.SelectedItem is not Room room)
            {
                MessageBox.Show("Please select a Room to delete", "ERROR");
            }
            else
            {
                string text = $"Are you sure you would like to delete {room?.RoomNumber ?? "this room"}?";
                if (MessageBox.Show(text, "Delete", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    var res = dao.Delete(room.Map());
                    UpdateList();
                    if (res > 0) MessageBox.Show($"Hotel {room!.RoomNumber} was deleted");
                    else MessageBox.Show($"Could not delete {room!.RoomNumber}, contact admin");
                }
            }

        }
        private void UpdateList()
        {
            RoomList.Clear();
            var room = new Room() { RoomTypeId = roomTypeID };
            dao.ReadAll(room.Map()).ToList().ForEach(room => RoomList.Add(room.Map()));
        }
    }
}
