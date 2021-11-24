using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WinApp.components.Customer
{
    /// <summary>
    /// Interaction logic for CustomerForm.xaml
    /// </summary>
    public partial class CustomerForm : Window
    {
        public CustomerForm()
        {
            InitializeComponent();

        }
        public CustomerForm(Model.Customer customer)
        {
            InitializeComponent();
                Name.Text = customer.Name;
                Phone.Text = customer.Phone;
                Email.Text = customer.Email;
                Datepicker.SelectedDate = customer.BirthDay;
        }

    private void Button_Click(object sender, RoutedEventArgs e)
     {
            if (validateForm())
            {
                DialogResult = true;
                Close();
            }
    }


        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
        private bool validateForm()
        {
            var errors = new List<string>();


            if (string.IsNullOrWhiteSpace(Name.Text))
            {
                errors.Add("Please enter a name");
            }

            if (string.IsNullOrWhiteSpace(Phone.Text))
            {
                errors.Add("Please enter a description");
            }

            if (string.IsNullOrWhiteSpace(Email.Text))
            {
                errors.Add("Please a time when the hotel is staffed");
            }

            if (string.IsNullOrWhiteSpace(Datepicker.SelectedDate.ToString()))
            {
                errors.Add("Please a valid location");
            }

            var any = errors.Any();
            if (any)
            {
                string error = string.Join(",\n", errors);
                MessageBox.Show(error, "Empty Fields");
            }

            return !any;
        }

    }
}
