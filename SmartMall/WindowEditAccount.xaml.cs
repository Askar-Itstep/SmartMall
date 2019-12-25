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

    public partial class WindowEditAccount : Window
    {
        Model1 dbTemp;
        public WindowEditAccount()
        {
            InitializeComponent();
            dbTemp = new Model1();
            BoxContactName.Text = WindowAutorization.CustomAuthoriz.fullname_customer;
            CountryRegion.Text = "Kazakhstan";
            Dictionary<string, string> sityes = new Dictionary<string, string> { { "010000", "Astana" }, { "050000", "Almaty" }, { "100000", "Karagandy" }, { "101000", "Temirtau" }, { "140015", "Pavlodar" } };
            foreach (var item in sityes)
            {
                if (WindowAutorization.CustomAuthoriz.address.Contains(item.Value))
                {
                    SityName.Text = item.Value;
                    PostalCode.Text = item.Key;
                }
            }

            WindowAutorization.CustomAuthoriz.address = CountryRegion.Text + StreetAddress.Text + Street.Text +
                ProvanceRegion.Text + SityName.Text + PostalCode.Text;
            WindowAutorization.CustomAuthoriz.phoneNum = CodeCountry.Text + MobileNum.Text;
        }

        private void Btn_Cansel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btn_Save_Click(object sender, RoutedEventArgs e)
        {
            dbTemp.SaveChanges();
        }

    }
}
