using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

    public partial class WindowRegistr : Window
    {
        Model1 dbReg = new Model1();
        public Customers Candidat { get; set; }

        public WindowRegistr()
        {
            InitializeComponent();
            WindowAutorization.List_customers = dbReg.Customers.ToList();
            Candidat = new Customers();
            Candidat.role_id = 3;
            Candidat.age_custom = 18;
            Candidat.address = " ";
            Candidat.phoneNum = " ";

            BlockCode.Text = RandStr();
        }
        //======================================================================================
        //созд. юзера
        private void CreateAccount_Click(object sender, RoutedEventArgs e)
        {
            Candidat.fullname_customer = LastName.Text + " " + FirstName.Text;
            Candidat.login_custom = Box_email.Text;
            Candidat.password_custom = ConfirmPassword.Text;
            if (FindCustomer()) return;

            if (!ReviseUser())
            {
                MessageBox.Show("При регистрации произошел сбой. Проверте введенные данные!");
                return;
            }

            MessageBox.Show("Новый пользователь создан!");
            dbReg.Customers.Add(Candidat);
            dbReg.SaveChanges();
            this.Close();

            #region "Vasiliy Terkin"
            //using (Model1 context = new Model1())         //testBlock
            //{
            //    Customers pers = new Customers
            //    {
            //        fullname_customer = "Vasiliy Terkin",
            //        age_custom = 32,
            //        role_id = 6,
            //        login_custom = "Hero@yandex.ru",
            //        password_custom = "vasya_Hero123",
            //    };

            //    context.Customers.Add(pers);
            //    context.SaveChanges();
            //}
            #endregion
        }
        //----------проверить-есть ли такой юзер-------------------
        public bool FindCustomer()
        {
            foreach (var item in WindowAutorization.List_customers)
            {
                if (Candidat.Equals(item))
                {
                    MessageBox.Show("Такой пользователь уже есть! Введите данные снова");
                    Candidat.fullname_customer = "";
                    Candidat.login_custom = "";
                    Candidat.password_custom = "";
                    return true;
                }
            }
            return false;
        }
        //----------если нет - дальнейш. проверка юзера----------------------------
        public bool ReviseUser()
        {
            if(!EmailValid() || !FullName_Valid() || !CreatePassword_valid() 
                || !Password_Confirm_valid() || !MatchCode())
            {
                return false;
            }
           
            foreach (var item in WindowAutorization.List_customers)
            {
                if (Candidat.login_custom == item.login_custom)
                {
                    Box_email.Foreground = Brushes.Red;
                    Box_email.Text = "Пользователь с таким email уже существует!";
                    return false;
                }
            }
                        
            return true;
        }
        //========================================================================
        //-----------------service methods----------------------
        public bool EmailValid()
        {
            string temp = @"(\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*)"; //string email = @"Bruce_Wayne@gmail.com";            
            if (Regex.IsMatch(Box_email.Text, temp))
            {
                Pleaser.Background = Brushes.LightSeaGreen;
                Pleaser.Text = "Email valid";
                Candidat.login_custom = Box_email.Text;
                return true;
            }
            else
            {
                Pleaser.Background = Brushes.Coral;
                Pleaser.Text = "Email is not valid";
                Candidat.login_custom = Box_email.Text;
                return false;
            }
        }
        public bool FullName_Valid()
        {
            if (FirstName.Text.Length > 2 && LastName.Text.Length > 2)
            {
                Candidat.fullname_customer = LastName.Text + " " + FirstName.Text;
                return true;
            }
            return false;
        }
        public bool CreatePassword_valid()  //этот метод надо перести в конец (после нажатия <Register>)
        {
            if (NewPassword.Text.Length < 6 || NewPassword.Text.Length > 20)
            {
                NewPassword.Foreground = Brushes.Red;
                NewPassword.Text = "Длина пароля не соответствует!";
            }
            Regex regex = new Regex("[A-Za-z]");
            Match match = regex.Match(NewPassword.Text);
            if (!match.Success)
            {
                NewPassword.Foreground = Brushes.Red;
                NewPassword.Text = "";
                NewPassword.Text = "В пароле должны быть только буквы латинского алфавита!";
                return false;
            }

            bool flag = false;
            foreach (var item in NewPassword.Text)
                if (char.IsUpper(item))
                {
                    flag = true;
                    break;
                }
            if (!flag)
            {
                NewPassword.Foreground = Brushes.Red;
                NewPassword.Text = "В пароле должен быть хотя бы 1 символ с большой или маленькой буквы!";
                return false;
            }
            return true;
        }
        public bool Password_Confirm_valid()
        {
            if (ConfirmPassword.Text != NewPassword.Text)
            {
                ConfirmPassword.Foreground = Brushes.Red;
                ConfirmPassword.Text = "";
                ConfirmPassword.Text = "Введенные пароли не совпадают!";
                return false;
            }
            Candidat.password_custom = ConfirmPassword.Text;
            return true;
        }
        public bool MatchCode() //проверка автобота
        {
            //BlockCode.Text += " ";

            if (DuplicateBox.Text != BlockCode.Text)
            {
                DuplicateBox.Foreground = Brushes.Red;
                DuplicateBox.Text = "Введенный код не совпадает!";
                BlockCode.Text = RandStr();

                return false;
            }
            //MessageBox.Show("!!!");
            return true;
        }
        public string RandStr()
        {
            StringBuilder strBld = new StringBuilder(100);
            Random rand = new Random();
            int len = rand.Next(4, 8);

            for (int i = 0; i < len; i++)
            {
                strBld.Append(Convert.ToChar(rand.Next(65, 90)));
            }
            string temp = Convert.ToString(strBld);
            return temp;
        }
        //===================================================================================
        //----------прочие обработчики---------------------------
        private void ClearBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ((TextBox)sender).Text = "";
            ((TextBox)sender).Foreground = Brushes.Black;
        }

        private void DuplicateBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Enter) return;
            //MatchCode();

        }

        private void Box_email_KeyDown(object sender, KeyEventArgs e)
        {
            //EmailValid();
        }


        private void FullName_KeyDown(object sender, KeyEventArgs e)
        {
            //FullName_Valid();
        }

        private void CreatePassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Enter) return;
            //CreatePassword_valid();
        }

        private void ConfirmPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Enter) return;
            //Password_Confirm_valid();
        }




    }
}
