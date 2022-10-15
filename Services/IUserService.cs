using CaseManagmentAPI.DataContext;
using CaseManagmentAPI.Helpers;
using CaseManagmentAPI.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using QConnectLibrary;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CaseManagmentAPI.Services
{

    public interface IUserService
    {

        CMUser Authenticate(string username, string password);


    }

    public class UserService : IUserService
    {

        // users hardcoded for simplicity, store in a db with hashed passwords in production applications





        //private List<User> _users = new List<User>
        //{
        //  //  new User { Id = 1, FirstName = "Test", LastName = "User", Username = "test", Password = "test" }
        //};

        private static ILogger<UserService> _logger;
        private readonly AppSettings _appSettings;

        public UserService(IOptions<AppSettings> appSettings, ILogger<UserService> logger)
        {
            _appSettings = appSettings.Value;
            _logger = logger;
        }


        public CMUser Authenticate(string username, string password)
        {
            var user = new CMUser();
            var db = new CMDataContext();
            _logger.LogInformation(1, "Authentication was called!");
            try
            {
                //string decryptPassword = SecureUtil.Decrypt(password);
                password = SecureUtil.Crypt(password);

                //  user = db.TokenUser.Where(c => c.UserName == username && c.Password == password).SingleOrDefault();
                var y = db.Database;
                // var x = db.SugarLevls.Where(c => c.Username == "a@b.com").SingleOrDefault();
                user = db.CMUser!.Where(c => c.UserName == username && c.Password == password).FirstOrDefault();


                //var user = _users.SingleOrDefault(x => x.Username == username && x.Password == password);

                // return null if user not found
                if (user == null)
                    return null;

                // authentication successful so generate jwt token
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                    new Claim(ClaimTypes.Name, user.ID.ToString())
                    }),
                    Expires = DateTime.UtcNow.AddMinutes(60),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                user.Token = tokenHandler.WriteToken(token);
                user.LastLogin = DateTime.Now;
                db.CMUser!.Attach(user);
                db.SaveChanges();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return user;

            }
            return user.WithoutPassword();
        }


    }
}
