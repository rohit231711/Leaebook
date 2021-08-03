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
using Twitterizer;

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
        var oauth_consumer_key = "sgp4lseF0LZCZJv82S8uw";//"                               ";
        var oauth_consumer_secret = "yb9efZztSuimkGZxQTdYq4ueTlaA4SCJ4TKEKE93LrU";// "                                             ";

        if (Request["oauth_token"] == null)
        {
            OAuthTokenResponse reqToken = OAuthUtility.GetRequestToken(
                oauth_consumer_key,
                oauth_consumer_secret,
                Request.Url.AbsoluteUri);

            Response.Redirect(string.Format("http://twitter.com/oauth/authorize?oauth_token={0}",
                reqToken.Token));

        }
        else
        {
            string requestToken = Request["oauth_token"].ToString();
            string pin = Request["oauth_verifier"].ToString();

            var tokens = OAuthUtility.GetAccessToken(
                oauth_consumer_key,
                oauth_consumer_secret,
                requestToken,
                pin);

            OAuthTokens accesstoken = new OAuthTokens()
            {
                AccessToken = tokens.Token,
                AccessTokenSecret = tokens.TokenSecret,
                ConsumerKey = oauth_consumer_key,
                ConsumerSecret = oauth_consumer_secret
            };

            TwitterResponse<TwitterStatus> response = TwitterStatus.Update(
                accesstoken,
                "India clinched the first Test against Australia with a comfortable eight-wicket victory new.");

            if (response.Result == RequestResult.Success)
            {
                Response.Write("Did it yaar");
            }
            else
            {
                Response.Write("Try some other time");
            }
        }
    }
}