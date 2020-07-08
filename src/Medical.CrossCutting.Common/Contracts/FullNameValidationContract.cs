using Medical.CrossCutting.Common.Entities;

namespace Medical.CrossCutting.Common.Contracts
{
    public class FullNameValidationContract : BaseValidationContract
    {

        #region Constructors

        /// <summary>
        /// Create a new instance of object
        /// </summary>
        /// <param name="name">Name object instance</param>
        public FullNameValidationContract(IFullName name) : this(name, string.Empty) { }

        /// <summary>
        /// Create a new instance of object
        /// </summary>
        /// <param name="name">Name object instance</param>
        /// <param name="charListAllowed">Char list of allowed characters</param>
        public FullNameValidationContract(IFullName name, string charListAllowed)
        {

            Contract
                .Requires()
                .IsNotNullOrEmpty(name.FirstName, "Primeiro Nome", "O primeiro nome deve ser informado")
                .HasMinLen(name.FirstName ?? string.Empty, 2, "Nome", "O nome deve conter no mínimo 2 caracteres")
                .HasMaxLen(name.FirstName ?? string.Empty, 50, "Nome", "O nome deve conter no máximo 50 caracteres")
                .Matchs(name.FirstName, $"^[a-zA-Z{charListAllowed} ,.'-]+$", "Nome", "o nome contém caracteres inválidos")
                .IsNotNullOrEmpty(name.LastName, "Sobrenome", "O sobrenome deve ser informado")
                .HasMinLen(name.LastName ?? string.Empty, 2, "Sobrenome", "O sobrenome deve conter no mínimo 2 caracteres")
                .HasMaxLen(name.LastName ?? string.Empty, 100, "Sobrenome", "O sobrenome deve conter no máximo 100 caracteres")
                .Matchs(name.FirstName, $"^[a-zA-Z{charListAllowed} ,.'-]+$", "Sobrenome", "O sobrenome contém caracteres inválidos")
            ;

        }

        #endregion

    }
}
