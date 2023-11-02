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
        private readonly PresenceTracker _tracker ;
        public PresenceHub(PresenceTracker tracker)
        {
            _tracker = tracker;

        }
        public async override Task OnConnectedAsync()
        {
           var isOnline= await _tracker.UserConnected(Context.User.GetUsername(),Context.ConnectionId);
            if(isOnline)
            await Clients.Others.SendAsync("UserIsOnline",Context.User.GetUsername());

            var currentUsers = await _tracker.GetOnlineUsers();

            // await Clients.All.SendAsync("GetOnlineUsers",currentUsers); this line cmntd/changed to all to caller in237 optimization

            await Clients.Caller.SendAsync("GetOnlineUsers",currentUsers);

       
        }
         public override async Task OnDisconnectedAsync(Exception exception)
        {
                
           var isOffline= await  _tracker.UserDisconnected(Context.User.GetUsername(),Context.ConnectionId);
            if(isOffline)
            await Clients.Others.SendAsync("UserIsOffline",Context.User.GetUsername());

            //  this below two line cmntd in 237 optimization 
        
            // var currentUser =   await _tracker.GetOnlineUsers();

            // await Clients.All.SendAsync("GetOnlineUsers",currentUser);

            await base.OnDisconnectedAsync(exception);

        }
    }
}