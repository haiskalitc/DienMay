using DienMay.ViewModel.Base;
using Service.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DienMay.ViewModel
{
    class ChiTietVayNoModel : BaseNotifyPropertyChange
    {
        public string STT { get; set; }
        public string TrangThai => muaHang.SoTienConLai != 0 ? "Còn nợ" : "Hoàn thành";
        public string MaMau => muaHang.SoTienConLai != 0 ? "#CC0000" : "#006600";


        private DateTime ngayPhaiTra;


        public DateTime NgayPhaiTra
        {
            get { return ngayPhaiTra; }
            set
            {
                SetProperty(ref ngayPhaiTra, value);
            }
        }
        private CHITIETVAYLAI muaHang;
        public CHITIETVAYLAI MuaHang
        {
            get { return muaHang; }
            set
            {
                if (value != null)
                {
                    SetProperty(ref muaHang, value, () => RaisePropertyChanged(nameof(MuaHang)));
                }
            }
        }
        public ChiTietVayNoModel(string stt, CHITIETVAYLAI chiTiet)
        {
            this.STT = stt;
            this.muaHang = chiTiet;
            ngayPhaiTra = new DateTime(int.Parse(muaHang.ChuoiNgayTra.Split('/')[2]),
            int.Parse(muaHang.ChuoiNgayTra.Split('/')[1]),
            int.Parse(muaHang.ChuoiNgayTra.Split('/')[0]));
        }
    }
}
