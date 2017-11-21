using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using CrossEntities;
using WebAppCrosses.Models;
using static WebAppCrosses.Utils;
using System.Collections.Generic;
using Web_service_for_work_with_crosses.tests.fakeentities;
using Repositories;

namespace WebAppCrosses.Controllers
{
    [Authorize]
    public class CarModelsController : BaseController<CarModelsModel, CarModels>
    {
        public CarModelsController() : base()
        { }
    }
}