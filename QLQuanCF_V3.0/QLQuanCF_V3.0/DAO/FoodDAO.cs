using QLQuanCF_V1._0.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLQuanCF_V1._0.DAO
{
    public class FoodDAO
    {
        private static FoodDAO instance;

        public static FoodDAO Instance
        {
            get { if (instance == null) instance = new FoodDAO(); return FoodDAO.instance; }
            private set { FoodDAO.instance = value; }
        }

        private FoodDAO() { }

        public List<Food> GetFoodByCategoryID(int id)
        {
            List<Food> list = new List<Food>();
            string query = "select *  from Food where idCategory = " + id;

            DataTable data = DataProvider.Insrance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                Food food = new Food(item);
                list.Add(food);
            }

            return list;
        }
        public List<Food> GetListFood()
        {
            List<Food> list = new List<Food>();
            string query = "select * from Food";
            //string query = "EXEC USP_GestListFood";
            DataTable data = DataProvider.Insrance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                Food food = new Food(item);
                list.Add(food);
            }

            return list;
        }

        public List<Food> SearrchFoodByName(string name)
        {
            List<Food> list = new List<Food>();
            //tìm chính xác
            //string query = string.Format("select * from Food where name = N'{0}'",name);
            //tìm gần đúng
            //string query = string.Format("select * from Food where name like N'%{0}%'", name);
            //string query = "EXEC USP_GestListFood";

            string query = string.Format("SELECT * FROM dbo.Food WHERE dbo.fChuyenCoDauThanhKhongDau(name) LIKE N'%' + dbo.fChuyenCoDauThanhKhongDau(N'{0}') + '%'", name);
            DataTable data = DataProvider.Insrance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                Food food = new Food(item);
                list.Add(food);
            }

            return list;
        }
        public bool InsertFood(string name, int id, float price)
        {
            //EXEC dbo.USP_InsertFood @name = '', -- varchar(100)@idcategory = 0, -- int@price = 0.0 -- float
            string query = string.Format("INSERT dbo.Food (name, idCategory, price ) VALUES (N'{0}', {1}, {2})", name, id, price);
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
        }
    }
}
