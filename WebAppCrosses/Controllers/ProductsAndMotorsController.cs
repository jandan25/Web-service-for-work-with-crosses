using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using CrossEntities;
using WebAppCrosses.Models;
using static WebAppCrosses.Utils;
using Repositories;

namespace WebAppCrosses.Controllers
{
    [Authorize]
    public class ProductsAndMotorsController : BaseController<ProductsAndMotorsModel, ProductsAndMotors>
    {
        public ProductsAndMotorsController() : base()
        { }

        protected override bool DbCheck(ProductsAndMotorsModel model)
        {
            using (IUnitOfWork unitOfWork = _factory.Create())
            {
                var repo = unitOfWork.GetStandardRepo<ProductsAndMotors>();
                var result = repo.GetByParam(x => x.Products.ProductID == model.ProductID &&
                x.Motors.MotorID == model.MotorID);
                if (result == null)
                {
                    ModelState.AddModelError("ProductsAndMotors", "ProductID or MotorID not exist's.");
                    return false;
                }
                else
                {
                    var exists = repo.GetByParam(
                                   x =>
                                       x.ProductAndMotorID != model.ProductAndMotorID && x.ProductID == model.ProductID &&
                                       x.MotorID == model.MotorID);
                    if (exists != null)
                    {
                        ModelState.AddModelError("ProductsAndMotors", "Already exists.");
                        return false;
                    }
                }
                return true;   
            }
        }
    }
}