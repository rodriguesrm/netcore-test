using Medical.Application.Models;
using Medical.Application.Services;
using Medical.Web.Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Medical.Web.Api.Controllers
{

    /// <summary>
    /// Patient api service
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class PatientController : ControllerBase
    {

        /// <summary>
        /// List patient's appointments
        /// </summary>
        /// <param name="appService">Appointment application service</param>
        /// <param name="patientId">Patient identification </param>
        /// <param name="date">Date do start list (year-month-day only, not hour)</param>
        /// <param name="cancellationToken">Cancelletion task token</param>
        /// <response code="400">The request is invalid</response>
        /// <response code="200">Trasaction operation sucessful, return list appointments</response>
        [ProducesResponseType(typeof(IEnumerable<GenericNotificationResponse>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(IEnumerable<PatientAppointmentsResponse>), StatusCodes.Status200OK)]
        [HttpGet("{patientId:guid}")]
        public async Task<IActionResult> ListSchedules([FromServices] IAppointmentAppService appService, [FromRoute] Guid patientId, [FromQuery] DateTime? date, CancellationToken cancellationToken = default)
        {

            ListSchedulesAppointmentForPatientResult result;
            if (date == null)
                result = await appService.ListSchedulesForPatient(patientId, cancellationToken);
            else
                result = await appService.ListSchedulesForPatient(patientId, date.Value, cancellationToken);

            if (!result.Valid)
                return BadRequest(Helpers.GetCriticals(result.Messages));

            return Ok(result.Appointments.Select(s => new PatientAppointmentsResponse()
            {
                DateTime = s.DateTime,
                Doctor = new SimplePersonResponse()
                {
                    Id = s.Doctor.Id,
                    FullName = s.Doctor.FullName
                }
            }));

        }

        /// <summary>
        /// Performs a schedule for a medical appointment
        /// </summary>
        /// <param name="appService">Appointment application service</param>
        /// <param name="patientId">Patient identification id</param>
        /// <param name="request">Request data information</param>
        /// <param name="cancellationToken">Cancelletion task token</param>
        /// <response code="400">The request is invalid</response>
        /// <response code="201">Trasaction operation sucessful, return appointment id</response>
        [ProducesResponseType(typeof(IEnumerable<GenericNotificationResponse>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
        [HttpPost("{patientId}/schedule")]
        public async Task<IActionResult> ScheduleAppointment([FromServices] IAppointmentAppService appService, [FromRoute] Guid patientId, [FromBody] PatientScheduleAppointmentRequest request, CancellationToken cancellationToken = default)
        {

            if (!ModelState.IsValid)
                return BadRequest(Helpers.GetCriticals(ModelState));


            ScheduleAppointmentArgs args = new ScheduleAppointmentArgs()
            {
                DoctorId = request.DoctorId.Value,
                PatientId = patientId,
                Date = request.Date.Value.Date,
                Hour = request.Hour.Value,
                Minute = request.Minute.Value
            };

            ScheduleAppointmentResult result = await appService.ScheduleAppointment(args, cancellationToken);

            if (!result.Sucess && !result.Valid)
                return BadRequest(Helpers.GetCriticals(result.Messages));

            return StatusCode(StatusCodes.Status201Created, result.AppointmentId);

        }

    }

}
