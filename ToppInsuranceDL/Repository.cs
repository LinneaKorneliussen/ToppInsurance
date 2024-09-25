using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;


namespace TopInsuranceDL
{
    public class Repository<T>
        where T : class
    {
        internal InsuranceContext context;
        internal DbSet<T> dbSet;

        public Repository(InsuranceContext context)
        {
            this.context = context;
            dbSet = context.Set<T>();
        }

        /// <summary>
        /// Add a new entity to the Table.
        /// </summary>
        /// <param name="entity"></param>
        public void Add(T entity)
        {
            dbSet.Add(entity);
        }
        /// <summary>
        /// Remove an entity from the Table.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>true if removed and false otherwise.</returns>
        public bool Remove(T entity)
        {
            dbSet.Remove(entity);
            return true;
        }
        /// <summary>
        /// Find a set of entities that match a predicate.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IEnumerable<T> Find(Func<T, bool> predicate)
        {
            return dbSet.Where(predicate);
        }
        /// <summary>
        /// Find the first entity that match a predicate.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public T FirstOrDefault(Func<T, bool> predicate)
        {
            return dbSet.FirstOrDefault(predicate);
        }


        /// <summary>
        /// Returns all entities of type T from the database
        /// </summary>
        /// <returns></returns>
        public IEnumerable<T> GetAll()
        {
            return dbSet;
        }

        /// <summary>
        /// Returns a queryable collection of entities of type T
        /// </summary>
        /// <returns></returns>
        public IQueryable<T> Query()
        {
            return dbSet;
        }

        /// <summary>
        /// Determines whether any element of the dataset satisfies a condition defined by a specified predicate function.
        /// </summary>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <returns>True if any element satisfies the condition; otherwise, false.</returns>
        public bool Any(Func<T, bool> predicate)
        {
            return dbSet.Any(predicate);
        }

    }
}
