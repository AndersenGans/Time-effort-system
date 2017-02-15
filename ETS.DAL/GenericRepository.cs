using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ETS.Contracts.DataContracts;
using ETS.Contracts.Interfaces;
using System.Linq.Expressions;
using ETS.DAL.Database;
using System.Data.Entity;

namespace ETS.DAL
{
    public class GenericRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        internal DatabaseContext db;
        internal DbSet<TEntity> dbSet;

        public GenericRepository(DatabaseContext db)
        {
            this.db = db;
            this.dbSet = db.Set<TEntity>();
        }

        public virtual async Task<List<TEntity>> GetAll()
        {
            var result = await dbSet.ToListAsync();
            return result;
        }

        public virtual async Task<TEntity> GetByID(object id)
        {
            return await dbSet.FindAsync(id);
        }

        public virtual void Insert(TEntity entity)
        {
            dbSet.Add(entity);
            db.SaveChanges();
        }

        public virtual void Delete(object id)
        {
            TEntity entityToDelete = dbSet.Find(id);
            Delete(entityToDelete);
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            if (db.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
            db.SaveChanges();
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            dbSet.Attach(entityToUpdate);
            db.Entry(entityToUpdate).State = EntityState.Modified;
        }
    }
}
