using Medicare2.APIs.DTOs;
using Medicare2.Core.Entities;
using Medicare2.Repository.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;

namespace Medicare2.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly StoreContext _storeContext;

        public UserController(StoreContext storeContext)
        {
            _storeContext = storeContext;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(UserRegisterRequest request)
        {
            if (_storeContext.User.Any(u => u.Email == request.Email))
            {
                return BadRequest("User Already Exists");
            }
            Createpasswordhash(request.Password, out byte[] passwordhash, out byte[] Passwordsalt);
            var User = new User
            {
                Username = request.Username,
                Email = request.Email,
                Dateofbirth = request.Dateofbirth,
                Phonenumber = request.Phonenumber,
                Type = "User",
                Passwordhash = passwordhash,
                PasswordSalt = Passwordsalt,
                VerificationToken = CreateRandomToken()
            };
            _storeContext.User.Add(User);
            await _storeContext.SaveChangesAsync();
            return Ok(User);
        }

        [HttpPost("Login")]
        public async Task<IActionResult>Login(UserLoginRequest request)
        {
            var User = await _storeContext.User.FirstOrDefaultAsync(u => u.Email == request.Email);
            if (User == null)
            {
                return BadRequest("User Not Found");
            }
            if (User.VerifiedAt == null)
            {
                return BadRequest("User is not verified");
            }
            if (!Verifiypasswordhash(request.Password,User.Passwordhash,User.PasswordSalt))
            {
                return BadRequest("password is Not Correct");
            }
            return Ok($"Welcome {User.Username}");
        }
        [HttpPost("Verify")]
        public async Task<IActionResult> Verify(string Token)
        {
            var User = await _storeContext.User.FirstOrDefaultAsync(u => u.VerificationToken == Token);
            if (User == null)
            {
                return BadRequest("Invalid Token");
            }
            User.VerifiedAt = DateTime.Now;
            await _storeContext.SaveChangesAsync();
            return Ok("User verified");
        }
        [HttpPost("Reset-Password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordRequest request)
        {
            var User = await _storeContext.User.FirstOrDefaultAsync(u => u.PasswordResetToken ==request.Token);
            if (User == null || User.PasswordResetExpire<DateTime.Now)
            {
                return BadRequest("Invalid Token");
            }
            Createpasswordhash(request.Password, out byte[] passwordhash, out byte[] Passwordsalt);
            User.Passwordhash = passwordhash;
            User.PasswordSalt = Passwordsalt;
            User.PasswordResetToken = null;
            User.PasswordResetExpire = null;
            await _storeContext.SaveChangesAsync();
            return Ok("The password successfully reset");
        }
        [HttpPost("ForgetPassword")]
        public async Task<IActionResult> ForgetPassword(string Email)
        {
            var User = await _storeContext.User.FirstOrDefaultAsync(u => u.Email == Email);
            if (User == null)
            {
                return BadRequest("User Not Found");
            }
            User.PasswordResetToken = CreateRandomToken();
            User.PasswordResetExpire = DateTime.Now.AddDays(1);
            await _storeContext.SaveChangesAsync();
            return Ok("You may now reset the password");
        }

        private bool Verifiypasswordhash(string password,  byte[] passwordhash,  byte[] passwordsalt)
        {
            using (var hmac = new HMACSHA512(passwordsalt))
            {
                var Computerhash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return Computerhash.SequenceEqual(passwordhash);
            }
        }
        private void Createpasswordhash(string password, out byte[] passwordhash, out byte[] passwordsalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordsalt = hmac.Key;
                passwordhash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
        private string CreateRandomToken()
        {
            return Convert.ToHexString(RandomNumberGenerator.GetBytes(64));
        }
    }
}
