

using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Helpers;
using API.interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class MessagesController:BaseapiController
    {
        private readonly IUserRepository _userRepository;
        private readonly IMessageRepository _messageRepository;
        private readonly IMapper _mapper;
        

        public MessagesController(IUserRepository userRepository,IMessageRepository messageRepository,IMapper mapper)
        {
           _userRepository = userRepository;
            _messageRepository = messageRepository;
            _mapper = mapper;

        }
        [HttpPost]
         public async Task<ActionResult<MessageDto>> CreateMessage(CreateMessageDto createMessageDto)
        {
            var username=User.GetUsername();
            if(username==createMessageDto.RecipientUsername.ToLower())
            return BadRequest("you cannot send messages to yourself");
            var sender=await _userRepository.GetUserByUsernameAsync(username);
            var recipient=await _userRepository.GetUserByUsernameAsync(createMessageDto.RecipientUsername);
            if(recipient==null)return NotFound();
            var message=new Message
            {
                Sender=sender,
                Recipient=recipient,
                SenderUsername=sender.UserName,
                // senderPhotoUrl=sender.PhotoUrl,
                RecipientUsername=recipient.UserName,
                Content=createMessageDto.Content

            };
            _messageRepository.AddMessage(message);
            if(await _userRepository.SaveAllAsync()) return Ok(_mapper.Map<MessageDto>(message));
            return BadRequest("failed to send to message");
        }
          [HttpGet]
        public async Task<ActionResult<PagedList<MessageDto>>> GetMessagesForUser([FromQuery]MessageParams messageParams)
        {
            messageParams.Username=User.GetUsername();
            var message=await _messageRepository.GetMessageForUser(messageParams);
            Response.AddPaginationHeader(new PaginationHeader(message.CurrentPage,
            message.PageSize,message.TotalCount,message.TotalPages));
            return message;

        }

        [HttpGet("thread/{username}")]
        public async Task<ActionResult<IEnumerable<MessageDto>>> GetMessageThread(string username)
        {
            var currentusername=User.GetUsername();
            return Ok(await _messageRepository.GetMessageThread(currentusername,username));


        }
        
    }
}