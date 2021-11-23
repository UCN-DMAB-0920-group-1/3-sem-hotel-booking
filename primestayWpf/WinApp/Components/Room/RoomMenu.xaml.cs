using DataAccessLayer;
using DataAccessLayer.DTO;
using Model;
using System.Collections.ObjectModel;
using System.Windows;

namespace WinApp.Components.RoomViews
{
    /// <summary>
    /// Interaction logic for RoomMenu.xaml
    /// </summary>
    public partial class RoomMenu : Window
    {
        private readonly IDao<RoomDto> dao;
        private ObservableCollection<Room> RoomList { get; set; } = new ObservableCollection<Room>();

        public RoomMenu(IDao<RoomDto> dao)
        {
            InitializeComponent();
            RoomListView.ItemsSource = RoomList;

            this.dao = dao;
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
    }
}
