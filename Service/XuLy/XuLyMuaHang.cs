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
        public bool SuaMuaHang(MUAHANG mh)
        {
            if (mh != null)
            {
                try
                {
                    var muaHangSua = db.MUAHANGs.FirstOrDefault(model => model.Id == mh.Id);
                    if (muaHangSua != null)
                    {
                        muaHangSua.IdKhachHang = mh.IdKhachHang;
                        muaHangSua.NgayMua = mh.NgayMua;
                        muaHangSua.SoThangTra = mh.SoThangTra;
                        muaHangSua.TenSanPham = mh.TenSanPham;
                        muaHangSua.TraTruoc = mh.TraTruoc;
                        muaHangSua.ChuoiNgayMua = mh.ChuoiNgayMua;
                        muaHangSua.GiaSanPham = mh.GiaSanPham;
                        muaHangSua.ConLai = mh.ConLai;
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
                catch (Exception ex)
                {
                    return false;
                }
            }
            return false;
        }
        public bool XuLyXoaMuaHang(MUAHANG mh)
        {
            if (mh != null)
            {
                try
                {
                    db.MUAHANGs.Remove(mh);
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

