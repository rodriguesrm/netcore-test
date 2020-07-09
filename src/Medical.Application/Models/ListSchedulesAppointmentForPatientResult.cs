using System.Collections.Generic;

namespace Medical.Application.Models
{

    /// <summary>
    /// List Schedule appointment result model
    /// </summary>
    public class ListSchedulesAppointmentForPatientResult : BaseResult
    {

        public IEnumerable<AppointmentForPatientResult> Appointments { get; set; }

    }
}
