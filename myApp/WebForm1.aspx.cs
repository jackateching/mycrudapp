using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Windows;

namespace myApp
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        private static string connect_string = "DATA SOURCE=ORCL;PERSIST SECURITY INFO=True;USER ID=C##JACK;PASSWORD=jack";
        OracleConnection conn = new OracleConnection(connect_string);
        protected void Page_Load(object sender, EventArgs e)
        {
            load_grid_view();
        }

        private void load_grid_view()
        {

            OracleCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "STUDENT_PROCEDURE.GET_ALL_STUDENTS";

            DataSet ds = new DataSet();
            cmd.Parameters.Add(new OracleParameter("p_recordset", OracleDbType.RefCursor, ParameterDirection.Output));

            OracleDataAdapter da = new OracleDataAdapter(cmd);
            da.Fill(ds, "DataTable1");

            if (cmd.Connection.State != ConnectionState.Closed)
                cmd.Connection.Close();

            //GridView1.DataSource = ds.Tables[0]; //error occurs here
            //GridView1.DataBind();
            viewList.Controls.Clear();

            foreach (DataTable table in ds.Tables) //1
            {
                foreach (DataRow row in table.Rows) //num of rows
                {
                    
                    HtmlGenericControl rowDiv = new HtmlGenericControl("DIV");
                    LinkButton rowBtnDelete = new LinkButton();
                    LinkButton rowBtnEdit = new LinkButton();
                    rowBtnEdit.Attributes.Add("class", "className");
                    rowBtnEdit.Text = "Edit" + " ";
                    rowBtnDelete.Text = "Delete";
                    string rowSId = row["SId"].ToString();
                    string rowName = row["Name"].ToString();
                    string rowEmail = row["Email"].ToString();
                    HtmlGenericControl itemspan = new HtmlGenericControl("SPAN");
                    itemspan.InnerHtml = rowSId;
                    itemspan.Style.Add("Width", "10%");
                    HtmlGenericControl itemspan2 = new HtmlGenericControl("SPAN");
                    itemspan2.InnerHtml = rowName;
                    itemspan2.Style.Add("Width", "20%");
                    HtmlGenericControl itemspan3 = new HtmlGenericControl("SPAN");
                    itemspan3.InnerHtml = rowEmail;
                    itemspan3.Style.Add("Width", "20%");

                    rowDiv.Controls.Add(itemspan);
                    rowDiv.Controls.Add(itemspan2);
                    rowDiv.Controls.Add(itemspan3);

                    rowDiv.Attributes.Add("class", "rowDiv");


                    void edit_btn_Click(object sender, EventArgs s)
                    {
                        conn.Open();
                        cmd.Connection = conn;
                        cmd.CommandType = CommandType.Text;

                        System.Diagnostics.Debug.WriteLine(rowSId + rowName + rowEmail);
                        sIDTextBox.Text = rowSId;
                        nameTextBox.Text = rowName;
                        emailTextBox.Text = rowEmail;


                        load_grid_view();
                    }

                    void delete_btn_Click(object sender, EventArgs e)
                    {
                        conn.Open();
                        cmd.Connection = conn;
                        cmd.CommandType = CommandType.Text;

                        cmd.CommandText = "delete from student where sid = '" + rowSId + "'";
                        cmd.ExecuteNonQuery();
                        load_grid_view();
                    }

                    rowBtnEdit.Click += new EventHandler(edit_btn_Click);
                    rowBtnDelete.Click += new EventHandler(delete_btn_Click);

                    rowDiv.Controls.Add(rowBtnEdit);
                    rowDiv.Controls.Add(rowBtnDelete);
                    viewList.Controls.Add(rowDiv);
                }
            }
        }

        protected void addBtn_Click(object sender, EventArgs e)
        {

            //string id = TextBoxId.Text;
            string name = nameTextBox.Text;
            string email = emailTextBox.Text;

            if (name == "" || email == "")
            {
                MessageBox.Show("You have a blank field");
                return;
            }

            string query1 = "insert into STUDENT (sid, name, email) values ((SELECT MAX( sId )+1 FROM STUDENT),'" + name + "','" + email + "')";

            conn.Open();
            OracleCommand cmd = conn.CreateCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.Text;

            cmd.CommandText = query1;
            cmd.ExecuteNonQuery();

            if (cmd.Connection.State != ConnectionState.Closed)
                cmd.Connection.Close();

            load_grid_view();

            sIDTextBox.Text = "";
            nameTextBox.Text = "";
            emailTextBox.Text = "";
        }

        protected void updateBtn_Click(object sender, EventArgs e)
        {
            string id = sIDTextBox.Text;
            string name = nameTextBox.Text;
            string email = emailTextBox.Text;
            if (name == "" && email == "")
            {
                //ShowMessage("The field cannot be empty", "Check Field");
                MessageBox.Show("Both fields are empty");
                return;
            }
            if (name == "" || email == "")
            {
                //ShowMessage("The field cannot be empty", "Check Field");
                MessageBox.Show("One of the field cannot be empty");
                return;
            }

            string query = string.Format("UPDATE STUDENT SET Name = '{0}', Email = '{1}' WHERE sId = '{2}'", nameTextBox.Text, emailTextBox.Text, sIDTextBox.Text);

            conn.Open();
            OracleCommand cmd = conn.CreateCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = query;

            cmd.ExecuteNonQuery();

            if (cmd.Connection.State != ConnectionState.Closed)
                cmd.Connection.Close();

            load_grid_view();

            sIDTextBox.Text = "";
            nameTextBox.Text = "";
            emailTextBox.Text = "";
        }

        private void ShowMessage(string Message, string Title)
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "alert", string.Format("alert('{1}', '{0}');", Message, Title), true);
        }
    }
}