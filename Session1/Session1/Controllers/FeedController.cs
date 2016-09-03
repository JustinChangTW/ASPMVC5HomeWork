using Session1.CustomResults;
using Session1.Models.DbModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Web;
using System.Web.Mvc;

namespace Session1.Controllers
{
    public class FeedsController : Controller
    {
        //private NorthwindEntities db = new NorthwindEntities();
        private DbModel _db;

        public FeedsController()
        {
            _db = new DbModel();
        }

        // GET: Orders
        public ActionResult Index()
        {
            var feed = this.GetFeedData();
            return new RssResult(feed);
        }
        private SyndicationFeed GetFeedData()
        {
            var feed = new SyndicationFeed(
                "北風測試資料",
                "訂單RSS！",
                new Uri(Url.Action("Rss", "Feed", null, "http")));

            var items = new List<SyndicationItem>();

            var orders = _db.AccountBook;
                //.Where(x => x.OrderDate <= DateTime.Now)
                //.OrderByDescending(x => x.OrderDate);

            foreach (var order in orders)
            {
                var item = new SyndicationItem(
                    order.Dateee.ToString(),
                    order.Amounttt.ToString(),
                    new Uri(Url.Action("Detail", "Order", new { id = order.Dateee }, "http")),
                    "ID",
                    DateTime.Now);

                items.Add(item);
            }

            feed.Items = items;
            return feed;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}