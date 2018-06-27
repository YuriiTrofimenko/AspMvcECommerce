using AspMvcECommerce.WebUi.Models;
using AspNetMvcECommerce.Domain;
using AspNetMvcECommerce.Domain.EntityController;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AspMvcECommerce.WebUi.Controllers
{
    public class AuthController : ApiController
    {
        //private AspNetMvcECommerceEntities mContext;
        private Repository mRepository;
        public AuthController()
        {
            //mContext = new AspNetMvcECommerceEntities();
            mRepository = new Repository();
        }

        // GET api/<controller>
        //public IEnumerable<string> Get()
        //public string Get()
        /*public ApiResponse Get()
        {
            //return new string[] { "value1", "value2" };
            //return mContext.Roles.Select(r => r.name).ToList();
            var resp = new ApiResponse();
            try
            {
                int x = 1;
                int y = 0;
                //resp.data = mContext.Roles.Select(r => r.name).ToList();
                resp.data = mContext.Roles.Where(u => u.id == (x / y)).Select(r => r.name).ToList();
            }
            catch (Exception ex)
            {
                resp.error = ex.Message;
            }
            return resp;
        }*/

        // GET api/<controller>/5
        /*public string Get(int id)
        {
            //return id.ToString();
            return mContext.Users.Where(u => u.id == id).Select(u => u.login).SingleOrDefault();
        }*/

        public ApiResponse Get([FromUri] SignupForm _signupForm)
        {
            try
            {
                Role role = mRepository.RoleEC.Find(1);
                User user = new User()
                {
                    login = _signupForm.Login
                    ,
                    password = _signupForm.Password
                    ,
                    Role = role
                    ,
                    role_id = role.id
                };
                mRepository.UserEC.Save(user);
                return new ApiResponse() { data = new List<User>() { user }, error = "" };
            }
            catch (Exception ex)
            {

                return new ApiResponse() { data = null, error = ex.Message };
            }
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}