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
            int lengthOfPassword = 8;
            string guid = Guid.NewGuid().ToString().Replace("-", "");

            Console.WriteLine(guid.Substring(0, lengthOfPassword));
            Console.ReadLine();

            //var guidstr = "BB6D3B7A-5F0F-0518-5DD0-4F7C94D02955";
            //Guid newGuid = Guid.Parse(guidstr);
            //Console.WriteLine("Converted {0} to a Guid", newGuid.ToString());
            //Console.ReadLine();

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
