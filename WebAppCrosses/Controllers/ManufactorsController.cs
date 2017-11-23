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
    public class ManufactorsController : BaseController<ManufactorsModel, Manufactors>
    {
        public ManufactorsController() : base()
        { }

        protected override void DbCheck(ManufactorsModel model)
        {
            using (IUnitOfWork unitOfWork = _factory.Create())
            {
                var repo = unitOfWork.GetStandardRepo<Manufactors>();
                var result = repo.GetByParam(x => x.VenycleTypes.VenycleTypeID == model.ManufactorID);
                if (result == null)
                    ModelState.AddModelError("VenycleTypes", "VenycleTypeID not exist's.");
            }
        }
    }
}