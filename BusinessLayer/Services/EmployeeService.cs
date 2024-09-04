﻿using AutoMapper;
using BusinessLayer.Model.Interfaces;
using BusinessLayer.Model.Models;
using DataAccessLayer.Model.Interfaces;
using DataAccessLayer.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public EmployeeService(IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<EmployeeInfo>> GetAllEmployeesAsync()
        {
            var res =  await _employeeRepository.GetAllAsync();

            return (IEnumerable<EmployeeInfo>)_mapper.Map<EmployeeInfo>(res);
        }

        public async Task<EmployeeInfo> GetEmployeeByCodeAsync(string employeeCode)
        {
            var res = await _employeeRepository.GetEmployeeByCodeAsync(employeeCode);

            return _mapper.Map<EmployeeInfo>(res);

        }

        public async Task SaveEmployeeAsync(EmployeeInfo employeeInfo)
        {
            var employee = _mapper.Map<Employee>(employeeInfo);

            await _employeeRepository.SaveEmployeeAsync(employee);
        }

        public async Task UpdateEmployeeAsync(string employeeCode, EmployeeInfo employeeInfo)
        {
            var existingEmployee = _employeeRepository.GetEmployeeByCodeAsync(employeeCode);

            if (existingEmployee == null)
            {
                throw new Exception("Employee doesn't exist");
            }
            else
            {
                var employee = _mapper.Map<Employee>(employeeInfo);
                await _employeeRepository.SaveEmployeeAsync(employee);
            }
        }

        public async Task DeleteEmployeeAsync(string employeeCode)
        {
            await _employeeRepository.DeleteEmployeeAsync(employeeCode);
        }
    }
}
