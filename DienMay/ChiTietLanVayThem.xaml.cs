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
    /// Interaction logic for ChiTietLanVayThem.xaml
    /// </summary>
    public partial class ChiTietLanVayThem : Window
    {
        public event EventHandler Back;
        public event EventHandler BackLuu;
        VAYLAI muaHang;
        KHACHHANGVAYLAI khachHang;
        public long isNo = 0;
        public long tongTienNo = -1;
        private List<CHITIETVAYLAI> ds;
        NotifiableCollection<ChiTietVayNoModel> danhSach = new NotifiableCollection<ChiTietVayNoModel>();
        public ChiTietLanVayThem(List<CHITIETVAYLAI> ds)
        {
            InitializeComponent();
            this.ds = ds;
            try
            {
                if (ds != null)
                {
                    if (ds.Count > 0)
                    {
                        for (int i = 0; i < ds.Count; i++)
                        {
                            danhSach.Add(new ChiTietVayNoModel("Lần" + (i + 1), ds[i]));
                        }
                        if (this.Content.DataContext == null)
                        {
                            this.DataContext = danhSach;
                        }
                        try
                        {
                            khachHang = XuLyKhachHangVay.getInstance.DocTheoId(ds[0].IdKhachHangVay);
                        }
                        catch (Exception)
                        {
                            khachHang = null;
                        }
                        try
                        {
                            muaHang = XuLyVayLai.getInstance.DocTheoId(ds[0].IdVayLai);
                        }
                        catch (Exception)
                        {
                            muaHang = null;
                        }
                    }
                }
            }
            catch (Exception)
            {

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
                foreach (ChiTietVayNoModel item in danhSach)
                {
                    item.MuaHang.ChuoiNgayTra = item.NgayPhaiTra.ToString("dd/MM/yyyy");
                    XuLyChiTietVayNo.getInstance.ThemChiTietMuaHang(item.MuaHang);
                }
                BackLuu(this, new EventArgs());
                return;
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
            if (ds != null)
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
                        this.Content.DataContext = null;
                        this.Content.DataContext = danhSach;
                        tongTienNo = 0;
                        foreach (var itemDetail in danhSach)
                        {
                            tongTienNo += itemDetail.MuaHang.SoTienConLai;
                        }
                        txtSoDu.Number = tongTienNo;
                    }
                }
            }
        }
        private void lsvKhachHang_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
