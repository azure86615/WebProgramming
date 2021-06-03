using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace 網頁設計
{
    public partial class store : System.Web.UI.Page
    {
        static int total;
        protected void Page_Load(object sender, EventArgs e)
        {
            Label1.Text = Session["user_name"] + "歡迎光臨<br>您還有" + Session["user_money"] + "元";
            Label2.Text = "每杯30元"; 
            Image1.ImageUrl = "./pic/咖啡.jpg";
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Image1.ImageUrl = "./pic/" + DropDownList1.SelectedItem.Text.Trim() + ".jpg";
            Label2.Text = "每杯" + DropDownList1.SelectedItem.Value + "元"; 
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            SqlDataSource1.Insert();
            SqlConnection conn = new SqlConnection("Data Source=.\\SQLEXPRESS;AttachDbFilename=|DataDirectory|\\Database1.mdf;Integrated Security=True;Connect Timeout=30;User Instance=True");
            conn.Open();
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("select top 1 id_order from [order] order by id_order Desc", conn);
            dr = cmd.ExecuteReader();
            if (dr.Read()) {
                Session["id_order"] = dr["id_order"];
                Button1.Text = dr["id_order"] + "號訂單";
                Button1.Enabled = false;
                Button2.Visible = true;
                Label3.Visible = true;
                numDropDownList.Visible = true;
                sweetDropDownList.Visible = true;
                iceDropDownList.Visible = true;
                GridView1.Visible = true;
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            SqlDataSource2.Insert();
            totalPrice.Visible = true;
            check.Visible = true;
            cancel.Visible = true;
            if (Convert.ToInt32(Session["user_money"]) < total)
                error.Visible = true;
            GridView2.DataBind();
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            countTotal();
            GridView2.DataBind();
            checkremain();
        }
        protected void countTotal() {
            total = 0;
            for (int i = 0; i < GridView1.Rows.Count; i++)
                total += Convert.ToInt32(((Label)GridView1.Rows[i].Cells[4].FindControl("Label5")).Text);
            String opu = "Total Price : " + total + "$";
            totalPrice.Text = opu;
            if (total == 0)
                check.Visible = false;
            else
                check.Visible = true;
            if (Convert.ToInt32(Session["user_money"]) >= total)
            {
                check.Enabled = true;
                error.Visible = false;
            }
            else
            {
                check.Enabled = false;
                error.Visible = true;
            }

        }

        protected void GridView1_RowDeleted(object sender, GridViewDeletedEventArgs e)
        {
            total = 0;
            for (int i = 0; i < GridView1.Rows.Count - 1; i++)
                total = Convert.ToInt32(((Label)GridView1.Rows[i].Cells[4].FindControl("Label5")).Text);
            String opu = "Total Price :" + total + "$";
            totalPrice.Text = opu;
            if (total == 0)
                check.Visible = false;
            else
                check.Visible = true;
            if (Convert.ToInt32(Session["user_money"]) >= total)
            {
                check.Enabled = true;
                error.Visible = false;
            }
            else
            {
                check.Enabled = false;
                error.Visible = true;
            }

            GridView2.DataBind();
            if (GridView2.Rows.Count == 0)
                notenough.Visible = false;            
            checkremain();
        }

        protected void check_Click(object sender, EventArgs e)
        {
            Button1.Enabled = true;
            Button1.Text = "開始下單";
            Button2.Visible = false;
            Label3.Visible = false;
            numDropDownList.Visible = false;
            sweetDropDownList.Visible = false;
            iceDropDownList.Visible = false;
            GridView1.Visible = false;
            totalPrice.Visible = false;
            check.Visible = false;
            cancel.Visible = false;

            int remain = Convert.ToInt32(Session["user_money"]) - total;
            Session["user_money"] = remain;
            Label1.Text = Session["user_name"] + "歡迎光臨<br>您還有" + Session["user_money"] + "元";
            SqlDataSource1.Update();

            GridView2.DataBind();
            update_qt();
            qtview.DataBind();
        }

        protected void cancel_Click(object sender, EventArgs e)
        {
            SqlDataSource4.Delete();
            Button1.Enabled = true;
            Button1.Text = "開始下單";
            Button2.Visible = false;
            Label3.Visible = false;
            numDropDownList.Visible = false;
            sweetDropDownList.Visible = false;
            iceDropDownList.Visible = false;
            GridView1.Visible = false;
            totalPrice.Visible = false;
            check.Visible = false;
            cancel.Visible = false;

            GridView2.DataBind();
        }
        protected void checkremain()
        {
            for (int i = 0; i < GridView2.Rows.Count; i++)
            {
                for (int j = 0; j < qtview.Rows.Count; j++)
                {
                    if (Convert.ToInt32(((Label)(GridView2.Rows[i].Cells[0].FindControl("Label1"))).Text) == Convert.ToInt32(((Label)(qtview.Rows[j].Cells[0].FindControl("Label1"))).Text))
                    {
                        int request = Convert.ToInt32(((Label)(GridView2.Rows[i].Cells[1].FindControl("Label2"))).Text);
                        int qt = Convert.ToInt32(((Label)(qtview.Rows[j].Cells[1].FindControl("Label2"))).Text);
                        Session["request"] = request;
                        Session["qt"] = qt;
                        if (request > qt)
                        {
                            notenough.Visible = true;
                            check.Enabled = false;
                        }
                        else 
                        {
                            notenough.Visible = false;
                            if (Convert.ToInt32(Session["user_money"])>=total)
                                check.Enabled = true;
                        }
                    }
                }
            }
        }
        protected void update_qt()
        {
            for (int i = 0; i < GridView2.Rows.Count; i++)
            {
                for (int j = 0; j < qtview.Rows.Count; j++)
                {
                    if (Convert.ToInt32(((Label)(GridView2.Rows[i].Cells[0].FindControl("Label1"))).Text) == Convert.ToInt32(((Label)(qtview.Rows[j].Cells[0].FindControl("Label1"))).Text))
                    {
                        Session["id_drink"] = Convert.ToInt32(((Label)(qtview.Rows[j].Cells[0].FindControl("Label1"))).Text);
                        Session["qt"] = Convert.ToInt32(Session["qt"]) - Convert.ToInt32(Session["request"]);
                        checkqt.Update();
                    }
                }
            }
        }


    }
}