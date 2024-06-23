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
    public class ChatController : ControllerBase
    {
        private readonly IGenaricRepositories<Chat> _chatRepo;
        private readonly StoreContext _context;

        public ChatController(IGenaricRepositories<Chat> ChatRepo, StoreContext context)
        {
            _chatRepo = ChatRepo;
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> GetChats()
        {
            var chats = await _chatRepo.GetAllAsync();
            return Ok(chats);

        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Chat>> GetChat(int id)
        {
            var chat = await _chatRepo.GetByIdAsync(id);
            return Ok(chat);
        }
        [HttpPost]
        public async Task<ActionResult<Chat>> CreateChat(ChatDto model)
        {
            var Chat = new Chat()
            {
                Message = model.message,
                Userid = model.Userid,
                Doctorid = model.Doctorid
            };
            _context.Add(Chat);
            await _context.SaveChangesAsync();
            return Ok(Chat);

        }
    }
}
