using PrimeStay.WPF.DataAccessLayer.DAO;
using PrimeStay.WPF.DataAccessLayer.DTO;
using primestayWpf.RoomTypeCRUD;
using PrimestayWpf.Model;
using PrimestayWPF.DataAccessLayer.DTO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;

namespace primestayWpf
{
    /// <summary>
    /// Interaction logic for RoomTypeWindow.xaml
    /// </summary>
    public partial class RoomTypeWindow : Window
    {
        private readonly IDao<RoomTypeDto> dao;
        private ObservableCollection<RoomType> roomTypeList { get; set; } = new ObservableCollection<RoomType>();

        public RoomTypeWindow(IDao<RoomTypeDto> _dao)
        {
            InitializeComponent();
            dao = _dao;
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

                string errors = validateForm(form.Type.Text, form.Description.Text, form.Beds.Text, form.HotelHref.Text, (int)form.Rating.Value);

                if (yesNo ?? false)
                {
                    if (!string.IsNullOrEmpty(errors))
                    {
                        MessageBox.Show(errors, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {
                        RoomType roomType = new()
                        {
                            Type = form.Type.Text,
                            Description = form.Description.Text,
                            Rating = (int)form.Rating.Value,
                            Beds = Int32.Parse(form.Beds.Text),
                            HotelHref = form.HotelHref.Text
                        };
                        var res = dao.Update(roomType.Map());
                        UpdateList();
                        if (res > 0) MessageBox.Show($"RoomType: {roomType.Type} was updated");
                        else MessageBox.Show($"Could not update {roomType.Type}, contact admin");
                    }
                }
            }

        }
        private void Delete(object sender, RoutedEventArgs e)
        {
            var roomType = RoomTypeListView.SelectedItem as RoomType;
            string text = $"Are you sure you would like to delete {roomType?.Type ?? "this RoomType"}?";
            if (MessageBox.Show(text, "Delete", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                if (MessageBox.Show($"Are you sure you would like to delete {roomType.Type}?", "Delete", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    var res = dao.Delete(roomType.Map());
                    UpdateList();
                    if (res > 0) MessageBox.Show($"RoomType: {roomType.Type} was deleted");
                    else MessageBox.Show($"Could not delete {roomType.Type}, contact admin");
                }

            }
        }

        private void Add(object sender, RoutedEventArgs e)
        {
            var form = new RoomTypeForm();
            var yesNo = form.ShowDialog();
            if (yesNo ?? false)
            {

                string errors = validateForm(form.Type.Text, form.Description.Text, form.Beds.Text, form.HotelHref.Text, (int)form.Rating.Value);

                if (!string.IsNullOrEmpty(errors))
                {
                    MessageBox.Show(errors, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    RoomType roomType = new()
                    {
                        Type = form.Type.Text,
                        Description = form.Description.Text,
                        Rating = (int)form.Rating.Value,
                        Beds = Int32.Parse(form.Beds.Text),
                        HotelHref = form.HotelHref.Text,
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
                Debug.WriteLine("No resource returned from API");
            }
        }

        private string validateForm(string? type, string? description, string? beds, string? hotel, int? rating)
        {
            List<string> errors = new List<string>();


            if (string.IsNullOrEmpty(type))
            {
                errors.Add("Please enter a name");
            }

            if (string.IsNullOrEmpty(description))
            {
                errors.Add("Please enter a description");
            }

            if (string.IsNullOrEmpty(hotel))
            {
                errors.Add("Please add a valid hotel");
            }

            if (string.IsNullOrEmpty(beds))
            {
                errors.Add("Please add how many beds");
            }

            if (rating == null)
            {
                errors.Add("Please add some ratings");
            }
            else if (rating < 1)
            {
                errors.Add("Please enter more than 0 ratings");
            }

            string error = string.Join(",\n", errors);

            return error;
        }
    }
}
