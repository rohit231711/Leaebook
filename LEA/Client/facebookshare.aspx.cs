using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BAL;
using System.Data;
using System.Configuration;
using System.Net;
using System.IO;
using Facebook;

public partial class Client_Login : System.Web.UI.Page
{
    RegistrationBAL ObjRegistration = new RegistrationBAL();
    DataTable dt = new DataTable();
    BookOrderBAL ObjBookOrderBal = new BookOrderBAL();
    HttpCookie cook;

    public string ApplicationId
    {
        get
        {
            return ConfigurationManager.AppSettings["ApplicationId"];
        }
    }

    public string ExtendedPermissions
    {
        get
        {
            return ConfigurationManager.AppSettings["ExtendedPermissions"];
        }
    }

    public string AppSecret
    {
        get
        {
            return ConfigurationManager.AppSettings["ApplicationSecret"];
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //var destinationURL = String.Format(
        //       @"https://www.facebook.com/dialog/oauth?client_id={0}&scope={1}&redirect_uri=http://www.facebook.com/connect/login_success.html&response_type=token",
        //       this.ApplicationId,
        //       this.ExtendedPermissions);

        //if (!IsPostBack)
        //{
        //    Response.Redirect(destinationURL);
        //}
        //string msg = "Manan patel";
        //Session["postStatus"] = msg;

        //Facebook facebook = auth.FacebookAuth();
        //if (Session["facebookQueryStringValue"] == null)
        //{
        //    string authLink = facebook.GetAuthorizationLink();
        //    //ClientScript.RegisterClientScriptBlock(this.GetType(), "asd1", "window.open(" + authLink + ", 'title', 'width=660,height=500,status=no,scrollbars=yes,toolbar=0,menubar=no,resizable=yes,top=60,left=320');", true);
        //    ScriptManager.RegisterStartupScript(Page, typeof(Page), "", "alert('hi'); window.open('" + authLink + "')", true);
        //}

        //if (Session["facebookQueryStringValue"] != null)
        //{
        //    facebook.GetAccessToken(Session["facebookQueryStringValue"].ToString());
        //    FBUser currentUser = facebook.GetLoggedInUserInfo();
        //    IFeedPost FBpost = new FeedPost();
        //    if (Session["postStatus"].ToString() != "")
        //    {
        //        FBpost.Message = Session["postStatus"].ToString();
        //        facebook.PostToWall(currentUser.id.GetValueOrDefault(), FBpost);
        //    }
        //}
        //CheckAuthorization();
        CheckAuthorization();
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {

    }
    protected void Post_Click(object sender, EventArgs e)
    {

    }

    private void CheckAuthorization()
    {
        string app_id = "216438618538882";// "741235135905751";
        string app_secret = "e23fa4e2b622e09f31a892341f019149";//"d1552e5acbb2b9c6e9f25e8cf7929d8a";
        string scope = "publish_stream,manage_pages";

        if (Request.QueryString["a"] != null)
        {
            app_id = Request.QueryString["a"].ToString();
        }
        if (Request.QueryString["s"] != null)
        {
            app_secret = Request.QueryString["s"].ToString();
        }

                //"https://graph.facebook.com/oauth/authorize?client_id={0}&redirect_uri={1}&scope={2}",
        if (Request["code"] == null)
        {
            Response.Redirect(string.Format(
                "https://www.facebook.com/dialog/feed?app_id={0}&display=popup&caption=TheMagz&link={1}&redirect_uri={2}&cancel_url={3}",
                app_id, "http://themagz.net/Client/Index.aspx", "http://themagz.net/Client/Index.aspx", "http://themagz.net/Client/Index.aspx"));//scope));


            
//https://www.facebook.com/dialog/feed?
//app_id=145634995501895&display=popup
//&caption=An%20example%20caption&
//link=https%3A%2F%2Fdevelopers.facebook.com%2Fdocs%2Fdialogs%2F&
//redirect_uri=https://developers.facebook.com/tools/explorer

        }
        else
        {
            Dictionary<string, string> tokens = new Dictionary<string, string>();

            string url = string.Format("https://graph.facebook.com/oauth/access_token?client_id={0}&redirect_uri={1}&scope={2}&code={3}&client_secret={4}",
                app_id, Request.Url.AbsoluteUri, scope, Request["code"].ToString(), app_secret);

            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;

            using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
            {
                StreamReader reader = new StreamReader(response.GetResponseStream());

                string vals = reader.ReadToEnd();

                foreach (string token in vals.Split('&'))
                {
                    //meh.aspx?token1=steve&token2=jake&...
                    tokens.Add(token.Substring(0, token.IndexOf("=")),
                        token.Substring(token.IndexOf("=") + 1, token.Length - token.IndexOf("=") - 1));
                }
            }

            string access_token = tokens["access_token"];

            var client = new FacebookClient(access_token);

            client.Post("/me/feed", new { message = "markhagan.me video tutorial" });
        }
    }
}