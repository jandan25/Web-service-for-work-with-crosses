using CrossEntities;
using WebAppCrosses.Models;
using Repositories;
using System.Web.Http;

namespace WebAppCrosses.Controllers
{
    [Authorize]
    public class CarModelsController : BaseController<CarModelsModel, CarModels>
    {
        public CarModelsController() : base()
        { }

        protected override void DbCheck(CarModelsModel model)
        {
            using (IUnitOfWork unitOfWork = _factory.Create())
            {
                var repo = unitOfWork.GetStandardRepo<CarModels>();
                var result = repo.GetByParam(x => x.Manufactors.ManufactorID == model.ManufactorID);
                if (result == null)
                    ModelState.AddModelError("Manufactor", "ManufactorID not exist's.");
            }
        }
    }
}