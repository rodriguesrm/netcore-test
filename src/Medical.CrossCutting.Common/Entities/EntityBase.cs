using FluentValidator;
using System;
using System.Linq;

namespace Medical.CrossCutting.Common.Entities
{

    /// <summary>
    /// Entity abstract class
    /// </summary>
    /// <typeparam name="TEntity">Tipo de objeto</typeparam>
    public abstract class EntityBase<TEntity> : Notifiable, IEntity
        where TEntity : EntityBase<TEntity>
    {

        #region Constructors

        /// <summary>
        /// Create a new entity instance
        /// </summary>
        protected EntityBase()
        {
            Id = Guid.NewGuid();
        }

        #endregion

        #region Properties

        /// <summary>
        /// entity identification guid
        /// </summary>
        public Guid? Id { get; set; }

        #endregion

        #region Public methods

        /// <summary>
        /// Execute validate
        /// </summary>
        public abstract void Validate();

        ///<inheritdoc/>
        public override string ToString()
        {
            return $"{GetType().Name}";
        }

        ///<inheritdoc/>
        public string GetName()
        {
            return GetType().Name.Split(".").ToList().Last();
        }

        #endregion

    }

}
