using Service.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Service.XuLy
{
    public class XuLyChiTietMuaHang
    {
        public DienMayThanhDanhEntities db = new DienMayThanhDanhEntities();
        private static XuLyChiTietMuaHang _instance;
        public static XuLyChiTietMuaHang getInstance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new XuLyChiTietMuaHang();
                }
                return _instance;
            }
        }

        public List<CHITIETMUAHANG> DocDanhSachTheoIdKhachHang(long id)
        {
            return db.CHITIETMUAHANGs.Where(model => model.IdKhachHang == id).ToList();
        }

        public XuLyChiTietMuaHang()
        {
            db = new DienMayThanhDanhEntities();
        }

        public bool ThemChiTietMuaHang(CHITIETMUAHANG chiTiet)
        {
            if (chiTiet != null)
            {
                try
                {
                    db.CHITIETMUAHANGs.Add(chiTiet);
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

        public bool XoaChiTietMuaHang(CHITIETMUAHANG chiTiet)
        {
            if (chiTiet != null)
            {
                try
                {
                    db.CHITIETMUAHANGs.Remove(chiTiet);
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
