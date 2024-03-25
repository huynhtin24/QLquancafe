using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLQuanCF_V1._0.DTO
{
    public class Account
    {
        public Account(string username, string displayname, int type, string pasword = null)
        {
            this.UserName = username;
            this.DisplayName = displayname;
            this.Type = type;
            this.PassWord = passWord;
        }
        public Account(DataRow row)
        {
            this.UserName = row["username"].ToString();
            this.DisplayName = row["displayname"].ToString();
            this.Type = (int)row["type"];
            this.PassWord = row["pasWord"].ToString();
        }

        private int type;
        public int Type
        {
            get { return type; }
            set { type = value; }
        }

        private string passWord;
        public string PassWord
        {
            get { return passWord; }
            set { passWord = value; }
        }

        private string displayName;
        public string DisplayName
        {
            get { return displayName; }
            set { displayName = value; }
        }

        private string userName;
        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }
    }
}
