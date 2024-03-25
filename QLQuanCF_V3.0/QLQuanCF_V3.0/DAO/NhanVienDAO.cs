using QLQuanCF_V1._0.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLQuanCF_V1._0.DAO
{
    public class NhanVienDAO
    {
        private static NhanVienDAO instance;

        public static NhanVienDAO Instance
        {
            get { if (instance == null) instance = new NhanVienDAO();  return NhanVienDAO.instance; }
            private set { NhanVienDAO.instance = value; }
        }
       
        private NhanVienDAO() { }

        public List<NhanVien> GetListNhanVien()
        {
            List<NhanVien> list = new List<NhanVien>();
            string query = "select * from NhanVien";
            //string query = "EXEC USP_GestListFood";
            DataTable data = DataProvider.Insrance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                NhanVien food = new NhanVien(item);
                list.Add(food);
            }

            return list;
        }
        /*//int id, string manv, string honv,string tennv,string dchi,string sdt,string namsinh,string ca,DateTime? ngayvaolam, int loai)
        public bool InsertNhanVien(string manv, string honv, string tennv, string dchi, string sdt, string namsinh, int loai)
        {
            // INSERT dbo.NhanVien( MaNV , HoNV ,TenNV ,DChi ,SDT ,NamSinh ,Ca ,NgayVaoLam ,LoaiNV) VALUES  ( N'' , N'' ,N'' , N'' ,N'' , '' , N'' , GETDATE(), 0)  
            string query = string.Format("INSERT dbo.NhanVien( MaNV , HoNV ,TenNV ,DChi ,SDT ,NamSinh ,Ca ,NgayVaoLam = 'GETDATE()',LoaiNV) VALUES  ( N'{0}' , N'{1}' ,N'{2}' , N'{3}' ,N'{4}' , '{5}' , N'{6}', {7})", manv, honv, tennv, dchi, namsinh, loai);
            //string query = string.Format("EXEC dbo.USP_InsertFood @name = N'{0}',@idcategory = {1},@price = {2}", name, id, price);
            int result = DataProvider.Insrance.ExecuteNonQuery(query);

            return result > 0;
        }
        public bool UpdateFood(int idFood, string name, int id, float price)
        {
            string query = string.Format("UPDATE dbo.Food SET name = N'{0}', idCategory = {1}, price = {2} WHERE id = {3}", name, id, price, idFood);
            int result = DataProvider.Insrance.ExecuteNonQuery(query);

            return result > 0;
        }
        public bool DeleteFood(int idFood)
        {
            BillInfoDAO.Instance.DeleteBillInfoByFoodID(idFood);

            string query = string.Format("DELETE FOOD where id = {0}",idFood);
            int result = DataProvider.Insrance.ExecuteNonQuery(query);

            return result > 0;
        }*/
    }
}
