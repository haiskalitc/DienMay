using Service.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Service.XuLy
{
    public class XuLyHinhThuc
    {
        public DienMayThanhDanhEntities db = new DienMayThanhDanhEntities();
        private static XuLyHinhThuc _instance;
        public static XuLyHinhThuc getInstance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new XuLyHinhThuc();
                }
                return _instance;
            }
        }
        public XuLyHinhThuc()
        {
            db = new DienMayThanhDanhEntities();
        }
        public List<HINHTHUC> DocDanhSachTatCa()
        {
            return db.HINHTHUCs.ToList();
        }
    }
}
/*
 
 */
