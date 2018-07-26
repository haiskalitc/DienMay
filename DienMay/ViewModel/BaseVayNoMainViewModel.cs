using Service.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DienMay.ViewModel
{
    public class BaseVayNoMainViewModel
    {
        public KHACHHANGVAYLAI ThongTinKhacHang { get; set; }
        public TRANGTHAI TrangThai { get; set; }
        public VAYLAI ThongTinMuaHang { get; set; }

        public string MaMau { get; set; }
        public BaseVayNoMainViewModel()
        {

        }
        public BaseVayNoMainViewModel(KHACHHANGVAYLAI kh, TRANGTHAI tt, VAYLAI mh)
        {
            this.ThongTinKhacHang = kh;
            this.TrangThai = tt;
            this.ThongTinMuaHang = mh;
            if (tt.Id == 1)
            {
                //con no
                this.MaMau = "#CC0000";
            }
            else
            {
                //hoan thanh
                this.MaMau = "#006600";

            }
        }
    }
}
