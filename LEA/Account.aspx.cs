using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BAL;
using System.Data;
using Localization;

public partial class Account : System.Web.UI.Page
{
    BAL_Account obj_login = new BAL_Account();
    BAL_Account Obj_Acc = new BAL_Account();
    Country Obj_Country = new Country();
    dbconnection d = new dbconnection();

    protected void Page_Load(object sender, EventArgs e)
    {
        txt_firstname.Focus();
        this.Form.DefaultButton = btn_submit.UniqueID;
        if (!IsPostBack)
        {
            BindRegion();
            BindData();
            string str = ResourceManager.GetString("Book already exists in your library");
        }
       
    }

    public void BindCountry()
    {
        DataTable dt = new DataTable();
        // dt = Obj_Country.SelectAllActiveCountry();
        dt = d.filltable("select * from [dbo].[tblcountry] where [IsActive] = 1 and [countryid] in (select [Country_Id] from [dbo].[Region_Country] where rid = '" + ddlregion.SelectedValue + "')");

        if (dt.Rows.Count > 0)
        {
            dd_country.DataSource = dt;
            dd_country.DataTextField = "countryname";
            dd_country.DataValueField = "countryid";
            dd_country.DataBind();
            dd_country.DataBind();
            //dd_country.Items.Insert(0, new ListItem("Select Country", "0"));
        }
    }

    private void BindRegion()
    {
        Country objCountry = new Country();
        DataTable dt = d.filltable("regionlist");

        if (dt.Rows.Count > 0)
        {
            ddlregion.DataSource = dt;
            ddlregion.DataTextField = "Region";
            ddlregion.DataValueField = "id";
            ddlregion.DataBind();
        }
        //ddlregion.Items.Insert(0, "Select Region");

    }

    protected void BindData()
    {
        if (Session["UserID"] != null && Session["UserID"].ToString() != "")
        {
            DataTable dt = new DataTable();
            Obj_Acc.RegistrationID = Convert.ToInt32(Session["UserID"].ToString());
            dt = Obj_Acc.Get_Registration_Detail_ByID();
            if (dt.Rows.Count > 0)
            {
                txt_firstname.Text = dt.Rows[0]["FirstName"].ToString();
                //txt_lastname.Text = dt.Rows[0]["LastName"].ToString();
                txt_email.Text = dt.Rows[0]["EmailAddress"].ToString();
                //if (dt.Rows[0]["GenderID"].ToString() == "2")
                //    rdo_Female.Checked = true;
                //else if (dt.Rows[0]["GenderID"].ToString() == "1")
                //    rdo_male.Checked = true;


                DataTable dtr = d.filltable("select rc.RId , r.* from [dbo].[Region_Country] rc inner join [dbo].[Region] r on r.Id = rc.RId where [Country_Id] = (select [CountryId] from[dbo].[tblcountry] where [countryid] = '" + dt.Rows[0]["Countryid"].ToString() + "' and isactive = 1) ");
                if (dtr.Rows.Count > 0)
                {
                    // BindRegion();
                    DataTable dt1 = d.filltable("regionlist");

                    if (dt1.Rows.Count > 0)
                    {
                        ddlregion.DataSource = dt1;
                        ddlregion.DataTextField = "Region";
                        ddlregion.DataValueField = "id";
                        ddlregion.DataBind();
                    }
                    //ddlregion.Items.Insert(0, "Select Region");
                    ddlregion.SelectedValue = dtr.Rows[0][0].ToString();
                    //BindCountryrpt(Convert.ToInt32(ddlregrpt.SelectedValue));
                    DataTable dt11 = d.filltable("select * from [dbo].[tblcountry] where [IsActive] = 1 and [countryid] in (select [Country_Id] from [dbo].[Region_Country] where rid = '" + Convert.ToInt32(ddlregion.SelectedValue) + "')");

                    if (dt11.Rows.Count > 0)
                    {
                        dd_country.DataSource = dt11;
                        dd_country.DataTextField = "countryname";
                        dd_country.DataValueField = "countryid";
                        dd_country.DataBind();
                    }

                    dd_country.SelectedValue = dt.Rows[0]["Countryid"].ToString();
                }

                //dd_country.SelectedValue = dt.Rows[0]["Countryid"].ToString();

                //txt_birthdate.Text = DateTime.Parse(dt.Rows[0]["BirthdayDate"].ToString()).ToString("MMM dd yyyy");
            }
        }
    }

