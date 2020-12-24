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
    public class PlayersController : Controller
    {

        // GET: Players
        public async Task<ActionResult> Index()
        {
            var players = await PlayerService.sharedInstance().GetPlayers();
            return View(players);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index(string playerName)
        {
            List <Player> players = await PlayerService.sharedInstance().SearchPlayer(playerName);
            return View(players);
        }

        // GET: Players/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Player player = await PlayerService.sharedInstance().GetPlayer((int)id); ;
            if (player == null)
            {
                return HttpNotFound();
            }
            return View(player);
        }

        // GET: Players/Create
        public async Task<ActionResult> Create()
        {
            var teams = await TeamService.sharedInstance().getTeams();
            ViewBag.TeamId = new SelectList(teams, "Id", "TeamName");
            return View();
        }

        // POST: Players/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,PlayerName,Birthday,StatusPlayer,StatusHealth,Salary,TeamId")] Player player)
        {
            if (ModelState.IsValid)
            {
                await PlayerService.sharedInstance().AddPLayer(player);
                return RedirectToAction("Index");
            }
            var teams = await TeamService.sharedInstance().getTeams();
            ViewBag.TeamId = new SelectList(teams, "Id", "TeamName", player.TeamId);
            return View(player);
        }

        // GET: Players/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Player player = await PlayerService.sharedInstance().GetPlayer((int)id);
            if (player == null)
            {
                return HttpNotFound();
            }
            var teams = await TeamService.sharedInstance().getTeams();
            ViewBag.TeamId = new SelectList(teams, "Id", "TeamName", player.TeamId);
            return View(player);
        }

        // POST: Players/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,PlayerName,Birthday,StatusPlayer,StatusHealth,Salary,TeamId")] Player player)
        {
            if (ModelState.IsValid)
            {
                await PlayerService.sharedInstance().UpdatePlayer(player);
                return RedirectToAction("Index");
            }
            var teams = await TeamService.sharedInstance().getTeams();
            ViewBag.TeamId = new SelectList(teams, "Id", "TeamName", player.TeamId);
            return View(player);
        }

        // GET: Players/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Player player = await PlayerService.sharedInstance().GetPlayer((int)id);
            if (player == null)
            {
                return HttpNotFound();
            }
            return View(player);
        }

        // POST: Players/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            await PlayerService.sharedInstance().DeletePlayer(id);
            return RedirectToAction("Index");
        }
    }
}
