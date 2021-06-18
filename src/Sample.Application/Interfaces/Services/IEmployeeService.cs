using Sample.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sample.Application.Interfaces.Services
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeModel>> GetEmployees();
        Task<EmployeeModel> GetEmployee(int id);
        Task<EmployeeModel> Save(EmployeeModel employeeModel, CancellationToken cancellationToken);
        Task<IEnumerable<EmployeeModel>> Save(IEnumerable<EmployeeModel> employeeModels, CancellationToken cancellationToken);
    }
}
