using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class PageGroupRepository : IPageGroupRepository
    {

        private  MyCmsContext db;

        public PageGroupRepository(MyCmsContext context)
        {
            this.db = context;
        }


        public IEnumerable<PageGroup> GetAllGroups()
        {
            return db.pageGroups;
        }

        public PageGroup GetGroupById(int groupId)
        {
            return db.pageGroups.Find(groupId);
        }

        public bool InsertGroup(PageGroup pageGroup)
        {
            try
            {
                db.pageGroups.Add(pageGroup);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdateGroup(PageGroup pageGroup)
        {
            try
            {
                db.Entry(pageGroup).State = EntityState.Modified;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteGroup(PageGroup pageGroup)
        {
            try
            {
                db.Entry(pageGroup).State = EntityState.Deleted;


                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteGroup(int groupId)
        {
            try
            {


                var group = GetGroupById(groupId);
                DeleteGroup(group);
                return true;
            }
            catch (Exception)
            {
                return false;
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

        public IEnumerable<ShowGroupViewModel> GetGroupsForView()
        {
            return db.pageGroups.Select(g => new ShowGroupViewModel()
            {
                GroupID = g.GroupID,
                GroupTitle = g.GroupTitle,
                PageCount = g.Pages.Count
            });
        }
    }
}
