//using form.econtactClasses;

using econtactClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Econtact
{
    public partial class Econtact : Form
    {
        public Econtact()
        {
            InitializeComponent();
        }
        //update and delete FAILED TRY TO SOLVE
        contactClass c = new contactClass(); //importu form ile yaptım  hata verebilir
        private void Econtact_Load(object sender, EventArgs e)
        {
            //Load data on data grid view
            DataTable dt = c.Select();
            dgvContactList.DataSource = dt;
        }


        private void btnAdd_Click(object sender, EventArgs e)
        {
            //get the value from the input fields
            c.FirstName = txtboxFirstName.Text;
            c.LastName = txtboxLastName.Text;
            c.ContactNo = txtboxContactNumber.Text;
            c.Address = txtboxAddress.Text;
            c.Gender = cmbGender.Text;

            //insert data intodatabase using the method we created 
            bool success = c.Insert(c);
            if (success == true)
            {
                //successfully inserted
                MessageBox.Show("New contact successfully inserted");
                //call the clear method here
                Clear();
            }
            else 
            {
                //failed to add contact
                MessageBox.Show("Failed to add new contact");
            }
            //Load data on data grid view
            DataTable dt = c.Select();
            dgvContactList.DataSource = dt;

        }

        private void txtboxFirstName_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void Clear()
        {
            txtboxFirstName.Text = "";
            txtboxLastName.Text = "";
            txtboxContactNumber.Text = "";
            txtboxAddress.Text = "";
            cmbGender.Text = "";
            txtboxContactID.Text = "";
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            //get the data from textboxes
            c.ContactID = int.Parse(txtboxContactID.Text);
            c.FirstName = txtboxFirstName.Text;
            c.LastName = txtboxLastName.Text;
            c.ContactNo = txtboxContactNumber.Text;
            c.Address = txtboxAddress.Text;
            c.Gender = cmbGender.Text;
            //update data in database
            bool success = c.Update(c);
            if (success == true)
            {
                //updated successfully
                MessageBox.Show("contact has been successfully updated");
                //Load Data on Data GRidview
                DataTable dt = c.Select();
                dgvContactList.DataSource = dt;
                //Call Clear Method
                Clear();
            }
            else
            {
                //failed to update
                MessageBox.Show("failed to update contact");
            }
        }
        private void dgvContactList_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //get the data from data grid view and load it to the textboxes 
            //identify the row on which mouse is click
            int rowIndex = e.RowIndex;
            txtboxContactID.Text = dgvContactList.Rows[rowIndex].Cells[0].Value.ToString();
            txtboxFirstName.Text = dgvContactList.Rows[rowIndex].Cells[1].Value.ToString();
            txtboxLastName.Text = dgvContactList.Rows[rowIndex].Cells[2].Value.ToString();
            txtboxContactNumber.Text = dgvContactList.Rows[rowIndex].Cells[3].Value.ToString();
            txtboxAddress.Text = dgvContactList.Rows[rowIndex].Cells[4].Value.ToString();
            cmbGender.Text = dgvContactList.Rows[rowIndex].Cells[5].Value.ToString();

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            //Get the Contact ID from the Application
            c.ContactID = Convert.ToInt32(txtboxContactID.Text);
            bool success = c.Delete(c);
            if (success == true)
            {
                //Successfully Deleted
                MessageBox.Show("Contact successfully deleted.");
                //Refresh Data GridView
                //Load Data on Data Gridview
                DataTable dt = c.Select();
                dgvContactList.DataSource = dt;
                //Call the Clear Method Here
                Clear();
            }
            else
            {
                //Failed to dElte
                MessageBox.Show("Failed to Delete Contact. Try Again.");
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            //Call Clear Method Here
            Clear();
        }

        static string myconnstr = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;
        private void txtboxSearch_TextChanged(object sender, EventArgs e)
        {
            //Get teh value from text box
            string keyword = txtboxSearch.Text;

            SqlConnection conn = new SqlConnection(myconnstr);
            SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM tbl_contact WHERE FirstName LIKE '%" + keyword + "%' OR LastName LIKE '%" + keyword + "%' OR Address LIKE '%" + keyword + "%'", conn);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dgvContactList.DataSource = dt;
        }
    }
}
