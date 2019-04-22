using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using DataAccess.Models;
using PagedList;

namespace DataAccess.Dao
{
    public class ArticleDao
    {
        DataContext db = null;
        public static string USER_SESSION = "USER_SESSION";
        public ArticleDao()
        {
            db = new DataContext();
        }
        public IEnumerable<Article> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<Article> model = db.Articles;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Title.Contains(searchString) || x.Title.Contains(searchString));
            }

            return model.OrderByDescending(x => x.PostDate).ToPagedList(page, pageSize);
        }
        /// <summary>
        /// List all content for client
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public IEnumerable<Article> ListAllPaging(int page, int pageSize)
        {
            IQueryable<Article> model = db.Articles;
            return model.OrderByDescending(x => x.PostDate).ToPagedList(page, pageSize);
        }
        public IEnumerable<Article> ListAllByTag(string tag, int page, int pageSize)
        {
            var model = (from a in db.Articles
                         join b in db.ArticleTags
                         on a.ArticleId equals b.ContentID
                         where b.TagID == tag
                         select new
                         {
                             Title = a.Title,
                             Photo = a.Photo,
                             Sumary = a.Sumary,
                             PostDate = a.PostDate,
                             Author = a.Author,
                             ArticleId = a.ArticleId

                         }).AsEnumerable().Select(x => new Article()
                         {
                             Title = x.Title,
                             Photo = x.Photo,
                             Sumary = x.Sumary,
                             PostDate = x.PostDate,
                             Author = x.Author,
                             ArticleId = x.ArticleId
                         });
            return model.OrderByDescending(x => x.PostDate).ToPagedList(page, pageSize);
        }

        public Article GetByID(long id)
        {
            return db.Articles.Find(id);
        }

        //public Tag GetTag(string id)
        //{
        //    return db.Tags.Find(id);
        //}
        //public long Create(Content content)
        //{
        //    //Xử lý alias
        //    if (string.IsNullOrEmpty(content.MetaTitle))
        //    {
        //        content.MetaTitle = StringHelper.ToUnsignString(content.Name);
        //    }
        //    content.CreatedDate = DateTime.Now;
        //    content.ViewCount = 0;
        //    db.Contents.Add(content);
        //    db.SaveChanges();

        //    //Xử lý tag
        //    if (!string.IsNullOrEmpty(content.Tags))
        //    {
        //        string[] tags = content.Tags.Split(',');
        //        foreach (var tag in tags)
        //        {
        //            var tagId = StringHelper.ToUnsignString(tag);
        //            var existedTag = this.CheckTag(tagId);

        //            //insert to to tag table
        //            if (!existedTag)
        //            {
        //                this.InsertTag(tagId, tag);
        //            }

        //            //insert to content tag
        //            this.InsertContentTag(content.ID, tagId);

        //        }
        //    }

        //    return content.ID;
        //}
        //public long Edit(Content content)
        //{
        //    //Xử lý alias
        //    if (string.IsNullOrEmpty(content.MetaTitle))
        //    {
        //        content.MetaTitle = StringHelper.ToUnsignString(content.Name);
        //    }
        //    content.CreatedDate = DateTime.Now;
        //    db.SaveChanges();

        //    //Xử lý tag
        //    if (!string.IsNullOrEmpty(content.Tags))
        //    {
        //        this.RemoveAllContentTag(content.ID);
        //        string[] tags = content.Tags.Split(',');
        //        foreach (var tag in tags)
        //        {
        //            var tagId = StringHelper.ToUnsignString(tag);
        //            var existedTag = this.CheckTag(tagId);

        //            //insert to to tag table
        //            if (!existedTag)
        //            {
        //                this.InsertTag(tagId, tag);
        //            }

        //            //insert to content tag
        //            this.InsertContentTag(content.ID, tagId);

        //        }
        //    }

        //    return content.ID;
        //}
        //public void RemoveAllContentTag(long contentId)
        //{
        //    db.ContentTags.RemoveRange(db.ContentTags.Where(x => x.ContentID == contentId));
        //    db.SaveChanges();
        //}
        //public void InsertTag(string id, string name)
        //{
        //    var tag = new Tag();
        //    tag.ID = id;
        //    tag.Name = name;
        //    db.Tags.Add(tag);
        //    db.SaveChanges();
        //}

        //public void InsertContentTag(long contentId, string tagId)
        //{
        //    var contentTag = new ContentTag();
        //    contentTag.ContentID = contentId;
        //    contentTag.TagID = tagId;
        //    db.ContentTags.Add(contentTag);
        //    db.SaveChanges();
        //}
        //public bool CheckTag(string id)
        //{
        //    return db.Tags.Count(x => x.ID == id) > 0;
        //}

        //public List<Tag> ListTag(long contentId)
        //{
        //    var model = (from a in db.Tags
        //                 join b in db.ContentTags
        //                 on a.ID equals b.TagID
        //                 where b.ContentID == contentId
        //                 select new
        //                 {
        //                     ID = b.TagID,
        //                     Name = a.Name
        //                 }).AsEnumerable().Select(x => new Tag()
        //                 {
        //                     ID = x.ID,
        //                     Name = x.Name
        //                 });
        //    return model.ToList();
        //}
    }
}
