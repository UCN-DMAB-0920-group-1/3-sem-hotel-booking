using DataAccessLayer;
using DataAccessLayer.DTO;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using WinApp.components.Customer;
using WinApp.src.auth;

namespace WinApp.Components.Customer
{
    public partial class CustomerMenu : Window
    {
        private readonly IDao<CustomerDto> dao;
        private ObservableCollection<Model.Customer> CustomerList { get; set; } = new ObservableCollection<Model.Customer>();

        public CustomerMenu(IDao<CustomerDto> _dao)
        {
            InitializeComponent();
            dao = _dao;
            CustomerListView.ItemsSource = CustomerList;
            UpdateList();
        }

        private void Delete(object sender, RoutedEventArgs e)
        {
            var customer = CustomerListView.SelectedItem as Model.Customer;
            string text = $"Are you sure that you would like to delete{customer?.Phone ?? "this customer"}?";
            if (MessageBox.Show(text, "Delete", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                if (MessageBox.Show($"Are you sure you would like to delete {customer.Name}?", "Delete", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    var res = dao.Delete(customer.Map(), Auth.AccessToken);
                    UpdateList();
                    if (res > 0) MessageBox.Show($"Customer{customer.Phone} was deleted");
                    else MessageBox.Show($"Could not delete {customer.Phone}, contact admin");
                }
            }
        }

        private void Add(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();

        }


        private void UpdateList()
        {
            var customers = dao.ReadAll(new CustomerDto(), Auth.AccessToken).Select(x => x.Map());
            CustomerList.Clear();
            customers.ToList().ForEach(x => CustomerList.Add(x));
        }

        private void Edit(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
            /**
            if (CustomerListView.SelectedItem is not Model.Customer oldCustomer)
            {
                MessageBox.Show("Please select a Customer", "Error");
            }
            else
            {
                var form = oldCustomer is null ? new CustomerForm() : new CustomerForm();
                var yesNo = form.ShowDialog();
                if(yesNo ?? false)
               {
                    Model.Customer customer = new()
                    {
                     //  Name = form.Name,
                        //Email = form.Email,
                       // Phone = form.Phone,
                       // BirthDay = form.BirthDay,

                    };

                 // mangler noget
                }
            }**/
        }

        private void Create(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
