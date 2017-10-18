using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using CrossEntities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GenericRepository.Implementation;
using GenericRepository.Interface;
using Moq;
using Repositories;
using RepositoriesTests.Production;
using Web_service_for_work_with_crosses.tests.fakeentities;

namespace Web_service_for_work_with_crosses.tests
{
    [TestClass]
    public class DaLTests
    {
        private static FakeSet<T> GetDbSetStub<T>(List<T> values) where T : class
        {
            return new FakeSet<T>(values);
        }

        /// <summary>
        /// Инициализация класса, имитирующего DbContext
        /// </summary>
        /// <returns></returns>
        private static FakeGoodwillEntitesContext CreateContextWithTestData()
        {
            // Добавляем в список 3 сущности
            List<FakeCarModels> list = new List<FakeCarModels>
            {
                new FakeCarModels {FakeCarModelID = 1, FakeManufactorID = 2, FakeName = "One"},
                new FakeCarModels {FakeCarModelID = 2, FakeManufactorID = 3, FakeName = "Two"},
                new FakeCarModels {FakeCarModelID = 3, FakeManufactorID = 4, FakeName = "Three"}
            };
            FakeSet<FakeCarModels> set = GetDbSetStub(list);


            Mock<FakeGoodwillEntitesContext> contextStub = new Mock<FakeGoodwillEntitesContext>();

            contextStub.Setup(x => x.Set<FakeCarModels>())
                .Returns(() => set);

            return contextStub.Object;
        }

        [TestMethod]
        public void Get_Repo_ReposelectionReturned()
        {
            //arrange
            var testContext = CreateContextWithTestData();
            var repo = new FakeGenericRepository(testContext);

            //act
            var entity = repo.Get();

            //asert
            //TODO: проверить еще количество полученных объектов
            Assert.AreNotEqual(entity, null);
        }

        [TestMethod]
        public void GetByParam_Query_OneParametredQueryReturned()
        {
            //arrange
            var testContext = CreateContextWithTestData();
            var repo = new FakeGenericRepository(testContext);
            int expectedId = 4;

            //act
            var actual = repo.GetByParam(x => x.FakeCarModelID > 1 && x.FakeName == "Three");

            //asert
            Assert.AreNotEqual(actual, null);
            Assert.AreEqual(actual.FakeManufactorID, expectedId);
        }

        [TestMethod]
        public void GetByParamQuery_query_ParametredQueryReturned()
        {
            //arrange
            var testContext = CreateContextWithTestData();
            var repo = new FakeGenericRepository(testContext);
            //act
            var actual = repo.GetByParam(x => x.FakeCarModelID > 1 && x.FakeManufactorID > 1);
            //asert
            Assert.AreNotEqual(actual, null);
        }

        [TestMethod]
        public void GetByParamAsynс_query_OneAsyncParametredQueryReturned()
        {
            //arrange
            var testContext = CreateContextWithTestData();
            var repo = new FakeGenericRepository(testContext);
            //act
            var actual = repo.GetByParamAsynс(x => x.FakeCarModelID > 1 && x.FakeManufactorID > 1);
            //asert
            Assert.AreNotEqual(actual, null);
        }

        [TestMethod]
        public void GetById_Repo_RepoSelectionReturned()
        {
            //arrange
            var testContext = CreateContextWithTestData();
            var repo = new FakeGenericRepository(testContext);
            int id = 1;

            //act
            var entity = repo.GetById(id);

            //assert
            Assert.AreNotEqual(entity, null);
            Assert.AreEqual(entity.FakeCarModelID, id);
        }

        [TestMethod]
        public void Insert_Repo_RepoPlusOneReturned()
        {
            //arrange
            var testContext = CreateContextWithTestData();
            var repo = new FakeGenericRepository(testContext);
            var model = new FakeCarModels
            {
                FakeName = "four",
                FakeCarModelID = 3,
                FakeManufactorID = 4
            };
            var actual = repo.Get();

            //act
            repo.Insert(model);
            var expected = repo.Get();

            //asert
            //TODO: проверка неполная. Надо, во-первых, проверять, что количество объектов возросло, во-вторых, что новый набор содержит добавленный объект 
            Assert.AreNotEqual(expected, actual);
        }

