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
    /// Interaction logic for MainChoVay.xaml
    /// </summary>
    public partial class MainChoVay : UserControl
    {
        private MainView mainView;
        public int pageIndex = 1;
        private int numberOfRecPerPage = 12;
        private enum PagingMode { First = 1, Next = 2, Previous = 3, Last = 4, PageCountChange = 5 };
        NotifiableCollection<BaseVayNoMainViewModel> danhSachKhacHang = new NotifiableCollection<BaseVayNoMainViewModel>();
        NotifiableCollection<BaseVayNoMainViewModel> danhSachKhacHangPhanTrang = new NotifiableCollection<BaseVayNoMainViewModel>();
        public MainChoVay()
        {
            InitializeComponent();
        }

        public MainChoVay(MainView mainView)
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
        public void LayDuLieu(NotifiableCollection<BaseVayNoMainViewModel> ds)
        {
            try
            {
                ds.Clear();
                XuLyKhachHangVay.getInstance.DocDanhSachTatCa().ForEach(model =>
                    ds.Add(new BaseVayNoMainViewModel(model,
                    XuLyTrangThai.getInstance.DocTheoId(model.TrangThai),
                    XuLyVayLai.getInstance.DocTheoIdKhachHang(model.Id))));
            }
            catch (Exception)
            {
            }
        }


        private void LsvDanhSachCauHoi_OnSelected(object sender, MouseButtonEventArgs e)
        {
            var itemSelected = lsvKhachHang.SelectedItem as BaseVayNoMainViewModel;
            ThongTinChiTietVayNo th = new ThongTinChiTietVayNo(itemSelected);
            th.Back += (sen, arg) =>
            {
                (sen as ThongTinChiTietVayNo).Close();
                LayDuLieu(danhSachKhacHang);
                Navigate((int)PagingMode.First);
                mainView.Show();
            };
            mainView.Hide();
            th.Show();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ThemKhachHangVayNo th = new ThemKhachHangVayNo();
            th.Show();
            mainView.Hide();
            th.QuayLai += Th_QuayLai;
        }

        private void Th_QuayLai(object sender, EventArgs e)
        {
            LayDuLieu(danhSachKhacHang);
            Navigate((int)PagingMode.Last);
            mainView.Show();
            (sender as ThemKhachHangVayNo).Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            DanhSachToiHangTraNoVay dachSachtoiHang = new DanhSachToiHangTraNoVay();
            dachSachtoiHang.Show();
            mainView.Hide();
            dachSachtoiHang.Back += DachSachtoiHang_Back;
        }

        private void DachSachtoiHang_Back(object sender, EventArgs e)
        {
            LayDuLieu(danhSachKhacHang);
            mainView.Show();
            (sender as DanhSachToiHangTraNoVay).Close();
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            Navigate((int)PagingMode.Next);
        }
        private void btnPrev_Click(object sender, RoutedEventArgs e)
        {
            Navigate((int)PagingMode.Previous);
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

    }
}
