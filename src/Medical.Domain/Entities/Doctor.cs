using Medical.CrossCutting.Common.Contracts;
using Medical.CrossCutting.Common.Entities;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Medical.Domain.Entities
{

    /// <summary>
    /// Doctor entity object
    /// </summary>
    public class Doctor : EntityBase<Doctor>, IFullName
    {

        #region Constructors

        /// <summary>
        /// Create a new doctor instance object
        /// </summary>
        public Doctor() : base()
        {
            Appointments = new HashSet<Appointment>();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Customer first name
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Customer last name
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// CRM Brasilian medical register number
        /// </summary>
        public string Crm { get; set; }

        #endregion

        #region Navigation/Lazy

        /// <summary>
        /// Appointment for this doctor
        /// </summary>
        public virtual ICollection<Appointment> Appointments { get; set; }

        #endregion

        #region Public Methods

        ///<inheritdoc/>
        public string GetFullName()
            => $"{FirstName ?? string.Empty} {LastName ?? string.Empty}".Trim();

        ///<inheritdoc/>
        public override void Validate()
        {
            AddNotifications(new RequiredValidationContract<Guid?>(Id, nameof(Id), "Id is required.").Contract.Notifications);
            AddNotifications(new FullNameValidationContract(this).Contract.Notifications);
        }

        #endregion

    }
}
