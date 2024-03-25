using QLQuanCF_V1._0.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLQuanCF_V1._0.DAO
{
    public class TableDAO
    {
        private static TableDAO instance; 

        public static TableDAO Instance
        {
            get { if (instance == null) instance = new TableDAO(); return TableDAO.instance; }
            private set { TableDAO.instance = value; }
        }
        public static int TableWidth = 95;
        public static int TableHeight = 95;

        private TableDAO() { }

        public void SwitchTable(int id, int id2)
        {
            DataProvider.Insrance.ExecuteQuery("EXEC USP_SwitchTable @idTable1 , @idTable2", new object[]{id, id2});
        }

        public List<Table> LoadTableList()
        {
            List<Table> tableList = new List<Table>();
            DataTable data = DataProvider.Insrance.ExecuteQuery("EXEC dbo.USP_GetTableList");
            foreach(DataRow item in data.Rows)
            {
                Table table = new Table(item);
                tableList.Add(table);
            }

            return tableList;
        }
        public bool InsertTable(string name)
        {
            string query = string.Format("INSERT dbo.TableFood (name) VALUES (N'{0}')", name);

            int result = DataProvider.Insrance.ExecuteNonQuery(query);

            return result > 0;
        }
        public bool UpdateTable(int id, string name)
        {
            string query = string.Format("UPDATE dbo.TableFood SET name = N'{1}'WHERE id = '{0}'", id, name);
            int result = DataProvider.Insrance.ExecuteNonQuery(query);

            return result > 0;
        }
        public bool DeleteTable(int id)
        {
            //string query = string.Format("DELETE dbo.TableFood where id = '{0}' and status = N'{2}'", id, status);
            //SELECT * FROM dbo.Bill WHERE idTable = " + id + " AND status = 0"
            string query = "DELETE dbo.TableFood where id = " + id + " and status = N'Trống' ";
            int result = DataProvider.Insrance.ExecuteNonQuery(query);

            return result > 0;
        }
    
    }
}
