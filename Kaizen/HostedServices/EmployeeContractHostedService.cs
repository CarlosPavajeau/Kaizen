using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Kaizen.Core.Defines;
using Kaizen.Core.Services;
using Kaizen.Domain.Entities;
using Kaizen.Domain.Repositories;

namespace Kaizen.HostedServices
{
    public class EmployeeContractHostedService : BackgroundService
    {
        private const int DELAY_TIME = 24 /*Hours*/ * TimeConstants.Minutes * TimeConstants.Seconds * TimeConstants.Milliseconds;

        private readonly IEmployeesRepository _employeesRepository;
        private readonly IMailService _mailService;
        private readonly IMailTemplate _mailTemplate;

        public EmployeeContractHostedService(IEmployeesRepository employeesRepository, IMailService mailService,
            IMailTemplate mailTemplate)
        {
            _employeesRepository = employeesRepository;
            _mailService = mailService;
            _mailTemplate = mailTemplate;
        }

        protected async override Task ExecuteAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                IEnumerable<Employee> employees = await _employeesRepository.EmployeesWithContractCloseToExpiration();
                foreach (var employee in employees)
                {
                    string mailMessage = _mailTemplate.LoadTemplate("ContractCloseToExpiration.html", $"{employee.LastName} {employee.FirstName}",
                                                                    employee.ContractCode,
                                                                    employee.EmployeeContract.EndDate.ToShortDateString());

                    await _mailService.SendEmailAsync(employee.User.Email, "Contrato a punto de vencer", mailMessage, true);
                }

                await Task.Delay(DELAY_TIME, cancellationToken);
            }
        }
    }
}
