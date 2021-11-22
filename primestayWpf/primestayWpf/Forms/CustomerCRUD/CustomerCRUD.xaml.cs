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

namespace primestayWpf.Forms.CustomerCRUD
{
    public partial class CustomerCRUD : Window
    {
        private readonly IDao<CustomerDto> dao;
        private ObservableCollection<Customer> CustomerList { get; set; } = new ObservableCollection<Customer>();

        public CustomerCRUD(IDao<CustomerDto> _dao)
        {
            InitializeComponent();
            dao = _dao;
            CustomerListView.ItemsSource = CustomerList;
            UpdateList();
        }




        private void UpdateList()
        {
            var customers = dao.ReadAll(new CustomerDto()).Select(x => x.Map());
            CustomerList.Clear();
            customers.ToList().ForEach(x => CustomerList.Add(x));
        }

    }
}
