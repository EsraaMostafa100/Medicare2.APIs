using AutoMapper;
using Medicare2.APIs.DTOs;
using Medicare2.Core.Entities;
using Medicare2.Core.Repositories;
using Medicare2.Core.Specifications;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Medicare2.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PharmacyController : ControllerBase
    {
        private readonly IGenaricRepositories<pharmacy> _pharmacyRepo;
        private readonly IMapper _mapper;

        public PharmacyController(IGenaricRepositories<pharmacy> pharmacyRepo,IMapper mapper)
        {
            _pharmacyRepo = pharmacyRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetPharmacies()
        {
            var Pharmacy = await _pharmacyRepo.GetAllAsync();
            return Ok(Pharmacy);

        }
        [HttpGet("{id}")]
        public async Task<ActionResult<pharmacy>> GetPharmacy(int id)
        {
            var pharmacy = await _pharmacyRepo.GetByIdAsync(id);
            return Ok(pharmacy);
        }
        [HttpGet("Saved")]
        public async Task<ActionResult<IReadOnlyList<PharmacyToReturnDTo>>> GetSavedPharmacies(bool? IsSaved)
        {
            var spec = new PharmacyWithUser(IsSaved);
            var Pharmacies = await _pharmacyRepo.GetAllSpec(spec);
            var MappedPharmacies = _mapper.Map<IReadOnlyList<pharmacy>, IReadOnlyList<PharmacyToReturnDTo>>(Pharmacies);
            return Ok(MappedPharmacies);
        }
    }
}
