using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ManagerProxy2.Services.RepoBaseUnit
{
    public class BaseRepository<TModel> : IBaseRepository<TModel> where TModel : class
    {
        protected readonly DbContext Context;

        public BaseRepository(DbContext context)
        {
            Context = context;
        }
        public TModel Get(int id)
        {
            return Context.Set<TModel>().Find(id);
        }

        public TModel GetString(string id)
        {
            return Context.Set<TModel>().Find(id);
        }

        public IEnumerable<TModel> GetAll()
        {
            return Context.Set<TModel>().ToList();
        }
        public IEnumerable<TModel> Find(Expression<Func<TModel, bool>> predicate)
        {
            return Context.Set<TModel>().Where(predicate);
        }
        public void Add(TModel model)
        {
            Context.Set<TModel>().Add(model);
        }
        public void AddRange(IEnumerable<TModel> models)
        {
            Context.Set<TModel>().AddRange(models);
        }
        public void Update(TModel model)
        {
            Context.Set<TModel>().Update(model);
        }

        public void Remove(TModel model)
        {
            Context.Set<TModel>().Remove(model);
        }
        public void RemoveRange(IEnumerable<TModel> models)
        {
            Context.Set<TModel>().RemoveRange(models);
        }

        public void delete(int status)
        {
            Context.Set<TModel>().Find(status);
            Context.Update(0);
        }

        public TModel InsertNotExists(Expression<Func<TModel, bool>> predicate, TModel entity)
        {
            if (Context.Set<TModel>().Any(predicate))
                return Context.Set<TModel>().SingleOrDefault(predicate.Compile());

            Context.Set<TModel>().Add(entity);
            return entity;
        }
    }
}
