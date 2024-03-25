using QLQuanCF_V1._0.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace QLQuanCF_V1._0.DAO
{
    public class AccountDAO
    {
        private static AccountDAO instance;

        public static AccountDAO Instance
        {
            get { if (instance == null) instance = new AccountDAO(); return instance; }
            private set { instance = value; }
        }

        private AccountDAO() { }

        public bool Login(string userName, string passWord)
        {
            //mã hóa mật khẩu
            byte[] temp = ASCIIEncoding.ASCII.GetBytes(passWord);
            byte[] hasData = new MD5CryptoServiceProvider().ComputeHash(temp);

            string hasPass = "";
            foreach(byte item in hasData)
            {
                hasPass += item;
            }
            //var list = hasData.ToString();
            //list.Reverse();

            string query = "USP_Login @userName , @passWord ";

            DataTable result = DataProvider.Insrance.ExecuteQuery(query, new object[] { userName, hasPass });
            return result.Rows.Count > 0;
        }
        public bool UpdateAccount(string userName, string displayName, string pass, string newPass)
        {
            int result = DataProvider.Insrance.ExecuteNonQuery("exec USP_UpdateAccount @userName , @displayName , @passWord , @newPassWord ", new object[]{userName, displayName, pass, newPass});

            return result > 0;
        }

        public DataTable GetLisstAccount()
        {
            return DataProvider.Insrance.ExecuteQuery("SELECT UserName, DisplayName, Type FROM dbo.Account");
        }

        public Account GetAccountByUserName(string userName)
        {
           DataTable data = DataProvider.Insrance.ExecuteQuery("Select *  from account where userName = '" + userName + "'");

            foreach(DataRow item in data.Rows)
            {
                return new Account(item);
            }
            return null;
        }
        public bool InsertAccount(string name, string displayname, int type)
        {
            string query = string.Format("INSERT dbo.Account (UserName, DisplayName, type, pasword ) VALUES (N'{0}', N'{1}', {2}, N'{3}')", name, displayname, type, "33354741122871651676713774147412831195");
            
            int result = DataProvider.Insrance.ExecuteNonQuery(query);

            return result > 0;
        }
        public bool UpdateAccount(string name, string displayname, int type)
        {
            string query = string.Format("UPDATE dbo.Account SET DisplayName = N'{1}', Type = {2} WHERE UserName = N'{0}'", name, displayname, type);
            int result = DataProvider.Insrance.ExecuteNonQuery(query);

            return result > 0;
        }
        public bool DeleteAccount(string name)
        {
            string query = string.Format("DELETE Account where UserName = N'{0}'", name);
            int result = DataProvider.Insrance.ExecuteNonQuery(query);

            return result > 0;
        }
        public bool ResetPassword(string name)
        {
            string query = string.Format("UPDATE Account set pasword = N'33354741122871651676713774147412831195' where UserName = N'{0}'", name);
            int result = DataProvider.Insrance.ExecuteNonQuery(query);

            return result > 0;
        }
    }
}
