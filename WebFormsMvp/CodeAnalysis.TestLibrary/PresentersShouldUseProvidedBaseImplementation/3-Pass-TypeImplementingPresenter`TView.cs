﻿using System;
using System.Web;
using System.Web.Caching;
using System.Web.Routing;
using WebFormsMvp;

namespace CodeAnalysis.TestLibrary.PresentersShouldUseProvidedBaseImplementation
{
    public class Test2 : IPresenter<IView>
    {
        public HttpContextBase HttpContext
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public HttpRequestBase Request
        {
            get { throw new NotImplementedException(); }
        }

        public HttpResponseBase Response
        {
            get { throw new NotImplementedException(); }
        }

        public HttpServerUtilityBase Server
        {
            get { throw new NotImplementedException(); }
        }

        public Cache Cache
        {
            get { throw new NotImplementedException(); }
        }

        public RouteData RouteData
        {
            get { throw new NotImplementedException(); }
        }

        public IAsyncTaskManager AsyncManager
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public IMessageBus Messages
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public IView View
        {
            get { throw new NotImplementedException(); }
        }
    }
}