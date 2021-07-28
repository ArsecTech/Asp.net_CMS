using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataLayer;

namespace MyCms.Controllers
{
    public class NewsController : Controller
    {
        MyCmsContext db = new MyCmsContext();
        private IPageGroupRepository pageGroupRepository;
        private IPageRepository pageRepository;
        private IPageCommentRepository pageCommentRepository;


        public NewsController()
        {
            pageGroupRepository = new PageGroupRepository(db);
            pageRepository = new PageRepository(db);
            pageCommentRepository = new PageCommentRepository(db);
        }

        // GET: News
        public ActionResult ShowGroups()
        {
            return PartialView(pageGroupRepository.GetGroupsForView());
        }

        public ActionResult ShowGroupsInMenu()
        {
            return PartialView(pageGroupRepository.GetAllGroups());
        }

        public ActionResult TopNews()
        {
            return PartialView(pageRepository.TopNews());
        }

        public ActionResult LastNews()
        {
            return PartialView(pageRepository.LastNews());
        }

        [Route("Archive")]
        public ActionResult ArchiveNews()
        {
            return View(pageRepository.GetAllPage());
        }
        [Route("Group/{id}/{title}")]
        public ActionResult ShowNewsByGroupId(int id, string title)
        {
            ViewBag.name = title;
            return View(pageRepository.ShowPageByGroupId(id));
        }


        [Route("News/{id}")]
        public ActionResult ShowNews(int id)
        {
            var news = pageRepository.GetPageById(id);

            if (news ==null)
            {
                return HttpNotFound();
            }

            news.visit += 1;

            pageRepository.UpadatePage(news);
            pageRepository.Save();

            return View(news);
        }

        public ActionResult AddComment(int id,string name,string email,string comment)
        {
            PageComment Addcomment = new PageComment()
            {
                CreatDate = DateTime.Now,
                PageID = id,
                Comment = comment,
                Email = email,
                Name = name
            };
            pageCommentRepository.AddComment(Addcomment);

            return PartialView("ShowComments",pageCommentRepository.GetCommentByNewsId(id));
        }

        public ActionResult ShowComments(int id)
        {
            return PartialView(pageCommentRepository.GetCommentByNewsId(id));
        }
    }
}