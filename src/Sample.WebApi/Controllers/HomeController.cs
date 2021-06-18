using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sample.WebApi.Models;
using System.Diagnostics;
using Sample.Application.Interfaces.Services;
using Sample.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using System;
using System.IO;
using ClosedXML.Excel;
using System.Linq;

namespace Sample.WebApi.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IEmployeeService employeeService;


        public HomeController(ILogger<HomeController> logger, IEmployeeService employeeService)
        {
            _logger = logger;
            this.employeeService = employeeService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost("import")]
        public async Task<IActionResult> Import(IFormFile formFile, CancellationToken cancellationToken)
        {
            if (formFile == null || formFile.Length <= 0)
            {
                return BadRequest();
            }

            if (!Path.GetExtension(formFile.FileName).Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
            {
                return BadRequest();
            }

            var employees = new List<EmployeeModel>();
            using (var stream = new MemoryStream())
            {
                await formFile.CopyToAsync(stream, cancellationToken);
                employees = ReadEmployees(stream);
            }

            await employeeService.Save(employees, cancellationToken);

            return RedirectToAction("Index");
        }

        private static List<EmployeeModel> ReadEmployees(MemoryStream stream)
        {
            var employees = new List<EmployeeModel>();

            using (var package = new XLWorkbook(stream))
            {

                var worksheet = package.Worksheets.First();
                var rowCount = worksheet.RowCount();

                for (int row = 2; row <= rowCount; row++)
                {
                    var id = worksheet.Cell(row, 1).Value.ToString().Trim();
                    if (string.IsNullOrWhiteSpace(id))
                    {
                        break;
                    }

                    employees.Add(new EmployeeModel
                    {
                        Id = int.Parse(worksheet.Cell(row, 1).Value.ToString().Trim()),
                        EmployeeNo = worksheet.Cell(row, 2).Value.ToString().Trim(),
                        FirstName = worksheet.Cell(row, 3).Value.ToString().Trim(),
                        LastName = worksheet.Cell(row, 4).Value.ToString().Trim(),
                        Email = worksheet.Cell(row, 5).Value.ToString().Trim(),
                    });
                }
            }
            return employees;
        }

        public async Task<IActionResult> Download()
        {
            string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            string fileName = "Employees.xlsx";
            try
            {
                using (var workbook = new XLWorkbook())
                {
                    IXLWorksheet worksheet =
                    workbook.Worksheets.Add("employees");
                    worksheet.Cell(1, 1).Value = "Id";
                    worksheet.Cell(1, 2).Value = "EmployeeNo";
                    worksheet.Cell(1, 3).Value = "FirstName";
                    worksheet.Cell(1, 4).Value = "LastName";
                    worksheet.Cell(1, 5).Value = "Email";
                    var employees = (await employeeService.GetEmployees()).ToList();
                    for (int index = 1; index <= employees.Count; index++)
                    {
                        worksheet.Cell(index + 1, 1).Value =
                        employees[index - 1].Id;
                        worksheet.Cell(index + 1, 2).Value =
                        employees[index - 1].EmployeeNo;
                        worksheet.Cell(index + 1, 3).Value =
                        employees[index - 1].FirstName;

                        worksheet.Cell(index + 1, 4).Value =
                        employees[index - 1].LastName;

                        worksheet.Cell(index + 1, 5).Value =
                        employees[index - 1].Email;
                    }
                    using (var stream = new MemoryStream())
                    {
                        workbook.SaveAs(stream);
                        var content = stream.ToArray();
                        return File(content, contentType, fileName);
                    }
                }
            }
            catch (Exception ex)
            {
                return Error();
            }
        }
    }
}
