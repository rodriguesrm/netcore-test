using System;

namespace Medical.CrossCutting.Common.Contracts
{

    /// <summary>
    /// Contract future date validation
    /// </summary>
    public class FutureDateValidationContract : BaseValidationContract
    {

        #region Constructors

        /// <summary>
        /// Create a new instance of contract
        /// </summary>
        /// <param name="date">Date to validate</param>
        /// <param name="field">Field name</param>
        /// <param name="message">Critical message</param>
        public FutureDateValidationContract(DateTime? date, string field, string message) : base()
        {

            Contract
                .IsNotNull(date, field, message)
                .IsGreaterOrEqualsThan(date.Value, DateTime.Now, field, $"A data '{field}' deve ser superior a data atual")
            ;

        }

        #endregion

    }
}
