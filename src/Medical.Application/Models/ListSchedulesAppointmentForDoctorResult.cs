using System.Collections.Generic;

namespace Medical.Application.Models
{

    /// <summary>
    /// List Schedule appointment result model
    /// </summary>
    public class ListSchedulesAppointmentForDoctorResult : BaseResult
    {

        public IEnumerable<AppointmentForDoctorResult> Appointments { get; set; }

    }
}
