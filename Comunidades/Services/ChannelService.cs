using Comunidades.Models;
using FireSharp;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;

namespace Comunidades.Services
{
    public class ChannelService
    {
        private readonly FirebaseService _firebaseService;


        public ChannelService(FirebaseService firebaseService)
        {
            _firebaseService = firebaseService;
        }

        public void CreateChannel(Channel channel)
        {
            _firebaseService.WriteData("channels", channel);
        }

        //obtener posts del canal
        public IEnumerable<Post> GetPostsByChannelId(string channelId)
        {
            return _firebaseService.GetPostsByChannelId(channelId);
        }
        // En ChannelService
        public async Task<List<Channel>> GetAllChannels()
        {
            return await _firebaseService.GetAllChannels();
        }

        public IEnumerable<Post> GetPostsByChannelIdPlus(string channelId)
        {
            return _firebaseService.GetPostsByChannelIdPlus(channelId);
        }
        //obtener canales token
        public IEnumerable<Channel> GetChannelsByUserId(string userId)
        {
            return _firebaseService.GetChannelsByUserId(userId);
        }






    }
}

        
