using CrossEntities;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAppCrosses.Models;

namespace WebAppCrosses.Controllers
{
    public class ProductsController : BaseController<ProductsModel, Products>
    {
        public ProductsController() : base()
        { }

        protected override bool DbCheck(ProductsModel model)
        {
            using (IUnitOfWork unitOfWork = _factory.Create())
            {
                var repo = unitOfWork.GetStandardRepo<Products>();
                var product = repo.GetByParam(
                           x =>
                               x.Code.Replace(" ", "").ToUpper().Equals(model.Code.Replace(" ", "").ToUpper()) &&
                               x.ProductID != model.ProductID);
                if (product != null)
                {
                    ModelState.AddModelError("Code", "Товар" + model.Code);
                    return false;
                }
                return true;
            }
        }
    }
}
