using Service.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Service.XuLy
{
    public class XuLyChiTietVayNo
    {
        public DienMayThanhDanhEntities db = new DienMayThanhDanhEntities();
        private static XuLyChiTietVayNo _instance;
        public static XuLyChiTietVayNo getInstance
        {
            get
            {
            //    if (_instance == null)
            //    {
                    _instance = new XuLyChiTietVayNo();
              //  }
                return _instance;
            }
        }
        public List<CHITIETVAYLAI> DocDanhSachTatCa(string chuoiNgay)
        {
            return db.CHITIETVAYLAIs.Where(model => model.ChuoiNgayTra.Equals(chuoiNgay) && model.TrangThai == 1).ToList();
        }
        public List<CHITIETVAYLAI> DocDanhSachTheoIdKhachHang(long id)
        {
            return db.CHITIETVAYLAIs.Where(model => model.IdKhachHangVay == id).ToList();
        }

        public XuLyChiTietVayNo()
        {
            db = new DienMayThanhDanhEntities();
        }

        public bool ThemChiTietMuaHang(CHITIETVAYLAI chiTiet)
        {
            if (chiTiet != null)
            {
                try
                {
                    db.CHITIETVAYLAIs.Add(chiTiet);
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

        public bool XoaChiTietMuaHang(CHITIETVAYLAI chiTiet)
        {
            if (chiTiet != null)
            {
                try
                {
                    db.CHITIETVAYLAIs.Remove(chiTiet);
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

        public bool SuaChiTietMuaHang(CHITIETVAYLAI chiTiet)
        {
            if (chiTiet != null)
            {
                try
                {
                    var chiTietMuaHangSua = db.CHITIETVAYLAIs.FirstOrDefault(model => model.Id == chiTiet.Id);
                    if (chiTietMuaHangSua != null)
                    {
                        chiTietMuaHangSua.IdKhachHangVay = chiTiet.IdKhachHangVay;
                        chiTietMuaHangSua.IdVayLai = chiTiet.IdVayLai;
                        chiTietMuaHangSua.ChuoiNgayTra = chiTiet.ChuoiNgayTra;
                        chiTietMuaHangSua.SoTienConLai = chiTiet.SoTienConLai;
                        chiTietMuaHangSua.TrangThai = chiTiet.TrangThai;
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
