using DienMay.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DienMay.ViewModel
{
    public class Gia : BaseNotifyPropertyChange
    {
        private long _giHienThi;
        public long GiaHienThi
        {
            get
            {
                return _giHienThi;
            }
            set
            {
                if (value != null)
                {
                    SetProperty(ref _giHienThi, value, () => { RaisePropertyChanged(nameof(GiaHienThi)); });
                }
            }
        }
     //   public long GiaSai => long.Parse(_giHienThi.Replace(".", ""));
    }
}
