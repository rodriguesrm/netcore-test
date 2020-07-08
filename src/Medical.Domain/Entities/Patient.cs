using Medical.CrossCutting.Common.Contracts;
using Medical.CrossCutting.Common.Entities;
using System;
using System.Collections.Generic;

namespace Medical.Domain.Entities
{

    /// <summary>
    /// Patient entity object
    /// </summary>
    public class Patient : EntityBase<Patient>, IFullName
    {

        #region Constructors

        /// <summary>
        /// Create a new Patient instance object
        /// </summary>
        public Patient() : base()
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
        /// CPF Brasilian person document number
        /// </summary>
        public string Cpf { get; set; }

        #endregion

        #region Navigation/Lazy

        /// <summary>
        /// Appointment for this patient
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
