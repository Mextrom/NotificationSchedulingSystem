using NotificationSchedulingSystem.Entities;
using NotificationSchedulingSystem.RepositoryContracts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace DataAccess
{
    /// <summary>
    /// Manages database requests
    /// </summary>
    public class DatabaseRepository : IRepository
    {
        private readonly string _connectionString;

        /// <summary>
        /// Creates an instanc of <see cref="DatabaseRepository"/>
        /// </summary>
        /// <param name="connectionString">Database connection string</param>
        /// <exception cref="ArgumentNullException" />
        public DatabaseRepository(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        /// <summary>
        /// Adds new company to the repository
        /// </summary>
        /// <param name="company">The company object (null is not allowed)</param>
        /// <returns>True if the opeartion is successful, otherwise - false</returns>
        /// <exception cref="ArgumentNullException" />
        public bool AddCompany(Company company)
        {
            if (company is null)
            {
                throw new ArgumentNullException(nameof(company));
            }

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand("AddCompany", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@id", company.Id);
                command.Parameters.AddWithValue("@name", company.Name);
                command.Parameters.AddWithValue("@number", company.Number);
                command.Parameters.AddWithValue("@type", (int)company.CompanyType);
                command.Parameters.AddWithValue("@market", (int)company.Market);
                command.Parameters.AddWithValue("@isScheduleCreated", false);

                connection.Open();

                int result = command.ExecuteNonQuery();
                return (result > 0);
            }
        }

        /// <summary>
        /// Gets the coompany by the spceified identifier
        /// </summary>
        /// <param name="id">The company identifier</param>
        /// <returns><see cref="Company"/></returns>
        public Company GetCompany(Guid id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand("GetCompanyById", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@companyId", id);

                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Company(
                            Guid.Parse(reader["Id"].ToString()),
                            (string)reader["Name"],
                            (string)reader["Number"],
                            (CompanyType)(int)reader["Type"],
                            (CountryIsoCode)(int)reader["Market"],
                            (bool)reader["IsScheduleCreated"]);
                    }

                    return null;
                }
            }
        }

        /// <summary>
        /// Gets a collection of dates representing notifications schedule
        /// </summary>
        /// <param name="companyId">The identifier of the company</param>
        /// <returns>A collection of dates</returns>
        public IEnumerable<DateTime> GetNotificationsSchedule(Guid companyId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand("GetScheduleByCompanyId", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@companyId", companyId);

                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    List<DateTime> dates = new List<DateTime>();
                    while (reader.Read())
                    {
                        dates.Add((DateTime)reader["SendDate"]);
                    }

                    return dates;
                }
            }
        }

        /// <summary>
        /// Saves the schedule for the company with the specified identifier
        /// </summary>
        /// <param name="companyId">The company identifier</param>
        /// <param name="notificationDates">A collection of the notification dates, null collection is not allowed</param>
        /// <returns>True if the operation successful, otherwise - false</returns>
        /// <exception cref="ArgumentNullException" />
        public bool SaveSchedule(Guid companyId, IEnumerable<DateTime> notificationDates)
        {
            if (notificationDates is null)
            {
                throw new ArgumentNullException(nameof(notificationDates));
            }

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand("SaveSchedule", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@companyId", companyId);
                command.Parameters.AddWithValue("@dates", string.Join(',', notificationDates.Select(date => date.ToString("yyyy-MM-dd"))));

                connection.Open();

                int result = command.ExecuteNonQuery();
                return (result > 0);
            }
        }
    }
}
