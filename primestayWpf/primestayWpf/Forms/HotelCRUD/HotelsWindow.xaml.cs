using PrimeStay.WPF.DataAccessLayer.DAO;
using PrimeStay.WPF.DataAccessLayer.DTO;
using primestayWpf.HotelCRUD;
using PrimestayWpf.Model;
using PrimestayWPF.DataAccessLayer.DTO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace primestayWpf
{
    /// <summary>
    /// Interaction logic for HotelsWindow.xaml
    /// </summary>
    public partial class HotelsWindow : Window
    {
        private readonly IDao<HotelDto> dao;
        private ObservableCollection<Hotel> hotelList { get; set; } = new ObservableCollection<Hotel>();

        public HotelsWindow(IDao<HotelDto> _dao)
        {
            InitializeComponent();
            dao = _dao;
            HotelListView.ItemsSource = hotelList;
            UpdateList();
        }


        private void Edit(object sender, RoutedEventArgs e)
        {
            var oldHotel = HotelListView.SelectedItem as Hotel;
            if (oldHotel is null) MessageBox.Show("Please select a Hotel to edit", "ERROR");
            else
            {
                var form = oldHotel is null ? new HotelForm() : new HotelForm(oldHotel);
                var yesNo = form.ShowDialog();

                string errors = validateForm(form.Name.Text, form.Description.Text, form.StaffedHours.Text, form.LocationHref.Text, (int)form.Stars.Value);

                if (yesNo ?? false)
                {
                    if (!string.IsNullOrEmpty(errors))
                    {
                        MessageBox.Show(errors, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {
                        Hotel hotel = new()
                        {
                            href = form.HotelHref,
                            Name = form.Name.Text,
                            Description = form.Description.Text,
                            LocationHref = form.LocationHref.Text,
                            StaffedHours = form.StaffedHours.Text,
                            Stars = (int)form.Stars.Value,
                        };
                        var res = dao.Update(hotel.Map());
                        UpdateList();
                        if (res > 0) MessageBox.Show($"Hotel {hotel.Name} was updated");
                        else MessageBox.Show($"Could not update {hotel.Name}, contact admin");
                    }
                }
            }

        }
        private void Delete(object sender, RoutedEventArgs e)
        {
            var hotel = HotelListView.SelectedItem as Hotel;
            string text = $"Are you sure you would like to delete {hotel?.Name ?? "this hotel"}?";
            if (MessageBox.Show(text, "Delete", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                if (MessageBox.Show($"Are you sure you would like to delete {hotel.Name}?", "Delete", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    var res = dao.Delete(hotel.Map());
                    UpdateList();
                    if (res > 0) MessageBox.Show($"Hotel {hotel.Name} was deleted");
                    else MessageBox.Show($"Could not delete {hotel.Name}, contact admin");
                }

            }
        }

        private void Add(object sender, RoutedEventArgs e)
        {
            var form = new HotelForm();
            var yesNo = form.ShowDialog();
            if (yesNo ?? false)
            {

                string errors = validateForm(form.Name.Text, form.Description.Text, form.StaffedHours.Text, form.LocationHref.Text, (int)form.Stars.Value);

                if (!string.IsNullOrEmpty(errors))
                {
                    MessageBox.Show(errors, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    Hotel hotel = new()
                    {
                        Name = form.Name.Text,
                        Description = form.Description.Text,
                        LocationHref = form.LocationHref.Text,
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

        }

        private void UpdateList()
        {
            var hotels = dao.ReadAll(new HotelDto()).Select(x => x.Map());
            hotelList.Clear();
            hotels.ToList().ForEach(x => hotelList.Add(x));
        }

        private string validateForm(string? name, string? Description, string? staffedHours, string? location, int? stars)
        {
            List<string> errors = new List<string>();


            if (string.IsNullOrEmpty(name))
            {
                errors.Add("Please enter a name");
            }

            if (string.IsNullOrEmpty(Description))
            {
                errors.Add("Please enter a description");
            }

            if (string.IsNullOrEmpty(staffedHours))
            {
                errors.Add("Please a time when the hotel is staffed");
            }

            if (string.IsNullOrEmpty(location))
            {
                errors.Add("Please a valid location");
            }

            if (stars == null)
            {
                errors.Add("Please some stars");
            }
            else if (stars < 1)
            {
                errors.Add("Please enter more than 0 stars");
            }

            string error = string.Join(",\n", errors);

            return error;
        }
    }
}
