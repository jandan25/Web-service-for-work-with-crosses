using Repositories;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using static WebAppCrosses.Utils;
using System.ComponentModel.DataAnnotations;

namespace WebAppCrosses.Controllers
{
    /// <summary>
    /// Базовый контроллер
    /// Инкапсулирует всю общую логику по работе с объектами базы данных
    /// </summary>
    /// <typeparam name="U">Тип модели WebAPI</typeparam>
    /// <typeparam name="T">Тип сущности EntityFramework</typeparam>
    public class BaseController<U, T> : ApiController where T : class, new() where U : class, new()
    {
        public IUnitOfWorkFactory _factory;

        protected virtual void DbCheck(U model) { }

        public BaseController(IUnitOfWorkFactory factory)
        {
            _factory = factory;
        }
        public BaseController()
        {
            _factory = new UnitOfWorkFactory();
        }

        [HttpGet]
        public virtual async Task<IHttpActionResult> Get()
        {
            using (IUnitOfWork unitOfWork = _factory.Create())
            {
                var repo = unitOfWork.GetStandardRepo<T>(); 
                var result = await Task.Factory.StartNew(() => repo.Get());
                return Ok(result);
            }
        }

        [HttpPost]
        public virtual async Task<IHttpActionResult> Post(U model)
        {
            DbCheck(model);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            using (IUnitOfWork unitOfWork = _factory.Create())
            {
                var newmodel = new T();
                CopyModeltoEntity (model, newmodel);
                var repo = unitOfWork.GetStandardRepo<T>();
                var id = model.PropertyByAtt<KeyAttribute>().GetValue(model);
                var result = await Task.Factory.StartNew(() => repo.GetById(id));
                if (result == null)
                {
                    repo.Insert(newmodel);
                    await unitOfWork.SaveChangesAsync();
                    return Ok(newmodel);
                }
                else
                    return BadRequest("Data you provided is not supported.");
            }
        }

        [HttpPut]
        public virtual async Task<IHttpActionResult> Put(int id, U model)
        {
            DbCheck(model);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var idModel = (int)model.PropertyByAtt<KeyAttribute>().GetValue(model);
            if (id != idModel)
            {
                return BadRequest();
            }

            using (IUnitOfWork unitOfWork = _factory.Create())
            {
                var newmodel = new T();
                CopyModeltoEntity(model, newmodel);
                var repo = unitOfWork.GetStandardRepo<T>();

                var result = await Task.Factory.StartNew(() => repo.GetById(idModel));
                if (result != null)
                {
                    CopyModeltoEntity(model, result);
                    repo.Update(result);
                    await unitOfWork.SaveChangesAsync();
                    return StatusCode(HttpStatusCode.NoContent);
                }
                else
                    return BadRequest("Data you provided is not supported.");
            }
        }
    }
}
