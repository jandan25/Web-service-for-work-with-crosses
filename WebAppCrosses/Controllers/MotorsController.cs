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
    public class MotorsController : BaseController<MotorsModel, Motors>
    {
        public MotorsController() : base()
        { }

        protected override void DbCheck(MotorsModel model)
        { }
    }
}