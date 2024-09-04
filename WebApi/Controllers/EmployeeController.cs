using AutoMapper;
using BusinessLayer.Model.Models;
using BusinessLayer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class EmployeeController: ApiController
    {
        private readonly EmployeeService _employeeService;
        private IMapper _mapper;

        public EmployeeController(EmployeeService employeeService, IMapper mapper)
        {
            _employeeService = employeeService;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IEnumerable<EmployeeDto>> GetAllEmployee()
        {
            var result = await _employeeService.GetAllEmployeesAsync();

            return _mapper.Map<IEnumerable<EmployeeDto>>(result);

        }
        [HttpGet]
        public async Task<EmployeeDto> GetEmployeeByCode(string employeeCode)
        {
            var result = await _employeeService.GetEmployeeByCodeAsync(employeeCode);

            return _mapper.Map<EmployeeDto>(result);
        }

        [HttpPost]
        public async Task<IHttpActionResult> SaveEmployee([FromBody]EmployeeDto employeeDto)
        {
            var employeeInfo = _mapper.Map<EmployeeInfo>(employeeDto);

            await _employeeService.SaveEmployeeAsync(employeeInfo);

            return Ok("Employee information saved");
        }

        [HttpPut]
        public async Task<IHttpActionResult> UpdateEmployee(string employeeCode, [FromBody]EmployeeDto employeeDto)
        {
            var exisitngEmployee = _mapper.Map<EmployeeInfo>(employeeDto);

            await _employeeService.UpdateEmployeeAsync(employeeCode, exisitngEmployee);

            return Ok("Employee Information Updated");

        }
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteEmployee(string employeeCode)
        {
            await _employeeService.DeleteEmployeeAsync(employeeCode);

            return Ok("Employee Information Deleted");
        }
    }
}