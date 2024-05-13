using Comunidades.Models;
using Comunidades.Services;
using Microsoft.AspNetCore.Mvc;

namespace Comunidades.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReactionsController : ControllerBase
    {
        private readonly ReactionService _reactionService;

        public ReactionsController(ReactionService reactionService)
        {
            _reactionService = reactionService;
        }

        [HttpPost]
        public IActionResult CreateReaction(Reaction reaction)
        {
            _reactionService.CreateReaction(reaction);
            return Ok();
        }
    }

}
