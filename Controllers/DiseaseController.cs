using Medicare2.APIs.DTOs;
using Medicare2.Core.Entities;
using Medicare2.Core.Repositories;
using Medicare2.Repository.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Medicare2.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiseaseController : ControllerBase
    {
        private readonly IGenaricRepositories<Disease> _diseaseRepo;
        private readonly StoreContext _context;

        public DiseaseController(IGenaricRepositories<Disease> diseaseRepo,StoreContext context)
        {
            _diseaseRepo = diseaseRepo;
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> GetDiseases()
        {
            var Disease = await _diseaseRepo.GetAllAsync();
            return Ok(Disease);

        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Disease>> GetDisease(int id)
        {
            var Disease = await _diseaseRepo.GetByIdAsync(id);
            return Ok(Disease);
        }
        [HttpPost]
        public async Task<ActionResult<DiseaseDTo>> CreateDisease (DiseaseRequest model)
        {
            var Disease= new Disease()
            {
               Name=model.Name,
               Userid = model.UserId
            };
            _context.Add(Disease);
            await _context.SaveChangesAsync();
            return Ok(Disease);

        }
    }
}
