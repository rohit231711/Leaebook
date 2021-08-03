using System;
using System.Globalization;
using System.Threading;
using System.Web;
using BAL;

namespace Localization
{
    /// <summary>
    /// Summary description for LocalizationHttpModule.
    /// </summary>
    public class LocalizationHttpModule : IHttpModule
    {
        public void Init(HttpApplication context)
        {
            try
            {
                context.BeginRequest += new EventHandler(context_BeginRequest);
                //context.Error += new EventHandler(context_Error);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void Dispose() { }

        private void context_BeginRequest(object sender, EventArgs e)
        {
            try
            {
                //Edited : Anjali, 20-Jun-2014

                ////Old Code
                //HttpRequest request = ((HttpApplication)sender).Request;
                //HttpContext context = ((HttpApplication)sender).Context;
                //string applicationPath = request.ApplicationPath;
                //if (applicationPath == "/")
                //{
                //    applicationPath = string.Empty;
                //}
                //string requestPath = request.Url.AbsolutePath.Substring(applicationPath.Length);
                //LoadCulture(ref requestPath);
                //context.RewritePath(applicationPath + requestPath);

                //New Code 

                HttpContext context = ((HttpApplication)sender).Context;
                if (context.Request.CurrentExecutionFilePathExtension == ".aspx" || context.Request.CurrentExecutionFilePathExtension == ".ascx" || context.Request.CurrentExecutionFilePathExtension == ".asmx")
                {
                    string lang = context.Request.QueryString["l"];
                    string cultureName = Thread.CurrentThread.CurrentCulture.Name;
                    if (lang != null)
                    {
                        switch (lang.ToLower())
                        {
                            case "es-es":
                                cultureName = "es-ES";
                                break;
                            default:
                                cultureName = "en-US";
                                break;
                        }
                    }
                    else
                    {
                        switch (cultureName.ToLower())
                        {
                            case "en-us":
                                cultureName = "es-ES";
                                break;
                            default:
                                cultureName = "en-US";
                                break;
                        }
                    }
                    LoadCulture(cultureName);
                }
                //End
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Edited : Anjali, 20-Jun-2014

        ////Old Method
        //private void LoadCulture(ref string path)
        //{
        //    string[] pathParts = path.Trim('/').Split('/');
        //    string defaultCulture = LocalizationConfiguration.GetConfig().DefaultCultureName;
        //    if (pathParts.Length > 0 && pathParts[0].Length > 0)
        //    {
        //        try
        //        {
        //            Thread.CurrentThread.CurrentCulture = new CultureInfo(pathParts[0]);
        //            path = path.Remove(0, pathParts[0].Length + 1);
        //        }
        //        catch (Exception ex)
        //        {
        //            if (!(ex is ArgumentNullException) && !(ex is ArgumentException))
        //            {
        //                throw;
        //            }
        //            Thread.CurrentThread.CurrentCulture = new CultureInfo(defaultCulture);
        //        }
        //    }
        //    else
        //    {
        //        Thread.CurrentThread.CurrentCulture = new CultureInfo(defaultCulture);
        //    }
        //    Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;
        //}

        //New Method
        
        private void LoadCulture(string strCurrentCulture = "")
        {
            try
            {
                string defaultCulture;
                defaultCulture = LocalizationConfiguration.GetConfig().DefaultCultureName;
                if (strCurrentCulture != "")
                {
                    try
                    {
                        Thread.CurrentThread.CurrentCulture = new CultureInfo(strCurrentCulture);
                    }
                    catch (Exception ex)
                    {
                        if (!(ex is ArgumentNullException) && !(ex is ArgumentException))
                        {
                            throw;
                        }
                        Thread.CurrentThread.CurrentCulture = new CultureInfo(defaultCulture);
                    }
                }
                else
                {
                    Thread.CurrentThread.CurrentCulture = new CultureInfo(defaultCulture);
                }
                Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //public void context_Error(object sender, EventArgs e)
        //{
            ////handle error
            //HttpContext ctx = HttpContext.Current;

            ////get the inner most exception
            //Exception exception;            
            //for (exception = ctx.Server.GetLastError(); exception.InnerException != null; exception = exception.InnerException) 
            //{ }

            //if (exception is HttpException && ((HttpException)exception).GetHttpCode() == 404)
            //{
            //    //logger.Warn("A 404 occurred", exception);
            //    ctx.Response.Redirect("/404.aspx");
            //}
            //else
            //{
            //    //logger.Error("ErrorModule caught an unhandled exception", exception);                
            //    //ctx.Response.Redirect("/Error.aspx");
            //}
        //}

        //End
    }
}