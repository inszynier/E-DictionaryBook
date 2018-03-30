using System.Web.Mvc;

namespace E_DictionaryBook.Areas.Email
{
    public class EmailAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Email";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Email_default",
                "Email/{controller}/{action}/{id}",

                new { Controller="Send" , action = "SendEmail", id = UrlParameter.Optional }
            );
        }
    }
}