using Comunidades.Models;
using Comunidades.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Comunidades.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class TokensController : Controller
    {
        private readonly TokenService _tokenService;

        public TokensController(TokenService tokenService)
        {
            _tokenService = tokenService;
        }

        
        


        [HttpGet("{userId}/channels")]

        public IActionResult GetChannelsByUserId(string userId)
        {

            var channels = _tokenService.GetChannelsByUserId(userId);
            return Ok(channels);
        }
    }
}
