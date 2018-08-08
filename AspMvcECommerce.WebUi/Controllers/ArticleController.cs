using AspMvcECommerce.WebUi.Models;
using AspMvcECommerce.WebUi.Share;
using AspNetMvcECommerce.Domain;
using AspNetMvcECommerce.Domain.EntityController;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace AspMvcECommerce.WebUi.Controllers
{
    public class ArticleController : ApiController
    {
        //private AspNetMvcECommerceEntities mContext;
        private Repository mRepository;
        public ArticleController()
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

        [Route("api/articles/get-all")]
        public Object Get()
        {
            if(HttpContext.Current.Session["username"] != null) {

                User user =
                    mRepository.UserEC.FindByLogin(HttpContext.Current.Session["username"].ToString());
                if (user.Role.name == "admin")
                {
                    return new ApiResponse() { data = mRepository.ArticleEC.Articles.ToList(), error = "" };
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

        [Route("api/articles/get-filtered")]
        public Object Get(Object _filterModel)
        {
            if (HttpContext.Current.Session["username"] != null)
            {

                /*User user =
                    mRepository.UserEC.FindByLogin(HttpContext.Current.Session["username"].ToString());*/

                return new ApiResponse() { data = mRepository.ArticleEC.Articles.ToList(), error = "" };
            }
            else
            {
                var response = Request.CreateResponse(HttpStatusCode.Moved);
                response.Headers.Location =
                    new Uri(Url.Content("~/wwwroot/pages/home.htm"));
                return response;
            }
        }

        [Route("api/articles/add")]
        //public Object Get([FromUri] ArticleForm _articleForm)
        public Object Post(ArticleForm _articleForm)
        {
            try
            {
                if (HttpContext.Current.Session["username"] != null)
                {
                    User user =
                        mRepository.UserEC.FindByLogin(HttpContext.Current.Session["username"].ToString());
                    if (user.Role.name == "admin")
                    {
                        Category category = mRepository.CategoryEC.Find(_articleForm.Categoryid);
                        Article article = new Article() {
                            title = Uri.UnescapeDataString(_articleForm.Title)
                            , category_id = _articleForm.Categoryid
                            , description = Uri.UnescapeDataString(_articleForm.Description)
                            , price = _articleForm.Price
                            , quantity = _articleForm.Quantity
                            , Category = category
                            , image_base64 = _articleForm.ImageBase64
                        };
                        mRepository.ArticleEC.Save(article);
                        return new ApiResponse() { data = new List<ArticleForm>() { _articleForm }, error = "" };
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
            catch (DbEntityValidationException e)
            {
                string errorString = "";
                foreach (var eve in e.EntityValidationErrors)
                {
                    errorString += String.Format("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    /*Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);*/
                    foreach (var ve in eve.ValidationErrors)
                    {
                        errorString += String.Format("- Property: \"{0}\", Error: \"{1}\"",
                        ve.PropertyName, ve.ErrorMessage);
                        /*Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);*/
                    }
                }
                throw;
            }
            catch (Exception ex)
            {

                return new ApiResponse() { data = null, error = ex.Message + " : " + ex.StackTrace};
            }
        }

        /*[Route("api/categories/delete")]
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
        }*/

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