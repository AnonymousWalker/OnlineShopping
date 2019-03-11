using System.Web.Mvc;
using System.Web.Mvc.Filters;

namespace OnlineShopping
{
    public class AuthAccess : ActionFilterAttribute, IAuthenticationFilter
    {
        private bool _isAuth;
        
        public void OnAuthentication(AuthenticationContext filterContext)
        {
            //_isAuth = filterContext.ActionDescriptor.GetCustomAttributes(typeof(OverrideAuthenticationAttribute),true).Length == 0);
            if (!filterContext.Principal.Identity.IsAuthenticated)  //check auth user
                filterContext.Result = new HttpUnauthorizedResult();
        }

        public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext) //this can run at various stages
        {
            // redirect the user to some form of log in
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //base.OnActionExecuting(filterContext);
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            //base.OnActionExecuted(filterContext);
        }

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            //base.OnResultExecuting(filterContext);
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            //base.OnResultExecuted(filterContext);
        }

    }
}