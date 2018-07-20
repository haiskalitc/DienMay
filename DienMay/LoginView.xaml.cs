using Service.XuLy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DienMay
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : Window
    {
        public LoginView()
        {
            InitializeComponent();
        }

        private void btnThoat_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnDangNhap_Click(object sender, RoutedEventArgs e)
        {
            if (XuLyTaiKhoan.getInstance.DangNhap(txtTenDangNhap.Text, txtMatKhau.Password.ToString()))
            {
                MainView main = new MainView();
                main.Show();
                this.Hide();
                main.QuayLai += (sen, arg) => {
                    (sen as MainView).Close();
                    this.Show();
                    txtTenDangNhap.Text = "";
                    txtMatKhau.Password = "";
                };
            }
            else
            {
                MessageBox.Show("Đăng nhập thất bại", "Thông báo", MessageBoxButton.OK,MessageBoxImage.Error);
            }
        }
    }
}
