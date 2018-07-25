using DienMay.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DienMay.ViewModel
{
    public class ModelThemKhachHang : BaseNotifyPropertyChange
    {
        private Gia _giaSanPham;
        public Gia GiaSanPham { get { return _giaSanPham; } set { if (value != null) { SetProperty(ref _giaSanPham, value); } } }

        private Gia _giatraTruoc;
        public Gia GiaTraTruoc { get { return _giatraTruoc; } set { if (value != null) { SetProperty(ref _giatraTruoc, value); } } }
    }
}
