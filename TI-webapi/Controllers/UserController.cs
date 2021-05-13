using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using TI_Domain.identity;
using TI_Repository.Interface;
using TI_Webapi.Dtos;

namespace TI_Webapi.Controllers {
    [Route ("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase {

        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly IRepository<User> rep;
        private readonly IMapper mapper;
        private readonly IConfiguration config;

        public UserController (IConfiguration config,
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            IRepository<User> rep,
            IMapper mapper
        ) {
            this.config = config;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.rep = rep;
            this.mapper = mapper;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Get () {
            try {
                var users = await this.rep.GetAllAsync ();
                var results = this.mapper.Map<UserDto[]> (users);

                return Ok (results);
                // return Ok (new UserDto());
            } catch (System.Exception) {
                return this.StatusCode (StatusCodes.Status500InternalServerError,
                    $"Banco de dados falhou ");
            }
        }

        // [HttpPost ("upload")]
        // [AllowAnonymous]
        // public async Task<IActionResult> upload () {
        //     try {
        //         var file = Request.Form.Files[0];
        //         var folderName = Path.Combine ("resources", "images");
        //         var pathToSave = Path.Combine (Directory.GetCurrentDirectory (), folderName);
        //         if (file.Length > 0) {
        //             var fileName = ContentDispositionHeaderValue.Parse (file.ContentDisposition).FileName;
        //             var fullPath = Path.Combine (pathToSave, fileName.Replace ("\"", " ").Trim ());

        //             using (var stream = new FileStream (fullPath, FileMode.Create)) {
        //                 file.CopyTo (stream);
        //             }
        //         }

        //         return Ok ();
        //     } catch (System.Exception ex) {
        //         return this.StatusCode (StatusCodes.Status500InternalServerError,
        //             $"Banco de dados falhou: {ex.Message}");
        //     }
        //     // return BadRequest ("Erro ao tentar realizar upload");
        // }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Post (UserDto model) {
            //Tela Usuario => Entidade Dto
            try {
                // var role = "Alunos";

                var user = this.mapper.Map<User> (model);

                var result = await this.userManager.CreateAsync (user, model.Password);

                var userToReturn = this.mapper.Map<UserDto> (user);

                if (result.Succeeded) {
                    // await userManager.AddToRoleAsync (user);
                    return Created ("GetUser", userToReturn);
                }
                return Ok (userToReturn);

            } catch (System.Exception ex) {
                return this.StatusCode (StatusCodes.Status500InternalServerError,
                    $"Banco de Dados Falhou: {ex.Message}");
            }

        }

        [HttpPost ("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login (UserLoginDto userLogin) {
            var user = await this.userManager.FindByEmailAsync (userLogin.Email);
            var name = await this.userManager.FindByNameAsync (user.UserName);

            var result = await this.signInManager.CheckPasswordSignInAsync (user, userLogin.Password, false);

            if (result.Succeeded) {
                var resultEmail = await this.userManager.Users
                    .FirstOrDefaultAsync (u => u.NormalizedEmail == userLogin.Email.ToUpper ());

                var userToReturn = this.mapper.Map<UserDto> (resultEmail);

                return Ok (new {
                    token = GenereteToken (resultEmail).Result,
                        user = userToReturn, name
                });

            }

            return Unauthorized ();
        }
        private async Task<string> GenereteToken (User user) {
            var claims = new List<Claim> {
                new Claim (ClaimTypes.NameIdentifier, user.Id.ToString ()),
                new Claim (ClaimTypes.Name, user.UserName),
                new Claim (ClaimTypes.Email, user.Email),
                new Claim (ClaimTypes.Actor, user.ImgPerfil),
                // new Claim (ClaimTypes.Locality, user.UserEmpresas.),
            };

            var roles = await this.userManager.GetRolesAsync (user);
            //  var roles = await this.rep.GetRole(user.Id);

            foreach (var role in roles) {
                claims.Add (new Claim (ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey (Encoding.ASCII
                .GetBytes (this.config.GetSection ("AppSetting:Token").Value));

            var credentials = new SigningCredentials (key, SecurityAlgorithms.HmacSha512Signature);

            var tokenInfo = new SecurityTokenDescriptor {
                Subject = new ClaimsIdentity (claims),
                Expires = DateTime.Now.AddDays (1),
                SigningCredentials = credentials
            };

            var tokenHandler = new JwtSecurityTokenHandler ();
            var token = tokenHandler.CreateToken (tokenInfo);

            return tokenHandler.WriteToken (token);
        }

        [HttpPut ("{Id}")]
        [AllowAnonymous]
        public async Task<IActionResult> Put (string id, UserDto model) {
            try {

                var user = await this.rep.GetByIdAsync (id);

                this.mapper.Map (model, user);

                this.rep.Udpate (user);

                if (await this.rep.SaveChangesAsync ())
                    return Created ($"/api/user/{model}", this.mapper.Map<UserDto> (user));
            } catch (System.Exception ex) {
                return this.StatusCode (StatusCodes.Status500InternalServerError,
                    $"Banco de dados falhou: {ex.Message}");
            }

            return BadRequest ();
        }

    }
}