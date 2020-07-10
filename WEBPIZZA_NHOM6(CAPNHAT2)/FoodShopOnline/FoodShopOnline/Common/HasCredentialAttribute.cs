using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.Func;

namespace FoodShopOnline.Common
{
    public class HasCredentialAttribute : AuthorizeAttribute
    {
        public string RoleID {get; set;}
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var session = (UserLogin)HttpContext.Current.Session[CommonConstants.User_Session];
            if(session == null)
            {
                return false;
            }
            List<string> privilegeLevels = this.GetCredentialByLoggerInUser(session.UserName);
            if(privilegeLevels.Contains(this.RoleID) || session.GroupID == CommonConstant.ADMIN_GROUP)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new ViewResult
            {
                ViewName = "~/Areas/Admin/Views/Shared/401.cshtml"
            };  
        }
        private List<string> GetCredentialByLoggerInUser(string userName)
        {
            var credential = (List<string>)HttpContext.Current.Session[CommonConstants.Session_Credential];
            return credential;
        }
    }
}