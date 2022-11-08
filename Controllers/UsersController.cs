using CaseManagmentAPI.DataContext;
using CaseManagmentAPI.Models;
using CaseManagmentAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;
using QConnectLibrary;

namespace CaseManagmentAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;
        private CMDataContext ctx = new CMDataContext();



        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] AuthenticateModel model)
        {
            var user = _userService.Authenticate(model.Username, model.Password);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(user);
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] AuthenticateModel model)
        {
            var user = _userService.Authenticate(model.Username, model.Password);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(user);
        }

        [Route("search")]
        [HttpGet]
        public async Task<UserResponseList> searchUser(string qry)
        {
            var resp = new UserResponseList();
            var users = new List<_User>();
            var currentUser = HttpContext.User;
            string name = currentUser.Identity.Name;

            try
            {
                var loggedinUser = getUser(Convert.ToInt32(name));



                var _r = ctx.CMUser.Where(c => c.UserName.StartsWith(qry) || c.FirstName.StartsWith(qry) || c.LastName.StartsWith(qry)

           || c.UserName.Contains(qry) || c.FirstName.Contains(qry) || c.LastName.Contains(qry) && c.IsDeleted == false)

               .ToList();

                var r = _r.Where(x => x.IsDeleted == false).ToList();

                foreach (var x in r)
                {
                    _User u = new _User();
                    u.FirstName = x.FirstName;
                    u.IsActive = x.IsActive;
                    u.LastName = x.LastName;
                    u.RoleId = x.RoleId;
                    u.ID = x.ID;
                    u.UserName = x.UserName;
                    users.Add(u);
                }



                resp.users = users;
                resp.Code = 00;
                resp.total = users.Count();
                resp.Message = "Successful";
            }
            catch (Exception ex)
            {
                resp.Code = 09;
                resp.Message = ex.Message;
            }

            return await Task.FromResult(resp);
        }


        [Route("GetUser")]
        [HttpGet]
        public async Task<UserResponse> GetUserDetails(int userid, string username)
        {
            var resp = new UserResponse();
            var currentUser = HttpContext.User;
            string name = currentUser.Identity.Name;

            try
            {
                var loggedinUser = getUser(Convert.ToInt32(name));
                var x = ctx.CMUser.Where(c => c.ID == userid || c.UserName == username).SingleOrDefault();


                cUser u = new cUser();
                u.FirstName = x.FirstName;
                u.IsActive = x.IsActive;
                u.LastName = x.LastName;
                u.RoleId = x.RoleId;
                u.ID = x.ID;
                u.UserName = x.UserName;
                u.LastLogin = x.LastLogin;
                u.PasswordExpired = x.PasswordExpired;




                resp.User = u;
                resp.Code = 00;
                resp.Message = "Successful";
            }
            catch (Exception ex)
            {
                resp.Code = 09;
                resp.Message = ex.Message;
            }

            return await Task.FromResult(resp);
        }

        [Route("GetUsers")]
        [HttpGet]

        public async Task<UserResponseList> GetAllUsers(string qry, int page, int limit)
        {
            var currentUser = HttpContext.User;
            string name = currentUser.Identity.Name;
            var users = new List<_User>();
            var resp = new UserResponseList();
            var loggedinUser = getUser(Convert.ToInt32(name));
            // var _resp = new BookingResponse();
            try
            {
                if (!String.IsNullOrEmpty(qry))
                {
                    var totalUsers = ctx.CMUser.Where(c => c.FirstName.StartsWith(qry) || c.LastName.StartsWith(qry) || c.UserName.StartsWith(qry)
                   || c.FirstName.Contains(qry) || c.LastName.Contains(qry) || c.UserName.Contains(qry)
                   ).ToList();
                    var u = totalUsers.Where(x => x.IsDeleted == false).Skip((page - 1) * limit)
                                         .Take(limit)
                    .ToList();


                    foreach (var x in u)
                    {
                        _User r = new _User();
                        r.FirstName = x.FirstName;
                        r.IsActive = x.IsActive;
                        r.LastLogin = x.LastLogin;
                        r.LastName = x.LastName;
                        r.PasswordExpired = x.PasswordExpired;
                        r.RoleId = x.RoleId;
                        r.ID = x.ID;
                        r.UserName = x.UserName;
                        users.Add(r);
                    }
                    // resp.total = bookings.Count;
                    resp.users = users;
                    resp.total = u.Count();

                    resp.Code = 00;
                    resp.Message = "Successful";
                }
                else
                {
                    var totalUsers = ctx.CMUser.Where(x => x.IsDeleted == false).ToList();
                    var u = totalUsers.Where(x => x.IsDeleted == false).Skip((page - 1) * limit)
                                         .Take(limit)
                                         .ToList();


                    foreach (var x in u)
                    {
                        _User r = new _User();
                        r.FirstName = x.FirstName;
                        r.IsActive = x.IsActive;
                        r.LastLogin = x.LastLogin;
                        r.LastName = x.LastName;
                        r.PasswordExpired = x.PasswordExpired;
                        r.RoleId = x.RoleId;
                        r.ID = x.ID;
                        r.UserName = x.UserName;
                        users.Add(r);
                    }
                    // resp.total = bookings.Count;
                    resp.users = users;
                    resp.total = totalUsers.Count();

                    resp.Code = 00;
                    resp.Message = "Successful";
                }




            }
            catch (Exception ex)
            {
                resp.Code = 09;
                resp.Message = ex.Message;

            }

            return await Task.FromResult(resp);
        }


        [Route("Delete")]
        [HttpPost]
        public async Task<UserResponseList> DeleteUser([FromBody] deleteList list)
        {
            var resp = new UserResponseList();
            var currentUser = HttpContext.User;
            string name = currentUser.Identity.Name;

            try
            {
                var loggedinUser = getUser(Convert.ToInt32(name));
                var _r = ctx.CMUser.Where(c => list.ids.Contains(c.ID)).ToList();

                if (_r.Count == 1)
                {
                    _r.SingleOrDefault().IsActive = false;
                    _r.SingleOrDefault().PasswordExpired = true;
                    _r.SingleOrDefault().LastModifiedBy = loggedinUser.UserName;
                    _r.SingleOrDefault().LastUpdateTime = DateTime.Now;
                    _r.SingleOrDefault().IsDeleted = true;
                }
                else if (_r.Count > 1)
                {
                    foreach (var x in _r)
                    {
                        x.IsActive = false;
                        x.PasswordExpired = true;
                        x.LastModifiedBy = loggedinUser.UserName; ;
                        x.LastUpdateTime = DateTime.Now;
                        x.IsDeleted = true;
                    }

                    // _r.RemoveAll(c => list.BookingId.Contains(c.Id));

                }

                ctx.SaveChanges();

                resp.Code = 00;
                resp.Message = "Successful";
            }
            catch (Exception ex)
            {
                resp.Code = 09;
                resp.Message = ex.Message;
            }

            return await Task.FromResult(resp);
        }

        [Route("Edit")]
        [HttpPost]

        public async Task<UserResponse> EditUser([FromBody] cUser u)
        {
            var resp = new UserResponse();
            var currentUser = HttpContext.User;
            string name = currentUser.Identity.Name;

            try
            {
                var loggedinUser = getUser(Convert.ToInt32(name));
                var r = ctx.CMUser.Where(c => c.ID == u.ID).SingleOrDefault();
                r.LastUpdateTime = u.LastUpdateTime;
                r.RoleId = u.RoleId;
                r.PasswordExpired = u.PasswordExpired;
                r.LastName = u.LastName;
                r.IsActive = u.IsActive;
                r.FirstName = u.FirstName;

                ctx.CMUser.Attach(r);
                ctx.SaveChanges();

                resp.User = u;
                resp.Code = 00;
                resp.Message = "Successful";
            }
            catch (Exception ex)
            {
                resp.Code = 09;
                resp.Message = ex.Message;
            }

            return await Task.FromResult(resp);
        }



        [Route("register")]
        [HttpPost]
        public IActionResult Register([FromBody] User u)
        {
            var db = new CMDataContext();


            var verify = db.CMUser.Where(c => c.UserName == u.Username).SingleOrDefault();

            if (verify == null)
            {


                var user = new CMUser()
                {
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Password = SecureUtil.Crypt(u.Password),
                    UserName = u.Username,
                    IsActive = true,
                    Created = DateTime.Now,
                    RoleId = (int)Roles.CMUser,
                    LastModifiedBy = u.Username,
                    LastUpdateTime = DateTime.Now
                };

                db.CMUser.Add(user);

                db.SaveChanges();


                /*  var cand = new Candidate()
                  {
                      FirstName = u.FirstName,
                      LastName = u.LastName,
                      User = user,
                      LastUpdateTime = user.LastUpdateTime,
                      Created = user.Created,
                      UpdatedBy = u.Username,
                      CreatedBy = user.UserName
                  };
                  db.Candidates.Add(cand);
                */
                db.SaveChanges();
                var createdUser = db.CMUser.Where(c => c.UserName == u.Username && c.IsActive == true).SingleOrDefault();

                if (createdUser != null && createdUser.IsActive == true)
                {
                    var resp = new Iresp(0, "User Regsitration Successful");
                    return Ok(resp);
                }
                else
                {
                    return NotFound(404);
                }
            }
            else
            {
                var resp = new Iresp(1, "User Already Exists");

                return Ok(resp);
            }



        }





        [Route("Add")]
        [HttpPost]
        public async Task<UserResponse> Register([FromBody] CMUser u)
        {
            var db = new CMDataContext();
            var resp = new UserResponse();
            var x = new cUser();

            var verify = db.CMUser.Where(c => c.UserName == u.UserName).SingleOrDefault();

            if (verify == null)
            {


                var user = new CMUser()
                {
                    FirstName = u.FirstName,

                    LastName = u.LastName,
                    Password = SecureUtil.Crypt(u.Password),
                    UserName = u.UserName,
                    IsActive = u.IsActive,
                    Created = DateTime.Now,
                    RoleId = u.RoleId,
                    LastModifiedBy = u.UserName,
                    LastUpdateTime = DateTime.Now,
                    PasswordExpired = false,
                    PhoneNumberOne = u.PhoneNumberOne,
                    PhoneNumberTwo = u.PhoneNumberTwo

                };

                db.CMUser.Add(user);

                db.SaveChanges();

                x.FirstName = u.FirstName;
                x.LastName = u.LastName;
                x.UserName = u.UserName;
                x.IsActive = u.IsActive;
                x.Created = user.Created;
                x.RoleId = user.RoleId;
                x.LastModifiedBy = user.LastModifiedBy;
                x.LastUpdateTime = user.LastUpdateTime;
                x.PasswordExpired = user.PasswordExpired;


                var createdUser = db.CMUser.Where(c => c.UserName == u.UserName && c.IsActive == true).SingleOrDefault();

                if (createdUser != null && createdUser.IsActive == true)
                {
                    resp.Code = 0;
                    resp.Message = "Successful";
                    resp.User = x;

                }
                else
                {
                    resp.Code = 1;
                    resp.Message = "Not Found";
                    resp.User = x;
                }
            }
            else
            {
                resp.Code = 1;
                resp.Message = "User Already Exists";
                resp.User = x;
            }


            return await Task.FromResult(resp);
        }



        private CMUser getUser(int userid)
        {
            var user = new CMUser();
            try
            {
                user = ctx.CMUser.Where(c => c.ID == userid).SingleOrDefault();



            }
            catch (Exception ex)
            {
                user.UserName = ex.Message;
                user.ID = 0;
            }

            return user;

        }


    }


}