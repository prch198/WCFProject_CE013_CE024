using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TenantFinderClient.ServiceReference1;

namespace TenantFinderClient
{
    public partial class Form1 : Form
    {
       
        public Form1()
        {
            InitializeComponent();
           
        }
        public void refreshForm()
        {
            TenantFinderClient.ServiceReference1.HouseServiceClient proxy = new TenantFinderClient.ServiceReference1.HouseServiceClient("BasicHttpBinding_IHouseService");
            DataSet ds = proxy.GetAllHouses();
            dataGridView2.DataSource = ds;
            dataGridView2.DataMember = ds.Tables[0].TableName.ToString();
            proxy.Close();
            save.Enabled = true;
            edit.Enabled = false;
            delete.Enabled = false;
            no1.Clear();
            name1.Clear();
            area1.Clear();
            city1.Clear();
            category1.Clear();
            rent1.Clear();
            req1.Clear();
        }

        private void save_Click(object sender, EventArgs e)
        {
            bool flag=false;
            try
            {
                TenantFinderClient.ServiceReference1.HouseServiceClient proxy = new TenantFinderClient.ServiceReference1.HouseServiceClient("BasicHttpBinding_IHouseService");
                House h = new House();
                h.No = int.Parse(no1.Text);
                h.Name = name1.Text;
                h.Area = area1.Text;
                h.City = city1.Text;
                h.Rent = float.Parse(rent1.Text);
                h.ReqTenant = req1.Text;
                h.Category = category1.Text;
                flag = proxy.AddHouse(h);
            }
            catch(Exception ex)
            {
                msg.Text = "Enter valid details.";
            }
            if(flag)
            {
                msg.Text = "Successfully Added.";
            }
            else
            {
                msg.Text += "Failed to Add.";
            }
            refreshForm();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            refreshForm(); 
        }

        private void edit_Click(object sender, EventArgs e)
        {
            
            TenantFinderClient.ServiceReference1.HouseServiceClient proxy = new TenantFinderClient.ServiceReference1.HouseServiceClient("BasicHttpBinding_IHouseService");
            House h2 = null;
            try
            {
                House h = new House();
                int id = int.Parse(dataGridView2.SelectedRows[0].Cells[0].Value.ToString());
                h.ID = id;
                h.No = int.Parse(no1.Text);
                h.Name = name1.Text;
                h.Area = area1.Text;
                h.City = city1.Text;
                h.Rent = float.Parse(rent1.Text);
                h.ReqTenant = req1.Text;
                h.Category = category1.Text;
                h2 = proxy.UpdateHouse(h);
            }
            catch(Exception ex)
            {
                msg.Text = "Enter valid details.";
            }
            if (h2!=null)
            {
                msg.Text = "Successfully Updated.";
            }
            else
            {
                msg.Text += "Failed to Update.";
            }
            refreshForm();

        }

        private void delete_Click(object sender, EventArgs e)
        {
            TenantFinderClient.ServiceReference1.HouseServiceClient proxy = new TenantFinderClient.ServiceReference1.HouseServiceClient("BasicHttpBinding_IHouseService");
            int id = int.Parse(dataGridView2.SelectedRows[0].Cells[0].Value.ToString());
            bool flag = proxy.DeleteHouse(id);
            if (flag)
            {
                msg.Text = "Successfully deleted.";
            }
            else
            {
                msg.Text = "Failed to delete.";
            }
            refreshForm();

        }

       

        private void dataGridView2_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                msg.Text = dataGridView2.SelectedRows[0].Cells[0].Value + string.Empty;
                no1.Text = dataGridView2.SelectedRows[0].Cells[1].Value + string.Empty;
                name1.Text = dataGridView2.SelectedRows[0].Cells[2].Value + string.Empty;
                area1.Text = dataGridView2.SelectedRows[0].Cells[3].Value + string.Empty;
                city1.Text = dataGridView2.SelectedRows[0].Cells[4].Value + string.Empty;
                category1.Text = dataGridView2.SelectedRows[0].Cells[5].Value + string.Empty;
                rent1.Text = dataGridView2.SelectedRows[0].Cells[6].Value + string.Empty;
                req1.Text = dataGridView2.SelectedRows[0].Cells[7].Value + string.Empty;

                save.Enabled = false;
                edit.Enabled = true;
                delete.Enabled = true;
            }
            catch (Exception ex)
            {
                msg.Text = "Select row by clicking first cell.";
            }
        }

        private void reset_Click(object sender, EventArgs e)
        {
            refreshForm();
        }
    }
}
