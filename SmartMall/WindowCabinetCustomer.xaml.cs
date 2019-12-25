using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SmartMall
{
    public partial class WindowCabinetCustomer : Window
    {
        Model1 db;
        public static IEnumerable ListMyOrders { get; set; }
        public WindowCabinetCustomer()
        {
            InitializeComponent();
            db = new Model1();

            wcc_name_customer.Text = WindowAutorization.CustomAuthoriz.fullname_customer;
            ListMyOrders = from ord in MainWindow.List_orders
                           join prod in MainWindow.List_products on ord.prod_id equals prod.id
                           join cust in MainWindow.List_customers on ord.custom_id equals cust.id
                           where cust.fullname_customer == WindowAutorization.CustomAuthoriz.fullname_customer  //выборка в элем. управл. <GridViewColumn>                                   
                                                                                                                //where cust.fullname_customer == "Петров Петр"       //заглуш
                           select new { id = ord.id, prod.name_prod, prod.price, ord.number_item, ord.date_ship, ord.sum_pay, cust.fullname_customer };

            myOrders.ItemsSource = ListMyOrders;

        }
    }
}
