﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;
using WebFormsMvp.Binder;

namespace WebFormsMvp.UnitTests.Binder
{
    [TestClass]
    public class DefaultPresenterFactoryTests
    {
        // ReSharper disable InconsistentNaming

        public class GetBuildMethodInternal_TestClassWithParameter
        {
            public GetBuildMethodInternal_TestClassWithParameter(string param)
            {
                Param = param;
            }
            public string Param { get; private set; }
        }

        [TestMethod]
        public void DefaultPresenterFactory_GetBuildMethodInternal_ShouldReturnWorkingBuildMethodForConstructorWithOneParameter()
        {
            // Arrange
            var typeToBuild = typeof(GetBuildMethodInternal_TestClassWithParameter);

            // Act
            var buildMethod = DefaultPresenterFactory.GetBuildMethodInternal(
                typeToBuild,
                typeof(string));
            var instance = buildMethod.Invoke(null, new[] { "test" });

            // Assert
            Assert.IsInstanceOfType(instance, typeToBuild);
            Assert.AreEqual("test", ((GetBuildMethodInternal_TestClassWithParameter)instance).Param);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void DefaultPresenterFactory_GetBuildMethodInternal_ShouldThrowArgumentExcpetionWhenMatchingConstructorNotFound()
        {
            // Arrange
            var typeToBuild = typeof(GetBuildMethodInternal_TestClassWithParameter);

            // Act
            DefaultPresenterFactory.GetBuildMethodInternal(
                typeToBuild,
                typeof(int));

            // Assert
        }

        [TestMethod]
        public void DefaultPresenterFactory_GetBuildMethod_ShouldReturnSameMethodMultipleTimes()
        {
            // Arrange
            
            // Act
            var buildMethod1 = DefaultPresenterFactory.GetBuildMethod(typeof(string), typeof(char[]));
            var buildMethod2 = DefaultPresenterFactory.GetBuildMethod(typeof(string), typeof(char[]));

            // Assert
            Assert.AreEqual(buildMethod1, buildMethod2);
        }

        [TestMethod]
        public void DefaultPresenterFactory_GetBuildMethod_ShouldReturnDifferentMethodsForDifferentConstructors()
        {
            // Arrange
            
            // Act
            var buildMethod1 = DefaultPresenterFactory.GetBuildMethod(typeof(string), typeof(char*));
            var buildMethod2 = DefaultPresenterFactory.GetBuildMethod(typeof(string), typeof(char[]));

            // Assert
            Assert.AreNotEqual(buildMethod1, buildMethod2);
        }

        class Release_DisposablePresenter : Presenter<IView>, IDisposable
        {
            public Release_DisposablePresenter(IView view)
                : base(view)
            {}

            public bool DisposeCalled { get; private set; }

            public void Dispose()
            {
                DisposeCalled = true;
            }
        }
        [TestMethod]
        public void DefaultPresenterFactory_Release_ShouldCallDispose()
        {
            // Arrange
            var view = MockRepository.GenerateMock<IView>();
            var presenter = new Release_DisposablePresenter(view);

            // Act
            new DefaultPresenterFactory().Release(presenter);

            // Assert
            Assert.IsTrue(presenter.DisposeCalled);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void DefaultPresenterFactory_Create_ShouldThrowArgumentNullExcpetionIfPresenterTypeIsNull()
        {
            // Arrange
            var viewType = typeof(IView);
            var viewInstance = MockRepository.GenerateMock<IView>();

            // Act
            new DefaultPresenterFactory().Create(
                null,
                viewType,
                viewInstance);

            // Assert
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void DefaultPresenterFactory_Create_ShouldThrowArgumentNullExcpetionIfViewTypeIsNull()
        {
            // Arrange
            var presenterType = typeof(Create_Presenter);
            var viewInstance = MockRepository.GenerateMock<IView>();

            // Act
            new DefaultPresenterFactory().Create(
                presenterType,
                null,
                viewInstance);

            // Assert
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void DefaultPresenterFactory_Create_ShouldThrowArgumentNullExcpetionIfViewInstanceIsNull()
        {
            // Arrange
            var presenterType = typeof(Create_Presenter);
            var viewType = typeof(IView);

            // Act
            new DefaultPresenterFactory().Create(
                presenterType,
                viewType,
                null);

            // Assert
        }

        public class Create_Presenter : Presenter<IView>
        {
            public Create_Presenter(IView view)
                : base(view)
            { }
        }
        [TestMethod]
        public void DefaultPresenterFactory_Create_ShouldReturnInstance()
        {
            // Arrange
            var presenterType = typeof (Create_Presenter);
            var viewType = typeof (IView);
            var viewInstance = MockRepository.GenerateMock<IView>();

            // Act
            var presenter = new DefaultPresenterFactory().Create(
                presenterType,
                viewType,
                viewInstance);

            // Assert
            Assert.IsInstanceOfType(presenter, presenterType);
        }

        public class Create_ErrorPresenter : Presenter<IView>
        {
            public Create_ErrorPresenter(IView view)
                : base(view)
            {
                throw new ApplicationException("test exception");
            }
        }
        [TestMethod]
        public void DefaultPresenterFactory_Create_ShouldWrapExceptions()
        {
            // Arrange
            var presenterType = typeof(Create_ErrorPresenter);
            var viewType = typeof(IView);
            var viewInstance = MockRepository.GenerateMock<IView>();

            try
            {
                // Act
                new DefaultPresenterFactory().Create(
                    presenterType,
                    viewType,
                    viewInstance);

                // Assert
                Assert.Fail();
            }
            catch (Exception ex)
            {
                // Assert
                Assert.IsInstanceOfType(ex, typeof(InvalidOperationException));
                Assert.IsInstanceOfType(ex.InnerException, typeof(ApplicationException));
                Assert.AreEqual(ex.InnerException.Message, "test exception");
            }
        }

        // ReSharper restore InconsistentNaming
    }
}