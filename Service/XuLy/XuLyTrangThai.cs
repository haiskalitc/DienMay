using Service.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Service.XuLy
{
    public class XuLyTrangThai
    {
        public DienMayThanhDanhEntities db = new DienMayThanhDanhEntities();
        private static XuLyTrangThai _instance;
        public static XuLyTrangThai getInstance
        {
            get
            {
             // //  if (_instance == null)
             //   {
                    _instance = new XuLyTrangThai();
             //   }
                return _instance;
            }
        }
        public XuLyTrangThai()
        {
            db = new DienMayThanhDanhEntities();
        }

        public TRANGTHAI DocTheoId(long id)
        {
            TRANGTHAI kh = db.TRANGTHAIs.FirstOrDefault(model => model.Id.Equals(id));
            if (kh != null)
            {
                return kh;
            }
            return null;
        }
        public bool ThemTrangThai(TRANGTHAI th)
        {
            if (th != null)
            {
                db.TRANGTHAIs.Add(th);
                if (db.SaveChanges() >= 0)
                {
                    return true;
                }
                return false;
            }
            return false;
        }
    }
}
