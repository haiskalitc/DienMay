using DienMay.ViewModel;
using DienMay.ViewModel.Base;
using Service.DataBase;
using Service.XuLy;
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
    /// Interaction logic for ThemKhachHangMuaTraGop.xaml
    /// </summary>
    public partial class ThemKhachHangMuaTraGop : Window
    {
        public long hinhThuc = 1;
        List<HINHTHUC> danhSachHinhThucMua = new List<HINHTHUC>();
        public event EventHandler QuayLai;
        public ThemKhachHangMuaTraGop()
        {
            InitializeComponent();
            danhSachHinhThucMua = XuLyHinhThuc.getInstance.DocDanhSachTatCa();
            dataHinhThucMua.DataContext = danhSachHinhThucMua;
        }

        private void btnDong_Click(object sender, RoutedEventArgs e)
        {
            QuayLai(this, new EventArgs());
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            hinhThuc = (long)cbbHinhThucMua.SelectedValue;
            txtDaTraTruoc.Text = txtGiaSanPham.Text;
            txtConNo.Text = "0";
            txtSoThangPhaiTra.Text = "0";
            if (hinhThuc == 1)
            {
                txtDaTraTruoc.IsReadOnly = true;
                txtSoThangPhaiTra.IsReadOnly = true;

            }
            else
            {
                txtDaTraTruoc.IsReadOnly = txtSoThangPhaiTra.IsReadOnly = false;
            }
        }

        private void txtGiaSanPham_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (hinhThuc == 1)
            {
                try
                {
                    txtDaTraTruoc.Text = txtGiaSanPham.Text;
                }
                catch (Exception)
                {
                }
            }
            else
            {
                try
                {
                    if (!string.IsNullOrEmpty(txtDaTraTruoc.Text.Trim()))
                    {
                        long result = long.Parse(string.IsNullOrEmpty(txtGiaSanPham.Text.Trim()) ? "0" : txtGiaSanPham.Text.Trim()) -
                            long.Parse(string.IsNullOrEmpty(txtDaTraTruoc.Text.Trim()) ? "0" : txtDaTraTruoc.Text.Trim());
                        if (result > 0)
                        {
                            txtConNo.Text = result + "";
                            txtConNo.Foreground = System.Windows.Media.Brushes.Black;
                        }
                        else
                        {
                            txtConNo.Text = result + "";
                            txtConNo.Foreground = System.Windows.Media.Brushes.Red;
                        }

                    }
                }
                catch (Exception)
                { }
            }
        }

        private void btnXacNhan_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtSoThangPhaiTra.Text))
            {
                MessageBox.Show("Thông báo", "Số tháng phải trả không hợp lệ!");
                return;
            }
            MessageBoxResult re = MessageBox.Show( "Xác nhận lưu?", "Thông báo!", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (re == MessageBoxResult.Yes)
            {
                KHACHANG khachHang = new KHACHANG();
                khachHang.TenKhachHang = txtTenKhachHang.Text;
                khachHang.SoDienThoai = txtSoDienThoai.Text;
                khachHang.TenNguoiBaoLanh = txtTenNguoiBaoLanh.Text;
                khachHang.DiaChi = txtDiaChi.Text;
                if (int.Parse(txtConNo.Text) == 0)
                {
                    khachHang.IdTrangThai = 2;
                }
                else
                {
                    khachHang.IdTrangThai = 1;
                }
                if (XuLyKhachHang.getInstance.ThemKhachHang(khachHang))
                {
                    MUAHANG muaHang = new MUAHANG();
                    muaHang.IdKhachHang = khachHang.Id;
                    muaHang.ChuoiNgayMua = txtNgayMua.Text;
                    muaHang.GiaSanPham = long.Parse(txtGiaSanPham.Text);
                    muaHang.SoThangTra = long.Parse(txtSoThangPhaiTra.Text);
                    muaHang.TraTruoc = long.Parse(txtDaTraTruoc.Text);
                    muaHang.ConLai = long.Parse(txtConNo.Text);
                    muaHang.TenSanPham = txtTenSanPham.Text;
                    if (XuLyMuaHang.getInstance.ThemMuaHang(muaHang))
                    {
                        if (khachHang.IdTrangThai == 2)
                        {
                            MessageBox.Show("Thêm thành công!", "Thông báo", MessageBoxButton.OK);
                            txtTenKhachHang.Text = "";
                            txtSoDienThoai.Text = "";
                            txtTenNguoiBaoLanh.Text = "";
                            txtDiaChi.Text = "";
                            txtGiaSanPham.Text = "0";
                            txtSoThangPhaiTra.Text = "0";
                            txtDaTraTruoc.Text = "0";
                            txtTenSanPham.Text = "";
                            txtNgayMua.Text = "";
                            txtConNo.Text = "0";


                        }
                        else
                        {

                        }
                    }

                }
            }
            else { }
        }
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void txtNgayMua_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txtDaTraTruoc_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (hinhThuc == 1)
                {
                }
                else
                {
                    if (!string.IsNullOrEmpty(txtDaTraTruoc.Text.Trim()))
                    {
                        long result = long.Parse(string.IsNullOrEmpty(txtGiaSanPham.Text.Trim()) ? "0" : txtGiaSanPham.Text.Trim()) -
                            long.Parse(string.IsNullOrEmpty(txtDaTraTruoc.Text.Trim()) ? "0" : txtDaTraTruoc.Text.Trim());
                        if (result > 0)
                        {
                            txtConNo.Text = result + "";
                            txtConNo.Foreground = System.Windows.Media.Brushes.Black;
                        }
                        else
                        {
                            txtConNo.Text = result + "";
                            txtConNo.Foreground = System.Windows.Media.Brushes.Red;
                        }

                    }
                }
            }
            catch (Exception) { }
        }
    }
}
