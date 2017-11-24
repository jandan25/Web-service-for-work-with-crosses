using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using CrossEntities;
using WebAppCrosses.Models;
using static WebAppCrosses.Utils;
using Repositories;
using System.Collections;

namespace WebAppCrosses.Controllers
{
    [Authorize]
    public class MotorsController : BaseController<MotorsModel, Motors>
    {
        public MotorsController() : base()
        { }

        protected override bool DbCheck(MotorsModel model)
        {
            using (IUnitOfWork unitOfWork = _factory.Create())
            {
                var repo = unitOfWork.GetStandardRepo<Motors>();
                var result = repo.Get(x => x.CarModelID == model.CarModelID);
                IEnumerator resultEn = result.GetEnumerator();
                if (!resultEn.MoveNext())
                {
                    ModelState.AddModelError("CarModels", "CarModelID not exists.");
                    return false;
                }
                else
                {
                    foreach (var enty in result)
                    {
                        if (enty.Name == model.Name)
                        {
                            ModelState.AddModelError("Motors", "Name already exists");
                            return false;
                        }
                    }
                }
                return true;
            }
        }
    }
}