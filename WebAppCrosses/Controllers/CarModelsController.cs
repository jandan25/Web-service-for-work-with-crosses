using CrossEntities;
using WebAppCrosses.Models;
using Repositories;
using System.Web.Http;
using System.Collections;

namespace WebAppCrosses.Controllers
{
    //[Authorize]
    public class CarModelsController : BaseController<CarModelsModel, CarModels>
    {
        public CarModelsController() : base()
        { }

        protected override bool DbCheck(CarModelsModel model)
        {
            using (IUnitOfWork unitOfWork = _factory.Create())
            {
                var repo = unitOfWork.GetStandardRepo<CarModels>();
                var result = repo.Get(x => x.Manufactors.ManufactorID == model.ManufactorID);
                IEnumerator resultEn = result.GetEnumerator();
                if (!resultEn.MoveNext())
                {
                    ModelState.AddModelError("Manufactor", "ManufactorID not exists.");
                    return false;
                }
                else
                {
                    foreach (var enty in result)
                    {
                        if (enty.Name == model.Name)
                        {
                            ModelState.AddModelError("CarModel", "Name already exists");
                            return false;
                        }
                    }
                }
                return true;
            }
        }
    }
}