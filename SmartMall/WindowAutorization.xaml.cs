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

    public partial class WindowAutorization : Window
    {
        Model1 dbTemp;
        public WindowCabinetCustomer winCabCust = null;
        public WindowStaff winStaff = null;
        public static Employees EmpAuthoriz { get; set; }
        public static Customers CustomAuthoriz { get; set; }
        public static List<Employees> List_emploees { get; set; }
        public static List<Customers> List_customers { get; set; }
        public WindowAutorization()
        {
            InitializeComponent();

        }
        private void Registred_Click(object sender, RoutedEventArgs e)
        {
            WindowRegistr windowRegistr = new WindowRegistr();
            windowRegistr.Show();
        }
        //-------------------------------------------------------------------------
        private void Enter_Click(object sender, RoutedEventArgs e)
        {
            ReviseUser();
        }
        private void Password_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                ReviseUser();
            }
        }
        private void Login_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                ReviseUser();
            }
        }
        private void Login_Double_Click(object sender, MouseButtonEventArgs e)
        {
            Login_box.Text = "";
            Error_box.Visibility = Visibility.Hidden;
        }
        private void Password_Double_Click(object sender, MouseButtonEventArgs e)
        {
            Password_box.Clear();
            Error_box.Visibility = Visibility.Hidden;
        }
        //----------------------------------------------------------------------------------
        private void ReviseUser()
        {
            #region Simple Revise
            //string tempLogin = "ivan_i@yandex.ru", tempPass = "ivan7013892614";   
            //string tempLogin = "petr_p@yandex.ru", tempPass = "petr123";
            //string tempLogAdmin = "chingo@yahoo.com", tempPassAdmin = "chingo45";
            //string cellerLogin = "inga@mail.ru", cellerPass = "inga36";

            //if (Login_box.Text == tempLogin && Password_box.Password == tempPass)
            //{
            //     this.Close();
            //}
            //else if (Login_box.Text == tempLogAdmin && Password_box.Password == tempPassAdmin)
            //{
            //    this.Close();

            //    WindowStaff windowStaff = new WindowStaff();
            //    windowStaff.Show();
            //}
            //else
            //{
            //   Error_box.Visibility = Visibility.Visible;
            //}
            #endregion

            dbTemp = new Model1();
            //работники
            List_emploees = dbTemp.Employees.ToList();
            //заказчики (зарегистр.)
            List_customers = dbTemp.Customers.ToList();
            #region foreach
            //foreach(var item in list_emploees)
            //{
            //    if (item.login_emp == Login_box.Text && item.password_emp == Password_box.Password)
            //        MessageBox.Show("Yes-s-s!");
            //    else MessageBox.Show("GoodBuy");
            //}
            #endregion

            int roleTemp = 0;
            string[] nik = Login_box.Text.Split('@');
            //System.Diagnostics.Debug.WriteLine(arrStr[0]);
            EmpAuthoriz = List_emploees.Where(x => x.password_emp.Equals(Password_box.Password))
                                            .Where(x => x.login_emp.Equals(Login_box.Text) || x.login_emp.Contains(nik[0])).FirstOrDefault();
            CustomAuthoriz = List_customers.Where(x => x.password_custom.Equals(Password_box.Password))
                                            .Where(x => x.login_custom.Equals(Login_box.Text) || x.login_custom.Contains(nik[0])).FirstOrDefault();
            //какая роль у вошедшего?
            if (EmpAuthoriz != null)
            {
                roleTemp = (int)EmpAuthoriz.role_id;
            }
            else if (CustomAuthoriz != null)
            {
                roleTemp = (int)CustomAuthoriz.role_id;
            }
            //и в завис. от роли...
            if (roleTemp == 1 || roleTemp == 2)
            {
                winStaff = new WindowStaff();
                winStaff.Show();
                this.Close();
            }
            else if (roleTemp == 3)
            {
                this.Close();
            }

            else
                Error_box.Visibility = Visibility.Visible;

        }

    }
}
