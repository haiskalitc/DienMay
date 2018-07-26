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
    /// Interaction logic for ThemKhachHangVayNo.xaml
    /// </summary>
    public partial class ThemKhachHangVayNo : Window
    {
        public event EventHandler QuayLai;
        public ThemKhachHangVayNo()
        {
            InitializeComponent();
        }
        private void btnXacNhan_Click(object sender, RoutedEventArgs e)
        {
            if (txtNgayVay.SelectedDate != null)
            {
                if (long.Parse(txtSoThang.Text) > 0)
                {
                    if (txtSoTienVay.Number > 0)
                    {
                        if (txtSoLaiHangThang.Number > 0)
                        {
                            MessageBoxResult re = MessageBox.Show("Xác nhận lưu?", "Thông báo", MessageBoxButton.YesNo, MessageBoxImage.Question);
                            if (re == MessageBoxResult.Yes)
                            {
                                KHACHHANGVAYLAI khachHang = new KHACHHANGVAYLAI();
                                khachHang.SoCMND = txtCMND.Text;
                                khachHang.SoDienThoai = txtSoDienThoai.Text;
                                khachHang.TaiSanTheChap = txtTaiSanTheChap.Text;
                                khachHang.DiaChi = txtDiaChi.Text;
                                khachHang.TenKhachHang = txtTenKhachHang.Text;
                                khachHang.TrangThai = 1;
                                if (XuLyKhachHangVay.getInstance.ThemKhachHang(khachHang))
                                {
                                    VAYLAI vayLai = new VAYLAI();
                                    vayLai.IdKhachHangVay = khachHang.Id;
                                    vayLai.NgayVay = txtNgayVay.SelectedDate.Value.ToString("dd/MM/yyyy");
                                    vayLai.SoTienVay = txtSoTienVay.Number;
                                    vayLai.SoThangVay = long.Parse(txtSoThang.Text);
                                    vayLai.SoLai = txtSoLaiHangThang.Number;
                                    if (XuLyVayLai.getInstance.ThemMuaHang(vayLai))
                                    {
                                        List<CHITIETVAYLAI> ds = new List<CHITIETVAYLAI>();
                                        for (int i = 0; i < long.Parse(txtSoThang.Text); i++)
                                        {
                                            CHITIETVAYLAI chiTietVayLai = new CHITIETVAYLAI();
                                            chiTietVayLai.IdKhachHangVay = khachHang.Id;
                                            chiTietVayLai.IdVayLai = vayLai.Id;
                                            chiTietVayLai.TrangThai = 1;
                                            chiTietVayLai.SoTienConLai = txtSoLaiHangThang.Number;
                                            chiTietVayLai.ChuoiNgayTra = txtNgayVay.SelectedDate.Value.AddMonths((i + 1)).ToString("dd/MM/yyyy");
                                            if (chiTietVayLai != null)
                                            {
                                                ds.Add(chiTietVayLai);
                                            }
                                        }
                                        if (ds.Count > 0)
                                        {
                                            ChiTietLanVayThem chiTietLanTraGopThem = new ChiTietLanVayThem(ds);
                                            this.Hide();
                                            chiTietLanTraGopThem.Back += (sen, arg) =>
                                            {
                                                //Xoa ds chi tiết tạm
                                                //Xóa khách hàng
                                                //Xóa mua hàng
                                                XuLyKhachHangVay.getInstance.XoaKhachHangTheoKhachHang(khachHang);
                                                XuLyVayLai.getInstance.XuLyXoaMuaHang(vayLai);
                                                 ds.Clear();
                                                (sen as ChiTietLanVayThem).Close();
                                                this.Show();
                                            };
                                            chiTietLanTraGopThem.BackLuu += (sen, arg) =>
                                            {

                                                MessageBox.Show("Thêm thành công!", "Thông báo", MessageBoxButton.OK);
                                                (sen as ChiTietLanVayThem).Close();
                                                txtTenKhachHang.Text = "";
                                                txtSoDienThoai.Text = "";
                                                txtCMND.Text = "";
                                                txtDiaChi.Text = "";
                                                txtTaiSanTheChap.Text = "";
                                                txtSoTienVay.Number = 0;
                                                txtLaiTamTinh.Number = 0;
                                                txtSoLaiHangThang.Number = 0;
                                                txtSoThang.Text = "0";
                                                this.Show();
                                            };
                                            chiTietLanTraGopThem.ShowDialog();

                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("Số lãi hàng tháng cho vay không hợp lệ!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Số tiền cho vay không hợp lệ!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Số tháng cho vay không hợp lệ!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
            else
            {
                MessageBox.Show("Chưa chọn ngày vay!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }
        private void btnDong_Click(object sender, RoutedEventArgs e)
        {
            QuayLai(this, new EventArgs());
        }
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        private void txtDaTraTruoc_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                txtLaiTamTinh.Number = txtSoLaiHangThang.Number * long.Parse(txtSoThang.Text);
            }
            catch (Exception)
            {
            }
        }
        private void txtSoThang_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                txtLaiTamTinh.Number = txtSoLaiHangThang.Number * long.Parse(txtSoThang.Text);
            }
            catch (Exception)
            {
            }
        }
    }
}
