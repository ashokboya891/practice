using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace API.SignalR
{
    [Authorize]
    public class PresenceHub:Hub
    {
        public async override Task OnConnectedAsync()
        {
      
            await Clients.Others.SendAsync("UserIsOnline",Context.User.GetUsername());
        }
         public override async Task OnDisconnectedAsync(Exception exception)
        {
           
            // bool isOffline =  await  _tracker.UserDisconnected(Context.User.GetUsername(),Context.ConnectionId);
            // if(isOffline)
            await Clients.Others.SendAsync("UserIsOffline",Context.User.GetUsername());
        
            await base.OnDisconnectedAsync(exception);
            

            // var currentUser =   await _tracker.GetOnlineUsers();

            // await Clients.All.SendAsync("GetOnlineUsers",currentUser);

        }
    }
}