using CheckersGame.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CheckersGame.Controllers
{
    public class HomeController : Controller
    {
        public static Player blackPlayer = new Player();
        public static Player redPlayer = new Player();
        public static Board board = new Board();
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        public ActionResult Play()
        {
            board.start();
            blackPlayer.addPlayer("black");
            redPlayer.addPlayer("red");
            ViewBag.Player = "red";
            return View();
        } 

        [HttpPost]
        public ActionResult ShowOption(string player, string soldierName)
        {
            List<OptionPoint> options = new List<OptionPoint>() { };
            if (player == "black")
                options = GetBlackOptions(soldierName);
            else
                if (player == "red")
                    options = GetRedOptions(soldierName);

            return Json(new { options = options },JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult ChooseOption(int x, int y, string soldierName)
        {
            string soldierFailedName = "";
            try
            {
                if (soldierName.StartsWith("b"))
                {
                    var soldier = blackPlayer.Soldiers.FirstOrDefault(s => s.Name == soldierName);
                    if(soldier.X - x > 1 || soldier.X - x < -1)
                    {
                        if (soldier.X - x > 1)
                            soldierFailedName = SoldierFailed(soldier.X - 1, soldier.Y + 1, "red");
                        else
                            soldierFailedName = SoldierFailed(soldier.X + 1, soldier.Y + 1, "red");
                    }
                    board.SetMatrix(soldier.X, soldier.Y,false) ;
                    board.SetMatrix(x, y, true);
                    blackPlayer.setPosition(soldierName, x, y);
                    ViewBag.Player = "red";
                }
                else
                    if (soldierName.StartsWith("r"))
                    {
                        var soldier = redPlayer.Soldiers.FirstOrDefault(s => s.Name == soldierName);
                        if (soldier.X - x > 1 || soldier.X - x < -1)
                        {
                            if (soldier.X - x > 1)
                                soldierFailedName = SoldierFailed(soldier.X - 1, soldier.Y - 1, "black");
                            else
                                soldierFailedName = SoldierFailed(soldier.X + 1, soldier.Y - 1, "black");
                        }
                        board.SetMatrix(soldier.X, soldier.Y, false);
                        board.SetMatrix(x, y, true);
                        redPlayer.setPosition(soldierName, x, y);
                        ViewBag.Player = "black";
                    }
            }
            catch (Exception ex)
            {
                return Json(new { succeeded = "failed" , soldierFailedName = soldierFailedName }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { succeeded = "succeeded" , soldierFailedName = soldierFailedName }, JsonRequestBehavior.AllowGet);
        }

        private List<OptionPoint> GetBlackOptions(string soldierName)
        {
            List<OptionPoint> optionPoints = new List<OptionPoint>();
            var currentSoldier = blackPlayer.Soldiers.FirstOrDefault(s => s.Name == soldierName);
            int x = currentSoldier.X + 1;
            int y = currentSoldier.Y + 1;

            if (x < 8 && y < 8)
            {
                if (board.Matrix[x, y] == false)
                    optionPoints.Add(new OptionPoint() { X = x, Y = y });
                else
                {
                    var reds = redPlayer.Soldiers.FirstOrDefault(s => s.X == x && s.Y == y);
                    if (reds != null && x + 1 < 8 && y + 1 < 8 && board.Matrix[x + 1, y + 1] == false)
                        optionPoints.Add(new OptionPoint() { X = x + 1, Y = y + 1 });
                }
            }

            //add red if

            x = currentSoldier.X - 1;
            if (x >= 0 && y < 8)
            {
                if (board.Matrix[x, y] == false)
                    optionPoints.Add(new OptionPoint() { X = x, Y = y });
                else
                {
                    var reds = redPlayer.Soldiers.FirstOrDefault(s => s.X == x && s.Y == y);
                    if (reds != null && x - 1 >= 0 && y + 1 < 8 && board.Matrix[x - 1, y + 1] == false)
                        optionPoints.Add(new OptionPoint() { X = x - 1, Y = y + 1 });
                }
            }

            return optionPoints;
        }

        private List<OptionPoint> GetRedOptions(string soldierName)
        {
            List<OptionPoint> optionPoints = new List<OptionPoint>();
            var currentSoldier = redPlayer.Soldiers.FirstOrDefault(s => s.Name == soldierName);
            int x = currentSoldier.X - 1;
            int y = currentSoldier.Y - 1;

            if (x >= 0 && y >= 0)
            {
                if (board.Matrix[x, y] == false)
                    optionPoints.Add(new OptionPoint() { X = x, Y = y });
                else
                {
                    var blacks = blackPlayer.Soldiers.FirstOrDefault(s => s.X == x && s.Y == y);
                    if (blacks != null && x - 1 >= 0 && y - 1 >= 0 && board.Matrix[x - 1, y - 1] == false)
                        optionPoints.Add(new OptionPoint() { X = x - 1, Y = y - 1 });
                }
            }

            x = currentSoldier.X + 1;
            if (x < 8 && y >= 0)
            {
                if (board.Matrix[x, y] == false)
                    optionPoints.Add(new OptionPoint() { X = x, Y = y });
                else
                {
                    var blacks = blackPlayer.Soldiers.FirstOrDefault(s => s.X == x && s.Y == y);
                    if (blacks != null && x + 1 < 8 && y - 1 >= 0 && board.Matrix[x + 1, y - 1] == false)
                        optionPoints.Add(new OptionPoint() { X = x + 1, Y = y - 1 });
                }
                    
            }

            return optionPoints;
        }

        private string SoldierFailed(int x, int y ,string color)
        {
            string soldierFailedName = "";
            board.SetMatrix(x, y, false);
            if (color == "red")
            {
                soldierFailedName = redPlayer.Soldiers.FirstOrDefault(s => s.X == x && s.Y == y).Name;
                redPlayer.Soldiers.FirstOrDefault(s => s.X == x && s.Y == y).X = -1;
                redPlayer.Soldiers.FirstOrDefault(s => s.X == -1 && s.Y == y).Y = -1;
            }
            else
                if (color == "black")
                {
                    soldierFailedName = blackPlayer.Soldiers.FirstOrDefault(s => s.X == x && s.Y == y).Name;
                    blackPlayer.Soldiers.FirstOrDefault(s => s.X == x && s.Y == y).X = -1;
                    blackPlayer.Soldiers.FirstOrDefault(s => s.X == -1 && s.Y == y).Y = -1;
                }
            return soldierFailedName;
        }
        
    }
}
