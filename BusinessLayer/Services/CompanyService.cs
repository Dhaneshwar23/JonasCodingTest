﻿using BusinessLayer.Model.Interfaces;
using System.Collections.Generic;
using AutoMapper;
using BusinessLayer.Model.Models;
using DataAccessLayer.Model.Interfaces;
using System.Threading.Tasks;
using DataAccessLayer.Model.Models;
using System;

namespace BusinessLayer.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IMapper _mapper;

        public CompanyService(ICompanyRepository companyRepository, IMapper mapper)
        {
            _companyRepository = companyRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CompanyInfo>> GetAllCompaniesAsync()
        {
            var res = await _companyRepository.GetAll();
            return _mapper.Map<IEnumerable<CompanyInfo>>(res);
        }

        public async Task<CompanyInfo> GetCompanyByCodeAsync(string companyCode)
        {
            var result = await _companyRepository.GetByCode(companyCode);
            return _mapper.Map<CompanyInfo>(result);
        }

        public async Task SaveCompanyAsync(CompanyInfo companyInfo)
        {
            var company = _mapper.Map<Company>(companyInfo);

            await _companyRepository.SaveCompany(company);
        }

        public async Task UpdateCompanyAsync(string companyCode, CompanyInfo companyInfo)
        {
            var existingCompany = await GetCompanyByCodeAsync(companyCode);

            if (existingCompany == null )
            {
                throw new Exception("Company doesn't exisst");
            }
            else
            {
                var updateCompany = _mapper.Map<Company>(companyInfo);
                await _companyRepository.SaveCompany(updateCompany);
            }
        }

        public async Task DeleteCompanyAsync(string companyCode)
        {
            await _companyRepository.DeleteCompany(companyCode);
        }

    }
}
