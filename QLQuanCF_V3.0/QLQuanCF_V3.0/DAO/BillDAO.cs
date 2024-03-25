using QLQuanCF_V1._0.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLQuanCF_V1._0.DAO
{
    public class BillDAO
    {
        private static BillDAO instance;

        public static BillDAO Instance
        {
            get { if (instance == null) instance = new BillDAO(); return BillDAO.instance; }
            private set { BillDAO.instance = value; }
        }
        private BillDAO()
        {

        }
        /// <summary>
        /// thất bại id -1
        /// thanh cong bill id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int GetUncheckBillIDByTableID(int id)
        {
            DataTable data = DataProvider.Insrance.ExecuteQuery("SELECT * FROM dbo.Bill WHERE idTable = " + id + " AND status = 0");

            if (data.Rows.Count > 0)
            {
                Bill bill = new Bill(data.Rows[0]);
                return bill.ID;
            }
            return -1;
        }
        public void CheckOut(int id, int discount, float totalPrice)
        {
            string query = "UPDATE dbo.Bill SET dateCheckOut = GETDATE(), status = 1, " + "discount = " + discount + ", totalPrice = "+ totalPrice + " WHERE id = " + id;

            DataProvider.Insrance.ExecuteNonQuery(query);
        }

        public void InsertBill(int id)
        {
            DataProvider.Insrance.ExecuteNonQuery("exec USP_InsertBill @idTable", new object[]{id});
        }

        public DataTable GetBillListByDate(DateTime checkIn, DateTime checkOut)
        {
            return DataProvider.Insrance.ExecuteQuery("exec USP_GetLisstBillByDate @checkIn , @checkOut", new object[] { checkIn, checkOut });
        }
        public DataTable GetBillListByDateAndPage(DateTime checkIn, DateTime checkOut , int pageNum)
        {
            return DataProvider.Insrance.ExecuteQuery("exec USP_GetLisstBillByDateAndPage @checkIn , @checkOut , @page", new object[] { checkIn, checkOut , pageNum});
        }
        public int GetNumBillListByDate(DateTime checkIn, DateTime checkOut)
        {
            return (int)DataProvider.Insrance.ExecuteScalar("exec USP_GetNumBillByDate @checkIn , @checkOut", new object[] { checkIn, checkOut });
        }
        public int GetMaxIDBill()
        {
            try
            {
                return (int)DataProvider.Insrance.ExecuteScalar("SELECT MAX(id) FROM dbo.Bill");
            }
            catch
            {
                return 1;
            }
        }
    }
}
