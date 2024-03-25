using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLQuanCF_V1._0.DTO
{
    public class NhanVien
    {
        public NhanVien(int id, string manv, string honv,string tennv,string dchi,string sdt,string namsinh,string ca,DateTime? ngayvaolam, int loai)
        {
            this.ID = id;
            this.MaNV = manv;
            this.HoNV = honv;
            this.TenNV = tennv;
            this.DChi = dchi;
            this.Sdt = sdt;
            this.NamSinh = namsinh;
            this.Ca = ca;
            this.NgayVaoLam = ngayvaolam;
            this.LoaiNV = loai;
        }
        public NhanVien(DataRow row)
        {
            this.ID = (int)row["id"];
            this.MaNV = row["manv"].ToString();
            this.HoNV = row["honv"].ToString();
            this.TenNV = row["tennv"].ToString();
            this.DChi = row["dchi"].ToString();
            this.Sdt = row["sdt"].ToString();
            this.NamSinh = row["namsinh"].ToString();
            this.Ca = row["ca"].ToString();
            this.NgayVaoLam = (DateTime?)row["ngayvaolam"];
            this.LoaiNV = (int)row["loainv"];
        }
        private int loaiNV;
        public int LoaiNV
        {
          get { return loaiNV; }
          set { loaiNV = value; }
        }

        private DateTime? ngayVaoLam;
        public DateTime? NgayVaoLam
        {
            get { return ngayVaoLam; }
            set { ngayVaoLam = value; }
        }

        private string ca;
        public string Ca
        {
          get { return ca; }
          set { ca = value; }
        }

        private string namSinh;

        public string NamSinh
        {
          get { return namSinh; }
          set { namSinh = value; }
        }

        private string sdt;
        public string Sdt
        {
          get { return sdt; }
          set { sdt = value; }
        }

        private string dChi;
        public string DChi
        {
          get { return dChi; }
          set { dChi = value; }
        }

        private string tenNV;
        public string TenNV
        {
          get { return tenNV; }
          set { tenNV = value; }
        }

        private string hoNV;
        public string HoNV
        {
          get { return hoNV; }
          set { hoNV = value; }
        }

        private string maNV;
        public string MaNV
        {
            get { return maNV; }
            set { maNV = value; }
        }

        private int iD;
        public int ID
        {
            get { return iD; }
            set { iD = value; }
        }
    }
}
