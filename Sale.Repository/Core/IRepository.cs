using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Sale.Repository.Core
{
	public interface IRepository<T> where T : class
	{
		IEnumerable<T> GetAll();
		IQueryable<T> GetQueryable();

		IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate);
		T Add(T entity);	

		T Delete(T entity);

		void Edit(T entity);

		T? GetById(object id);

		void DeleteRange(IEnumerable<T> entities);
		void AddRange(IEnumerable<T> entites);
		void UpdateRange(IEnumerable<T> entities);

		Task Save();
	}
}
