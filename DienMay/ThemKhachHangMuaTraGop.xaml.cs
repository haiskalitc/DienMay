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
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
/// <summary>
/// TRẠNG THÁI 
/// 1 : Nợ
/// 2 : Hoàn thành
/// 
/// HÌNH THỨC
/// 1: Đủ
/// 2: Góp
/// </summary>
/// 
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
        ModelThemKhachHang modelThemKhachHang = null;
        public ThemKhachHangMuaTraGop()
        {
            InitializeComponent();
            danhSachHinhThucMua = XuLyHinhThuc.getInstance.DocDanhSachTatCa();
            dataHinhThucMua.DataContext = danhSachHinhThucMua;
            modelThemKhachHang = new ModelThemKhachHang()
            {
                GiaSanPham = new Gia() { GiaHienThi = 100000
                    },
                GiaTraTruoc = new Gia() { GiaHienThi = 2000}
            };
            if(GiaCa.DataContext==null)
            {
                GiaCa.DataContext = modelThemKhachHang;
            }
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

            //if (hinhThuc == 1)
            //{
            //    try
            //    {
            //        txtDaTraTruoc.Text = txtGiaSanPham.Text;
            //    }
            //    catch (Exception)
            //    {
            //    }
            //}
            //else
            //{
            //    try
            //    {
            //        if (!string.IsNullOrEmpty(txtDaTraTruoc.Text.Trim()))
            //        {
            //            long result = long.Parse(string.IsNullOrEmpty(txtGiaSanPham.Text.Trim()) ? "0" : txtGiaSanPham.Text.Trim()) -
            //                long.Parse(string.IsNullOrEmpty(txtDaTraTruoc.Text.Trim()) ? "0" : txtDaTraTruoc.Text.Trim());
            //            if (result > 0)
            //            {
            //                txtConNo.Text = result + "";
            //                txtConNo.Foreground = System.Windows.Media.Brushes.Black;
            //            }
            //            else
            //            {
            //                txtConNo.Text = result + "";
            //                txtConNo.Foreground = System.Windows.Media.Brushes.Red;
            //            }

            //        }
            //    }
            //    catch (Exception)
            //    { }
            //}
        }

        private void btnXacNhan_Click(object sender, RoutedEventArgs e)
        {
            if (long.Parse(txtConNo.Text) < 0)
            {
                MessageBox.Show("Số tiền không hợp lệ!", "Thông báo");
                return;
            }
            if (txtNgayMua.SelectedDate == null)
            {
                MessageBox.Show("Chưa chọn ngày bán!", "Thông báo");
                return;
            }
            if (string.IsNullOrEmpty(txtSoThangPhaiTra.Text))
            {
                MessageBox.Show("Số tháng phải trả không hợp lệ!","Thông báo");
                return;
            }
            if (int.Parse(txtConNo.Text) != 0)
            {
                if (int.Parse(txtSoThangPhaiTra.Text) == 0)
                {
                    MessageBox.Show("Số tháng phải trả không hợp lệ!", "Thông báo");
                    return;
                }
            }
            MessageBoxResult re = MessageBox.Show("Xác nhận lưu?", "Thông báo!", MessageBoxButton.YesNo, MessageBoxImage.Question);
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
                    muaHang.ChuoiNgayMua = txtNgayMua.SelectedDate.Value.ToString("dd/MM/yyyy");
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
                            List<CHITIETMUAHANG> dsChiTietMuaHangTam = new List<CHITIETMUAHANG>();
                            List<long> dsChiaDeu = XuLyChung.DivideEvenly(long.Parse(txtConNo.Text), long.Parse(txtSoThangPhaiTra.Text)).ToList();
                            for (int i = 0; i < dsChiaDeu.Count; i++)
                            {
                                try
                                {
                                    CHITIETMUAHANG chiTietMuaHang = new CHITIETMUAHANG()
                                    {
                                        IdKhachHang = khachHang.Id,
                                        IdMuaHang = muaHang.Id,
                                        SoTienConLai = dsChiaDeu[i],
                                        ChuoiNgayTra = txtNgayMua.SelectedDate.Value.AddMonths((i + 1)).ToString("dd/MM/yyyy"),
                                        DaHoanThanh = dsChiaDeu[i] == 0 ? 2 : 1
                                    };
                                    if (chiTietMuaHang != null)
                                    {
                                        dsChiTietMuaHangTam.Add(chiTietMuaHang);

                                    }
                                }
                                catch (Exception) { }
                            }
                            if (dsChiTietMuaHangTam.Count > 0)
                            {
                                ChiTietLanTraGopThem chiTietLanTraGopThem = new ChiTietLanTraGopThem(dsChiTietMuaHangTam);
                                this.Hide();
                                chiTietLanTraGopThem.Back += (sen, arg) =>
                                {
                                    //Xoa ds chi tiết tạm
                                    //Xóa khách hàng
                                    //Xóa mua hàng
                                    XuLyKhachHang.getInstance.XoaKhachHangTheoKhachHang(khachHang);
                                    XuLyMuaHang.getInstance.XuLyXoaMuaHang(muaHang);
                                    dsChiTietMuaHangTam.Clear();
                                    (sen as ChiTietLanTraGopThem).Close();
                                    this.Show();
                                };
                                chiTietLanTraGopThem.BackLuu += (sen, arg) =>
                                {
      
                                    MessageBox.Show("Thêm thành công!", "Thông báo", MessageBoxButton.OK);
                                    (sen as ChiTietLanTraGopThem).Close();
                                    txtTenKhachHang.Text = "";
                                    txtSoDienThoai.Text = "";
                                    txtTenNguoiBaoLanh.Text = "";
                                    txtDiaChi.Text = "";
                                    txtGiaSanPham.Text = "0";
                                    txtSoThangPhaiTra.Text = "0";
                                    txtDaTraTruoc.Text = "0";
                                    txtTenSanPham.Text = "";
                                    txtNgayMua.Text = "";
                                    txtConNo.Text = "0"; this.Show();
                                };
                                chiTietLanTraGopThem.ShowDialog();

                                //Show popup
                            }
                        }
                    }

                }
            }
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
