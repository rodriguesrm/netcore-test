using brdoc = RSoft.Helpers.Validations.BrasilianDocument;

namespace Medical.CrossCutting.Common.Contracts
{

    /// <summary>
    /// Cpf validation contract
    /// </summary>
    public class CpfContract : BaseValidationContract
    {

        #region Constructors

        /// <summary>
        /// Create a new instance of contract
        /// </summary>
        /// <param name="cpf">Cpf number to validate</param>
        public CpfContract(string cpf) : base()
        {

            Contract
                .Requires()
                .IsNotNull(cpf, "Cpf", "Cpf document number is required")
                .IsTrue(brdoc.CheckDocument(cpf), "Cpf", "Invalid Cpf number")
            ;

        }

        #endregion

    }
}