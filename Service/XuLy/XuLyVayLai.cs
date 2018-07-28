using Service.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Service.XuLy
{
    public class XuLyVayLai
    {
        public DienMayThanhDanhEntities db = new DienMayThanhDanhEntities();
        private static XuLyVayLai _instance;
        public static XuLyVayLai getInstance
        {
            get
            {
               // if (_instance == null)
               // {
                    _instance = new XuLyVayLai();
               // }
                return _instance;
            }
        }
        public XuLyVayLai()
        {
            db = new DienMayThanhDanhEntities();
        }

        public List<VAYLAI> DocDanhSachTatCa()
        {
            return db.VAYLAIs.ToList();
        }
        public VAYLAI DocTheoId(long id)
        {
            VAYLAI kh = db.VAYLAIs.FirstOrDefault(model => model.Id.Equals(id));
            if (kh != null)
            {
                return kh;
            }
            return null;
        }
        public VAYLAI DocTheoIdKhachHang(long id)
        {
            VAYLAI kh = db.VAYLAIs.FirstOrDefault(model => model.IdKhachHangVay.Equals(id));
            if (kh != null)
            {
                return kh;
            }
            return null;
        }
        public bool ThemMuaHang(VAYLAI mh)
        {
            if (mh != null)
            {
                try
                {
                    db.VAYLAIs.Add(mh);
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
        public bool SuaMuaHang(VAYLAI mh)
        {
            if (mh != null)
            {
                try
                {
                    var muaHangSua = db.VAYLAIs.FirstOrDefault(model => model.Id == mh.Id);
                    if (muaHangSua != null)
                    {
                        muaHangSua.IdKhachHangVay = mh.IdKhachHangVay;
                        muaHangSua.NgayVay = mh.NgayVay;
                        muaHangSua.SoThangVay = mh.SoThangVay;
                        muaHangSua.SoTienVay = mh.SoTienVay;
                        muaHangSua.SoLai = mh.SoLai;
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
        public bool XuLyXoaMuaHang(VAYLAI mh)
        {
            if (mh != null)
            {
                try
                {
                   // db.VAYLAIs.Remove(mh);
                    db.Entry(mh).State = System.Data.Entity.EntityState.Deleted;

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
