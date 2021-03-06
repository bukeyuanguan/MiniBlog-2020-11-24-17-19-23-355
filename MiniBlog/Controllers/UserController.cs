using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using MiniBlog.Model;
using MiniBlog.Services;
using MiniBlog.Stores;

namespace MiniBlog.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserStore userStore;
        private readonly IArticleStore articleStore;
        private readonly UserService userService;

        public UserController(UserService userService, IUserStore userStore, IArticleStore articleStore)
        {
            this.userStore = userStore;
            this.articleStore = articleStore;
            this.userService = userService;
        }

        [HttpPost]
        public async Task<ActionResult<User>> Register(User user)
        {
            this.userService.RegisterUserByName(user.Name, user.Email);
            return CreatedAtAction(nameof(GetByName), new { name = user.Name }, user);
        }

        [HttpGet]
        public List<User> GetAll()
        {
            return userStore.Users;
        }

        [HttpPut]
        public User Update(User user)
        {
            return this.userService.UpdateUser(user);
        }

        [HttpDelete]
        public User Delete(string name)
        {
            return this.userService.DeleteUser(name);
        }

        [HttpGet("{name}")]
        public User GetByName(string name)
        {
            return this.userService.GetUserByName(name);
        }
    }
}