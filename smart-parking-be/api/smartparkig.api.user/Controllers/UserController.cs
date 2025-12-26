
using Microsoft.AspNetCore.Mvc;
using smartparking.db.postgres;

namespace user.api.user
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly PostGresClient _userClient;
        private readonly ILogger<UserController> _logger;

        public UserController(
            PostGresClient userClient,
            ILogger<UserController> logger

         ){

            _logger = logger;
            _userClient = userClient;
            
        }

    


        [HttpGet("{Id}")]
        public UserInfo GetUserById(int Id)
        {
            _logger.LogInformation($"Getting user with id: {Id}");
            //TO DO AWAIT CLIENT TO COMPLETE THE OPERATION

            var user = _userClient.GetUserById(Id);
            return new UserInfo(user);

        }

          [HttpGet("{mail}")]
        public UserInfo GetUserByMail(string mail)
        {
            _logger.LogInformation($"Getting user with mail: {mail}");
            //TO DO AWAIT CLIENT TO COMPLETE THE OPERATION

            var user = _userClient.GetUserByMail(mail);
            return new UserInfo(user);

        }

        [HttpGet]
        public List<UserInfo> GetUsers()
        {
            _logger.LogInformation($"Getting users ");
            //TO DO AWAIT CLIENT TO COMPLETE THE OPERATION

            var users = _userClient.GetUsers();
            return users.Select(x => new UserInfo(x)).ToList();

        }

        [HttpPost]
        public async Task CreateUser(UserInfo userInfo)
        {
            Console.WriteLine($"User with made with userId: {userInfo.Mail}");
            
            await _userClient.CreateUser(userInfo.Convert());

        }

         [HttpPatch]
        public async Task UpdateUser(UserInfo userInfo)
        {
            Console.WriteLine($"User with Id: {userInfo.Mail} updating...");
            
            await _userClient.UpdateUser(userInfo.Convert());

        }


         [HttpDelete("{Id}")]
        public async Task DeleteUserById(int Id)
        {
            Console.WriteLine($"User with Id: {Id} deleting...");
            
            await _userClient.DeleteUserById( Id);

        }

         [HttpDelete("{mail}")]
        public async Task DeleteUserBymail(string mail)
        {
            Console.WriteLine($"User with mail: {mail} deleting...");
            
            await _userClient.DeleteUserByMail( mail);

        }
    }
}