    protected void btn_submit_Click(object sender, EventArgs e)
    {
        int result = 0;
        if (Session["UserID"] != null && Session["UserID"].ToString() != "")
        {
            BAL_Account Obj_account = new BAL_Account();
            string returnValue = string.Empty;
            Obj_account.Email = txt_email.Text.Trim();
            Obj_account.RegistrationID = Convert.ToInt32(HttpContext.Current.Session["UserID"].ToString());
            DataTable dt = Obj_account.Check_Email_Duplication();
            result = Convert.ToInt32(dt.Rows[0]["emailcount"].ToString());
        }

        if (result == 0)
        {
            if (Session["UserID"] != null && Session["UserID"].ToString() != "")
            {
                Obj_Acc.RegistrationID = Convert.ToInt32(Session["UserID"].ToString());
                Obj_Acc.FirstName = txt_firstname.Text.ToString().Trim();
                Obj_Acc.LastName = "";//txt_lastname.Text.ToString().Trim();
                Obj_Acc.Email = txt_email.Text.ToString().ToString();
                Obj_Acc.BirthdayDate = Convert.ToDateTime("01/01/1990");
                Obj_Acc.GenderID = 0;
                //Obj_Acc.BirthdayDate = Convert.ToDateTime(txt_birthdate.Text.ToString());
                //if (rdo_male.Checked == true)
                //{
                //    Obj_Acc.GenderID = 1;
                //}
                //else
                //{
                //    Obj_Acc.GenderID = 2;
                //}
                //Obj_Acc.BirthdayDate = Convert.ToDateTime(txt_birthdate.Text.ToString());
                Obj_Acc.Countryid = Convert.ToInt32(dd_country.SelectedValue);
                Obj_Acc.update_user_profile();
            }

            if (chk_changepass.Checked)
            {
                    obj_login.UserName = txt_email.Text.ToString().Trim();
                    obj_login.Password = txt_currentpass.Text.ToString().Trim();
                    DataTable dt = new DataTable();
                    dt = obj_login.Check_Login();
                    if (dt.Rows.Count > 0)
                    {
                        Obj_Acc.RegistrationID = Convert.ToInt32(Session["UserID"].ToString());
                        Obj_Acc.Password = txt_newpass.Text.ToString().Trim();
                        Obj_Acc.Change_User_Password();
                        {
                            chk_changepass.Checked = false;
                            //if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "es-ES")
                            //{
                            //    ScriptManager.RegisterStartupScript(this, GetType(), "for", "alert('Su contraseña se ha cambiado satisfactoriamente');", true);
                                
                            //}
                            //else
                            //{
                            ScriptManager.RegisterStartupScript(this, GetType(), "for", String.Format("alert('{0}');", ResourceManager.GetString("Yourpasswordchangedsuccessfully", System.Threading.Thread.CurrentThread.CurrentCulture.Name)), true);
                                
                            //}
                        }
                       
                    }
                    else
                    {
                        //string old = Localization.ResourceManager.GetString("Pleaseentercorrectoldpassword");
                        //chk_changepass.Checked = false;
                        //if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "es-ES")
                        //{
                        //    ScriptManager.RegisterStartupScript(this, GetType(), "for", "alert('Su contraseña antigua es incorrecta.');", true);
                            
                        //}
                        //else
                        //{
                        ScriptManager.RegisterStartupScript(this, GetType(), "for", String.Format("alert('{0}');", ResourceManager.GetString("Pleaseentercorrectoldpassword", System.Threading.Thread.CurrentThread.CurrentCulture.Name)), true);
                            
                        //}
                        
                        
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "Test", "change();", true);
                    }
                    
            }
            //if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "es-ES")Yourprofilehasbeenupdatedsuccessfully
            //{
            //    ScriptManager.RegisterStartupScript(this, GetType(), "for", "alert('Su perfil ha sido actualizado correctamente');", true);
            //}
            //else
            //{
            ScriptManager.RegisterStartupScript(this, GetType(), "for", String.Format("alert('{0}');", ResourceManager.GetString("Yourprofilehasbeenupdatedsuccessfully", System.Threading.Thread.CurrentThread.CurrentCulture.Name)), true);
            //}
        }
        else
        {
            //if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "es-ES")
            //{
            //    ScriptManager.RegisterStartupScript(this, GetType(), "for", "alert('Su perfil ha sido actualizado correctamente');", true);
            //}
            //else
            //{
            //    ScriptManager.RegisterStartupScript(this, GetType(), "for", "alert('Your profile has been updated successfully');", true);
            //}
            ScriptManager.RegisterStartupScript(this, GetType(), "for", String.Format("alert('{0}');", ResourceManager.GetString("Yourprofilehasbeenupdatedsuccessfully", System.Threading.Thread.CurrentThread.CurrentCulture.Name)), true);
        }
    }

    protected void ddlregion_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindCountry();
    }
}