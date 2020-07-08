using System;

namespace Medical.CrossCutting.Common.Contracts
{
    public class PeriodDateValidationContract : BaseValidationContract
    {

        #region Constructors

        /// <summary>
        /// Create a new instance of contract
        /// </summary>
        /// <param name="startDate">Start date to validate</param>
        /// <param name="endDate">End date to validate</param>
        /// <param name="field">Field name</param>
        public PeriodDateValidationContract(DateTime? startDate, DateTime? endDate, string field) : base()
        {

            Contract
                .IsNotNull(startDate, field, $"[{field}] Data inicial inválida")
                .IsNotNull(endDate, field, $"[{field}] Data final inválida")
                .IsGreaterOrEqualsThan(endDate.Value, startDate.Value, field, $"[{field}] A data final deve ser superior a data inicial")
            ;

        }

        #endregion

    }
}
