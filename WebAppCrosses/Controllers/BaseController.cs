﻿using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using ConverterEntities;
using static WebAppCrosses.Utils;
using System.ComponentModel.DataAnnotations;
using CrossEntities;

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
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            using (IUnitOfWork unitOfWork = _factory.Create())
            {
                var newcarmodel = new T();
                CopyModeltoEntity (model, newcarmodel);

                var repo = unitOfWork.GetStandardRepo<T>();
                repo.Insert(newcarmodel);
                await unitOfWork.SaveChangesAsync();
                return Ok(newcarmodel);
            }
        }

        [HttpPut]
        public virtual async Task<IHttpActionResult> Put(int id, U model)
        {
            var idModel = (int)model.PropertyByAtt<KeyAttribute>().GetValue(model);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != idModel)
            {
                return BadRequest();
            }

            using (IUnitOfWork unitOfWork = _factory.Create())
            {
                var newmodel = new T();
                CopyModeltoEntity(model, newmodel);

                var repo = unitOfWork.GetStandardRepo<T>();
                repo.Update(newmodel);
                repo.SetEntityStateModified(newmodel);
                await unitOfWork.SaveChangesAsync();
                return StatusCode(HttpStatusCode.NoContent);
            }
        }
    }
}
