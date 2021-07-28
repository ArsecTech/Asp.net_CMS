using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataLayer;
using System.Web.Security;

namespace MyCms.Controllers
{
    public class AcountController : Controller
    {
        private ILoginRepository loginRepository;
        MyCmsContext db = new MyCmsContext();

        public AcountController()
        {
            loginRepository = new LoginRepository(db);
        }


        // GET: Acount
        public ActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Login(LoginViewModel login,string ReturnUrl="/")
        {
            if (ModelState.IsValid)
            {

                if (loginRepository.IsExistUser(login.UserName, login.Password))
                {
                    FormsAuthentication.SetAuthCookie(login.UserName,login.RememberMe);
                    return Redirect("/");
                }
                else
                {
                    ModelState.AddModelError("UserName","کاربری یافت نشد");
                }

            }
            return View(login);
        }

        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();
            return Redirect("/");
        }
    }
}