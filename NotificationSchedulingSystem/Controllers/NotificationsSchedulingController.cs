using Microsoft.AspNetCore.Mvc;
using NotificationSchedulingSystem.Core;
using NotificationSchedulingSystem.RepositoryContracts;
using System;
using System.Collections.Generic;

namespace NotificationSchedulingSystem.Controllers
{
    /// <summary>
    /// Handles scheduling requests
    /// </summary>
    [Route("notification-scheduling")]
    [ApiController]
    public class NotificationsSchedulingController : ControllerBase
    {
        private readonly NotificationSchedulingService _service;

        /// <summary>
        /// Creates an instance of <see cref="NotificationsSchedulingController"/>
        /// </summary>
        /// <param name="repository">The data access object</param>
        /// <exception cref="ArgumentNullException" />
        public NotificationsSchedulingController(IRepository repository)
        {
            if (repository is null)
            {
                throw new ArgumentNullException(nameof(repository));
            }

            _service = new NotificationSchedulingService(repository);
        }

        /// <summary>
        /// Creates a test company and returns its identifier
        /// </summary>
        /// <returns>Http status 200 with a object which contains company identifier, BAD REQUEST in case of error</returns>
        [HttpGet]
        [Route("create-test-company")]
        public IActionResult CreateTestCompany()
        {
            try
            {
                return Ok(new { companyId = _service.CreateTestCompany() });
            }
            catch
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Gets a schedule for the company with the specified identifier
        /// </summary>
        /// <param name="companyId">The company identifier</param>
        /// <returns>OK with and an object containing company schedule. BAD REQUEST in case of error</returns>
        [HttpGet]
        [Route("schedule/{companyId}")]
        public IActionResult GetNotificationSchedule(Guid companyId)
        {
            try
            {
                IEnumerable<DateTime> schedule = _service.GetNotificationSchedule(companyId);
                return Ok(
                    new
                    {
                        companyId = companyId,
                        notifications = schedule
                    });
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
