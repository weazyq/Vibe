using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Distributed;
using StackExchange.Redis;
using System.Text.Json;
using Vibe.Chat.Models;
using Vibe.Domain.SupportRequests.SupportMessages;

namespace Vibe.Chat.Hubs
{
    public interface IChatClient
    {
        Task ReceiveMessage(SupportMessage message);
    }

    public class ChatHub : Hub<IChatClient>
    {
        //DockerCompose
        /*private readonly IConnectionMultiplexer _redis;*/
        private readonly IDistributedCache _redis;

        public ChatHub(IDistributedCache redis)
        {
            _redis = redis;
        }

        public async Task JoinChat(UserConnection connection)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, connection.SupportRequestId.ToString());

            //Docker
            /*var db = _redis.GetDatabase();
            await db.StringSetAsync(Context.ConnectionId, JsonSerializer.Serialize(connection));*/

            _redis.SetString(Context.ConnectionId, JsonSerializer.Serialize(connection));
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var stringConnection = _redis.GetString(Context.ConnectionId);

            //Docker
            /*var db = _redis.GetDatabase();
            var stringConnection = await db.StringGetAsync(Context.ConnectionId);*/
            UserConnection? connection = JsonSerializer.Deserialize<UserConnection?>(stringConnection);

            if (connection is not null)
            {
                /* DOCKER
                await db.KeyDeleteAsync(Context.ConnectionId);
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, connection.SupportRequestId.ToString());
                 */
                _redis.Remove(Context.ConnectionId);
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, connection.SupportRequestId.ToString());
            }


            await base.OnDisconnectedAsync(exception);
        }
    }
}
