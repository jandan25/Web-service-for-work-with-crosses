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
    public class ManufactorsController : BaseController<ManufactorsModel, Manufactors>
    {
        public ManufactorsController() : base()
        { }

        protected override bool DbCheck(ManufactorsModel model)
        {
            using (IUnitOfWork unitOfWork = _factory.Create())
            {
                var repo = unitOfWork.GetStandardRepo<Manufactors>();
                var result = repo.Get(x => x.VenycleTypeID == model.VenycleTypeID);
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
                            ModelState.AddModelError("Manufactors", "Name already exists");
                            return false;
                        }
                    }
                }
                return true;
            }
        }
    }
}