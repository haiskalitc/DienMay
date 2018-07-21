using Service.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Service.XuLy
{
    public class XuLyKhachHang
    {
        public DienMayThanhDanhEntities db = new DienMayThanhDanhEntities();
        private static XuLyKhachHang _instance;
        public static XuLyKhachHang getInstance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new XuLyKhachHang();
                }
                return _instance;
            }
        }
        public XuLyKhachHang()
        {
            db = new DienMayThanhDanhEntities();
        }
        public List<KHACHANG> DocDanhSachTatCa()
        {
            return db.KHACHANGs.ToList();
        }
        public KHACHANG DocTheoId(long id)
        {
            KHACHANG kh = db.KHACHANGs.FirstOrDefault(model => model.Id.Equals(id));
            if (kh != null)
            {
                return kh;
            }
            return null;
        }
        public bool ThemKhachHang(KHACHANG kh)
        {
            if (kh != null)
            {
                try
                {
                    db.KHACHANGs.Add(kh);
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
    }
}
