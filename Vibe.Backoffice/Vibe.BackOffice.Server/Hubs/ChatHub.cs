using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Distributed;
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
        private readonly IDistributedCache _cache;

        public ChatHub(IDistributedCache cache)
        {
            _cache = cache;
        }

        public async Task JoinChat(UserConnection connection)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, connection.SupportRequestId.ToString());

            await _cache.SetStringAsync(Context.ConnectionId, JsonSerializer.Serialize(connection));
        }

        /*public async Task SendMessage(String message)
        {
            var stringConnection = await _cache.GetAsync(Context.ConnectionId);

            UserConnection? connection = JsonSerializer.Deserialize<UserConnection>(stringConnection);
            if (connection is not null)
            {
                await Clients
                    .Group(connection.SupportRequestId.ToString())
                    .ReceiveMessage(message);
            }
        }*/

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var stringConnection = await _cache.GetAsync(Context.ConnectionId);
            UserConnection? connection = JsonSerializer.Deserialize<UserConnection>(stringConnection);

            if (connection is not null)
            {
                await _cache.RemoveAsync(Context.ConnectionId);
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, connection.SupportRequestId.ToString());
            }

            await base.OnDisconnectedAsync(exception);
        }
    }
}
