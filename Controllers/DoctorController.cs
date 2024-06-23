using Medicare2.Core.Entities;
using Medicare2.Core.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Medicare2.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly IGenaricRepositories<Doctor> _doctorRepo;

        public DoctorController(IGenaricRepositories<Doctor> doctorRepo)
        {
           _doctorRepo = doctorRepo;
        }
        [HttpGet]
        public async Task<IActionResult> GetDoctors()
        {
            var Doctor = await _doctorRepo.GetAllAsync();
            return Ok(Doctor);

        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Doctor>> GetDoctor(int id)
        {
            var Doctor = await _doctorRepo.GetByIdAsync(id);
            return Ok(Doctor);
        }
    }
}
