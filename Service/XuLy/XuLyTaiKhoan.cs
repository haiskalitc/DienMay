using Service.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Service.XuLy
{
    public class XuLyTaiKhoan
    {
        public DienMayThanhDanhEntities db = new DienMayThanhDanhEntities();
        private static XuLyTaiKhoan _instance;
        public static XuLyTaiKhoan getInstance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new XuLyTaiKhoan();
                }
                return _instance;
            }
        }
        public XuLyTaiKhoan()
        {
            db = new DienMayThanhDanhEntities();
        }

        public bool ThemTaiKhoan(TAIKHOAN taiKhoan)
        {
            if (taiKhoan != null)
            {
                try
                {
                    db.TAIKHOANs.Add(taiKhoan);
                    if (db.SaveChanges() >= 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
            return false;
        }
        public bool DangNhap(string taiKhoan, string matKhau)
        {
            if (string.IsNullOrEmpty(taiKhoan) && string.IsNullOrEmpty(matKhau))
            {
                return false;
            }
            else
            {
                if (db.TAIKHOANs.FirstOrDefault(model => model.TaiKhoan1.Equals(taiKhoan) && model.MatKhau.Equals(matKhau)) != null)
                {
                    return true;
                }
                else
                    return false;
            }
        }
    }
}
