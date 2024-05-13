using Comunidades.Models;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

public class FirebaseService
{
    private readonly IFirebaseClient _client;
    private readonly IConfiguration _config;

    public FirebaseService(IConfiguration config)
    {
        _config = config;

        IFirebaseConfig firebaseConfig = new FirebaseConfig
        {
            AuthSecret = _config["FirebaseConfig:AuthSecret"],
            BasePath = _config["FirebaseConfig:BaseUrl"]
        };

        _client = new FireSharp.FirebaseClient(firebaseConfig);
    }
    public string WriteData(string node, object data)
    {
        var response = _client.Push(node, data);
        return response.Result.name;
    }
    //Users
    public string AuthenticateUser(string username, string password)
    {
        FirebaseResponse firebaseResponse = _client.Get("users");
        if (firebaseResponse.Body != "null")
        {
            dynamic data = JsonConvert.DeserializeObject<dynamic>(firebaseResponse.Body);

            foreach (var userKey in data)
            {
                var user = JsonConvert.DeserializeObject<User>(((JProperty)userKey).Value.ToString());
                user.Key = ((JProperty)userKey).Name; // Asignar la clave primaria

                if (user.Username == username && user.Password == password)
                {
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var key = Encoding.ASCII.GetBytes(_config["JwtSettings:SecretKey"]);
                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(new Claim[]
                        {
                        new Claim(ClaimTypes.Name, user.Username),
                            // Puedes agregar más claims según tus necesidades
                        }),
                        Expires = DateTime.UtcNow.AddHours(1),
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                    };
                    var token = tokenHandler.CreateToken(tokenDescriptor);
                    return tokenHandler.WriteToken(token);
                }
            }
        }
        return null; // Autenticación fallida
    }
    public async Task<List<User>> GetAllUsers()
    {
        var users = new List<User>();
        var response = await _client.GetAsync("users");
        if (response.Body != "null")
        {
            dynamic data = response.ResultAs<dynamic>();
            foreach (var userKey in data)
            {
                var user = JsonConvert.DeserializeObject<User>(userKey.Value.ToString());
                user.Key = userKey.Name; // Asignar la clave primaria
                users.Add(user);
            }
        }
        return users;
    }





    //obtener un canal de un token
    //fue modificado para que intercambie el campo de key de la bd por la key generada por firebase
    public IEnumerable<Channel> GetChannelsByUserId(string userId)
    {
        var channels = new List<Channel>();

        FirebaseResponse firebaseResponse = _client.Get("channels");
        if (firebaseResponse.Body != "null")
        {
            dynamic data = JsonConvert.DeserializeObject<dynamic>(firebaseResponse.Body);

            foreach (var channelKey in data)
            {
                var channel = JsonConvert.DeserializeObject<Channel>(((JProperty)channelKey).Value.ToString());
                channel.Key = ((JProperty)channelKey).Name; // Asignar la clave primaria

                if (channel.IdUser == userId)
                {
                    channels.Add(channel);
                }
            }
        }
        return channels;
    }
    // Obtener todos los Channels
    public async Task<List<Channel>> GetAllChannels()
    {
        var channels = new List<Channel>();
        var response = await _client.GetAsync("channels");
        if (response.Body != "null")
        {
            dynamic data = response.ResultAs<dynamic>();
            foreach (var channelKey in data)
            {
                var channel = JsonConvert.DeserializeObject<Channel>(channelKey.Value.ToString());
                channel.Key = channelKey.Name; // Asignar la clave primaria
                channels.Add(channel);
            }
        }
        return channels;
    }

    // Resto de la implementación del servicio
    //obtener un post. 
    public IEnumerable<Post> GetPostsByChannelId(string channelId)
    {
        var posts = new List<Post>(); // Creamos una lista para almacenar los posts

        FirebaseResponse firebaseResponse = _client.Get("posts"); // Obtenemos los posts de Firebase
        if (firebaseResponse.Body != "null") // Verificamos si la respuesta no está vacía
        {
            dynamic data = JsonConvert.DeserializeObject<dynamic>(firebaseResponse.Body); // Convertimos la respuesta en un objeto dinámico

            foreach (var postKey in data) // Recorremos las claves de los posts
            {
                var post = JsonConvert.DeserializeObject<Post>(((JProperty)postKey).Value.ToString());
                post.PostKey = ((JProperty)postKey).Name; // Asignar la clave primaria

                if (post.IdChannel == channelId)
                {
                    posts.Add(post);
                }
            }
        }

        return posts; // Devolvemos la lista de posts que coinciden con el channelId
    }
    public IEnumerable<Post> GetPostsByChannelIdPlus(string channelId)
    {
        var posts = new List<Post>();

        FirebaseResponse firebaseResponse = _client.Get("posts");
        if (firebaseResponse.Body != "null")
        {
            dynamic postData = JsonConvert.DeserializeObject<dynamic>(firebaseResponse.Body);

            foreach (var postKey in postData)
            {
                var post = JsonConvert.DeserializeObject<Post>(((JProperty)postKey).Value.ToString());
                post.PostKey = ((JProperty)postKey).Name;

                if (post.IdChannel == channelId)
                {
                    // Obtener reacciones del post
                    post.Reactions = GetReactionsByPostId(post.PostKey).ToList();


                    // Obtener comentarios del post
                    post.Comments = GetCommentsByPostId(post.PostKey).ToList();
                    posts.Add(post);
                }
            }
        }

        return posts;
    }


    //obtener Comentarios
    public IEnumerable<Comment> GetCommentsByPostId(string postId)
    {
        var comments = new List<Comment>(); 

        FirebaseResponse firebaseResponse = _client.Get("comments"); 
        if (firebaseResponse.Body != "null") 
        {
            dynamic data = JsonConvert.DeserializeObject<dynamic>(firebaseResponse.Body); 

            foreach (var commentKey in data) 
            {
                var comment = JsonConvert.DeserializeObject<Comment>(((JProperty)commentKey).Value.ToString()); 

                if (comment.IdPost == postId) 
                {
                    comments.Add(comment); 
                }
            }
        }
        return comments; 
    }

    //obtener Comentarios
    public IEnumerable<Reaction> GetReactionsByPostId(string postId)
    {
        var reactions = new List<Reaction>();

        FirebaseResponse firebaseResponse = _client.Get("reactions");
        if (firebaseResponse.Body != "null")
        {
            dynamic data = JsonConvert.DeserializeObject<dynamic>(firebaseResponse.Body);

            foreach (var reactionKey in data)
            {
                var reaction = JsonConvert.DeserializeObject<Reaction>(((JProperty)reactionKey).Value.ToString());

                if (reaction.IdPost == postId)
                {
                    reactions.Add(reaction);
                }
            }
        }
        return reactions;
    }


}

