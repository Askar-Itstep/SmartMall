using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
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
    public partial class WindowStaff : Window
    {
        Model1 db;
        public IEnumerable List_orders_query { get; set; }
        public WindowStaff()
        {
            InitializeComponent();
            db = new Model1();
            //роли
            List<Roles> list_roles = db.Roles.ToList();
            roles.ItemsSource = list_roles;

            //товары
            db.Products.Load();
            productsGrid.ItemsSource = db.Products.Local.ToBindingList();

            //заказы
            List<Orders> list_orders = db.Orders.ToList();
            ordersGrid.ItemsSource = list_orders;


            List_orders_query = from ord in MainWindow.List_orders
                                join prod in MainWindow.List_products on ord.prod_id equals prod.id
                                join cust in MainWindow.List_customers on ord.custom_id equals cust.id
                                select new
                                {
                                    id = ord.id,
                                    custom_fullname = cust.fullname_customer,
                                    prod_name = prod.name_prod,
                                    number_item = ord.number_item,
                                    price = prod.price,
                                    date_ship = ord.date_ship,
                                    sum_pay = ord.sum_pay,
                                };
            ordersGridGeneric.ItemsSource = List_orders_query;


            //дебиторы
            List<Debitors> list_debitors = db.Debitors.ToList();
            debitorsGrid.ItemsSource = list_debitors;

            //заказчики (зарегистр.)
            List<Customers> list_customers = db.Customers.ToList();
            customersGrid.ItemsSource = list_customers;

            //работники
            List<Employees> list_emploees = db.Employees.ToList();
            employeesGrid.ItemsSource = list_emploees;

            if (WindowAutorization.EmpAuthoriz.role_id == 2) // (2) заблокир. продавцу
            {
                ItemRoles.Visibility = Visibility.Hidden;       //данн. по ролям
                ItemEmployees.Visibility = Visibility.Hidden;   //данн. по работн.
                ItemDebitors.Visibility = Visibility.Hidden;    //данн. по дебиторам                
            }
            ItemStatOrder.Visibility = Visibility.Hidden;
            ItemStatOrder.Visibility = Visibility.Hidden;
        }

        private void File_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Извините. Доступ к файлам закрыт. Обратитесь позднее.");
        }
        private void Help_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Извините. Мануал находится в разработке. Обратитесь позднее.");
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


        private void Delete_Click_(object sender, RoutedEventArgs e)
        {
            if (ItemProducts.IsSelected)
            {
                for (int i = 0; i < productsGrid.SelectedItems.Count; i++)
                {
                    Products product = productsGrid.SelectedItems[i] as Products;
                    if (product != null)
                        db.Products.Remove(product);
                }
            }
            if (ItemOrders.IsSelected)
            {
                for (int i = 0; i < ordersGrid.SelectedItems.Count; i++)
                {
                    Orders order = ordersGrid.SelectedItems[i] as Orders;
                    if (order != null)
                        db.Orders.Remove(order);
                }
            }
            if (ItemDebitors.IsSelected)
            {
                for (int i = 0; i < debitorsGrid.SelectedItems.Count; i++)
                {
                    Debitors debitor = debitorsGrid.SelectedItems[i] as Debitors;
                    if (debitor != null)
                        db.Debitors.Remove(debitor);
                }
            }
            if (ItemCustomers.IsSelected)
            {
                for (int i = 0; i < customersGrid.SelectedItems.Count; i++)
                {
                    Customers customer = customersGrid.SelectedItems[i] as Customers;
                    if (customer != null)
                        db.Customers.Remove(customer);
                }
            }
            if (ItemEmployees.IsSelected)
            {
                for (int i = 0; i < employeesGrid.SelectedItems.Count; i++)
                {
                    Employees employee = employeesGrid.SelectedItems[i] as Employees;
                    if (employee != null)
                        db.Employees.Remove(employee);
                }
            }
            db.SaveChanges();
        }

        private void Update_Click_(object sender, RoutedEventArgs e)
        {
            db.SaveChanges();
        }
        //=========================================================================================================================
        //статист. продаж по товарам
        private void StatProduct_Click_(object sender, RoutedEventArgs e)
        {
            IEnumerable listStatProd = (from ord in MainWindow.List_orders
                                        join prod in MainWindow.List_products on ord.prod_id equals prod.id
                                        let num_ord = MainWindow.List_orders.Where(x => x.prod_id == prod.id).Count()
                                        let numSellProd = num_ord * ord.number_item
                                        orderby numSellProd descending
                                        select new
                                        {
                                            id = prod.id,
                                            prod_name = prod.name_prod,
                                            num_orders = num_ord,
                                            num_sell_prod = numSellProd,
                                            sum_cash = ord.sum_order * numSellProd
                                        }).Distinct();

            statOrdersGrid.ItemsSource = listStatProd;
            ItemStatOrder.Visibility = Visibility.Visible;
        }
        //===========================================================================================================================
        //статист. продаж по продавцам
        private void StatSeller_Click_(object sender, RoutedEventArgs e)
        {
            #region query#100500
            //IEnumerable listStatSeller = (from ord in MainWindow.List_orders
            //                              join seller in MainWindow.List_employees on ord.seller_id equals seller.id
            //                              join prod in MainWindow.List_products on ord.prod_id equals prod.id
            //                              let num_ord = MainWindow.List_orders.Where(o => o.seller_id == seller.id).Count()
            //                              let numSellProd = num_ord * ord.number_item
            //                              //let numSellProd = MainWindow.List_orders.Select(o => o.prod_id == prod.id).Count()
            //                              select new
            //                              {
            //                                  id = seller.id,
            //                                  seller_name = seller.fullname_emp,
            //                                  num_orders = num_ord,
            //                                  //num_sell_prod = numSellProd,
            //                                  //sum_cash = prod.price * numSellProd
            //                              }).Distinct();
            #endregion
            //IQueryable<StatisticSeller> listStatSeller = from seller in db.StatisticSeller select seller;
            //statSellersGrid.ItemsSource = listStatSeller;   //error - need load++

            db.StatisticSeller.Load();
            statSellersGrid.ItemsSource = db.StatisticSeller.Local.ToBindingList();            

            ItemStatSeller.Visibility = Visibility.Visible;
        }
    }
}
