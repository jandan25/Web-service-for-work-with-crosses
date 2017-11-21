using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RepositoriesTests.Production;
using Web_service_for_work_with_crosses.tests.fakeentities;
using CrossEntities;

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
            var context = new FakeGoodwillEntitesContext
            {
                CarModels =
                {
                    new CarModels {CarModelID = 1, ManufactorID = 1, Name = "One", IsShown = true},
                    new CarModels {CarModelID = 2, ManufactorID = 2, Name = "Two", IsShown = true},
                    new CarModels {CarModelID = 3, ManufactorID = 3, Name = "Three", IsShown = true}
                }
            };
            return context;
        }

            //// Добавляем в список 3 сущности
            //List<FakeCarModels> list = new List<FakeCarModels>
            //{
            //    new FakeCarModels {FakeCarModelID = 1, FakeManufactorID = 2, FakeName = "One"},
            //    new FakeCarModels {FakeCarModelID = 2, FakeManufactorID = 3, FakeName = "Two"},
            //    new FakeCarModels {FakeCarModelID = 3, FakeManufactorID = 4, FakeName = "Three"}
            //};
            //FakeSet<FakeCarModels> set = GetDbSetStub(list);


            //Mock<FakeGoodwillEntitesContext> contextStub = new Mock<FakeGoodwillEntitesContext>();

            //contextStub.Setup(x => x.Set<FakeCarModels>())
            //    .Returns(() => set);

            //return contextStub.Object;
        //}

        [TestMethod]
        public void Get_Repo_ReposelectionReturned()
        {
            //arrange
            var testContext = CreateContextWithTestData();
            var repo = new FakeGenericRepository(testContext);
            var actualCount = 3;
            //act
            var entity = repo.Get();
            var expectedCount = entity.Count();

            //asert
            Assert.AreNotEqual(entity, null);
            Assert.AreEqual(actualCount,expectedCount);
        }

        [TestMethod]
        public void GetByParam_Query_OneParametredQueryReturned()
        {
            //arrange
            var testContext = CreateContextWithTestData();
            var repo = new FakeGenericRepository(testContext);
            int expectedId = 3;

            //act
            var actual = repo.GetByParam(x => x.CarModelID > 1 && x.Name == "Three");

            //asert
            Assert.AreNotEqual(actual, null);
            Assert.AreEqual(actual.ManufactorID, expectedId);
        }

        [TestMethod]
        public void GetByParamQuery_query_ParametredQueryReturned()
        {
            //arrange
            var testContext = CreateContextWithTestData();
            var repo = new FakeGenericRepository(testContext);
            //act
            var actual = repo.GetByParam(x => x.CarModelID > 1 && x.ManufactorID > 1);
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
            var actual = repo.GetByParamAsynс(x => x.CarModelID > 1 && x.ManufactorID > 1);
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
            Assert.AreEqual(entity.CarModelID, id);
        }

        [TestMethod]
        public void Insert_Repo_RepoPlusOneReturned()
        {
            //arrange
            var testContext = CreateContextWithTestData();
            var repo = new FakeGenericRepository(testContext);
            var model = new CarModels
            {
                Name = "four",
                CarModelID = 4,
                ManufactorID = 5
            };
            var actualCount = repo.Get().Count();

            //act
            repo.Insert(model);
            var expectedCount = repo.Get().Count();
            var expectedRecord = repo.GetByParam(x=> x.CarModelID == 4);

            //asert
            Assert.AreEqual(actualCount + 1, expectedCount);
            Assert.AreEqual(expectedRecord.Name,model.Name);
        }

        [TestMethod]
        public void Insert_Repos_SomeReposCountReturned()
        {
            //arrange
            var testContext = CreateContextWithTestData();
            var repo = new FakeGenericRepository(testContext);
            List<CarModels> list = new List<CarModels>
            {
                new CarModels {CarModelID = 4, ManufactorID = 5, Name = "Four"},
                new CarModels {CarModelID = 5, ManufactorID = 6, Name = "Five"},
                new CarModels {CarModelID = 6, ManufactorID = 7, Name = "Six"}
            };
            var actualCount = repo.Get().Count();

            //act
            repo.Insert(list);
            var expectedCount = repo.Get().Count();
            var expected = repo.GetByParam(x => x.CarModelID == 6);

            //asert
            Assert.AreEqual(actualCount + 3, expectedCount);
            Assert.AreEqual(list[2].Name,expected.Name);
        }

        [TestMethod]
        public void Update_Repo_OneRepoPlusReturned()
        {
            FakeGoodwillEntitesContext testContext = CreateContextWithTestData();
            var repo = new FakeGenericRepository(testContext);
            
            //arrange
            var car = repo.GetByParam(x => x.CarModelID == 1);
            string fakeName = "Alfasud1";

            //act
            car.Name = fakeName;
            repo.Update(car);
            testContext.SaveChanges();
            var newCar = repo.GetByParam(x => x.CarModelID == 1);

            //asert
            Assert.AreEqual(fakeName, newCar.Name);
        }

        [TestMethod]
        public void Update_Repos_SomeReposCountReturned()
        {
            //arrange
            var testContext = CreateContextWithTestData();
            var repo = new FakeGenericRepository(testContext);
            string expectedName = "change";
            var car = repo.Get(x => x.CarModelID >= 2);
            foreach (var c in car)
            {
                c.Name = expectedName;
            }

            //act
            repo.Update(car);
            testContext.SaveChanges();
            var newCar = repo.Get(x => x.CarModelID >= 2);

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
            var actual = repo.Get(x => x.CarModelID == deletedID);
            repo.Delete(deletedID);
            //repo.Delete(deletedID);
            var expected = repo.Get(x => x.CarModelID == deletedID);

            //asert
            Assert.AreNotEqual(expected, actual);
        }

        [TestMethod]
        public void Delete_Repos_SomeReposDeleteReturned()
        {
            //arrange
            var testContext = CreateContextWithTestData();
            var repo = new FakeGenericRepository(testContext);
            List<CarModels> list = new List<CarModels>
            {
                new CarModels {CarModelID = 3, ManufactorID = 4, Name = "Three"}
            };

            //act
            var actual = repo.Get(x => x.CarModelID == 3);
            repo.Delete(list);
            var expected = repo.Get(x => x.CarModelID == 3);

            //asert
            Assert.AreNotEqual(expected, actual);
        }
    }
}
