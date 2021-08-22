using NotificationSchedulingSystem.Entities;
using NotificationSchedulingSystem.RepositoryContracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NotificationSchedulingSystem.Core
{
    /// <summary>
    /// Handles notification
    /// </summary>
    public class NotificationSchedulingService
    {
        private readonly IRepository _repository;

        /// <summary>
        /// Creates an instance of <see cref="NotificationSchedulingService"/>
        /// </summary>
        /// <param name="repository">The repository object</param>
        /// <exception cref="ArgumentNullException" />
        public NotificationSchedulingService(IRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        /// <summary>
        /// Creates test company (data is taken from requirements document)
        /// </summary>
        /// <returns>Identifier of the new company</returns>
        /// <remarks>Since it is not fully specified how exactly we should create a company,
        /// we create a single one with the information provided in the reqiurements</remarks>
        public Guid CreateTestCompany()
        {
            Guid id = Guid.Parse("aad7a630-af1c-4952-9cb4-44b8b847853b");
            Company company = _repository.GetCompany(id);

            if (company == null)
            {
                company = new Company(id,
                    "Test Company", "0123456789", CompanyType.Small, CountryIsoCode.Denmark);
                _repository.AddCompany(company);
            }

            return company.Id;
        }

        /// <summary>
        /// Gets a collection of notifiaction dates representing a schedule for
        /// the company with the specified identifier
        /// </summary>
        /// <param name="companyId">The company identifier</param>
        /// <returns>A collection of dates</returns>
        /// <exception cref="ArgumentException" />
        /// <exception cref="InvalidOperationException" />
        public IEnumerable<DateTime> GetNotificationSchedule(Guid companyId)
        {
            Company company = _repository.GetCompany(companyId);
            if (company is null)
            {
                throw new ArgumentException("The company with the specified identifier is not found", nameof(companyId));
            }

            if (company.IsScheduleCreated)
            {
                return _repository.GetNotificationsSchedule(companyId);
            }
            else
            {
                IEnumerable<DateTime> schedule = CreateSchedule(company.Market, company.CompanyType);
                if (!_repository.SaveSchedule(company.Id, schedule))
                {
                    throw new InvalidOperationException();
                }

                return schedule;
            }
        }

        /// <summary>
        /// Creates notification schedule
        /// </summary>
        /// <param name="market">Company market</param>
        /// <param name="companyType">Company type</param>
        /// <returns>A coollection of notification dates</returns>
        private IEnumerable<DateTime> CreateSchedule(CountryIsoCode market, CompanyType companyType)
        {
            int[] periods;
            switch (market)
            {
                case CountryIsoCode.Denmark:
                    periods = new int[] { 1, 5, 10, 15, 20 };
                    break;
                case CountryIsoCode.Finland:
                    if (companyType == CompanyType.Large)
                    {
                        periods = new int[] { 1, 5, 10, 15, 20 };
                    }
                    else
                    {
                        periods = new int[0];
                    }
                    break;
                case CountryIsoCode.Norway:
                    periods = new int[] { 1, 5, 10, 20 };
                    break;
                case CountryIsoCode.Sweden:
                    if (companyType == CompanyType.Small || companyType == CompanyType.Medium)
                    {
                        periods = new int[] { 1, 7, 14, 28 };
                    }
                    else
                    {
                        periods = new int[0];
                    }
                    break;
                default:
                    periods = new int[0];
                    break;
            }

            DateTime currentDate = DateTime.Today.Date;
            return periods.Select(period => currentDate.AddDays(period));
        }
    }
}
