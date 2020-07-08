using System;

namespace Medical.CrossCutting.Common.Contracts
{
    public class PastDateValidationContract : BaseValidationContract
    {

        #region Constructors

        /// <summary>
        /// Create a new instance of contract
        /// </summary>
        /// <param name="date">Date to validate</param>
        /// <param name="field">Field name</param>
        /// <param name="message">Critical message</param>
        public PastDateValidationContract(DateTime? date, string field, string message) : base()
        {

            Contract
                .IsNotNull(date, field, message)
                .IsLowerOrEqualsThan(date == null ? DateTime.Now : date.Value, DateTime.Now, field, $"A data '{field}' deve ser inferior a data atual")
            ;

        }

        #endregion

    }
}
