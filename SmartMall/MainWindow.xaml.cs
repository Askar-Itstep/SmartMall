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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SmartMall
{
    public partial class MainWindow : Window
    {
        public WindowAutorization windowAutorization = null;
        public WindowStaff windowStaff = null;
        public WindowCabinetCustomer windowCabinCustom = null;
        public WindowCurrOrder winCurrOrd = null;

        public static List<Orders> List_orders { get; set; }        //список всех заказов
        public static List<Customers> List_customers { get; set; }
        public static List<Employees> List_employees { get; set; }

        public static List<Products> List_products { get; set; }    //список всех товаров в магаз
        public static List<Products> SelectProducts { get; set; }  //товары в корзине
        public Products Product { get; set; }                      //выбранн. товар

        public static List<Products> ListNotebooks { get; set; }
        public static List<Products> ListMonitors { get; set; }
        public static List<Products> ListMonoblock { get; set; }

        public static int countNotebooks = 0;
        public static int countMonitors = 0;
        public static int countMonoblocks = 0;
        public MainWindow()
        {
            InitializeComponent();
            using (Model1 db = new Model1())
            {
                List_products = db.Products.ToList();
                List_orders = db.Orders.ToList();
                List_customers = db.Customers.ToList();
                List_employees = db.Employees.ToList();

                //2 ноутбуки
                ListNotebooks = db.Products.Where(x => x.name_prod.Contains("book")).Where(x => x.quantity_on_storage > 0).ToList();
                notebookList.ItemsSource = ListNotebooks;

                //3-мониторы
                ListMonitors = db.Products.Where(x => x.name_prod.Contains("monitor")).Where(x => x.quantity_on_storage > 0).ToList();
                monitorList.ItemsSource = ListMonitors;

                //4-моноблоки
                ListMonoblock = db.Products.Where(x => x.name_prod.Contains("monoblock")).Where(x => x.quantity_on_storage > 0).ToList();
                monoblockList.ItemsSource = ListMonoblock;

                SelectProducts = new List<Products>();
            }
        }

        private void RegistrWindow_Click(object sender, RoutedEventArgs e)
        {
            WindowRegistr windowReg = new WindowRegistr();
            windowReg.Show();
        }

        private void GoCabin_Click(object sender, RoutedEventArgs e)
        {
            if (WindowAutorization.EmpAuthoriz != null)
            {
                windowStaff = new WindowStaff();
                windowStaff.Show();
            }
            else
            {
                windowCabinCustom = new WindowCabinetCustomer();
                windowCabinCustom.Show();
            }
        }
        public bool RevizeProduct()
        {
            //foreach (var item in List_products)               //моделир.: товар закончился
            //{
            //    if (item.model == "Macbook Air13 MQD32RU/A")
            //    {
            //        item.quantity_on_storage = 0;
            //        break;
            //    }
            //}            

            foreach (var item in List_products)
            {
                if (item.quantity_on_storage <= 0)
                {
                    MessageBox.Show("К сожалению данный товар закончился. Выберите другой товар");
                    notebookList.SelectedItem = null;
                    List_products.Remove(item);
                    notebookList.ItemsSource = List_products.Where(x => x.name_prod.Contains("book")).Where(x => x.quantity_on_storage > 0).ToList();
                    return false;
                }
            }
            return true;
        }

        private void NotebookList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!RevizeProduct()) return;
            Product = (Products)notebookList.SelectedItems[countNotebooks++];
            SelectProducts.Add(Product);
        }
        private void MonitorList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!RevizeProduct()) return;
            Product = (Products)monitorList.SelectedItems[countMonitors++];
            SelectProducts.Add(Product);
        }
        private void MonoblockList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!RevizeProduct()) return;
            Product = (Products)monoblockList.SelectedItems[countMonoblocks++];
            SelectProducts.Add(Product);
        }
        private void WindowAutorization_loaded(object sender, RoutedEventArgs e)
        {
            windowAutorization = new WindowAutorization();
            windowAutorization.ShowDialog();
        }

        //для тестир.---------------------------------------------------------------------------
        private void WindowStaff_loaded(object sender, RoutedEventArgs e)
        {
            windowStaff = new WindowStaff();
            windowStaff.ShowDialog();
        }

        private void WindowCabinetCustomer_loaded(object sender, RoutedEventArgs e)
        {
            windowCabinCustom = new WindowCabinetCustomer();
            windowCabinCustom.Show();
        }

        private void WindowCurrOrder_loaded(object sender, RoutedEventArgs e)
        {
            if (WindowAutorization.CustomAuthoriz == null)
            {
                windowAutorization = new WindowAutorization();
                windowAutorization.Show();
            }
            else
            {
                winCurrOrd = new WindowCurrOrder();
                winCurrOrd.name_cust_ord.Text = WindowAutorization.CustomAuthoriz.fullname_customer;
                winCurrOrd.Show();
                this.Close();
            }

        }

        private void WindowRegistr_loaded(object sender, RoutedEventArgs e)
        {
            WindowRegistr windowReg = new WindowRegistr();
            windowReg.Show();
        }

        private void WindowEditAccount_loaded(object sender, RoutedEventArgs e)
        {
            WindowEditAccount windowEdit = new WindowEditAccount();
            windowEdit.Show();
        }
    }
}

