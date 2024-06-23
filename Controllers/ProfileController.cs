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
    public class ProfileController : ControllerBase
    {
        private readonly IGenaricRepositories<Profile> _profileRepo;
        private readonly StoreContext _context;

        public ProfileController(IGenaricRepositories<Profile> profileRepo, StoreContext context)
        {
            _profileRepo = profileRepo;
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> GetProfiles()
        {
            var Profile = await _profileRepo.GetAllAsync();
            return Ok(Profile);

        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Profile>> GetProfile(int id)
        {
            var profile = await _profileRepo.GetByIdAsync(id);
            return Ok(profile);
        }
        [HttpPost]
        public async Task<ActionResult<ProfileDto>> CreateProfile(ProfileDto model)
        {
            var Profile = new Profile()
            {
                Age = (int)model.Age,
                Bloodtype = model.BloodType,
                Allergies = model.Allergies,
                Userid = model.Userid
            };
            _context.Add(Profile);
            await _context.SaveChangesAsync();
            return Ok(Profile);

        }
    }
}
