using DataAccessLayer;
using DataAccessLayer.DTO;
using Model;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using WinApp.components.Customer;
using WinApp.Components.BookingView;

namespace WinApp.Components.CustomerView
{
    public partial class CustomerMenu : Window
    {
        private readonly IDao<CustomerDto> dao;
        private readonly IDao<BookingDto> bookingDao;
        private ObservableCollection<Customer> CustomerList { get; set; } = new ObservableCollection<Model.Customer>();

        public CustomerMenu(IDao<CustomerDto> _dao, IDao<BookingDto> bookingDao)
        {
            InitializeComponent();
            dao = _dao;
            CustomerListView.ItemsSource = CustomerList;
            UpdateList();
            this.bookingDao = bookingDao;
        }

        private void Delete(object sender, RoutedEventArgs e)
        {
            var customer = CustomerListView.SelectedItem as Customer;

            if (MessageBox.Show($"Are you sure you would like to delete {customer!.Name}?", "Delete", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                var res = dao.Delete(customer.Map());
                UpdateList();
                if (res > 0) MessageBox.Show($"Customer{customer.Phone} was deleted");
                else MessageBox.Show($"Could not delete {customer.Phone}, contact admin");
            }

        }



        private void Add(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();

        }


        private void UpdateList()
        {
            var customers = dao.ReadAll(new CustomerDto()).Select(x => x.Map());
            CustomerList.Clear();
            customers.ToList().ForEach(x => CustomerList.Add(x));
        }

        private void Edit(object sender, RoutedEventArgs e)
        {
            if (CustomerListView.SelectedItem is not Customer oldCustomer)
            {
                MessageBox.Show("Please select a Customer", "Error");
            }
            else
            {
                var form = oldCustomer is null ? new CustomerForm() : new CustomerForm(oldCustomer);
                var yesNo = form.ShowDialog();
                if (yesNo ?? false)
                {
                    Customer customer = new()
                    {
                        Id = oldCustomer.Id,
                        Name = form.Name.Text,
                        Email = form.Email.Text,
                        Phone = form.Phone.Text,
                        UserId = int.TryParse(form.UserId.Text, out int userId) ? userId : -1,
                        BirthDay = form.Datepicker.SelectedDate!.Value,
                    };
                    var res = dao.Update(customer.Map());
                    UpdateList();
                    if (res > 0) MessageBox.Show($"Customer {customer.Name} was updated");
                    else MessageBox.Show($"Could not update{customer.Name}, contact admin");

                }
            }
        }

        private void Create(object sender, RoutedEventArgs e)
        {
            var form = new CustomerForm();
            var yesNo = form.ShowDialog();
            if (yesNo ?? false)
            {
                Model.Customer customer = new()
                {
                    Name = form.Name.Text,
                    Email = form.Email.Text,
                    Phone = form.Phone.Text,
                    BirthDay = form.Datepicker.SelectedDate!.Value,
                    UserId = int.TryParse(form.UserId.Text, out int userId) ? userId : null,
                };
                var newCustomerHref = dao.Create(customer.Map());
                if (newCustomerHref is null) MessageBox.Show("Could not create new customer");
                else
                {
                    MessageBox.Show($"Customer: {customer.Name} was created in the system");
                    UpdateList();
                }
            }
        }

        private void Bookings(object sender, RoutedEventArgs e)
        {
            if (CustomerListView.SelectedItem is not Customer customer)
            {
                MessageBox.Show("Please select a Hotel to view bookings", "ERROR");
            }
            else
            {
                new BookingMenu(bookingDao, customer: customer).ShowDialog();
            }

        }

    }
}
