using Medicare2.APIs.DTOs;
using Medicare2.Core.Entities;
using Medicare2.Core.Repositories;
using Medicare2.Repository.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Medicare2.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicineController : ControllerBase
    {
        private readonly IGenaricRepositories<Medicine> _medicineRepo;
        private readonly StoreContext _context;

        public MedicineController(IGenaricRepositories<Medicine> MedicineRepo, StoreContext context)
        {
            _medicineRepo = MedicineRepo;
            _context = context;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetMedicines()
        {
            var Medicine = await _medicineRepo.GetAllAsync();
            return Ok(Medicine);

        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Medicine>> GetMedicine(int id)
        {
            var Medicine = await _medicineRepo.GetByIdAsync(id);
            return Ok(Medicine);
        }
        

        [HttpPost]
        public async Task<ActionResult<MedicineDTo>> CreateMedicine(MedicineRequest model)
        {
            var medicine = new Medicine()
            {
                Name = model.Name,
                Frequancy = model.Frequancy,
                Form = model.Form,
                Dose = (int)model.Dose,
                Duration = model.Duration,
                Time = model.Time,
                Userid = model.UserId,
                Startdate = model.Startdate
            };
            _context.Medicine.Add(medicine);
            await _context.SaveChangesAsync();
            return Ok(model);


        }

        
       
    }
}
