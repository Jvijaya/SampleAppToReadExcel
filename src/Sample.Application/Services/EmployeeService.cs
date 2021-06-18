using AutoMapper;
using Sample.Application.Interfaces;
using Sample.Application.Interfaces.Services;
using Sample.Domain.Entities;
using Sample.Domain.Models;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Sample.Application.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly ISampleDbContext context;
        private readonly IMapper mapper;

        public EmployeeService(ISampleDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public async Task<IEnumerable<EmployeeModel>> GetEmployees()
        {
            var employees = context.Employees.ToList();

            if (employees == null)
                return null;

            var result = mapper.Map<List<EmployeeModel>>(employees);

            return await Task.FromResult(result);
        }

        public async Task<EmployeeModel> GetEmployee(int id)
        {
            var employee = context.Employees.FirstOrDefault(t => t.Id == id);

            if (employee == null)
                return null;

            var result = mapper.Map<EmployeeModel>(employee);

            return await Task.FromResult(result);
        }

        public async Task<EmployeeModel> Save(EmployeeModel employeeModel, CancellationToken cancellationToken)
        {
            var entity = mapper.Map<Employee>(employeeModel);

            context.Employees.Add(entity);
            var result = await context.SaveChangesAsync(cancellationToken);

            if (result > 0)
            {
                return mapper.Map<EmployeeModel>(entity);
            }

            return null;
        }

        public async Task<IEnumerable<EmployeeModel>> Save(IEnumerable<EmployeeModel> employeeModels, CancellationToken cancellationToken)
        {
            if (employeeModels == null || !employeeModels.Any())
            {
                throw new ArgumentNullException(nameof(employeeModels));
            }

            var resultList = new List<EmployeeModel>();

            foreach (var employeeModel in employeeModels)
            {
                var entity = mapper.Map<Employee>(employeeModel);
                Employee savedEntity = null;

                if (employeeModel.Id > 0)
                {
                    savedEntity = context.Employees.FirstOrDefault(t => t.Id == employeeModel.Id);
                }

                if(savedEntity ==null)
                {
                    entity.Id = 0;
                    context.Employees.Add(entity);
                }
                else
                {
                    savedEntity.EmployeeNo = entity.EmployeeNo;
                    savedEntity.FirstName = entity.FirstName;
                    savedEntity.LastName = entity.LastName;
                    savedEntity.Email = entity.Email;
                    context.Employees.Update(savedEntity);
                }
                var result = await context.SaveChangesAsync(cancellationToken);

                if (result > 0)
                {
                    resultList.Add(mapper.Map<EmployeeModel>(entity));
                }
            }

            return resultList;
        }
    }
}
