using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace 網頁設計
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            DetailsView1.DataBind();
            DetailsView1.Visible = false;
            LinkButton1.Visible = false;
            if (DetailsView1.DataItemCount == 1)
            {
                Session["user_name"] = DetailsView1.Rows[0].Cells[1].Text;
                Session["user_money"] = DetailsView1.Rows[1].Cells[1].Text;
                LinkButton1.Visible = true;
                Session["pass"] = TextBox2.Text;
            }
            else
                DetailsView1.Visible = true;
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {

        }
    }
}