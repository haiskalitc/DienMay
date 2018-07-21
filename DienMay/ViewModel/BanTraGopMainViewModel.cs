﻿using DienMay.ViewModel.Base;
using Service.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DienMay.ViewModel
{
    public class BanTraGopMainViewModel : BaseNotifyPropertyChange
    {
        public KHACHANG ThongTinKhacHang { get; set; }
        public TRANGTHAI TrangThai { get; set; }
        public MUAHANG ThongTinMuaHang { get; set; }
        public BanTraGopMainViewModel()
        {

        }
        public BanTraGopMainViewModel(KHACHANG kh, TRANGTHAI tt,MUAHANG mh)
        {
            this.ThongTinKhacHang = kh;
            this.TrangThai = tt;
            this.ThongTinMuaHang = mh;
        }
    }
}