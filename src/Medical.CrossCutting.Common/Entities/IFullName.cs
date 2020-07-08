namespace Medical.CrossCutting.Common.Entities
{


    /// <summary>
    /// Full name data interface
    /// </summary>
    public interface IFullName
    {

        #region Properties

        /// <summary>
        /// First name
        /// </summary>
        string FirstName { get; set; }

        /// <summary>
        /// Last name
        /// </summary>
        string LastName { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Get full name
        /// </summary>
        string GetFullName();

        #endregion

    }

}
