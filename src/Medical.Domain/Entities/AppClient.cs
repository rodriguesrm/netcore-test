using Medical.CrossCutting.Common.Contracts;
using Medical.CrossCutting.Common.Entities;
using System;

namespace Medical.Domain.Entities
{

    /// <summary>
    /// AppClient entity object
    /// </summary>
    public class AppClient : EntityBase<AppClient>
    {

        #region Properties

        /// <summary>
        /// AppClient Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// AppClient key
        /// </summary>
        public string Key { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Validate entity
        /// </summary>
        public override void Validate()
        {

            AddNotifications(new RequiredValidationContract<Guid?>(Id, nameof(Id), "Id is required").Contract.Notifications);
            AddNotifications(new SingleStringValidationContract(Name, nameof(Name), true).Contract.Notifications);
            AddNotifications(new SingleStringValidationContract(Key, nameof(Key), true).Contract.Notifications);

        }

        #endregion

    }
}
