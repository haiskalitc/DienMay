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
    /// Interaction logic for ThongTinChiTietVayNo.xaml
    /// </summary>
    public partial class ThongTinChiTietVayNo : Window
    {
        private BaseVayNoMainViewModel itemSelected;
        public event EventHandler Back;
        NotifiableCollection<ChiTietVayNoModel> danhSach = new NotifiableCollection<ChiTietVayNoModel>();
        List<CHITIETVAYLAI> dsChiTietMuaHang = new List<CHITIETVAYLAI>();

        public ThongTinChiTietVayNo(BaseVayNoMainViewModel itemSelected)
        {
            InitializeComponent();
            this.itemSelected = itemSelected;
            if (itemSelected != null)
            {
                DoDuLieu(itemSelected.ThongTinKhacHang, itemSelected.ThongTinMuaHang);
                dsChiTietMuaHang = XuLyChiTietVayNo.getInstance.DocDanhSachTheoIdKhachHang(itemSelected.ThongTinKhacHang == null ? 0 : itemSelected.ThongTinKhacHang.Id);
                if (dsChiTietMuaHang.Count > 0)
                {
                    for (int i = 0; i < dsChiTietMuaHang.Count; i++)
                    {
                        danhSach.Add(new ChiTietVayNoModel("Lần" + (i + 1), dsChiTietMuaHang[i]));
                    }
                    if (this.gridDanhSachChiTietMuaHang.DataContext == null)
                    {
                        this.gridDanhSachChiTietMuaHang.DataContext = danhSach;
                        LoadLaiSoNo();
                    }
                    else
                    {
                        this.gridDanhSachChiTietMuaHang.DataContext = null;
                        this.gridDanhSachChiTietMuaHang.DataContext = danhSach;
                        LoadLaiSoNo();
                    }
                }
                else
                {
                    txtTrangThai.Text = "Hoàn thành";
                }
            }
        }
        public void LoadLaiSoNo()
        {
            long so = 0;
            foreach (var item in danhSach)
            {
                so += item.MuaHang.SoTienConLai;
            }
            txtTongConNo.Number = so;//.ToString();`1
        }
        public void DoDuLieu(KHACHHANGVAYLAI khTemp, VAYLAI mhTemp)
        {
            if (khTemp != null)
            {
                txtTenKhachHang.Text = khTemp.TenKhachHang;
                txtSoDienThoai.Text = khTemp.SoDienThoai;
                txtDiaChi.Text = khTemp.DiaChi;
                txtSoCMND.Text = khTemp.SoCMND;
            }
            if (mhTemp != null)
            {
                txtNgayVay.Text = mhTemp.NgayVay;
                txtTaiSanTheChap.Text = khTemp.TaiSanTheChap ;
                txtTienVay.Number = mhTemp.SoTienVay;
                txtTienLai.Number = mhTemp.SoLai;
                txtSoThang.Text = mhTemp.SoThangVay+"";
            }
            if (khTemp.TrangThai == 1)
            {
                txtTrangThai.Text = "Còn nợ";
            }
            else
            {
                txtTrangThai.Text = "Hoàn thành";
            }

        }
        private void lsvKhachHang_MouseUp_1(object sender, MouseButtonEventArgs e)
        {

        }

        private void btnSua_Click(object sender, RoutedEventArgs e)
        {
            if (danhSach != null)
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

                        ChiTietVayNoModel chiTiet = danhSach[lsvKhachHang.SelectedIndex];
                        chiTiet.MuaHang.ChuoiNgayTra = chiTiet.NgayPhaiTra.ToString("dd/MM/yyyy");
                        if (chiTiet.MuaHang.SoTienConLai == 0)
                        {
                            chiTiet.MuaHang.TrangThai = 2;
                        }
                        else
                        {
                            chiTiet.MuaHang.TrangThai = 1;
                        }
                        XuLyChiTietVayNo.getInstance.SuaChiTietMuaHang(chiTiet.MuaHang);

                        long so = 0;
                        foreach (var itemChiTiet in danhSach)
                        {
                            so += itemChiTiet.MuaHang.SoTienConLai;
                        }
                        txtTongConNo.Number = so;//.ToString();

                        if (so == 0)
                        {
                            itemSelected.ThongTinKhacHang.TrangThai = 2;
                            XuLyKhachHangVay.getInstance.SuaKhachHang(itemSelected.ThongTinKhacHang);
                        }
                        else
                        {
                            itemSelected.ThongTinKhacHang.TrangThai = 1;
                            XuLyKhachHangVay.getInstance.SuaKhachHang(itemSelected.ThongTinKhacHang);
                        }

                        DoDuLieu(itemSelected.ThongTinKhacHang, itemSelected.ThongTinMuaHang);
                        this.gridDanhSachChiTietMuaHang.DataContext = null;
                        this.gridDanhSachChiTietMuaHang.DataContext = danhSach;

                    }
                }
            }
        }
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void btnThoat_Click(object sender, RoutedEventArgs e)
        {
            Back(this, new EventArgs());
        }
    }
}
