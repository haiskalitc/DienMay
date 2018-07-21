using Service.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Service.XuLy
{
    public class XuLyMuaHang
    {
        public DienMayThanhDanhEntities db = new DienMayThanhDanhEntities();
        private static XuLyMuaHang _instance;
        public static XuLyMuaHang getInstance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new XuLyMuaHang();
                }
                return _instance;
            }
        }
        public XuLyMuaHang()
        {
            db = new DienMayThanhDanhEntities();
        }

        public List<MUAHANG> DocDanhSachTatCa()
        {
            return db.MUAHANGs.ToList();
        }
        public MUAHANG DocTheoId(long id)
        {
            MUAHANG kh = db.MUAHANGs.FirstOrDefault(model => model.Id.Equals(id));
            if (kh != null)
            {
                return kh;
            }
            return null;
        }
        public MUAHANG DocTheoIdKhachHang(long id)
        {
            MUAHANG kh = db.MUAHANGs.FirstOrDefault(model => model.IdKhachHang.Equals(id));
            if (kh != null)
            {
                return kh;
            }
            return null;
        }
        public bool ThemMuaHang(MUAHANG mh)
        {
            if (mh != null)
            {
                try
                {
                    db.MUAHANGs.Add(mh);
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

