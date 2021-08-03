using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BAL;

public partial class Admin_accessrights : System.Web.UI.Page
{
    DataTable dt = new DataTable();
    RegistrationBAL objuser = new RegistrationBAL();
    MenuBAL objmenu = new MenuBAL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            objuser.RegistrationID = Convert.ToInt32(Request.QueryString["id"]);

            dt = objuser.SelectRegistraionByID();
            if (dt.Rows.Count > 0)
            {
                lblUsername.Text = dt.Rows[0]["FirstName"].ToString() + " " + dt.Rows[0]["LastName"].ToString();
            }
            BindTreeViewControl();
            if (Request.QueryString["id"] != null)
            {
                setdata();
            }

        }

    }
    private void setdata()
    {
        objmenu.UserID = Convert.ToInt32(Request.QueryString["id"]);
        dt = objmenu.GetRightsByUser();
        if (dt.Rows.Count > 0)
        {

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (Convert.ToInt32(dt.Rows[i]["MenuID"].ToString()) == 3 || Convert.ToInt32(dt.Rows[i]["MenuID"].ToString()) == 4 || Convert.ToInt32(dt.Rows[i]["MenuID"].ToString()) == 5)
                {
                    if (Convert.ToInt32(dt.Rows[i]["MenuID"].ToString()) == 3)
                    {
                        tvMenu.Nodes[1].ChildNodes[0].ChildNodes[Convert.ToInt32(dt.Rows[i]["AccTypeID"].ToString()) - 1].Checked = true;
                    }
                    else if (Convert.ToInt32(dt.Rows[i]["MenuID"].ToString()) == 4)
                    {
                        tvMenu.Nodes[1].ChildNodes[1].ChildNodes[Convert.ToInt32(dt.Rows[i]["AccTypeID"].ToString()) - 1].Checked = true;
                    }
                    else
                    {
                        tvMenu.Nodes[1].ChildNodes[2].ChildNodes[Convert.ToInt32(dt.Rows[i]["AccTypeID"].ToString()) - 1].Checked = true;
                    }
                }
                else
                {
                    if (Convert.ToInt32(dt.Rows[i]["MenuID"].ToString()) > 5)
                    {
                        tvMenu.Nodes[Convert.ToInt32(dt.Rows[i]["MenuID"].ToString()) - 4].ChildNodes[Convert.ToInt32(dt.Rows[i]["AccTypeID"].ToString()) - 1].Checked = true;
                    }
                    else
                    {
                        if (Convert.ToInt32(dt.Rows[i]["MenuID"].ToString()) !=2)
                        {
                            tvMenu.Nodes[Convert.ToInt32(dt.Rows[i]["MenuID"].ToString()) - 1].ChildNodes[Convert.ToInt32(dt.Rows[i]["AccTypeID"].ToString()) - 1].Checked = true;
                        }
                    }
                }
            }
        }
    }

    private void BindTreeViewControl()
    {

        dt = objmenu.GetAllMenu();
        DataRow[] Rows = dt.Select("ParentID = 0");

        for (int i = 0; i < Rows.Length; i++)
        {
            TreeNode root = new TreeNode(Rows[i]["Page"].ToString(), Rows[i]["ID"].ToString());
            root.SelectAction = TreeNodeSelectAction.Expand;
            //accessnode(root);
            CreateNode(root, dt);
            tvMenu.Nodes.Add(root);

        }
    }

    public void CreateNode(TreeNode node, DataTable Dt)
    {
        DataRow[] Rows = Dt.Select("ParentID =" + node.Value);
        if (Rows.Length == 0) { accessnode(node);
            return; }
        for (int i = 0; i < Rows.Length; i++)
        {
            TreeNode Childnode = new TreeNode(Rows[i]["Page"].ToString(), Rows[i]["ID"].ToString());
            Childnode.SelectAction = TreeNodeSelectAction.Expand;
            node.ChildNodes.Add(Childnode);
           // accessnode(node);
            CreateNode(Childnode, Dt);
        }
    }
    public void accessnode(TreeNode node)
    {
        DataTable dt1 = new DataTable();
        dt1 = objmenu.Getaccesstype();
        if (dt1.Rows.Count > 0)
        {
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                TreeNode Childnode = new TreeNode(dt1.Rows[i]["AccessType"].ToString(), dt1.Rows[i]["ID"].ToString());
                Childnode.SelectAction = TreeNodeSelectAction.Expand;
                node.ChildNodes.Add(Childnode);
            }
        }
    }
    protected void btnsave_Click(object sender, EventArgs e)
    {
        objmenu.UserID = Convert.ToInt32(Request.QueryString["id"]);
        objmenu.DeleteMenuAccessRights();

        for (int i = 0; i < tvMenu.Nodes.Count; i++)
        {
            for (int j = 0; j < tvMenu.Nodes[i].ChildNodes.Count; j++)
            {

                if (tvMenu.Nodes[i].ChildNodes[j].ChildNodes.Count > 0)
                {
                    for (int k = 0; k < tvMenu.Nodes[i].ChildNodes[j].ChildNodes.Count; k++)
                    {
                        if (tvMenu.Nodes[i].ChildNodes[j].ChildNodes[k].Checked == true)
                        {
                            objmenu.UserID = Convert.ToInt32(Request.QueryString["id"]);
                            objmenu.MenuID = Convert.ToInt32(tvMenu.Nodes[i].ChildNodes[j].Value);
                            objmenu.AddMenuRightsUser(Convert.ToInt32(tvMenu.Nodes[i].ChildNodes[j].ChildNodes[k].Value));
                        }
                    }
                }
                else
                {

                    if (tvMenu.Nodes[i].ChildNodes[j].Checked == true)
                    {
                        objmenu.UserID = Convert.ToInt32(Request.QueryString["id"]);
                        objmenu.MenuID = Convert.ToInt32(tvMenu.Nodes[i].Value);
                        objmenu.AddMenuRightsUser(Convert.ToInt32(tvMenu.Nodes[i].ChildNodes[j].Value));
                    }
                }
            }
        }
        Response.Redirect("ManageAdmin.aspx");
    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("ManageAdmin.aspx");
    }
    protected void tvMenu_SelectedNodeChanged(object sender, EventArgs e)
    {

    }
}