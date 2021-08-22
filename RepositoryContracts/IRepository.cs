using NotificationSchedulingSystem.Entities;
using System;
using System.Collections.Generic;

namespace NotificationSchedulingSystem.RepositoryContracts
{
    /// <summary>
    /// Represents a data access object
    /// </summary>
    public interface IRepository
    {
        /// <summary>
        /// Gets a collection of dates representing notifications schedule
        /// </summary>
        /// <param name="companyId">The identifier of the company</param>
        /// <returns>A collection of dates</returns>
        IEnumerable<DateTime> GetNotificationsSchedule(Guid companyId);

        /// <summary>
        /// Adds new company to the repository
        /// </summary>
        /// <param name="company">The company object (null is not allowed)</param>
        /// <returns>True if the opeartion is successful, otherwise - false</returns>
        /// <exception cref="ArgumentNullException" />
        bool AddCompany(Company company);

        /// <summary>
        /// Gets the coompany by the spceified identifier
        /// </summary>
        /// <param name="id">The company identifier</param>
        /// <returns><see cref="Company"/></returns>
        Company GetCompany(Guid id);

        /// <summary>
        /// Saves the schedule for the company with the specified identifier
        /// </summary>
        /// <param name="companyId">The company identifier</param>
        /// <param name="notificationDates">A collection of the notification dates</param>
        /// <returns>True if the operation successful, otherwise - false</returns>
        /// <exception cref="ArgumentNullException" />
        bool SaveSchedule(Guid companyId, IEnumerable<DateTime> notificationDates);
    }
}
