using PrimeStay.WPF.DataAccessLayer.DAO;
using PrimeStay.WPF.DataAccessLayer.DTO;
using primestayWpf.Forms;
using primestayWpf.src.auth;
using PrimestayWpf.Model;
using PrimestayWPF.DataAccessLayer.DTO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace primestayWpf.
{
    /// <summary>
    /// Interaction logic for CustomerMenu.xaml
    /// </summary>
    public partial class CustomerMenu : Window
    {
        private readonly IDao<CustomerDto> dao;
        private ObservableCollection<Customer> CustomerList  { get; set; } = new ObservableCollection<Customer>();
        public CustomerMenu(IDao<CustomerDto> _dao)
        {
            InitializeComponent();
            dao = _dao;
            CustomerListView.ItemsSource = CustomerList;
            UpdateList();
        }
    }
}
