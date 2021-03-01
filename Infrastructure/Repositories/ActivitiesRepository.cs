using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kaizen.Domain.Data;
using Kaizen.Domain.Entities;
using Kaizen.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Kaizen.Infrastructure.Repositories
{
    public class ActivitiesRepository : RepositoryBase<Activity, int>, IActivitiesRepository
    {
        private DateTime _limitDate = DateTime.Parse($"{DateTime.Now.Year}/12/31");
        private readonly IEmployeesRepository _employeesRepository;

        public ActivitiesRepository(ApplicationDbContext applicationDbContext, IEmployeesRepository employeesRepository)
            : base(applicationDbContext)
        {
            _employeesRepository = employeesRepository;
        }

        public async Task ScheduleActivities(Activity activity)
        {
            if (activity.Periodicity == PeriodicityType.Casual)
            {
                return;
            }

            int dayInterval = GetDayInterval(activity.Periodicity);
            if (dayInterval == -1)
            {
                return;
            }

            string[] activityServiceCodes = activity.ActivitiesServices.Select(s => s.ServiceCode).ToArray();
            List<string> activityEmployeeCodes = activity.ActivitiesEmployees.Select(a => a.EmployeeId).ToList();

            DateTime newDate = activity.Date.AddDays(dayInterval);
            while (newDate < _limitDate)
            {
                if (activity.Clone() is Activity newActivity)
                {
                    newActivity.Date = newDate;

                    IEnumerable<Employee> availableEmployees =
                        await GetTechniciansAvailable(newActivity.Date, activityServiceCodes);
                    bool canBeScheduled = ActivityCanBeScheduled(activityEmployeeCodes, availableEmployees);
                    while (!canBeScheduled)
                    {
                        newActivity.Date = newActivity.Date.AddHours(1);
                        availableEmployees = await GetTechniciansAvailable(newActivity.Date, activityServiceCodes);
                        canBeScheduled = ActivityCanBeScheduled(activityEmployeeCodes, availableEmployees);
                    }

                    Insert(newActivity);
                }

                newDate = newDate.AddDays(dayInterval);
            }

            await ApplicationDbContext.SaveChangesAsync();
        }

        private static bool ActivityCanBeScheduled(ICollection<string> activityEmployeeCodes,
            IEnumerable<Employee> availableEmployees)
        {
            return activityEmployeeCodes.All(_ => availableEmployees.Any(a => activityEmployeeCodes.Contains(a.Id)));
        }

        private async Task<IEnumerable<Employee>> GetTechniciansAvailable(DateTime date, string[] serviceCodes)
        {
            return await _employeesRepository.GetTechniciansAvailable(date, serviceCodes);
        }

        private static int GetDayInterval(PeriodicityType periodicityType)
        {
            return periodicityType switch
            {
                PeriodicityType.Biweekly => 15,
                PeriodicityType.Monthly => 30,
                PeriodicityType.BiMonthly => 60,
                PeriodicityType.Trimester => 90,
                PeriodicityType.Quarter => 120,
                PeriodicityType.FiveMonths => 150,
                PeriodicityType.Biannual => 180,
                PeriodicityType.Annual => 360,
                _ => -1,
            };
        }

        public override async Task<Activity> FindByIdAsync(int id)
        {
            return await ApplicationDbContext.Activities
                .Include(a => a.Client)
                .ThenInclude(a => a.ClientAddress)
                .Include(a => a.ActivitiesServices)
                .ThenInclude(a => a.Service)
                .Include(a => a.ActivitiesEmployees).ThenInclude(a => a.Employee)
                .FirstOrDefaultAsync(a => a.Code == id);
        }

        public async Task<IEnumerable<Activity>> GetPendingEmployeeActivities(string employeeId, DateTime date)
        {
            return await GetAll().Include(a => a.ActivitiesEmployees)
                .Include(a => a.Client)
                .ThenInclude(a => a.ClientAddress)
                .Include(a => a.ActivitiesServices)
                .ThenInclude(a => a.Service)
                .ThenInclude(a => a.ProductsServices)
                .ThenInclude(a => a.Product)
                .Include(a => a.ActivitiesServices)
                .ThenInclude(a => a.Service)
                .ThenInclude(a => a.EquipmentsServices)
                .ThenInclude(a => a.Equipment)
                .Where(a => a.State == ActivityState.Pending &&
                            a.Date.Month == date.Month && a.Date.Day == date.Day &&
                            a.ActivitiesEmployees.Select(activityEmployee => activityEmployee.EmployeeId)
                                .Contains(employeeId))
                .ToListAsync();
        }

        private async Task<IEnumerable<Activity>> GetClientActivities(string clientId,
            ActivityState state = ActivityState.Pending)
        {
            return await GetAll().Include(a => a.ActivitiesServices).ThenInclude(a => a.Service)
                .Include(a => a.ActivitiesEmployees).ThenInclude(a => a.Employee)
                .Where(a => a.State == state && a.ClientId == clientId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Activity>> GetPendingClientActivities(string clientId)
        {
            return await GetClientActivities(clientId);
        }

        public async Task<IEnumerable<Activity>> GetAppliedClientActivities(string clientId)
        {
            return await GetClientActivities(clientId, ActivityState.Applied);
        }

        public async Task<IEnumerable<Activity>> GetPendingActivitiesToConfirmed()
        {
            DateTime today = DateTime.Now;

            return await GetAll()
                .Include(a => a.Client)
                .ThenInclude(c => c.User)
                .Where(
                    a => a.State == ActivityState.Pending &&
                         MySqlDbFunctionsExtensions.DateDiffDay(EF.Functions, a.Date, today) <= 3 &&
                         MySqlDbFunctionsExtensions.DateDiffDay(EF.Functions, a.Date, today) > 0
                )
                .ToListAsync();
        }

        public void UpdateLimitDate()
        {
            int currentYear = DateTime.Now.Year;
            if (currentYear > _limitDate.Year)
            {
                _limitDate = DateTime.Parse($"{currentYear}/12/31");
            }
        }
    }
}
