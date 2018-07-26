using DienMay.ViewModel;
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
    /// Interaction logic for DanhSachToiHangTraNoVay.xaml
    /// </summary>
    public partial class DanhSachToiHangTraNoVay : Window
    {
        NotifiableCollection<BaseVayNoMainViewModel> danhSachKhacHang = new NotifiableCollection<BaseVayNoMainViewModel>();
        public event EventHandler Back;
        public DanhSachToiHangTraNoVay()
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
            var itemSelected = lsvKhachHang.SelectedItem as BaseVayNoMainViewModel;
            ThongTinChiTietVayNo th = new ThongTinChiTietVayNo(itemSelected);
            th.Back += (sen, arg) =>
            {
                (sen as ThongTinChiTietVayNo).Close();
                LayDuLieu(danhSachKhacHang, txtNgayToiHang.SelectedDate.Value.ToString("dd/MM/yyyy"));
                this.Show();
            };
            this.Hide();
            th.Show();
        }
        public void LayDuLieu(NotifiableCollection<BaseVayNoMainViewModel> ds, string chuoiNgay)
        {
            try
            {
                ds.Clear();
                List<CHITIETVAYLAI> dsChiTiet = XuLyChiTietVayNo.getInstance.DocDanhSachTatCa(chuoiNgay);
                if (dsChiTiet.Count > 0)
                {
                    foreach (var item in dsChiTiet)
                    {
                        KHACHHANGVAYLAI khachHang = XuLyKhachHangVay.getInstance.DocTheoId(item.IdKhachHangVay);
                        if (khachHang != null)
                        {
                            ds.Add(new BaseVayNoMainViewModel(
                                khachHang,
                                XuLyTrangThai.getInstance.DocTheoId(khachHang.TrangThai),
                                XuLyVayLai.getInstance.DocTheoIdKhachHang(khachHang.Id)));
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void txtNgayToiHang_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            LayDuLieu(danhSachKhacHang, txtNgayToiHang.SelectedDate.Value.ToString("dd/MM/yyyy"));
            if (gridDanhSach.DataContext == null)
            {
                gridDanhSach.DataContext = danhSachKhacHang;
            }
        }
    }
}
