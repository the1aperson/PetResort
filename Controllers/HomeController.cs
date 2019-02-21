using PetResort.UI.Models;
using System.Net;
using System.Net.Mail;
using System.Web.Mvc;
using System.Web;

namespace IdentitySample.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        
        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Contact(ContactViewModel contact)
        {
            string body = string.Format("Name {0}<br/>Email: {1}" + "<br/>Subject: {2}<br/>Message:<br/>{3}",
                contact.Name,
                contact.Email,
                contact.Subject,
                contact.Message);

            MailMessage msg = new MailMessage(
                "no-reply@andrewkpearson.com",
                "the1aperson@gmail.com",
                contact.Subject,
                body);

            msg.IsBodyHtml = true;
            msg.CC.Add("the1aperson@gmail.com");

            SmtpClient client = new SmtpClient("mail.andrewkpearson.com");
            client.Credentials = new NetworkCredential("no-reply@andrewkpearson.com", "Scooter90!");

            using (client)
            {
                try
                {
                    client.Send(msg);
                }
                catch
                {
                    ViewBag.ErrorMessage = "There was an error sending your email. Please try again.";
                    return View();
                }
            }
            return View("ContactConfirmation", contact);
        }
        public ActionResult ContactConfirmation()
        {
            ViewBag.Message = "Your contact confirmation page.";

            return View();
        }

        public ActionResult Gallery()
        {
            ViewBag.Message = "Pet Gallery";

            return View();
        }
    }
}
