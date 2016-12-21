using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ABC.LeaveManagement.Core.Entities;

namespace ABC.LeaveManagement.Core.Data
{
    public interface IRepository<T> where T : BaseEntity
    {
        /// <summary>
        /// Gets entities
        /// </summary>
        IQueryable<T> GetAll();

        /// <summary>
        /// Get entity by specifying an expression
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        T GetAll(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Get entity by identifier
        /// </summary>
        /// <param name="id">Identifier</param>
        /// <returns>Entity</returns>
        T Get(int id);

        /// <summary>
        /// Get entity by specifying an expression
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        T Get(Expression<Func<T, bool>> predicate);
        
        /// <summary>
        /// Insert entity
        /// </summary>
        /// <param name="entity">Entity</param>
        void Insert(T entity);

        /// <summary>
        /// Update entity
        /// </summary>
        /// <param name="entity">Entity</param>
        void Update(T entity);

        /// <summary>
        /// Delete entity
        /// </summary>
        /// <param name="entity">Entity</param>
        void Delete(T entity);

        /// <summary>
        /// Commit changes
        /// </summary>
        Task<bool> Commit();
    }
}
