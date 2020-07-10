using Medical.CrossCutting.Common.Configs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.ComponentModel.DataAnnotations;

namespace Medical.Web.Api.Models
{

    /// <summary>
    /// Schedule request model
    /// </summary>
    public class ScheduleAppointmentRequest
    {

        /// <summary>
        /// Doctor identification
        /// </summary>
        [Required(ErrorMessage = "Doctor id is required")]
        public Guid? DoctorId { get; set; }

        /// <summary>
        /// Patient identification
        /// </summary>
        [Required(ErrorMessage = "Patient id is required")]
        public Guid? PatientId { get; set; }

        /// <summary>
        /// Date to schedule (only yyyy-mm-dd, hours will be discarted)
        /// </summary>
        /// <example>2020-07-09</example>
        [Required(ErrorMessage = "Date is required")]
        public DateTime? Date { get; set; }

        /// <summary>
        /// Hour to schedule
        /// </summary>
        /// <example>15</example>
        [Required(ErrorMessage = "Hour is required")]
        public int? Hour { get; set; }

        /// <summary>
        /// Minute to schedule
        /// </summary>
        /// <example>30</example>
        [Required(ErrorMessage = "Minute is required")]
        public int? Minute { get; set; }

    }
}
