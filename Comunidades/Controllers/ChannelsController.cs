using Comunidades.Services;
using Microsoft.AspNetCore.Mvc;
using Comunidades.Models;
using Microsoft.AspNetCore.Authorization;

namespace Comunidades.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class ChannelsController : ControllerBase
    {
        private readonly ChannelService _channelService;

        public ChannelsController(ChannelService channelService)
        {
            _channelService = channelService;
        }

        [HttpPost]
        public IActionResult CreateChannel(Channel channel)
        {
            _channelService.CreateChannel(channel);
            return Ok();
        }/*
        [HttpGet("{channelId}/posts")]
        public IActionResult GetPostsByChannelId(string channelId)
        {
            var posts = _channelService.GetPostsByChannelId(channelId);
            return Ok(posts);
        }*/

        // En ChannelsController
        [HttpGet]
        public async Task<ActionResult<List<Channel>>> Get()
        {
            var channels = await _channelService.GetAllChannels();
            return Ok(channels);
        }
        [HttpGet("{channelId}/posts")]
        public IActionResult GetPostsByChannelIdPlus(string channelId)
        {
            var posts = _channelService.GetPostsByChannelIdPlus(channelId);
            return Ok(posts);
        }
        [HttpGet("{userId}/channels")]
        
        public IActionResult GetChannelsByUserId(string userId)
        {
            
            var channels = _channelService.GetChannelsByUserId(userId);
            return Ok(channels);
        }





    }

}
