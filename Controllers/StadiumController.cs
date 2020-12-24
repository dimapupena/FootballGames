using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using FootballGameManager;
using FootballGameManager.Data;
using FootballGameManager.Services;

namespace FootballGameManager.Controllers
{
    public class StadiumController : Controller
    {
        // GET: Stadium
        public async Task<ActionResult> Index()
        {
            var stadiums = await StadiumService.sharedInstance().GetStadiums();
            return View(stadiums);
        }

        // GET: Stadium/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stadium stadium = await StadiumService.sharedInstance().GetStadium((int)id);
            if (stadium == null)
            {
                return HttpNotFound();
            }
            return View(stadium);
        }

        // GET: Stadium/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Stadium/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Places,PayForPlace")] Stadium stadium)
        {
            if (ModelState.IsValid)
            {
                await StadiumService.sharedInstance().AddStadium(stadium);
                return RedirectToAction("Index");
            }

            return View(stadium);
        }

        // GET: Stadium/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stadium stadium = await StadiumService.sharedInstance().GetStadium((int)id);
            if (stadium == null)
            {
                return HttpNotFound();
            }
            return View(stadium);
        }

        // POST: Stadium/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Places,PayForPlace")] Stadium stadium)
        {
            if (ModelState.IsValid)
            {
                await StadiumService.sharedInstance().UpdateStadiums(stadium);
                return RedirectToAction("Index");
            }
            return View(stadium);
        }

        // GET: Stadium/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stadium stadium = await StadiumService.sharedInstance().GetStadium((int)id);
            if (stadium == null)
            {
                return HttpNotFound();
            }
            return View(stadium);
        }

        // POST: Stadium/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            await StadiumService.sharedInstance().DeleteStadium(id);
            return RedirectToAction("Index");
        }
    }
}
