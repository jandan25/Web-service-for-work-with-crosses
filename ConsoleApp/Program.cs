using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CrossEntities;
using Repositories.Implementations;
using Repositories.Interfaces;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var unitOfWork = new Repositories.UnitOfWork())
            {
                var repo = unitOfWork.GetStandardRepo<CarModels>();
                var products = repo.GetByParamAsynс(x => x.CarModelID > 1 && x.ManufactorID > 1);
               // foreach (var p in products)
                {
                    Console.WriteLine(products);
                }
                Console.ReadLine();

                //var repo = unitOfWork.GetStandardRepo<VenycleTypes>();


                //  var type = new VenycleTypes();
                //  repo.Delete(type.VenycleTypeID = 1010);
                ////  repo.Delete(type);
                //  unitOfWork.SaveChanges();

                //var product = new Firm();
                ////product.FirmID = 11;
                ////product.Code1C = "00020";
                //repo.Delete(product.FirmID = 11);

                //unitOfWork.SaveChanges();

                //var customRepo = unitOfWork.GetRepo<IProductsRepository, Product>();
                //var product = new Product();
                //product.Name = "LADA KALINA";
                //customRepo.Insert(product);

                //unitOfWork.SaveChanges();

                //var repo = unitOfWork.GetStandardRepo<User>();

                //var products = repo.Get(x => x.CategoryID == 1 && x.IsShown);
                //var products = repo.Get(x => x.CarModelID > 1);
                //foreach (var p in products)
                //{
                //    Console.WriteLine(p.Name);
                //}
                //Console.ReadLine();

                //var products = customRepo.GetProducts();
                //foreach (var p in products)
                //{
                //    System.Console.WriteLine(p.Code);
                //}
                //Console.ReadLine();

            }
        }
    }
}
