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
                //var products = unitOfWork.GetCrossSelection();
            }
        }
    }
}
