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
using gamesDataLayer.Services;

namespace FootballGameManager.Controllers
{
    public class GamesController : Controller
    {

        // GET: Games
        public async Task<ActionResult> Index()
        {
            var games = await GameService.sharedInstance().getGames();
            ViewBag.teams = await TeamService.sharedInstance().getTeams();  
            ViewBag.results = new List<GameResult> { GameResult.Victory, GameResult.Draw, GameResult.Defeat, GameResult.NC };
            return View(games);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index(GameResult? gameResult,string OppTeamName, string gameTime, int dataSort = -1)
        {
            List<Game> games;
            if (gameResult != null && dataSort != null)
            {
                games = await GameService.sharedInstance().SortGames((int)dataSort, gameResult);
            } else
            {
                    games = await GameService.sharedInstance().getGames();
            }
            games = await GameService.sharedInstance().GameAgainstTeam(OppTeamName, games);
            games = await GameService.sharedInstance().GetGameByTime(gameTime, games);

            ViewBag.teams = await TeamService.sharedInstance().getTeams();
            ViewBag.results = new List<GameResult> { GameResult.Victory, GameResult.Draw, GameResult.Defeat, GameResult.NC };
            return View(games);
        }
        // GET: Games/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Game game = await GameService.sharedInstance().getGame((int)id);
            if (game == null)
            {
                return HttpNotFound();
            }
            return View(game);
        }

        // GET: Games/Create
        public async Task<ActionResult> Create()
        {
            var stadiums = await StadiumService.sharedInstance().GetStadiums();
            var teams = await TeamService.sharedInstance().getTeams();
            ViewBag.StadiumId = new SelectList(stadiums, "Id", "Id");
            ViewBag.FirstTeamId = new SelectList(teams, "Id", "TeamName");
            ViewBag.SecondTeamId = new SelectList(teams, "Id", "TeamName");
            return View();
        }

        // POST: Games/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,StadiumId,FirstTeamId,SecondTeamId,Result,Audience,GameDate")] Game game)
        {
            if (ModelState.IsValid)
            {
                await GameService.sharedInstance().addGame(game);
                return RedirectToAction("Index");
            }
            var stadiums = await StadiumService.sharedInstance().GetStadiums();
            ViewBag.StadiumId = new SelectList(stadiums, "Id", "Id", game.StadiumId);
            return View(game);
        }

        // GET: Games/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Game game = await GameService.sharedInstance().getGame((int)id);
            if (game == null)
            {
                return HttpNotFound();
            }
            var stadiums = await StadiumService.sharedInstance().GetStadiums();
            var teams = await TeamService.sharedInstance().getTeams();
            ViewBag.StadiumId = new SelectList(stadiums, "Id", "Id");
            ViewBag.FirstTeamId = new SelectList(teams, "Id", "TeamName");
            ViewBag.SecondTeamId = new SelectList(teams, "Id", "TeamName");
            return View(game);
        }

        // POST: Games/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,StadiumId,FirstTeamId,SecondTeamId,Result,Audience,GameDate")] Game game)
        {
            if (ModelState.IsValid)
            {
                await GameService.sharedInstance().updateGames(game);
                return RedirectToAction("Index");
            }
            var stadiums = await StadiumService.sharedInstance().GetStadiums();
            ViewBag.StadiumId = new SelectList(stadiums, "Id", "Id", game.StadiumId);
            return View(game);
        }

        // GET: Games/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Game game = await GameService.sharedInstance().getGame((int) id);
            if (game == null)
            {
                return HttpNotFound();
            }
            return View(game);
        }

        // POST: Games/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            await GameService.sharedInstance().deleteGame(id);
            return RedirectToAction("Index");
        }
    }
}
