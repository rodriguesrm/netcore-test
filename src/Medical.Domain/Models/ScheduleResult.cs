using Medical.Domain.Entities;

namespace Medical.Domain.Models
{

    /// <summary>
    /// Schedule operation result model
    /// </summary>
    public class ScheduleResult
    {

        /// <summary>
        /// Create a new schedule-result model object with sucess-result information
        /// </summary>
        /// <param name="appointment">Scheduled appointment</param>
        public ScheduleResult(Appointment appointment)
            : this(true, "OK", appointment) { }

        /// <summary>
        /// Create a new schedule-result model object with fail-result information
        /// </summary>
        /// <param name="message">Operation message</param>
        public ScheduleResult(string message)
            : this(false, message, null) { }

        /// <summary>
        /// Create a new schedule-result model object
        /// </summary>
        /// <param name="sucess">Operation sucess flag</param>
        /// <param name="message">Operation message</param>
        /// <param name="appointment">Scheduled appointment</param>
        public ScheduleResult(bool sucess, string message, Appointment appointment)
        {
            Sucess = sucess;
            Message = message;
            Appointment = appointment;
        }

        /// <summary>
        /// Operation sucess flag
        /// </summary>
        public bool Sucess { get; private set; }

        /// <summary>
        /// Operation message
        /// </summary>
        public string Message { get; private set; }

        /// <summary>
        /// Scheduled appointment
        /// </summary>
        public Appointment Appointment { get; private set; }

    }

}