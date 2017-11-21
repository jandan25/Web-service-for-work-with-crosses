using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using CrossEntities;
using Repositories;
using WebAppCrosses.Controllers;
using System.Web.Script.Serialization;
using WebAppCrosses.Models;
using WebAppCrosses.Tests.entities;
using System.Web.Http.Results;
using System.Net.Http;
using System.Web.Http;
using System;
using Microsoft.Owin.Security.OAuth;
using Microsoft.Owin;
using System.Web.Http.Routing;
using WebAppCrosses.Tests.entities.FakeDbsets;

namespace WebAppCrosses.Tests
{
    [TestClass]
    public class BaseControllerTests
    {
        [TestMethod]
        public void Get_Context_ShouldReturn()
        {
            //Arrange
            UnitOfWorkFactory factory = new UnitOfWorkFactory(GetContext());
            var controller = new BaseController<CarModelsModel, CarModels>(factory);
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();

            //Act
            var output = controller.Get().Result;
            var json = output.ExecuteAsync(new System.Threading.CancellationToken()).Result.Content.ReadAsStringAsync().Result;
            var data = new JavaScriptSerializer().Deserialize<List<CarModelsModel>>(json);

            //Assert
            Assert.IsNotNull(output);
            Assert.IsTrue(data.Where(x => x.CarModelID == 1) != null);
            Assert.IsTrue(data.Where(x => x.CarModelID == 2) != null);
            Assert.IsTrue(data.Where(x => x.CarModelID == 3) != null);
        }

        [TestMethod]
        public void Post_Model_ShouldInsert()
        {
            //Arrange
            UnitOfWorkFactory factory = new UnitOfWorkFactory(GetContext());
            var controller = new BaseController<CarModelsModel, CarModels>(factory);
            var testdata = SetTestPostModel();
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();

            //Act
            var post = controller.Post(testdata).Result;
            var output = controller.Get().Result;
            var json = output.ExecuteAsync(new System.Threading.CancellationToken()).Result.Content.ReadAsStringAsync().Result;
            var data = new JavaScriptSerializer().Deserialize<List<CarModelsModel>>(json);
            var expected = data.Find(x => x.CarModelID == 4);

            //Assert
            Assert.IsTrue(data.Where(x => x.CarModelID == 4) != null);
            Assert.AreEqual(expected.Name,testdata.Name);
        }

        [TestMethod]
        public void Post_Model_ShouldReturnError()
        {
            //Arrange
            UnitOfWorkFactory factory = new UnitOfWorkFactory(GetContext());
            var controller = new BaseController<CarModelsModel, CarModels>(factory);
            var testdata = SetTestPostErrorModel();

            //Act
            controller.ModelState.AddModelError("Error", new Exception());
            var posterror = controller.Post(testdata).Result;

            //Assert
            Assert.IsInstanceOfType(posterror, typeof(InvalidModelStateResult));
            using (IUnitOfWork unitOfWork = factory.Create())
            {
                var repo = unitOfWork.GetStandardRepo<CarModels>();
                var output = repo.GetById(testdata.CarModelID);
                Assert.IsNull(output);
            }
        }

        [TestMethod]
        public void Put_Model_ShouldUpdate()
        {
            //Arrange
            UnitOfWorkFactory factory = new UnitOfWorkFactory(GetContext());
            var controller = new BaseController<CarModelsModel, CarModels>(factory);
            var testdata = SetTestPutModel();
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();

            //Act
            var postmodel = controller.Put(testdata.CarModelID,testdata).Result;
            var output = controller.Get().Result;
            var json = output.ExecuteAsync(new System.Threading.CancellationToken()).Result.Content.ReadAsStringAsync().Result;
            var data = new JavaScriptSerializer().Deserialize<List<CarModelsModel>>(json);
            var expected = data.Find(x => x.CarModelID == 3);

            //Assert
            Assert.IsTrue(data.Where(x => x.CarModelID == 3) != null);
            Assert.AreEqual(expected.Name, testdata.Name);
        }

        [TestMethod]
        public void Put_Model_ShouldReturnError()
        {
            //Arrange
            UnitOfWorkFactory factory = new UnitOfWorkFactory(GetContext());
            var controller = new BaseController<CarModelsModel, CarModels>(factory);
            var testdata = SetTestPutErrorModel();
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();

            //Act
            controller.ModelState.AddModelError("Error", new Exception());
            var puterror = controller.Put(testdata.CarModelID, testdata).Result;

            //Assert
            Assert.IsInstanceOfType(puterror, typeof(InvalidModelStateResult));
            using (IUnitOfWork unitOfWork = factory.Create())
            {
                var repo = unitOfWork.GetStandardRepo<CarModels>();
                var output = repo.GetById(testdata.CarModelID);
                Assert.IsNull(output);
            }
        }

        [TestMethod]
        public void Authorize_Authorized_ShouldReturn()
        {
            //Arrange
            UnitOfWorkFactory factory = new UnitOfWorkFactory(GetContext());
            MyAuthorizarionServerProvider provider = new MyAuthorizarionServerProvider(factory) { };
            var auth_context = new OAuthGrantResourceOwnerCredentialsContext(new OwinContext(), new OAuthAuthorizationServerOptions(), "id", "admin", "cd6b8f09-2146-73d3-cade-4e832627b4f6", new List<string> { });
            var noauth_context = new OAuthGrantResourceOwnerCredentialsContext(new OwinContext(), new OAuthAuthorizationServerOptions(), "id", "vasya", "vasin1992", new List<string> { });

            //Act
            provider.GrantResourceOwnerCredentials(auth_context);
            provider.GrantResourceOwnerCredentials(noauth_context);

            //Assert
            Assert.IsTrue(auth_context.IsValidated);
            Assert.IsFalse(noauth_context.IsValidated);

        }

        public FakeGoodWillContext GetContext()
        {
            var context = new FakeGoodWillContext
            {
                CarModels =
                {
                    new CarModels { CarModelID = 1, ManufactorID = 1, Name = "Жигули1", IsShown = true},
                    new CarModels { CarModelID = 2, ManufactorID = 2, Name = "Жигули2", IsShown = true},
                    new CarModels { CarModelID = 3, ManufactorID = 3, Name = "Жигули3", IsShown = true}
                }
            };
            return context;
        }

        public CarModelsModel SetTestPostModel()
        {
            var testdata = new CarModelsModel
            {
                CarModelID = 4,
                ManufactorID = 4,
                Name = "Жигули4",
                IsShown = true
            };
            return testdata;
        }
        public CarModelsModel SetTestPostErrorModel()
        {
            var testdata = new CarModelsModel
            {
                CarModelID = 4,
                ManufactorID = 4,
                Name = "NO",
                IsShown = true
            };
            return testdata;
        }

        public CarModelsModel SetTestPutModel()
        {
            var testdata = new CarModelsModel
            {
                CarModelID = 3,
                ManufactorID = 3,
                Name = "Жигули3",
                IsShown = true
            };
            return testdata;
        }
        public CarModelsModel SetTestPutErrorModel()
        {
            var testdata = new CarModelsModel
            {
                CarModelID = 5,
                ManufactorID = 5,
                Name = "Жигули5",
                IsShown = true
            };
            return testdata;
        }
    }
}
