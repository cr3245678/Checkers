using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CheckersGame.Classes
{
    public class Board
    {
        public bool[,] Matrix  = new bool[8, 8];
        public Board()
        {
            
            
        }

        public void start()
        {
            AddBlackPlayer();
            AddRedPlayer();
        }


        public void AddBlackPlayer()
        {
            for (int i = 1; i < 8; i += 2)
            {
                Matrix[i, 0] = true;
            }
            for (int i = 0; i < 7; i += 2)
            {
                Matrix[i, 1] = true;
            }
            for (int i = 1; i < 8; i += 2)
            {
                Matrix[i, 2] = true;
            }
        }

        public void AddRedPlayer()
        {
            for (int i = 0; i < 7; i += 2)
            {
                Matrix[i, 5] = true;
            }
            for (int i = 1; i < 8; i += 2)
            {
                Matrix[i, 6] = true;
            }
            for (int i = 0; i < 7; i += 2)
            {
                Matrix[i, 7] = true;
            }
        }

        public void SetMatrix(int x,int y,bool setTo)
        {
            this.Matrix[x, y] = setTo;
        }
    }

    public class Player
    {
        public List<Soldier> Soldiers = new List<Soldier>();

        public Player()
        {
           
        }

        public void addPlayer(string color)
        {
            if (color == "black")
            {
                Soldiers.Add(new Soldier() { X = 1, Y = 0, Name = "bs1" });
                Soldiers.Add(new Soldier() { X = 3, Y = 0, Name = "bs2" });
                Soldiers.Add(new Soldier() { X = 5, Y = 0, Name = "bs3" });
                Soldiers.Add(new Soldier() { X = 7, Y = 0, Name = "bs4" });
                Soldiers.Add(new Soldier() { X = 0, Y = 1, Name = "bs5" });
                Soldiers.Add(new Soldier() { X = 2, Y = 1, Name = "bs6" });
                Soldiers.Add(new Soldier() { X = 4, Y = 1, Name = "bs7" });
                Soldiers.Add(new Soldier() { X = 6, Y = 1, Name = "bs8" });
                Soldiers.Add(new Soldier() { X = 1, Y = 2, Name = "bs9" });
                Soldiers.Add(new Soldier() { X = 3, Y = 2, Name = "bs10" });
                Soldiers.Add(new Soldier() { X = 5, Y = 2, Name = "bs11" });
                Soldiers.Add(new Soldier() { X = 7, Y = 2, Name = "bs12" });
            }
            else
            {
                Soldiers.Add(new Soldier() { X = 0, Y = 5, Name = "rs1" });
                Soldiers.Add(new Soldier() { X = 2, Y = 5, Name = "rs2" });
                Soldiers.Add(new Soldier() { X = 4, Y = 5, Name = "rs3" });
                Soldiers.Add(new Soldier() { X = 6, Y = 5, Name = "rs4" });
                Soldiers.Add(new Soldier() { X = 1, Y = 6, Name = "rs5" });
                Soldiers.Add(new Soldier() { X = 3, Y = 6, Name = "rs6" });
                Soldiers.Add(new Soldier() { X = 5, Y = 6, Name = "rs7" });
                Soldiers.Add(new Soldier() { X = 7, Y = 6, Name = "rs8" });
                Soldiers.Add(new Soldier() { X = 0, Y = 7, Name = "rs9" });
                Soldiers.Add(new Soldier() { X = 2, Y = 7, Name = "rs10" });
                Soldiers.Add(new Soldier() { X = 4, Y = 7, Name = "rs11" });
                Soldiers.Add(new Soldier() { X = 6, Y = 7, Name = "rs12" });
            }
        }

        public void setPosition(string name, int x, int y)
        {
            this.Soldiers.FirstOrDefault(s => s.Name == name).X = x;
            this.Soldiers.FirstOrDefault(s => s.Name == name).Y = y;
        }
    }

    public class Soldier
    {
        public int X { get; set; }
        public int Y { get; set; }
        public string Name { get; set; }

    }

    public class OptionPoint
    {
        public int X { get; set; }
        public int Y { get; set; }
    }
}