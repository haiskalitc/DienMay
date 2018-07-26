using Service.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Service.XuLy
{
    public class XuLyKhachHangVay
    {
        public DienMayThanhDanhEntities db = new DienMayThanhDanhEntities();
        private static XuLyKhachHangVay _instance;
        public static XuLyKhachHangVay getInstance
        {
            get
            {
          //      if (_instance == null)
            //    {
                    _instance = new XuLyKhachHangVay();
            //    }
                return _instance;
            }
        }
        public XuLyKhachHangVay()
        {
            db = new DienMayThanhDanhEntities();
        }
        public List<KHACHHANGVAYLAI> DocDanhSachTatCa()
        {
            return db.KHACHHANGVAYLAIs.ToList();
        }
        public KHACHHANGVAYLAI DocTheoId(long id)
        {
            KHACHHANGVAYLAI kh = db.KHACHHANGVAYLAIs.FirstOrDefault(model => model.Id.Equals(id));
            if (kh != null)
            {
                return kh;
            }
            return null;
        }
        public bool ThemKhachHang(KHACHHANGVAYLAI kh)
        {
            if (kh != null)
            {
                try
                {
                    db.KHACHHANGVAYLAIs.Add(kh);
                    if (db.SaveChanges() >= 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception EX)
                {
                    return false;
                }
            }
            return false;
        }
        public bool SuaKhachHang(KHACHHANGVAYLAI mh)
        {
            if (mh != null)
            {
                try
                {
                    var muaHangSua = db.KHACHHANGVAYLAIs.FirstOrDefault(model => model.Id.Equals(mh.Id));
                    if (muaHangSua != null)
                    {
                        muaHangSua.TrangThai = mh.TrangThai;
                        db.Entry(muaHangSua).State = System.Data.Entity.EntityState.Modified;
                        if (db.SaveChanges() >= 0)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }


                }
                catch (Exception)
                {
                    return false;
                }
            }
            return false;
        }
        public bool XoaKhachHangTheoKhachHang(KHACHHANGVAYLAI kh)
        {
            if (kh != null)
            {
                try
                {
                    db.KHACHHANGVAYLAIs.Remove(kh);
                    if (db.SaveChanges() >= 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return false;
        }
    }
}
