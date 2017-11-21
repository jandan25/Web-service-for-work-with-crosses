using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using CrossEntities;
using WebAppCrosses.Models;
using static WebAppCrosses.Utils;


namespace WebAppCrosses.Controllers
{
    [Authorize]
    public class VenycleTypesController : BaseController<VenycleTypesModel, VenycleTypes>
    {
        public VenycleTypesController() : base()
        { }
    }
}