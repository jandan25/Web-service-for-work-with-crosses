using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using CrossEntities;
using WebAppCrosses.Models;
using static WebAppCrosses.Utils;
using System.Web.UI;
using Repositories;

namespace WebAppCrosses.Controllers
{
    //[Authorize]
    public class ProductsAndOesController : BaseController<ProductsAndOesModel, ProductsAndOes>
    {
        public ProductsAndOesController() : base()
        { }

        //For Unit tests
        public ProductsAndOesController(IUnitOfWorkFactory factory)
        {
            _factory = factory;
        }

        // GET: api/Crosses
        [Route("api/Crosses")]
        //server 5600ms //client 3906 //wt 5169 //ds 4296
        [System.Web.Mvc.OutputCache(Duration = 3600, Location = OutputCacheLocation.Client)]
        public async Task<IHttpActionResult> GetCrosses()
        {
            using (IUnitOfWork unitOfWork = _factory.Create())
            {
                var result = await Task.Factory.StartNew(() => unitOfWork.GetCrossSelection().ToList());

                IEnumerable<CrossSelectionResult> records = (
                    from prGetCrossses in result
                    select new CrossSelectionResult
                    {
                        Model = prGetCrossses.Model,
                        ManufactorTS = prGetCrossses.ManufactorTS,
                        Code = prGetCrossses.Code,
                        MotorName = prGetCrossses.MotorName,
                        Comment = prGetCrossses.Comment,
                        MotorEngine = prGetCrossses.MotorEngine,
                        TypeTs = prGetCrossses.TypeTs
                    }).ToList();

                return Ok(records);
            }
        }
    }
}