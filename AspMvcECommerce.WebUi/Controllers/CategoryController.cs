using AspMvcECommerce.WebUi.Models;
using AspMvcECommerce.WebUi.Share;
using AspNetMvcECommerce.Domain;
using AspNetMvcECommerce.Domain.EntityController;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace AspMvcECommerce.WebUi.Controllers
{
    public class CategoryController : ApiController
    {
        //private AspNetMvcECommerceEntities mContext;
        private Repository mRepository;
        public CategoryController()
        {
            //mContext = new AspNetMvcECommerceEntities();
            mRepository = new Repository();
        }

        /*public ApiResponse Get([FromUri] SignupForm _signupForm)
        {
            switch (_signupForm.Action)
            {
                case HttpRequestParams.signup : {

                        try
                        {
                            Role role = mRepository.RoleEC.Find(2);
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
                case HttpRequestParams.signin:
                    {
                        try
                        {
                            User user =
                                mRepository.UserEC.FindByLogin(_signupForm.Login);

                            if (user != null && _signupForm.Password == user.password)
                            {

                                HttpContext.Current.Session["username"] = _signupForm.Login;
                                return new ApiResponse() { data = new List<string>() { user.login }, error = "" };
                            }
                            else {

                                return new ApiResponse() { data = null, error = "auth_error" };
                            }
                        }
                        catch (Exception ex)
                        {

                            return new ApiResponse() { data = null, error = ex.Message };
                        }
                    }
                case HttpRequestParams.signout:
                    {
                        try
                        {
                            HttpContext.Current.Session["username"] = null;
                            return new ApiResponse() { data = new List<string>() { "logout" }, error = "" };
                        }
                        catch (Exception ex)
                        {

                            return new ApiResponse() { data = null, error = ex.Message };
                        }
                    }
                default:
                    return new ApiResponse() { data = null, error = "params_error" };
            }
        }*/

        [Route("api/categories/get-all")]
        public Object Get()
        {
            if(HttpContext.Current.Session["username"] != null) {

                User user =
                    mRepository.UserEC.FindByLogin(HttpContext.Current.Session["username"].ToString());
                if (user.Role.name == "admin")
                {
                    return new ApiResponse() { data = mRepository.CategoryEC.Categories.ToList(), error = "" };
                }
                else
                {
                    var response = Request.CreateResponse(HttpStatusCode.Moved);
                    response.Headers.Location =
                        new Uri(Url.Content("~/wwwroot/pages/home.htm"));
                    return response;
                }
            }
            else
            {
                var response = Request.CreateResponse(HttpStatusCode.Moved);
                response.Headers.Location =
                    new Uri(Url.Content("~/wwwroot/pages/home.htm"));
                return response;
            }
        }

        [Route("api/categories/add")]
        public Object Get(string catname)
        {
            try
            {
                if (HttpContext.Current.Session["username"] != null)
                {
                    User user =
                        mRepository.UserEC.FindByLogin(HttpContext.Current.Session["username"].ToString());
                    if (user.Role.name == "admin")
                    {
                        Category category = new Category() { name = catname, Articles = new List<Article>() };
                        mRepository.CategoryEC.Save(category);
                        return new ApiResponse() { data = new List<Category>() { category }, error = "" };
                    }
                    else
                    {
                        var response = Request.CreateResponse(HttpStatusCode.Moved);
                        response.Headers.Location =
                            new Uri(Url.Content("~/wwwroot/pages/home.htm"));
                        return response;
                    }
                }
                else
                {
                    var response = Request.CreateResponse(HttpStatusCode.Moved);
                    response.Headers.Location =
                        new Uri(Url.Content("~/wwwroot/pages/home.htm"));
                    return response;
                }
            }
            catch (Exception ex)
            {

                return new ApiResponse() { data = null, error = ex.Message + " : " + ex.StackTrace};
            }
        }

        [Route("api/categories/delete")]
        public Object Get(int catid)
        {
            try
            {
                if (HttpContext.Current.Session["username"] != null)
                {
                    User user =
                        mRepository.UserEC.FindByLogin(HttpContext.Current.Session["username"].ToString());
                    if (user.Role.name == "admin")
                    {
                        Category category = mRepository.CategoryEC.Find(catid);
                        mRepository.CategoryEC.Remove(category);
                        return new ApiResponse() { data = new List<Category>() { category }, error = "" };
                    }
                    else
                    {
                        var response = Request.CreateResponse(HttpStatusCode.Moved);
                        response.Headers.Location =
                            new Uri(Url.Content("~/wwwroot/pages/home.htm"));
                        return response;
                    }
                }
                else
                {
                    var response = Request.CreateResponse(HttpStatusCode.Moved);
                    response.Headers.Location =
                        new Uri(Url.Content("~/wwwroot/pages/home.htm"));
                    return response;
                }
            }
            catch (Exception ex)
            {

                return new ApiResponse() { data = null, error = ex.Message + " : " + ex.StackTrace };
            }
        }

        /*public ApiResponse Get([FromUri] string action)
        {
            switch (action)
            {
                case HttpRequestParams.signout:
                    {
                        try
                        {
                            HttpContext.Current.Session["username"] = null;
                            return new ApiResponse() { data = new List<string>() { "logout" }, error = "" };
                        }
                        catch (Exception ex)
                        {

                            return new ApiResponse() { data = null, error = ex.Message };
                        }
                    }
                default:
                    return new ApiResponse() { data = null, error = "params_error" };
            }
        }*/

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