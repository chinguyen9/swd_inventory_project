using PhoneInventoryManagement.Models;
using System.Data;
using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web;
using PhoneInventoryManagement.ViewModels;

namespace PhoneInventoryManagement.Controllers
{
    [RoutePrefix("Logins")]
    public class LoginController : ApiController
    {
        private PhoneIMDbContext db = new PhoneIMDbContext();
       
        // GET: api/Users/5
        [HttpPost, Route("")]
        public IHttpActionResult PostUser(LoginVM loginVM)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var model = db.User.FirstOrDefault(x => x.UserName == loginVM.Username && x.Password == loginVM.Password);
                if (model == null)
                {
                    return NotFound();
                }
                return Ok(model);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

    }
}
