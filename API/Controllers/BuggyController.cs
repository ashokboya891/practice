using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
  
    public class BuggyController:BaseapiController
    {

        private readonly DataContext _context;
        public BuggyController(DataContext context )
        {
            _context=context;
        }
        [Authorize]
        [HttpGet("auth")]
        public ActionResult<string> GetSecret()
        {
            return "secret key";

        }
        [HttpGet("not-found")]
        public ActionResult<AppUser> GetNotFound()
        {
           var thing=_context.Users.Find(-1);
           if(thing==null)return NotFound(thing);
           return thing;
        }
        [HttpGet("server-error")]
        public ActionResult<string> GetServerError()
        {
             var thing=_context.Users.Find(-1);
                var thingtoreturn=thing.ToString();
                return thingtoreturn;
        // try{    this complete code si used to generate custom exceptoion and resolving in here it self but we are goint to deal the exception 
        // by using middle ware for all exception rather using try/catch for every exception
        //         var thing=_context.Users.Find(-1);
        //         var thingtoreturn=thing.ToString();
        //         return thingtoreturn;
        //     }
        //     catch(Exception ex)
        //     {
        //     return StatusCode(500,"computer  says no!!!");
        //     }
        }
        [HttpGet("bad-request")]
        public ActionResult<AppUser> GetBadRequest()
        {
          return BadRequest("this is not a good request");
        }
         
    }
}