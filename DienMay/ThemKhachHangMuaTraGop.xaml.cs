using DienMay.ViewModel.Base;
using Service.DataBase;
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
    /// Interaction logic for ThemKhachHangMuaTraGop.xaml
    /// </summary>
    public partial class ThemKhachHangMuaTraGop : Window
    {
        List<HINHTHUC> danhSachHinhThucMua = new List<HINHTHUC>();
        public ThemKhachHangMuaTraGop()
        {
            InitializeComponent();
            danhSachHinhThucMua = XuLyHinhThuc.getInstance.DocDanhSachTatCa();
            dataHinhThucMua.DataContext = danhSachHinhThucMua;
        }

        private void btnDong_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            long hinhThuc = (long)cbbHinhThucMua.SelectedValue;
        }
    }
}
