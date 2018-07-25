using DienMay.ViewModel;
using DienMay.ViewModel.Base;
using Service.DataBase;
using Service.XuLy;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
    /// Interaction logic for DanhSachToiHangTraNo.xaml
    /// </summary>
    public partial class DanhSachToiHangTraNo : Window
    {
        NotifiableCollection<BanTraGopMainViewModel> danhSachKhacHang = new NotifiableCollection<BanTraGopMainViewModel>();
        public event EventHandler Back;
        public DanhSachToiHangTraNo()
        {
            InitializeComponent();
            txtNgayToiHang.SelectedDate = DateTime.Now;
            LayDuLieu(danhSachKhacHang, txtNgayToiHang.SelectedDate.Value.ToString("dd/MM/yyyy"));
            if (gridDanhSach.DataContext == null)
            {
                gridDanhSach.DataContext = danhSachKhacHang;
            }
        }

        private void btnThoat_Click(object sender, RoutedEventArgs e)
        {
            Back(this, new EventArgs());
        }

        private void LsvDanhSachCauHoi_OnSelected(object sender, MouseButtonEventArgs e)
        {
            var itemSelected = lsvKhachHang.SelectedItem as BanTraGopMainViewModel;
            ThongTinChiTietKhachHang th = new ThongTinChiTietKhachHang(itemSelected);
            th.Back += (sen, arg) =>
            {
                (sen as ThongTinChiTietKhachHang).Close();
                LayDuLieu(danhSachKhacHang,txtNgayToiHang.SelectedDate.Value.ToString("dd/MM/yyyy"));
                this.Show();
            };
            this.Hide();
            th.Show();
        }

        private void txtNgayToiHang_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            LayDuLieu(danhSachKhacHang, txtNgayToiHang.SelectedDate.Value.ToString("dd/MM/yyyy"));
            if (gridDanhSach.DataContext == null)
            {
                gridDanhSach.DataContext = danhSachKhacHang;
            }
        }
        public void LayDuLieu(NotifiableCollection<BanTraGopMainViewModel> ds,string chuoiNgay)
        {
            try
            {
                ds.Clear();
                List<CHITIETMUAHANG> dsChiTiet = XuLyChiTietMuaHang.getInstance.DocDanhSachTatCa(chuoiNgay);
                if (dsChiTiet.Count > 0)
                {
                    foreach(var item in dsChiTiet)
                    {
                        KHACHANG khachHang = XuLyKhachHang.getInstance.DocTheoId(item.IdKhachHang);
                        if (khachHang != null)
                        {
                            ds.Add(new BanTraGopMainViewModel(
                                khachHang,
                                XuLyTrangThai.getInstance.DocTheoId(khachHang.IdTrangThai),
                                XuLyMuaHang.getInstance.DocTheoIdKhachHang(khachHang.Id)));
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
        }
    }
}
