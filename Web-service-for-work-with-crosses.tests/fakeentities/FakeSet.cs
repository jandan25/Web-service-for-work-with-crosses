using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Castle.Components.DictionaryAdapter.Xml;
using Web_service_for_work_with_crosses.tests.fakeentities;

namespace RepositoriesTests.Production
{
    public class FakeSet<T> : DbSet<T>, IDbSet<T> where T : class
    {
        ObservableCollection<T> _data;
        IQueryable _query;

        public FakeSet()
        {
            _data = new ObservableCollection<T>();
            _query = _data.AsQueryable();
        }

        public FakeSet(IEnumerable<T> values)
        {
            _data = new ObservableCollection<T>(values);
            _query = _data.AsQueryable();
        }

        public override T Find(params object[] keyValues)
        {
            //проверка только для первого элемента тестовых данных
            return _data[0];
        }

        public override T Add(T item)
        {
            _data.Add(item);
            return item;
        }

        public override T Remove(T item)
        {
            _data.Remove(item);
            return item;
        }

        public override T Attach(T item)
        {
            _data.Add(item);
            return item;
        }

        public T Detach(T item)
        {
            _data.Remove(item);
            return item;
        }

        public T Create()
        {
            return Activator.CreateInstance<T>();
        }

        public TDerivedEntity Create<TDerivedEntity>() where TDerivedEntity : class, T
        {
            return Activator.CreateInstance<TDerivedEntity>();
        }

        public ObservableCollection<T> Local
        {
            get { return _data; }
        }

        Type IQueryable.ElementType
        {
            get { return _query.ElementType; }
        }

        System.Linq.Expressions.Expression IQueryable.Expression
        {
            get { return _query.Expression; }
        }

        IQueryProvider IQueryable.Provider
        {
            get { return _query.Provider; }
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _data.GetEnumerator();
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return _data.GetEnumerator();
        }

    }
}
