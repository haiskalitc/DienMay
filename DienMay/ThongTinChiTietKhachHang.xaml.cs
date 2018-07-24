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
using DienMay.ViewModel;
using Service.DataBase;
using DienMay.ViewModel.Base;
using Service.XuLy;

namespace DienMay
{
    /// <summary>
    /// Interaction logic for ThongTinChiTietKhachHang.xaml
    /// </summary>
    public partial class ThongTinChiTietKhachHang : Window
    {
        private BanTraGopMainViewModel itemSelected;
        public event EventHandler Back;
        NotifiableCollection<ChiTietTraGopModel> danhSach = new NotifiableCollection<ChiTietTraGopModel>();
        List<CHITIETMUAHANG> dsChiTietMuaHang = new List<CHITIETMUAHANG>();
        public ThongTinChiTietKhachHang()
        {
            InitializeComponent();
        }

        public ThongTinChiTietKhachHang(BanTraGopMainViewModel itemSelected)
        {
            InitializeComponent();
            this.itemSelected = itemSelected;
            if (itemSelected != null)
            {
                DoDuLieu(itemSelected.ThongTinKhacHang, itemSelected.ThongTinMuaHang);
                dsChiTietMuaHang = XuLyChiTietMuaHang.getInstance.DocDanhSachTheoIdKhachHang(itemSelected.ThongTinKhacHang == null ? 0 : itemSelected.ThongTinKhacHang.Id);
                if (dsChiTietMuaHang.Count > 0)
                {
                    for (int i = 0; i < dsChiTietMuaHang.Count; i++)
                    {
                        danhSach.Add(new ChiTietTraGopModel("Lần" + (i + 1), dsChiTietMuaHang[i]));
                    }
                    if (this.gridDanhSachChiTietMuaHang.DataContext == null)
                    {
                        this.gridDanhSachChiTietMuaHang.DataContext = danhSach;
                        LoadLaiSoNo(danhSach);
                    }
                }
                else
                {
                    txtTrangThai.Text = "Hoàn thành";
                }
            }
        }
        public void DoDuLieu(KHACHANG khTemp, MUAHANG mhTemp)
        {
            if (khTemp != null)
            {
                txtTenKhachHang.Text = khTemp.TenKhachHang;
                txtSoDienThoai.Text = khTemp.SoDienThoai;
                txtDiaChi.Text = khTemp.DiaChi;
            }
            if (mhTemp != null)
            {
                txtTenSanPham.Text = mhTemp.TenSanPham;
                txtNgayMua.Text = mhTemp.ChuoiNgayMua;
                txtSoThangTra.Text = mhTemp.SoThangTra.ToString();
                txtGiaSanPham.Text = mhTemp.GiaSanPham.ToString();
                txtDaTraTruoc.Text = mhTemp.TraTruoc.ToString();
                txtTenNguoiBaoLanh.Text = khTemp.TenNguoiBaoLanh;
            }

        }
        public void LoadLaiSoNo(NotifiableCollection<ChiTietTraGopModel> danhSachT)
        {
            long so = 0;
            foreach (var item in danhSachT)
            {
                so += item.MuaHang.SoTienConLai;
            }
            txtTongConNo.Text = so.ToString();
            if (so > 0)
            {
                txtTrangThai.Text = "Còn nợ";
            }
            else if (so == 0)   
            {
                txtTrangThai.Text = "Hoàn thành";
            }
        }

        private void btnThoat_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult mess = MessageBox.Show("Xác nhận thoát?", "Thông báo", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (mess == MessageBoxResult.Yes)
            {
                Back(this, new EventArgs());
            }
        }

        private void lsvKhachHang_MouseUp_1(object sender, MouseButtonEventArgs e)
        {

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
                    if (checkbox != null)
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





                    danhSach.Clear();
                    dsChiTietMuaHang.Clear();
                    for (int i = 0; i < dsChiTietMuaHang.Count; i++)
                    {
                        danhSach.Add(new ChiTietTraGopModel("Lần" + (i + 1), dsChiTietMuaHang[i]));
                    }
                    if (this.gridDanhSachChiTietMuaHang.DataContext == null)
                    {
                        this.gridDanhSachChiTietMuaHang.DataContext = danhSach;
                        LoadLaiSoNo(danhSach);
                    }
                }
            }
        }
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
