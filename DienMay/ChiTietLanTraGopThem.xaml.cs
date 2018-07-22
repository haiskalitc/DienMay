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
using Service.DataBase;
using DienMay.ViewModel.Base;
using System.Text.RegularExpressions;
using System.Data;
using Service.XuLy;

namespace DienMay
{
    /// <summary>
    /// Interaction logic for ChiTietLanTraGopThem.xaml
    /// </summary>
    public partial class ChiTietLanTraGopThem : Window
    {
        private List<CHITIETMUAHANG> dsChiTietMuaHangTam;
        NotifiableCollection<ChiTietTraGopModel> danhSach = new NotifiableCollection<ChiTietTraGopModel>();
        public event EventHandler Back;
        public ChiTietLanTraGopThem()
        {
            InitializeComponent();
          
                danhSach.Add(new ChiTietTraGopModel("Lần" + 1,new CHITIETMUAHANG() {ChuoiNgayTra = "05/11/1995" ,SoTienConLai = 1000}));
          
            if (this.Content.DataContext == null)
            {
                this.Content.DataContext = danhSach;
            }
           
        }
        public ChiTietLanTraGopThem(List<CHITIETMUAHANG> dsChiTietMuaHangTam)
        {
            InitializeComponent();
            this.dsChiTietMuaHangTam = dsChiTietMuaHangTam;
            for (int i = 0; i < dsChiTietMuaHangTam.Count; i++)
            {
                danhSach.Add(new ChiTietTraGopModel("Lần" + (i+1),dsChiTietMuaHangTam[i]));
            }
            if (this.Content.DataContext == null)
            {
                this.DataContext = danhSach;
            }
        }

        private void btnThoat_Click(object sender, RoutedEventArgs e)
        {
            Back(this, new EventArgs());
        }

        private void btnDangNhap_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult re = MessageBox.Show("Xác nhận lưu?", "Thông báo!", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (re == MessageBoxResult.Yes)
            {
                foreach (ChiTietTraGopModel item in danhSach)
                {
                    XuLyChiTietMuaHang.getInstance.ThemChiTietMuaHang(item.MuaHang);
                }
                Back(this, new EventArgs());
            }
        }

        private void LsvDanhSachCauHoi_OnSelected(object sender, MouseButtonEventArgs e)
        {

        }
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
          
        }

        private void btnSua_Click(object sender, RoutedEventArgs e)
        {
  
            var item = (sender as Button);
            if (item != null)
            {
                if (item.Content.Equals("Sửa"))
                {
                    item.Content = "Lưu";
                    item.Foreground = System.Windows.Media.Brushes.Green;

                        DataGridRow row = (DataGridRow)lsvKhachHang.ItemContainerGenerator.ContainerFromItem(lsvKhachHang.Items[lsvKhachHang.SelectedIndex]);
                         var checkbox = ((DataGridTemplateColumn)lsvKhachHang.Columns[2]).CellTemplate.FindName("Gia", lsvKhachHang.Columns[2].GetCellContent(row)) as TextBox;
                        if(checkbox!=null)
                        {
                            checkbox.IsEnabled = true;
                        }

                    var db = ((DataGridTemplateColumn)lsvKhachHang.Columns[1]).CellTemplate.FindName("Ngay", lsvKhachHang.Columns[1].GetCellContent(row)) as DatePicker;
                    if (db != null)
                    {
                        db.IsEnabled = true;
                    }
                }
                else
                {
                    //Hoàn thành
                    item.Content = "Sửa";
                    item.Foreground = System.Windows.Media.Brushes.Red;
                    this.Content.DataContext = null;
                    this.Content.DataContext = danhSach;
                }
            }
        }

        private void lsvKhachHang_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
