using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataAccess
{
    public class EfEntityRepositoryBase<TEntity, TContext> : IEntityRepository<TEntity>
        where TEntity : class
        where TContext : DbContext, new()
    {

        public void Add(TEntity entity)
        {

            using (TContext context = new TContext())
            {
                context.Entry(entity).State = EntityState.Added;
                context.SaveChanges();
            }

        }

        public void Delete(TEntity entity)
        {
            using (TContext context = new TContext())
            {
                var deletedEntity = context.Entry(entity);
                deletedEntity.State = EntityState.Deleted;
                context.SaveChanges();
            }
        }

        public TEntity Get(Expression<Func<TEntity, bool>> filter)
        {
            using (TContext context = new TContext())
            {
                return context.Set<TEntity>().SingleOrDefault(filter);
            }
        }

        public List<TEntity> GetAll(int page, int size,
            Expression<Func<TEntity, object>> orderBy = null, bool ascending = true)
        {
            var pageResults = (float) size;

            using (TContext context = new TContext())
            {
                IQueryable<TEntity> query = context.Set<TEntity>() ;

                if (orderBy != null )
                {
                    query = ascending ? query.OrderBy(orderBy) : query.OrderByDescending(orderBy);
                }

                var pageCount = Math.Ceiling(context.Set<TEntity>().Count() / pageResults);

                return query
                    .Skip((page - 1) * (int)pageResults)
                    .Take((int)pageResults)
                    .ToList();
            }
        }

        public void Update(TEntity entity)
        {
            using (TContext context = new TContext())
            {
                var updatedEntity = context.Entry(entity);
                updatedEntity.State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public List<TEntity> GetAllByFilter(Expression<Func<TEntity, bool>> filter = null)
        {
            using (TContext context = new TContext())
            {
                IQueryable<TEntity> query = filter == null ? context.Set<TEntity>() : context.Set<TEntity>().Where(filter);

                return query.ToList();
            }
        }
    }
}
