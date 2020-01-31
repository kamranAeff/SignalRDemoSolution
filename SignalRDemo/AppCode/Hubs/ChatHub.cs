using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace SignalRDemo.AppCode.Hubs
{

    public interface IChatHub
    {
        Task Received(string sender,string message);
    }

    public class ChatHub : Hub<IChatHub>
    {
        static ConcurrentDictionary<string,string> clients = new ConcurrentDictionary<string, string>();

        async public Task SendMessage(string to, string message)
        {
            var httpContext = Context.GetHttpContext();
            var email = httpContext.Request.Query["email"];

            if (clients.TryGetValue(to, out string connectionId))
            {
                await Clients.Client(connectionId).Received(email, message);
                return;
            }
            await Clients.Caller.Received("From Server", $"{to} is disconnected!");
        }


        public override Task OnConnectedAsync()
        {
            var httpContext = Context.GetHttpContext();
            var email = httpContext.Request.Query["email"];

            if (!string.IsNullOrWhiteSpace(email))
            {
                clients.AddOrUpdate(email, Context.ConnectionId, (k, v) => Context.ConnectionId);

                Clients.Caller.Received("FromServer","Welcome");
            }
            

            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            return base.OnDisconnectedAsync(exception);
        }
    }
}
