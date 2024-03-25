using QLQuanCF_V1._0.DAO;
using QLQuanCF_V1._0.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLQuanCF_V1._0.PRE
{
    public partial class fAdmin : Form
    {
        BindingSource foodList = new BindingSource();
        BindingSource tableList = new BindingSource();
        BindingSource accountList = new BindingSource();
        BindingSource categoryList = new BindingSource();
        BindingSource NhanVienList = new BindingSource();

        public Account loginAccount;

        public fAdmin()
        {
            InitializeComponent();
            LoadData();
            dtgvFood.Columns[0].HeaderText = "Giá";
            dtgvFood.Columns[1].HeaderText = "Loại món";
            dtgvFood.Columns[2].HeaderText = "Tên món";
            dtgvFood.Columns[3].HeaderText = "ID";
        }
        #region methods

        List<Food> SearchFoodByName(string name)
        {
            List<Food> listFood = FoodDAO.Instance.SearrchFoodByName(name);

            return listFood;
        }

        void LoadData()
        {
            dtgvFood.DataSource = foodList;
            dtgvAccount.DataSource = accountList;
            dtgvTable.DataSource = tableList;
            dtgvCategory.DataSource = categoryList;

            LoadDateTimePickerBill();
            LoadListBillByDate(dtpkFromDate.Value, dtpkToDate.Value);
            LoadListFood();
            LoadAccount();
            LoadListTable();
            LoadListCategory();
            LoadListNhanVien();
  
            LoadCategoryIntoCombobox(cbFoodCategory);
            this.lblTBFood.Text = "Có tổng số " + dtgvFood.RowCount.ToString() + " món ăn";
            this.lblTBTableFood.Text = "Có tổng số " + dtgvTable.RowCount.ToString() + " bàn";
            this.lblTBCategory.Text = "Có tổng số " + dtgvCategory.RowCount.ToString() + " loại";
            AddAccountBinding();
            AddFoodBinding();
            AddTableBinding();
            AddCategoryBinding();
            AddNhanVIenBinding();
        }
        void AddNhanVIenBinding()
        {
            txbMaNV.DataBindings.Add(new Binding("Text", dtgvNhanVien.DataSource, "manv", true, DataSourceUpdateMode.Never));
            txbHoNV.DataBindings.Add(new Binding("Text", dtgvNhanVien.DataSource, "honv", true, DataSourceUpdateMode.Never));
            txbTenNV.DataBindings.Add(new Binding("Text", dtgvNhanVien.DataSource, "tennv", true, DataSourceUpdateMode.Never));
            txbDiaChi.DataBindings.Add(new Binding("Text", dtgvNhanVien.DataSource, "dchi", true, DataSourceUpdateMode.Never));
            txbSDT.DataBindings.Add(new Binding("Text", dtgvNhanVien.DataSource, "sdt", true, DataSourceUpdateMode.Never));
            txbNamSinh.DataBindings.Add(new Binding("Text", dtgvNhanVien.DataSource, "namsinh", true, DataSourceUpdateMode.Never));
            txbCa.DataBindings.Add(new Binding("Text", dtgvNhanVien.DataSource, "ca", true, DataSourceUpdateMode.Never));
            txbNgayVaoLam.DataBindings.Add(new Binding("Text", dtgvNhanVien.DataSource, "ngayvaolam", true, DataSourceUpdateMode.Never));
            nmUpLoaiNV.DataBindings.Add(new Binding("Value", dtgvNhanVien.DataSource, "loainv", true, DataSourceUpdateMode.Never));
        }
        void AddCategoryBinding()
        {
            txbCategoryID.DataBindings.Add(new Binding("Text", dtgvCategory.DataSource, "id", true, DataSourceUpdateMode.Never));
            txbNameCategory.DataBindings.Add(new Binding("Text", dtgvCategory.DataSource, "Name", true, DataSourceUpdateMode.Never));
        }
        void AddTableBinding()
        {
            // 
            txbIDTable.DataBindings.Add(new Binding("Text", dtgvTable.DataSource, "id", true, DataSourceUpdateMode.Never));
            txbTableName.DataBindings.Add(new Binding("Text", dtgvTable.DataSource, "Name", true, DataSourceUpdateMode.Never));     
        }
        void AddAccountBinding()
        {
            txbUserName.DataBindings.Add(new Binding("Text", dtgvAccount.DataSource, "UserName", true, DataSourceUpdateMode.Never));
            txbDisplayName.DataBindings.Add(new Binding("Text", dtgvAccount.DataSource, "DisplayName", true, DataSourceUpdateMode.Never));
            nmUpLoaiTK.DataBindings.Add(new Binding("Value", dtgvAccount.DataSource, "Type", true, DataSourceUpdateMode.Never));
        }
        void AddFoodBinding()
        {
            txbFoodName.DataBindings.Add(new Binding("Text", dtgvFood.DataSource, "Name", true, DataSourceUpdateMode.Never));
            txbFoodID.DataBindings.Add(new Binding("Text", dtgvFood.DataSource, "id", true, DataSourceUpdateMode.Never));
            nmFoodPrice.DataBindings.Add(new Binding("Value", dtgvFood.DataSource, "price", true, DataSourceUpdateMode.Never));

        }

        void LoadAccount()
        {
            accountList.DataSource = AccountDAO.Instance.GetLisstAccount();
        }

        void LoadDateTimePickerBill()
        {
            DateTime today = DateTime.Now;
            dtpkFromDate.Value = new DateTime(today.Year, today.Month, 1);
            dtpkToDate.Value = dtpkFromDate.Value.AddMonths(+1).AddDays(-1);
        }
        void LoadListBillByDate(DateTime checkIn, DateTime checkOut)
        {
            dtgvBill.DataSource = BillDAO.Instance.GetBillListByDate(checkIn, checkOut);
        }

        void LoadCategoryIntoCombobox(ComboBox cb)
        {
            cb.DataSource = CategoryDAO.Instance.GetListCategory();
            cb.DisplayMember = "Name";
        }
        void LoadListFood()
        {
            // dtgvFood.DataSource = FoodDAO.Instance.GetListFood();
            foodList.DataSource = FoodDAO.Instance.GetListFood();
        }
        void LoadListTable()
        {
            tableList.DataSource = TableDAO.Instance.LoadTableList();
            //dtgvTable.DataSource = TableDAO.Instance.LoadTableList();
        }
        void LoadListCategory()
        {
            categoryList.DataSource = CategoryDAO.Instance.GetListCategory();
            //dtgvCategory.DataSource = CategoryDAO.Instance.GetListCategory();
        }
        void LoadListNhanVien()
        {
            dtgvNhanVien.DataSource = NhanVienDAO.Instance.GetListNhanVien();
            //NhanVienList.DataSource = NhanVienDAO.Instance.GetListNhanVien();
        }
        void AddAccount(string userName, string displayname, int type)
        {
            if (AccountDAO.Instance.InsertAccount(userName, displayname, type))
            {
                MessageBox.Show("Thêm tài khoản thành công", "Thông báo");
            }
            else
            {
                MessageBox.Show("Thêm tài khoản thất bại", "Thông báo");
            }
            LoadAccount();
        }
        void EditAccount(string userName, string displayname, int type)
        {
            if (AccountDAO.Instance.UpdateAccount(userName, displayname, type))
            {
                MessageBox.Show("Cập nhật tài khoản thành công", "Thông báo");
            }
            else
            {
                MessageBox.Show("Cập nhật tài khoản thất bại", "Thông báo");
            }
            LoadAccount();
        }
        void DeleteAccount(string userName)
        {   
            if(loginAccount.UserName.Equals(userName))
            {
                MessageBox.Show("Bạn không thể xóa tài khoản này khi đang đăng nhập", "Thông báo");
                return;
            }
            if (AccountDAO.Instance.DeleteAccount(userName))
            {
                MessageBox.Show("Xóa tài khoản thành công", "Thông báo");
            }
            else
            {
                MessageBox.Show("Xóa tài khoản thất bại", "Thông báo");
            }
            LoadAccount();
        }
        void ResetPass(string userName)
        {
            if (AccountDAO.Instance.ResetPassword(userName))
            {
                MessageBox.Show("Đặt lại mật khẩu thành công", "Thông báo");
            }
            else
            {
                MessageBox.Show("Đặt lại mật khẩu thất bại", "Thông báo");
            }
        }
        void AddTableFood(string name)
        {
            if (TableDAO.Instance.InsertTable(name))
            {
                MessageBox.Show("Thêm bàn thành công", "Thông báo");
            }
            else
            {
                MessageBox.Show("Thêm bàn thất bại", "Thông báo");
            }
            LoadListTable();
        }
        void EditTableFood(int id, string name)
        {
            if (TableDAO.Instance.UpdateTable(id, name))
            {
                MessageBox.Show("Cập nhật bàn thành công", "Thông báo");
            }
            else
            {
                MessageBox.Show("Cập nhật bàn thất bại", "Thông báo");
            }
            LoadListTable();
        }
        void DeleteTableFood(int id)
        {
            if (TableDAO.Instance.DeleteTable(id))
            {
                MessageBox.Show("Xóa bàn thành công", "Thông báo");
            }
            else
            {
                MessageBox.Show("Bàn hiện tại bạn đang xóa có người", "Thông báo");
            }
            LoadListTable();
        }
        /*void AddCategory(string name)
        {
            if (CategoryDAO.Instance.InsertCategory(name))
            {
                MessageBox.Show("Thêm loại thức ăn công", "Thông báo");
            }
            else
            {
                MessageBox.Show("Thêm loại thức ăn thất bại", "Thông báo");
            }
            LoadListCategory();
        }
        void EditCategory(int id, string name)
        {
            if (CategoryDAO.Instance.UpdateCategory(id, name))
            {
                MessageBox.Show("Cập nhật thức ăn thành công", "Thông báo");
            }
            else
            {
                MessageBox.Show("Cập nhật thức ăn thất bại", "Thông báo");
            }
            LoadListCategory();
        }
        void DeleteCategory(int id)
        {
            if (CategoryDAO.Instance.DeleteCategory(id))
            {
                MessageBox.Show("Xóa thức ăn thành công", "Thông báo");
            }
            else
            {
                MessageBox.Show("Có lỗi xảy ra khi xóa thức ăn", "Thông báo");
            }
            LoadListCategory();

        }*/
        #endregion

        #region events

        private void btnAddTable_Click(object sender, EventArgs e)
        {
            string name = txbTableName.Text;

            AddTableFood(name);
        }

        private void btnDeleteTable_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txbIDTable.Text);

            DeleteTableFood(id);
        }

        private void btnEditTable_Click(object sender, EventArgs e)
        {
            string name = txbTableName.Text;
            int id = Convert.ToInt32(txbIDTable.Text);
            EditTableFood(id,name);
        }

        private void btnShowTable_Click(object sender, EventArgs e)
        {
            LoadListTable();
        }

        private void btnAddAccout_Click(object sender, EventArgs e)
        {
            string userName = txbUserName.Text;
            string displayName = txbDisplayName.Text;
            int type = (int)nmUpLoaiTK.Value;

            AddAccount(userName, displayName, type);
        }

        private void btnDeleteAccount_Click(object sender, EventArgs e)
        {
            string userName = txbUserName.Text;
            
            DeleteAccount(userName);
        }

        private void btnEditAccout_Click(object sender, EventArgs e)
        {
            string userName = txbUserName.Text;
            string displayName = txbDisplayName.Text;
            int type = (int)nmUpLoaiTK.Value;

            EditAccount(userName, displayName, type);
        }
        private void btnResertPassWord_Click(object sender, EventArgs e)
        {
            string userName = txbUserName.Text;
          
            ResetPass(userName);
        }
        private void btnShowAccount_Click(object sender, EventArgs e)
        {
            //Nhấn nút hiển thị danh sách tài khảon
            LoadAccount();
        }
        private void btnShowCategory_Click(object sender, EventArgs e)
        {
            LoadListCategory();
        }

        private void btnEditCategory_Click(object sender, EventArgs e)
        {
            string name = txbNameCategory.Text;
            int id = Convert.ToInt32(txbCategoryID.Text);
            if (this.txbNameCategory.TextLength == 0)
            {
                MessageBox.Show("Bạn chưa nhập tên loại thức ăn", "Thông báo");
            }
            else
                if (CategoryDAO.Instance.UpdateCategory(id,name))
                {
                    MessageBox.Show("Bạn đã sửa loại thức ăn thành công", "Thông báo");
                    LoadListCategory();
                }
                else
                {
                    MessageBox.Show("Có lỗi xảy ra khi sửa loại thức ăn. Vui lòng thử lại", "Thông báo");
                }
        }

        private void btnDeleteCategory_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txbCategoryID.Text);
            if (this.txbNameCategory.TextLength == 0)
            {
                MessageBox.Show("Bạn chưa nhập tên loại thức ăn", "Thông báo");
            }
            else
                if (CategoryDAO.Instance.DeleteCategory(id))
                {
                    MessageBox.Show("Bạn đã xóa loại thức ăn thành công", "Thông báo");
                    LoadListCategory();
                }
                else
                {
                    MessageBox.Show("Có lỗi xảy ra khi xóa loại thức ăn. Vui lòng thử lại", "Thông báo");
                }
        }

        private void btnAddCategory_Click(object sender, EventArgs e)
        {
            string name = txbNameCategory.Text;

            if (this.txbNameCategory.TextLength == 0)
            {
                MessageBox.Show("Bạn chưa nhập tên loại thức ăn", "Thông báo");
            }
            else
                if(CategoryDAO.Instance.InsertCategory(name))       
                    {
                        MessageBox.Show("Bạn đã thêm loại thức ăn thành công", "Thông báo");
                        LoadListCategory();
                    }
                else
                {
                    MessageBox.Show("Có lỗi xảy ra khi thêm loại thức ăn. Vui lòng thử lại", "Thông báo");
                }
        }

        private void btnSeachFood_Click(object sender, EventArgs e)
        {
            foodList.DataSource = SearchFoodByName(txbSeachFoodName.Text);
        }
        private void txbFoodID_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (dtgvFood.SelectedCells.Count > 0)
                {
                    int id = (int)dtgvFood.SelectedCells[0].OwningRow.Cells["CategoryID"].Value;
                    Category category = CategoryDAO.Instance.GetCategoryByID(id);

                    cbFoodCategory.SelectedItem = category;

                    int index = -1;
                    int i = 0;
                    foreach (Category item in cbFoodCategory.Items)
                    {
                        if (item.ID == category.ID)
                        {
                            index = i;
                            break;
                        }
                        i++;
                    }
                    cbFoodCategory.SelectedIndex = index;
                }
            }
            catch
            { }
                
        }

        private void btnAddFood_Click(object sender, EventArgs e)
        {
            string name = txbFoodName.Text;
            int categoryID = (cbFoodCategory.SelectedItem as Category).ID;
            float price = (float)nmFoodPrice.Value;

            if (this.txbFoodID.TextLength == 0 && this.txbFoodName.TextLength == 0)
                MessageBox.Show("Bạn chưa nhập Tên món và Giá", "Thông báo");
            else
                if (this.txbFoodName.TextLength == 0)
                    MessageBox.Show("Tên món ăn không được bỏ trống", "Thông báo");
                else
                    if (FoodDAO.Instance.InsertFood(name, categoryID, price))
                    {
                        MessageBox.Show("Bạn đã thêm món thành công", "Thông báo");
                        LoadListFood();
                        if (insertFood != null)
                            insertFood(this, new EventArgs());
                    }
                    else
                    {
                        MessageBox.Show("Có lỗi xảy ra khi thêm thức ăn. Vui lòng thử lại", "Thông báo");
                    }
        }

        private void btnEditFood_Click(object sender, EventArgs e)
        {
            string name = txbFoodName.Text;
            int categoryID = (cbFoodCategory.SelectedItem as Category).ID;
            float price = (float)nmFoodPrice.Value;
            int id = Convert.ToInt32(txbFoodID.Text);

            if (this.txbFoodID.TextLength == 0 && this.txbFoodName.TextLength == 0)
                MessageBox.Show("Bạn chưa nhập Tên món và Giá", "Thông báo");
            else
                if (this.txbFoodName.TextLength == 0)
                    MessageBox.Show("Tên món ăn không được bỏ trống", "Thông báo");
                else
                    if (FoodDAO.Instance.UpdateFood(id, name, categoryID, price))
                    {
                        MessageBox.Show("Bạn đã sửa món thành công", "Thông báo");
                        LoadListFood();
                        if (updateFood != null)
                            updateFood(this, new EventArgs());
                    }
                    else
                    {
                        MessageBox.Show("Có lỗi xảy ra khi sửa thức ăn", "Thông báo");
                    }
        }

        private void btnDeleteFood_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txbFoodID.Text);
            if (FoodDAO.Instance.DeleteFood(id))
            {
                MessageBox.Show("Bạn đã xóa món thành công", "Thông báo");
                LoadListFood();
                if (deleteFood != null)
                    deleteFood(this, new EventArgs());
            }
            else
            {
                MessageBox.Show("Có lỗi xảy ra khi xóa thức ăn", "Thông báo");
            }
        }

        private void btnViewBill_Click(object sender, EventArgs e)
        {
            LoadListBillByDate(dtpkFromDate.Value, dtpkToDate.Value);
        }
        private void btnShowFood_Click(object sender, EventArgs e)
        {
            LoadListFood();
        }

        private void dtgvFood_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnUpdateNV_Click(object sender, EventArgs e)
        {
            /*string manv = txbMaNV.Text;
            string honv = txbHoNV.Text;
            string tennv = txbTenNV.Text;
            string dchi = txbDiaChi.Text;
            string sdt = txbSDT.Text;
            string namsinh = txbNamSinh.Text;
            string ca = txbCa.Text;
            int loainv = Convert.ToInt32(nmUpLoaiNV.Text);

            if (this.txbMaNV.TextLength == 0)
            {
                MessageBox.Show("Bạn chưa nhập mã NV", "Thông báo");
            }
            else
                if (this.txbHoNV.TextLength == 0)
                {
                    MessageBox.Show("Bạn chưa nhập họ NV", "Thông báo");
                }
                else
                if(NhanVienDAO.Instance.InsertNhanVien(manv, honv, tennv, dchi, sdt,  namsinh, loainv))       
                    {
                        MessageBox.Show("Bạn đã thêm loại nhân viên thành công", "Thông báo");
                        LoadListCategory();
                    }
                else
                {
                    MessageBox.Show("Có lỗi xảy ra khi thêm loại thức ăn. Vui lòng thử lại", "Thông báo");
                }*/
        
        }

        private void btnInsertNhanVien_Click(object sender, EventArgs e)
        {

        }

        private void btnDeleteNV_Click(object sender, EventArgs e)
        {

        }

        private void btnShowNV_Click(object sender, EventArgs e)
        {
            LoadListNhanVien();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private event EventHandler insertFood;
        public event EventHandler InsertFood
        {
            add { insertFood += value; }
            remove { insertFood -= value; }
        }

        private event EventHandler deleteFood;
        public event EventHandler DeleteFood
        {
            add { deleteFood += value; }
            remove { deleteFood -= value; }
        }

        private event EventHandler updateFood;
        public event EventHandler UpdateFood
        {
            add { updateFood += value; }
            remove { updateFood -= value; }
        }
        #endregion

        private void btnFirstBillPage_Click(object sender, EventArgs e)
        {
            txbPageBill.Text = "1";
        }

        private void btnLastBillPage_Click(object sender, EventArgs e)
        {
            int sumRecord = BillDAO.Instance.GetNumBillListByDate(dtpkFromDate.Value, dtpkToDate.Value);

            int lastPage = sumRecord / 10;
            if (sumRecord % 10 != 0)
                lastPage++;
            txbPageBill.Text = lastPage.ToString();
        }

        private void txbPageBill_TextChanged(object sender, EventArgs e)
        {
            dtgvBill.DataSource = BillDAO.Instance.GetBillListByDateAndPage(dtpkFromDate.Value, dtpkToDate.Value, Convert.ToInt32(txbPageBill.Text));
        }

        private void btnPreviousBillPage_Click(object sender, EventArgs e)
        {
            int page = Convert.ToInt32(txbPageBill.Text);

            if (page > 1)
                page--;
            txbPageBill.Text = page.ToString();
        }

        private void btnNextBillPage_Click(object sender, EventArgs e)
        {
            int page = Convert.ToInt32(txbPageBill.Text);
            int sumRecord = BillDAO.Instance.GetNumBillListByDate(dtpkFromDate.Value, dtpkToDate.Value);

            if (page < sumRecord)
                page++;
            txbPageBill.Text = page.ToString();
        }

    }
}
