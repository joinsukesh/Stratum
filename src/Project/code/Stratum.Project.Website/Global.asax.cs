namespace Stratum.Project.Website
{
    using System;
    using System.Net;
    using System.Web;
    //https://sitecore.stackexchange.com/questions/221/how-do-you-setup-a-404-and-500-error-page-for-missing-files-and-media-items
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {

        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Exception error = Server.GetLastError();
            Server.ClearError();
            HttpException httpException = error as HttpException;
            Response.StatusCode = httpException != null ? httpException.GetHttpCode() : (int)HttpStatusCode.InternalServerError;
            Sitecore.Diagnostics.Log.Fatal("Uncaught application error", error, this);
        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}