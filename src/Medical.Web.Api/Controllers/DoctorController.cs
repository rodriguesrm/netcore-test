using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Medical.Application.Models;
using Medical.Application.Services;
using Medical.Web.Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Medical.Web.Api.Controllers
{

    /// <summary>
    /// Doctor api service
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class DoctorController : ControllerBase
    {

        /// <summary>
        /// List doctor's appointments
        /// </summary>
        /// <param name="appService">Appointment application service</param>
        /// <param name="doctorId">Doctor identification </param>
        /// <param name="date">Date do start list (year-month-day only, not hour)</param>
        /// <param name="cancellationToken">Cancelletion task token</param>
        /// <response code="400">The request is invalid</response>
        /// <response code="200">Trasaction operation sucessful, return list appointments</response>
        [ProducesResponseType(typeof(IEnumerable<GenericNotificationResponse>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(IEnumerable<DoctorAppointmentsResponse>), StatusCodes.Status200OK)]
        [HttpGet("{doctorId:guid}")]
        public async Task<IActionResult> ListSchedules([FromServices] IAppointmentAppService appService, [FromRoute] Guid doctorId, [FromQuery] DateTime? date, CancellationToken cancellationToken = default)
        {

            ListSchedulesAppointmentForDoctorResult result;
            if (date == null)
                result = await appService.ListSchedulesForDoctor(doctorId, cancellationToken);
            else
                result = await appService.ListSchedulesForDoctor(doctorId, date.Value, cancellationToken);

            if (!result.Valid)
                return BadRequest(Helpers.GetCriticals(result.Messages));

            return Ok(result.Appointments.Select(s => new DoctorAppointmentsResponse()
            {
                DateTime = s.DateTime,
                Patient = new SimplePersonResponse()
                {
                    Id = s.Patient.Id,
                    FullName = s.Patient.FullName
                }
            }));

        }

        /// <summary>
        /// Performs a schedule for a medical appointment
        /// </summary>
        /// <param name="appService">Appointment application service</param>
        /// <param name="request">Request data information</param>
        /// <param name="cancellationToken">Cancelletion task token</param>
        /// <response code="400">The request is invalid</response>
        /// <response code="201">Trasaction operation sucessful, return appointment id</response>
        [ProducesResponseType(typeof(IEnumerable<GenericNotificationResponse>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
        [HttpPost("schedule")]
        public async Task<IActionResult> ScheduleAppointment([FromServices] IAppointmentAppService appService, [FromBody] ScheduleAppointmentRequest request, CancellationToken cancellationToken = default)
        {

            if (!ModelState.IsValid)
                return BadRequest(Helpers.GetCriticals(ModelState));


            ScheduleAppointmentArgs args = new ScheduleAppointmentArgs()
            {
                DoctorId = request.DoctorId.Value,
                PatientId = request.PatientId.Value,
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
