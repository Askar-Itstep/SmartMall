using System;
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

    public partial class WindowCurrOrder : Window
    {
        Model1 dbOrder;
        public Customers CurrCustomer { get; set; }
        public Orders CurrOrders { get; set; }

        public WindowCurrOrder()
        {
            InitializeComponent();
            dbOrder = new Model1();

            //int id = 3;                               //заглуш. 1
            //CurrCustomer = dbOrder.Customers.Where(x => x.id == id).ToList().FirstOrDefault();      //заглуш. 1      
            CurrCustomer = dbOrder.Customers.Where(x => x.id == WindowAutorization.CustomAuthoriz.id).ToList().FirstOrDefault();  //врем. откл.
            lstBoxOrder.ItemsSource = MainWindow.SelectProducts;
            name_cust_ord.Text = CurrCustomer.fullname_customer;
            adress.Text = CurrCustomer.address;
            phoneNum.Text = CurrCustomer.phoneNum;
            int totalSum = 0;
            foreach (var item in MainWindow.SelectProducts)
            {
                totalSum += (int)item.price;
            }


            TotalSum.Text = totalSum.ToString();

        }

        public void CreateOrder()//изм.  объекта Orders
        {
            List<Orders> newListOrder = new List<Orders>(MainWindow.SelectProducts.Count);
            int i = 0;
            foreach (var item in newListOrder)
            {
                item.custom_id = CurrCustomer.id;
                item.prod_id = MainWindow.SelectProducts[i++].id;
                item.number_item = 1;                               //заглуш.2
                item.date_ship = DateTime.Now.AddDays(3);
                item.sum_pay = Convert.ToDecimal(TotalSum.Text);
                item.sum_order = item.sum_pay;
                item.sum_debit = 0;
                dbOrder.Orders.Add(item);
            }
            dbOrder.SaveChanges();
        }
        public void UpdateProducts()
        {
            foreach (var item in MainWindow.List_products)
            {
                --item.quantity_on_storage;
            }
            dbOrder.SaveChanges();
        }
        private void Comment_Double_Click(object sender, MouseButtonEventArgs e)
        {
            CommentBox.Clear();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            ComboBoxItem selectedItem = (ComboBoxItem)comboBox.SelectedItem;
            var temp = selectedItem.Content;
            if (((TextBlock)temp).Text == "DHL")
            {
                //PriceDelivery.Text = price_delivery;
                //MessageBox.Show(priceDelivery.Text);
                MessageBox.Show("Извините. Данная функция сейчас не может быть вызвана.");
            }
        }

        private void Edit_btn_Click(object sender, RoutedEventArgs e)
        {
            WindowEditAccount windowEdit = new WindowEditAccount();
            windowEdit.Show();
        }


        private void Back_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void Btn_Pay_Click(object sender, RoutedEventArgs e)
        {
            CreateOrder();
            UpdateProducts();
            this.Close();
        }
    }
}
