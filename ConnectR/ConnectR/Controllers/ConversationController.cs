using System.Web.Mvc;
using ConnectR.Models;
using System.Data.Entity;
using System.Linq;

namespace ConnectR.Controllers
{
    public class ConversationController : Controller
    {
        private ConnectRContext db = new ConnectRContext();

        public ActionResult Index()
        {
            var Conversation = db.Conversations;
            return View(Conversation.ToList());
        }

        public ActionResult Create()
        {
            int userId = 1;
            Message newMsg = new Message();
            newMsg.user_id = userId;
            return View();
        }
    }
}