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
                },
                Products =
                {
                    new Products {ProductID = 1, BrandID = 1, CategoryID = 4, ProductStateID = 1, FormID = 13, Code = "1001 F", Code1C = "00001763", Name = "Колодки запасные" },
                    new Products {ProductID = 2, BrandID = 1, CategoryID = 4, ProductStateID = 1, FormID = 13, Code = "1002 F", Code1C = "00001764", Name = "Колодки основные" },
                    new Products {ProductID = 3, BrandID = 1, CategoryID = 4, ProductStateID = 1, FormID = 13, Code = "1003 F", Code1C = "00001765", Name = "Колодки запасные" }
                },
                VenycleTypes =
                {
                    new VenycleTypes {VenycleTypeID = 1, Name = "Легковые Авто", IsShown = true},
                    new VenycleTypes {VenycleTypeID = 2, Name = "Грузовые Авто", IsShown = true},
                    new VenycleTypes {VenycleTypeID = 3, Name = "Специальные Авто", IsShown = true}
                },
                Manufactors =
                {
                    new Manufactors { ManufactorID = 1, VenycleTypeID = 1, Name = "ALFA ROMEO", IsShown = true},
                    new Manufactors { ManufactorID = 2, VenycleTypeID = 2, Name = "PERKINS", IsShown = true},
                    new Manufactors { ManufactorID = 3, VenycleTypeID = 3, Name = "BELAZ", IsShown = true}
                },
                Motors =
                {
                    new Motors { MotorID = 1, CarModelID = 1, Name="1.9 JTD 16V", Engine = "", CardanShaft = "110", HoursePower = "150", StartDate = null, EndDate = null, IsShown = true },
                    new Motors { MotorID = 2, CarModelID = 2, Name="3.0 JTD V6", Engine = "", CardanShaft = "141", HoursePower = "192", StartDate = null, EndDate = null, IsShown = true },
                    new Motors { MotorID = 3, CarModelID = 3, Name="3.2 JTD V6", Engine = "", CardanShaft = "76", HoursePower = "103", StartDate = null, EndDate = null, IsShown = true }
                },
                ProductsAndMotors =
                {
                    new ProductsAndMotors { ProductAndMotorID = 20435, ProductID = 789, MotorID = 1, InPrice = true, Comment = "Комментарий"},
                    new ProductsAndMotors { ProductAndMotorID = 20436, ProductID = 1004, MotorID = 3, InPrice = true, Comment = "Комментарий"},
                    new ProductsAndMotors { ProductAndMotorID = 20437, ProductID = 1000, MotorID = 3, InPrice = true, Comment = "Комментарий"}
                },
                Users =
                {
                    new Users { UserID = 1, PartnerID = 1, RoleID = 1, Login = "admin", FirstName = "admin", LastName = "admin", MiddleName = "admin", Phone = "(800)5553535", Email = "admin@email.com", CreationDate = new DateTime(), FireDate = null, IsActive = true, Password = Guid.Parse("CD6B8F09-2146-73D3-CADE-4E832627B4F6") },
                    new Users { UserID = 2, PartnerID = 2, RoleID = 2, Login = "moder", FirstName = "moder", LastName = "moder", MiddleName = "moder", Phone = "(800)5553535", Email = "moder@email.com", CreationDate = new DateTime(), FireDate = null, IsActive = true, Password = Guid.Parse("2168ED7A-666D-5BA4-8A73-BD6552750770") },
                    new Users { UserID = 3, PartnerID = 3, RoleID = 3, Login = "user", FirstName = "user", LastName = "user", MiddleName = "user", Phone = "(800)5553535", Email = "user@email.com", CreationDate = new DateTime(), FireDate = null, IsActive = true, Password = Guid.Parse("BB6D3B7A-5F0F-0518-5DD0-4F7C94D02955") }
                },
                UserRoles =
                {
                    new UserRoles { UserRoleID = 1, Name = "Admin", Description = "Administrator"},
                    new UserRoles { UserRoleID = 2, Name = "Moder", Description = "Moderator"},
                    new UserRoles { UserRoleID = 3, Name = "User", Description = "User"}
                },
                ProductsAndOes =
                {
                    new ProductsAndOes { ProductAndOeID = 1, ProductID = 1, OeID = 1, InPrice = true},
                    new ProductsAndOes { ProductAndOeID = 2, ProductID = 2, OeID = 2, InPrice = true},
                    new ProductsAndOes { ProductAndOeID = 3, ProductID = 3, OeID = 3, InPrice = true}
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
