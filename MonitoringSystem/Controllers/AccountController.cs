using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MonitoringSystem.Models;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using System.Text;
using MonitoringSystem.Data;
using MonitoringSystem.Models.AccountViewModels;
using MonitoringSystem.Persistences.IRepositories;
using AutoMapper;
using MonitoringSystem.Resources;

namespace MonitoringSystem.Controllers
{

    [Route("/api/accounts")]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _config;

        private readonly ApplicationDbContext _context;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IConfiguration config, ApplicationDbContext context,
            IUserRepository userRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _config = config;
            _context = context;
            _userRepository = userRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody]RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await _userManager.CreateAsync(user, model.Password);
                await _userManager.AddToRoleAsync(user, "Customer");
                if (!result.Succeeded)
                {
                    return BadRequest("Could not register."); ;
                }
                else
                {
                    var userCreated = await _userManager.FindByEmailAsync(model.Email);
                    userCreated.FullName = model.FullName;
                    userCreated.PhoneNumber = model.PhoneNumber;
                    await _context.SaveChangesAsync();
                    return Ok(userCreated);
                }
            }

            return BadRequest("data which sent to server is not valid.");
        }

        [HttpPost]
        [Route("generatetoken")]
        public async Task<IActionResult> GenerateToken([FromBody] LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);

                if (user != null)
                {
                    var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
                    if (result.Succeeded)
                    {
                        var claims = new Claim[]
                        {
                          new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                          new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                          new Claim("role", _userManager.GetRolesAsync(user).Result[0]),
                        };

                        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));
                        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                        var token = new JwtSecurityToken(_config["Tokens:Issuer"],
                          _config["Tokens:Issuer"],
                          claims,
                          expires: DateTime.Now.AddDays(1),
                          signingCredentials: creds);

                        return Ok(
                            new
                            {
                                access_token = new JwtSecurityTokenHandler().WriteToken(token),
                                userName = user.UserName,
                                email = user.Email,
                                role = _userManager.GetRolesAsync(user).Result[0]
                            }
                        );
                    }
                }
            }

            return BadRequest("Could not create token");
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("getall")]
        //[AllowAnonymous]
        public async Task<IActionResult> GetAllUser()
        {
            var users = await _userRepository.GetUsers();
            return Ok(users);
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        [Route("update/{email}")]
        //[AllowAnonymous]
        public async Task<IActionResult> UpdateUser(string email, [FromBody]UserResource userResource)
        {
            //check model is valid?
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userRepository.GetUserByEmail(email);

            //check if user with the id dont exist in the database
            if (user == null)
            {
                return NotFound();
            }

            //map USerResource json into ApplicationUser model
            _mapper.Map<UserResource, ApplicationUser>(userResource, user);

            await _unitOfWork.Complete();

            // converting ApplicationUser object to json result
            var result = _mapper.Map<ApplicationUser, UserResource>(user);
            return Ok(result);

        }

        [HttpDelete]
        [AllowAnonymous]
        [Route("delete/{email}")]
        public async Task<IActionResult> DeleteUser(string email)
        {
            var user = await _userRepository.GetUserByEmail(email);

            //check if user with the id dont exist in the database
            if (user == null)
            {
                return NotFound();
            }

            //just change the IsDeleted of user into true
            _userRepository.RemoveUser(user);
            await _unitOfWork.Complete();

            return Ok(email);
        }
    }
}