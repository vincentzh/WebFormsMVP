﻿using System;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using WebFormsMvp.Binder;
using WebFormsMvp.Castle;
using WebFormsMvp.FeatureDemos.Logic.Presenters;
using WebFormsMvp.FeatureDemos.Logic.Data;

namespace WebFormsMvp.FeatureDemos.Web
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_PreRequestHandlerExecute(object sender, EventArgs e)
        {
            // Add page PreRender event handler
            HttpApplication app = sender as HttpApplication;
            Page page = app.Context.CurrentHandler as Page;
            if (page != null)
            {
                page.PreRender += new EventHandler(page_PreRender);
            }
        }

        void page_PreRender(object sender, EventArgs e)
        {
            // Move content-type meta tag to top of head, ASP.NET inserts Theme stylesheet links before it
            Page page = sender as Page;
            var meta = page.Header.Controls
                .OfType<HtmlMeta>()
                .FirstOrDefault(m => m.HttpEquiv == "Content-Type");
            if (meta != null && page.Header.Controls.IndexOf(meta) > 0)
            {
                page.Header.Controls.Remove(meta);
                page.Header.Controls.AddAt(0, meta);
            }
        }

        protected void Application_EndRequest(object sender, EventArgs e)
        {
            // Remove page PreRender event handler
            HttpApplication app = sender as HttpApplication;
            Page page = app.Context.CurrentHandler as Page;

            if (page != null)
            {
                page.PreRender -= page_PreRender;
            }
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {
        }
    }
}