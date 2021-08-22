using System;
using System.Text.RegularExpressions;

namespace NotificationSchedulingSystem.Entities
{
    /// <summary>
    /// Represents comnpany information
    /// </summary>
    public class Company
    {
        /// <summary>
        /// Gets the comapny identifier
        /// </summary>
        public Guid Id { get; private set; }

        /// <summary>
        /// Gets the name of the ocmpany
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets company number (10 numeric-only symbols)
        /// </summary>
        public string Number { get; private set; }

        /// <summary>
        /// Gets or sets the company type
        /// </summary>
        public CompanyType CompanyType { get; set; }

        /// <summary>
        /// Gets or sets company market
        /// </summary>
        public CountryIsoCode Market { get; set; }

        /// <summary>
        /// Gets or sets the flag indicating if the schedule is created
        /// </summary>
        public bool IsScheduleCreated { get; set; }

        /// <summary>
        /// Creates an instance of <see cref="Company"/>
        /// </summary>
        /// <param name="id">The identifier of the company (must be positive)</param>
        /// <param name="name">The name of the comany (empty values are not allowed)</param>
        /// <param name="companyNumber">Company number (10 numeric-only symbols)</param>
        /// <param name="companyType">The company type</param>
        /// <param name="market">The company market</param>
        /// <exception cref="ArgumentException" />
        /// <exception cref="ArgumentNullException" />
        public Company(Guid id, string name, string companyNumber, CompanyType companyType, CountryIsoCode market)
            : this(id, name, companyNumber, companyType, market, false)
        {
        }

        /// <summary>
        /// Creates an instance of <see cref="Company"/>
        /// </summary>
        /// <param name="id">The identifier of the company (must be positive)</param>
        /// <param name="name">The name of the comany (empty values are not allowed)</param>
        /// <param name="companyNumber">Company number (10 numeric-only symbols)</param>
        /// <param name="companyType">The company type</param>
        /// <param name="market">The company market</param>
        /// <param name="isScheduleCreated">The flag indicating if the schedul is created for the company</param>
        /// <exception cref="ArgumentException" />
        /// <exception cref="ArgumentNullException" />
        public Company(Guid id, string name, string companyNumber, CompanyType companyType, CountryIsoCode market, bool isScheduleCreated)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Empty values are not allowed.", nameof(name));
            }

            if (companyNumber is null)
            {
                throw new ArgumentNullException(nameof(companyNumber));
            }

            if (!Regex.IsMatch(companyNumber, @"^\d{10}$"))
            {
                throw new ArgumentException("The value must contain 10 numeric-only digits", nameof(companyNumber));
            }

            Id = id;
            Name = name;
            Number = companyNumber;
            CompanyType = companyType;
            Market = market;
            IsScheduleCreated = isScheduleCreated;
        }
    }
}
