using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using BusinessLayer.Model.Interfaces;
using BusinessLayer.Model.Models;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class CompanyController : ApiController
    {
        private readonly ICompanyService _companyService;
        private readonly IMapper _mapper;

        public CompanyController(ICompanyService companyService, IMapper mapper)
        {
            _companyService = companyService;
            _mapper = mapper;
        }
        // GET api/<controller>
        [HttpGet]
        [Route("api/GetAll")]
        public async Task<IEnumerable<CompanyDto>> GetAll()
        {
            var items = await _companyService.GetAllCompaniesAsync();
            return _mapper.Map<IEnumerable<CompanyDto>>(items);
        }

        // GET api/<controller>/5
        public async Task<CompanyDto> Get(string companyCode)
        {
            var item = await _companyService.GetCompanyByCodeAsync(companyCode);
            return _mapper.Map<CompanyDto>(item);
        }

        // POST api/<controller>
        [HttpPost]
        [Route("api/InsertCompany")]
        public async Task<IHttpActionResult> Post([FromBody]CompanyDto companyDto)
        {
            var company = _mapper.Map<CompanyInfo>(companyDto);

            await _companyService.SaveCompanyAsync(company);

            return Ok("Company Inserted");
        }

        // PUT api/<controller>/5
        public async Task<IHttpActionResult> Put(string companyId, [FromBody]CompanyDto companyDto)
        {
            var company = _mapper.Map<CompanyInfo>(companyDto);

            await _companyService.UpdateCompanyAsync(companyId, company);

            return Ok("Company details updated");

        }

        // DELETE api/<controller>/5
        public async Task<IHttpActionResult> Delete(string companyId)
        {
            await _companyService.DeleteCompanyAsync(companyId);

            return Ok("company deleted");
        }
    }
}