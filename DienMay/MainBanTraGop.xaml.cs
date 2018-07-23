using DienMay.ViewModel;
using DienMay.ViewModel.Base;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DienMay
{
    /// <summary>
    /// Interaction logic for MainBanTraGop.xaml
    /// </summary>
    public partial class MainBanTraGop : UserControl
    {
        public int pageIndex = 1;
        private int numberOfRecPerPage = 12;
        private enum PagingMode { First = 1, Next = 2, Previous = 3, Last = 4, PageCountChange = 5 };
        NotifiableCollection<BanTraGopMainViewModel> danhSachKhacHang = new NotifiableCollection<BanTraGopMainViewModel>();
        NotifiableCollection<BanTraGopMainViewModel> danhSachKhacHangPhanTrang = new NotifiableCollection<BanTraGopMainViewModel>();
        private MainView mainView;

        public MainBanTraGop(MainView mainView)
        {
            InitializeComponent();
            this.mainView = mainView;
            try
            {
                LayDuLieu(danhSachKhacHang);

                danhSachKhacHangPhanTrang.Clear();
                danhSachKhacHang.Take(numberOfRecPerPage).ToList().ForEach(model => { danhSachKhacHangPhanTrang.Add(model); });

            }
            catch (Exception)
            {
            }
            finally
            {
                if (this.lsvKhachHang.DataContext == null)
                {
                    this.lsvKhachHang.DataContext = danhSachKhacHangPhanTrang;
                }
            }

        }

  
        public void LayDuLieu(NotifiableCollection<BanTraGopMainViewModel> ds)
        {
            try
            {
                ds.Clear();
                XuLyKhachHang.getInstance.DocDanhSachTatCa().ForEach(model =>
                    ds.Add(new BanTraGopMainViewModel(model,
                    XuLyTrangThai.getInstance.DocTheoId(model.IdTrangThai),
                    XuLyMuaHang.getInstance.DocTheoIdKhachHang(model.Id))));
            }
            catch (Exception)
            {
            }
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            Navigate((int)PagingMode.Next);
        }
        private void btnPrev_Click(object sender, RoutedEventArgs e)
        {
            Navigate((int)PagingMode.Previous);
        }
        private void LsvDanhSachCauHoi_OnSelected(object sender, MouseButtonEventArgs e)
        {
            var itemSelect = lsvKhachHang.SelectedItem as BanTraGopMainViewModel;
            if (itemSelect != null)
            {
                ThongTinChiTietKhachHang thongTin = new ThongTinChiTietKhachHang(itemSelect);
                thongTin.Back += ThongTin_Back;
                thongTin.Show();
                mainView.Hide();
            }
        }

        private void ThongTin_Back(object sender, EventArgs e)
        {
            (sender as ThongTinChiTietKhachHang).Close();
            mainView.Show();
        }

        private void Navigate(int mode)
        {
            int count;
            switch (mode)
            {
                case (int)PagingMode.Next:
                    btnPrev.Visibility = Visibility.Visible;
                    if (danhSachKhacHang.Count >= (pageIndex * numberOfRecPerPage))
                    {
                        if (danhSachKhacHang.Skip(pageIndex *
                        numberOfRecPerPage).Take(numberOfRecPerPage).Count() == 0)
                        {
                            danhSachKhacHangPhanTrang.Clear();
                            danhSachKhacHang.Skip((pageIndex *
                            numberOfRecPerPage) - numberOfRecPerPage).Take(numberOfRecPerPage).ToList().ForEach(model => { danhSachKhacHangPhanTrang.Add(model); });
                            count = (pageIndex * numberOfRecPerPage) +
                            (danhSachKhacHang.Skip(pageIndex *
                            numberOfRecPerPage).Take(numberOfRecPerPage)).Count();
                        }
                        else
                        {
                            danhSachKhacHangPhanTrang.Clear();
                            danhSachKhacHang.Skip(pageIndex *
                            numberOfRecPerPage).Take(numberOfRecPerPage).ToList().ForEach(model => { danhSachKhacHangPhanTrang.Add(model); });
                            count = (pageIndex * numberOfRecPerPage) +
                            (danhSachKhacHang.Skip(pageIndex *
                            numberOfRecPerPage).Take(numberOfRecPerPage)).Count();
                            pageIndex++;
                        }

                        lblpageInformation.Text = count + "/" + danhSachKhacHang.Count;
                        if (danhSachKhacHang.Count < (pageIndex * numberOfRecPerPage))
                        {
                            btnNext.Visibility = Visibility.Hidden;
                        }
                    }

                    else
                    {
                        btnNext.Visibility = Visibility.Hidden;
                    }

                    break;
                case (int)PagingMode.Previous:
                    btnNext.Visibility = Visibility.Visible;
                    if (pageIndex > 1)
                    {
                        pageIndex -= 1;
                        if (pageIndex == 1)
                        {
                            danhSachKhacHangPhanTrang.Clear();
                           danhSachKhacHang.Take(numberOfRecPerPage).ToList().ForEach(model => { danhSachKhacHangPhanTrang.Add(model); });
                            count = danhSachKhacHang.Take(numberOfRecPerPage).Count();
                            lblpageInformation.Text = count + "/" + danhSachKhacHang.Count;
                        }
                        else
                        {

                            danhSachKhacHangPhanTrang.Clear();
                            danhSachKhacHang.Skip
                            ((pageIndex - 1) * numberOfRecPerPage).Take(numberOfRecPerPage).ToList().ForEach(model => { danhSachKhacHangPhanTrang.Add(model); });
                            count = Math.Min(pageIndex * numberOfRecPerPage, danhSachKhacHang.Count);
                            lblpageInformation.Text = count + "/" + danhSachKhacHang.Count;
                        }
                        if (pageIndex <= 1)
                        {
                            danhSachKhacHangPhanTrang.Clear();
                           danhSachKhacHang.Take(numberOfRecPerPage).ToList().ForEach(model => { danhSachKhacHangPhanTrang.Add(model); });
                            count = danhSachKhacHang.Take(numberOfRecPerPage).Count();
                            lblpageInformation.Text = count + "/" + danhSachKhacHang.Count;
                            btnPrev.Visibility = Visibility.Hidden;
                        }
                    }
                    else
                    {
                        count = danhSachKhacHang.Take(numberOfRecPerPage).Count();
                        lblpageInformation.Text = count + "/" + danhSachKhacHang.Count;
                        btnPrev.Visibility = Visibility.Hidden;
                    }
                    break;

                case (int)PagingMode.First:
                    pageIndex = 2;
                    Navigate((int)PagingMode.Previous);
                    break;
                case (int)PagingMode.Last:
                    pageIndex = (danhSachKhacHang.Count / numberOfRecPerPage);
                    Navigate((int)PagingMode.Next);
                    break;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ThemKhachHangMuaTraGop themBanHang = new ThemKhachHangMuaTraGop();
            themBanHang.Show();
            mainView.Hide();
            themBanHang.QuayLai += ThemBanHang_QuayLai;
            
        }

        private void ThemBanHang_QuayLai(object sender, EventArgs e)
        {
            LayDuLieu(danhSachKhacHang);
            Navigate((int)PagingMode.Last);
            mainView.Show();
            (sender as ThemKhachHangMuaTraGop).Close();
        }
    }
}
