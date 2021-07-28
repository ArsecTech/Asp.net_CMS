using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class PageRepository : IPageRepository
    {
          private  MyCmsContext db;

        public PageRepository(MyCmsContext context)
        {
            this.db = context;
        }

        public IEnumerable<Page> GetAllPage()
        {
            return db.pages;
        }

        public Page GetPageById(int pageId)
        {
            return db.pages.Find(pageId);
        }

        public bool InsertPage(Page page)
        {
            try
            {
                db.pages.Add(page);
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }



        public bool UpadatePage(Page page)
        {
            try
            {
                db.Entry(page).State = EntityState.Modified;
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }

        public bool DeletePage(Page page)
        {
            try
            {
                db.Entry(page).State = EntityState.Deleted;
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }

        }

        public bool DeletePage(int pageId)
        {
            try
            {
                var page = GetPageById(pageId);
                DeletePage(page);
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        } 

        public void Save()
        {
            db.SaveChanges();
        }

        public void Dispose()
        {
            db.Dispose();
        }

        public IEnumerable<Page> TopNews(int take = 4)
        {
            return db.pages.OrderByDescending(p => p.visit).Take(take);
        }

        public IEnumerable<Page> PagesInSlider()
        {
            return db.pages.Where(p => p.ShowInSlider == true); 
        }

        public IEnumerable<Page> LastNews(int take = 4)
        {
            return db.pages.OrderByDescending(p => p.CreatDate).Take(take);
        }

        public IEnumerable<Page> ShowPageByGroupId(int groupId)
        {
            return db.pages.Where(p => p.GroupID == groupId);
        }

        public IEnumerable<Page> SearchPage(string search)
        {
            return db.pages.Where(p => p.Title.Contains(search) || p.ShortDescription.Contains(search) || p.Tags.Contains(search) || p.Text.Contains(search)).Distinct();
        }
    }
}
