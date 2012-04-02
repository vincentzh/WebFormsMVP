﻿using System;
using System.Security.Principal;
using System.Web;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;
using WebFormsMvp.FeatureDemos.Logic.Presenters;
using WebFormsMvp.FeatureDemos.Logic.Views.Models;

namespace WebFormsMvp.FeatureDemos.UnitTests
{
    [TestClass]
    public class HelloWorldPresenterTests
    {
        [TestMethod]
        public void HelloWorldPresenterSetsViewMessageForAnonymousUser()
        {
            // Arrange
            var view = MockRepository.GenerateStub<IView<HelloWorldModel>>();
            var httpContext = MockRepository.GenerateMock<HttpContextBase>();
            var identity = MockRepository.GenerateMock<IIdentity>();
            var user = MockRepository.GenerateMock<IPrincipal>();

            httpContext.Stub(h => h.User).Return(user);
            user.Stub(u => u.Identity).Return(identity);
            identity.Stub(i => i.IsAuthenticated).Return(false);

            var presenter = new HelloWorldPresenter(view)
            {
                HttpContext = httpContext
            };

            // Act
            view.Raise(v => v.Load += null, view, new EventArgs());

            // Assert
            Assert.AreEqual("Hello World!", view.Model.Message);
        }

        [TestMethod]
        public void HelloWorldPresenterSetsViewMessageForAuthenticatedUser()
        {
            // Arrange
            var view = MockRepository.GenerateStub<IView<HelloWorldModel>>();
            var httpContext = MockRepository.GenerateMock<HttpContextBase>();
            var identity = MockRepository.GenerateMock<IIdentity>();
            var user = MockRepository.GenerateMock<IPrincipal>();

            httpContext.Stub(h => h.User).Return(user).Repeat.Twice();
            user.Stub(u => u.Identity).Return(identity).Repeat.Twice();
            identity.Stub(i => i.IsAuthenticated).Return(true);
            const string name = "Bob";
            identity.Stub(i => i.Name).Return(name);

            var presenter = new HelloWorldPresenter(view)
            {
                HttpContext = httpContext
            };

            // Act
            view.Raise(v => v.Load += null, view, new EventArgs());

            // Assert
            Assert.AreEqual(string.Format("Hello {0}!", name), view.Model.Message);
        }
    }
}