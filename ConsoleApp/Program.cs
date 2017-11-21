using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CrossEntities;
using Repositories.Implementations;
using Repositories.Interfaces;
using Repositories;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //UnitOfWorkFactory Factory = new UnitOfWorkFactory();
            //using (var unitOfWork = Factory.Create())
            //{
            //    var repo = unitOfWork.GetStandardRepo<Users>();
            //    var user = repo.GetByParam(u => u.Login == "rerer" && u.Password.ToString() == "4545ea38-9f8f-47f4-8354-cbca5bcb67b5");
            //    Console.WriteLine(user.FirstName);
            //    Console.ReadLine();
            //}


            //using (var unitOfWork = new Repositories.UnitOfWork())
            //{
            //    var result = unitOfWork.GetCrossSelection();
            //    foreach (var testResult in result)
            //    {
            //        Console.WriteLine(testResult.Code);
            //    }

            //    Console.ReadLine();



            //    var phones = unit.Database.SqlQuery<DiscountPhone>("SELECT * FROM GetPriceWithDiscount (@discount)", param);
            //    foreach (var p in phones)
            //        Console.WriteLine("{0} - {1}", p.Name, p.Price);
            //}
        }
    }
}