        [TestMethod]
        public void Insert_Repos_SomeReposCountReturned()
        {
            //arrange
            var testContext = CreateContextWithTestData();
            var repo = new FakeGenericRepository(testContext);
            List<FakeCarModels> list = new List<FakeCarModels>
            {
                new FakeCarModels {FakeCarModelID = 4, FakeManufactorID = 5, FakeName = "One"},
                new FakeCarModels {FakeCarModelID = 5, FakeManufactorID = 6, FakeName = "Two"},
                new FakeCarModels {FakeCarModelID = 6, FakeManufactorID = 7, FakeName = "Three"}
            };
            var actual = repo.Get();

            //act
            repo.Insert(list);
            var expected = repo.Get();

            //asert
            //TODO: проверка неполная. Надо, во-первых, проверять, что количество объектов возросло, во-вторых, что новый набор содержит добавленные объекты 
            Assert.AreNotEqual(expected, actual);
        }

        [TestMethod]
        public void Update_Repo_OneRepoPlusReturned()
        {
            FakeGoodwillEntitesContext testContext = CreateContextWithTestData();
            var contextStub = new Mock<GenericRepository<FakeCarModels>>(testContext);
            contextStub.Setup(x => x.SetEntityStateModified(It.IsAny<FakeCarModels>()));
            var repo = new FakeGenericRepository(testContext);

            //arrange
            var car = repo.GetByParam(x => x.FakeCarModelID == 1);
            string fakeName = "Alfasud1";

            //act
            car.FakeName = fakeName;
            
            // TODO: почему обновление не через метод репозитория? Что тут тогда тестируется? 
            contextStub.Object.Update(car);
            var newCar = repo.GetById(1);

            //asert
            Assert.AreEqual(fakeName, newCar.FakeName);
        }

        [TestMethod]
        public void Update_Repos_SomeReposCountReturned()
        {
            //arrange
            var testContext = CreateContextWithTestData();
            var contextStub = new Mock<GenericRepository<FakeCarModels>>(testContext);
            contextStub.Setup(x => x.SetEntityStateModified(It.IsAny<FakeCarModels>()));
            var repo = new FakeGenericRepository(testContext);
            string expectedName = "change";
            var car = repo.Get(x => x.FakeCarModelID >= 2);
            foreach (var c in car)
            {
                c.FakeName = expectedName;
            }
            
            //act
            // TODO: почему обновление не через метод репозитория? Что тут тогда тестируется? 
            contextStub.Object.Update(car);
            var newCar = repo.Get(x => x.FakeCarModelID >= 2);

            //asert
            Assert.AreNotEqual(car, newCar);
        }

        [TestMethod]
        public void Delete_Repo_OneRepoMinusReturned()
        {
            //arrange
            var testContext = CreateContextWithTestData();
            var repo = new FakeGenericRepository(testContext);
            int deletedID = 2;

            //act
            var actual = repo.Get(x => x.FakeCarModelID == deletedID);
            repo.Delete(deletedID);
            //repo.Delete(deletedID);
            var expected = repo.Get(x => x.FakeCarModelID == deletedID);

            //asert
            Assert.AreNotEqual(expected, actual);
        }

        [TestMethod]
        public void Delete_Repos_SomeReposDeleteReturned()
        {
            //arrange
            var testContext = CreateContextWithTestData();
            var repo = new FakeGenericRepository(testContext);
            List<FakeCarModels> list = new List<FakeCarModels>
            {
                new FakeCarModels {FakeCarModelID = 3, FakeManufactorID = 4, FakeName = "Three"}
            };
            string expectedName = "change";

            //act
            var actual = repo.Get(x => x.FakeCarModelID == 3);
            repo.Delete(list);
            var expected = repo.Get(x => x.FakeCarModelID == 3);

            //asert
            Assert.AreNotEqual(expected, actual);
        }
    }
}
