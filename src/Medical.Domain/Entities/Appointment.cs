using Medical.CrossCutting.Common.Contracts;
using Medical.CrossCutting.Common.Entities;
using System;

namespace Medical.Domain.Entities
{

    /// <summary>
    /// Appointment entity object
    /// </summary>
    public class Appointment : EntityBase<Appointment>
    {

        #region Constructors

        /// <summary>
        /// Create a new Appointment instance object
        /// </summary>
        public Appointment() : base() { }

        #endregion

        #region Properties

        /// <summary>
        /// Doctor id
        /// </summary>
        public Guid? DoctorId { get; set; }

        /// <summary>
        /// Patient id
        /// </summary>
        public Guid? PatientId { get; set; }

        /// <summary>
        /// Date/time medical appointment
        /// </summary>
        public DateTime DateTime { get; set; }

        #endregion

        #region Navigation/Lazy

        /// <summary>
        /// Doctor data information
        /// </summary>
        public virtual Doctor Doctor { get; set; }

        /// <summary>
        /// Patient data information
        /// </summary>
        public virtual Patient Patient { get; set; }

        #endregion

        #region Public Methods

        ///<inheritdoc/>
        public override void Validate()
        {
            AddNotifications(new RequiredValidationContract<Guid?>(Id, nameof(Id), "Id is required.").Contract.Notifications);
            AddNotifications(new RequiredValidationContract<Guid?>(DoctorId, nameof(DoctorId), "DoctorId is required.").Contract.Notifications);
            AddNotifications(new RequiredValidationContract<Guid?>(PatientId, nameof(PatientId), "PatientIdis required.").Contract.Notifications);
            AddNotifications(new FutureDateValidationContract(DateTime, nameof(DateTime), "Appointment date must be in future").Contract.Notifications);
        }

        #endregion

    }
}
