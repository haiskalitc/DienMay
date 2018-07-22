using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for ThongTinChiTietKhachHang.xaml
    /// </summary>
    public partial class ThongTinChiTietKhachHang : Window
    {
        public ThongTinChiTietKhachHang()
        {
            InitializeComponent();
        }

        private void btnThoat_Click(object sender, RoutedEventArgs e)
        {

        }
    
        private void lsvKhachHang_MouseUp_1(object sender, MouseButtonEventArgs e)
        {

        }

        private void btnSua_Click(object sender, RoutedEventArgs e)
        {

        }

        private void lsvKhachHang_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);

        }
    }
}
