using Sale.Repository.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Sale.Service.Core
{
	public class Service<T> : IService<T> where T : class
	{
		IRepository<T> _repository;
		public Service(IRepository<T> repository)
		{
			_repository = repository;
		}

		public virtual async Task Create(T entity)
		{
			_repository.Add(entity);
			await _repository.Save();
		}

		public async Task Create(IEnumerable<T> entities)
		{
			_repository.AddRange(entities);
			await _repository.Save();
		}

		public async Task Delete(T entity)
		{
			_repository.Delete(entity);
			await _repository.Save();
		}

		public async Task Delete(IEnumerable<T> entities)
		{
			_repository.DeleteRange(entities);
			await _repository.Save();
		}

		public IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate)
		{
		   
			 return _repository.FindBy(predicate);
		}

		public T? GetById(Guid? Id)
		{
			return Id != null ? _repository.GetById(Id) : null;
		}

		public IQueryable<T> GetQueryable()
		{
			return _repository.GetQueryable();
		}

		public async Task Update(T entity)
		{
			_repository.Edit(entity);
			await _repository.Save();
		}

		public async Task Update(IEnumerable<T> entities)
		{
			_repository.UpdateRange(entities);
			await _repository.Save();
		}

		public int Count(Expression<Func<T, bool>> predicate)
		{
			return _repository.GetQueryable().Count(predicate);
		}

		public T? FirstOrDefault(Expression<Func<T, bool>> predicate)
		{
			return _repository.GetQueryable().FirstOrDefault(predicate);
		}

		public IQueryable<T> Where(Expression<Func<T, bool>> predicate)
		{
			return _repository.GetQueryable().Where(predicate);
		}
	}
}
