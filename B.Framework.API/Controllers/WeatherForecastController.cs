using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using B.Framework.API.Testes;
using B.Framework.Application.Attribute.Extensions;
using B.Framework.Application.Extensiosn.Object;
using B.Framework.Application.Security.JWT;
using B.Framework.Application.Utility;
using B.Framework.Domain.Shared.User;
using B.Framework.Domain.User;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace B.Framework.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private IUserService _userService;
        private BTokenHandler _tokenHandler;

        public WeatherForecastController(ILogger<WeatherForecastController> logger,IUserService userService,BTokenHandler tokenHandler)
        {
            _tokenHandler = tokenHandler;
            _userService = userService;
            _logger = logger;
        }



        [Route("/tokenget")]
        [HttpGet]
        public async Task<IActionResult> TokenGet()
        {
          
            var user = new UserBase()
            {
                FirstName = "burak",
                Username = "unknow001",
                LastName = "temellioğlu",
                Password = "123123",
                Roles = new List<string>()
                {
                    "Admin", "Admin5"
                }
            };

            var token = _tokenHandler.CreateAccessToken(user);

            var cancelTokensource = new CancellationTokenSource();
            
            
            return Ok(token);
        }

        [Route("/tokenset")]
        [HttpGet]
        public async Task<IActionResult> TokenSet()
        {
            var user = HttpContext.User;
            return Ok(user.Claims);
        }
        
        [Route("/testme")]
        [HttpGet]
        public async Task<IActionResult> TestMe()
        {
            var instance = new Deneme();
            instance.qWery = "burak temellioğlu";
            var x = 2;
            var bla = Check.IsNumeric((object)x.ToString());
            var instancevalue = instance.GetBAttributevalue(b => b.qWery);
            var xx = nameof(instance.qWery);

            var inn = instance.GetPropertyValue<string>(nameof(instance.qWery));
            return Ok(instancevalue);

        }
        

        [Route("/buraktest/test")]
        [HttpGet]
        public async Task<IActionResult> Deneme()
        {

            var defaultClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, "burak"),
                new Claim(ClaimTypes.Email,"mail@buraktemellioglu.com")
            };
            var defaultIdentityProvider = new ClaimsIdentity(defaultClaims, "default");
            var userPrincipal = new ClaimsPrincipal(defaultIdentityProvider);
            await HttpContext.SignInAsync(userPrincipal);
            return Ok("Burak");
        }
        
        [Authorize]
        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
                {
                    Date = DateTime.Now.AddDays(index),
                    TemperatureC = rng.Next(-20, 55),
                    Summary = Summaries[rng.Next(Summaries.Length)]
                })
                .ToArray();
        }
    }
}