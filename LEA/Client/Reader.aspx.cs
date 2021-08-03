using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BAL;
public partial class Client_Reader : System.Web.UI.Page
{
    BookIssueBAL ObjBookIssue = new BookIssueBAL();

    public string Reader
    {
        get { return !string.IsNullOrEmpty(Convert.ToString(ViewState["Reader"])) ? Convert.ToString(ViewState["Reader"]) : ""; }
        set { ViewState["Reader"] = value; }
    }

    public int BookID
    {
        get { return ViewState["BookID"] != null ? Convert.ToInt32(ViewState["BookID"]) : -1; }
        set { ViewState["BookID"] = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindReaderString();
        }

    }
    public void BindReaderString()
    {
        DataTable dt = new DataTable();

        if (Request.QueryString["id"] != null)
        {
            BookID = Convert.ToInt32(Request.QueryString["id"]);
            ObjBookIssue.ID = BookID;

        }
        dt = ObjBookIssue.GetSmallImageMagazinIssue();

        System.Text.StringBuilder strBuild = new System.Text.StringBuilder();


        if (dt != null && dt.Rows.Count >= 1)
        {
            if (dt.Rows[0]["ExplorerPdfStartNo"] != null && Convert.ToInt32(dt.Rows[0]["ExplorerPdfStartNo"]) == 0)
            {
                dt.Rows[0]["ExplorerPdfStartNo"] = 1;
            }

            if (dt.Rows[0]["ExplorerPdfEndNo"] != null && Convert.ToInt32(dt.Rows[0]["ExplorerPdfEndNo"]) == 0)
            {
                dt.Rows[0]["ExplorerPdfEndNo"] = 1;
            }
        }

        int i = 0;

        int StartPage = 1;
        int EndPage = 1;
        if (dt != null && dt.Rows.Count > 0)
        {
            StartPage = Convert.ToInt32(dt.Rows[0]["ExplorerPdfStartNo"]);
            EndPage = Convert.ToInt32(dt.Rows[0]["ExplorerPdfEndNo"]);
        }

        if (StartPage != 0 && EndPage != 0)
        {
            DataView dv = new DataView(dt);
            dv.RowFilter = "rno <= " + EndPage + " and rno >=  " + StartPage;
            dt = dv.ToTable();
        }

        //if (dt.Rows.Count % 2 == 1)
        //{
        //    dt.Rows.RemoveAt(dt.Rows.Count - 1);
        //   // dt.Rows
        //}
        rptImages.DataSource = dt;
        rptImages.DataBind();

        int j = 0;
        if (dt.Rows.Count > 0)
        {
            for (int cntRow = 0; cntRow < (dt.Rows.Count == 1 ? 1 : (dt.Rows.Count % 2 == 1 ? (dt.Rows.Count / 2) + 1 : (dt.Rows.Count / 2))); cntRow++)
            {

                j += 2;
                strBuild.Append("<div class='bb-item' id='item" + (cntRow + 1) + "'>");
                strBuild.Append("<div class='content'>");
                strBuild.Append("<div class='scroller' style='padding:10px 4%'>");

                //if ((i + 2) < dt.Rows.Count)
                //{


                strBuild.Append("<img  src='" + dt.Rows[i]["Image"] + "' height='80%' width='50%'  />");
                if (dt.Rows.Count == i + 1)
                {
                    strBuild.Append("<img src='~/Client/images/ngp-bg.png' height='80%' width='50%' />");
                }
                else
                {
                    strBuild.Append("<img src='" + dt.Rows[i + 1]["Image"] + "' height='80%' width='50%' />");
                }

                i += 2;
                //}

                //strBuild.Append("<img src='http://themagz.net/Book/13/Original" + 34 + "_" + (cntRow + 1) + ".jpg' height='800px' />");
                //strBuild.Append("<img src='http://themagz.net/Book/13/Original" + 34 + "_" + (cntRow + 2) + ".jpg' height='800px' />");
                strBuild.Append("</div>");
                strBuild.Append("</div>");
                strBuild.Append("</div>");



            }
        }
        Reader = strBuild.ToString();
    }

    protected void rptImages_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        int j = 0;
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            j = e.Item.ItemIndex;


            if (string.IsNullOrEmpty(Convert.ToString(ViewState["index"])))
            {
                ViewState["index"] = 1;
            }
            System.Web.UI.HtmlControls.HtmlAnchor link = (e.Item.FindControl("link") as System.Web.UI.HtmlControls.HtmlAnchor);

            link.HRef = "#item" + (j + 1);
            link.InnerText = "Page" + (Convert.ToInt32(ViewState["index"])) + " - " + (Convert.ToInt32(ViewState["index"]) + 1);
            j = Convert.ToInt32(ViewState["index"]);
            j += 2;
            ViewState["index"] = j;
        }
    }
}