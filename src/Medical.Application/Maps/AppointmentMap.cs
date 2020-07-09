using Medical.Application.Models;
using Medical.Domain.Entities;

namespace Medical.Application.Maps
{

    /// <summary>
    /// Appointment maps
    /// </summary>
    public static class AppointmentMap
    {

        /// <summary>
        /// Map entity to appointment for doctor result model
        /// </summary>
        /// <param name="appointment"></param>
        /// <returns></returns>
        public static AppointmentForDoctorResult MapToDoctorResult(this Appointment appointment)
        {
            return new AppointmentForDoctorResult()
            {
                DateTime = appointment.DateTime,
                Patient = new SingleIdentificationResult()
                {
                    Id = appointment.Patient.Id.Value,
                    FullName = appointment.Patient.GetFullName()
                }
            };
        }

        public static AppointmentForPatientResult MapToPatientResult(this Appointment appointment)
        {
            return new AppointmentForPatientResult()
            {
                DateTime = appointment.DateTime,
                Patient = new SingleIdentificationResult()
                {
                    Id = appointment.Doctor.Id.Value,
                    FullName = appointment.Doctor.GetFullName()
                }
            };
        }

    }

}
