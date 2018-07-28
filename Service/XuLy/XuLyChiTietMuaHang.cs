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
                _instance = new XuLyChiTietMuaHang();               
                return _instance;
            }
        }
        public List<CHITIETMUAHANG> DocDanhSachTatCa(string chuoiNgay)
        {
            return db.CHITIETMUAHANGs.Where(model => model.ChuoiNgayTra.Equals(chuoiNgay)&& model.DaHoanThanh == 1).ToList();
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
                catch (Exception)
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
                  //  db.CHITIETMUAHANGs.Remove(chiTiet);
                    db.Entry(chiTiet).State = System.Data.Entity.EntityState.Deleted;

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

        public bool SuaChiTietMuaHang(CHITIETMUAHANG chiTiet)
        {
            if (chiTiet != null)
            {
                try
                {
                    var chiTietMuaHangSua = db.CHITIETMUAHANGs.FirstOrDefault(model => model.Id == chiTiet.Id);
                    if (chiTietMuaHangSua != null)
                    {
                        chiTietMuaHangSua.IdKhachHang = chiTiet.IdKhachHang;
                        chiTietMuaHangSua.IdMuaHang = chiTiet.IdMuaHang;
                        chiTietMuaHangSua.NgayTra = chiTiet.NgayTra;
                        chiTietMuaHangSua.SoTienConLai = chiTiet.SoTienConLai;
                        chiTietMuaHangSua.ChuoiNgayTra = chiTiet.ChuoiNgayTra;
                        chiTietMuaHangSua.DaHoanThanh = chiTiet.DaHoanThanh;
                    }

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
