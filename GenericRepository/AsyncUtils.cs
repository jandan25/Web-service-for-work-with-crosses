using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericRepository
{
    //класс для описания асинхронных методов
    public static class AsyncUtils
    {
        public static async Task<List<T>> ToListAsync<T>(this ObjectResult<T> source)
        {
            var list = new List<T>();
            await Task.Run(() => list.AddRange(source.ToList()));
            return list;
        }
    }
}
