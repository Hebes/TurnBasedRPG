using System;
using LogUtils;

namespace ServerLogTest
{
    class Root
    {
        public void Init()
        {
            this.Log(" InitRootLog.");
            Mgr mgr = new Mgr();
            mgr.Init();
        }
    }
    class Mgr
    {
        public void Init()
        {
            this.Warn("Init Mgr Warn.");
            Item item = new Item();
            item.Init();
        }
    }
    class Item
    {
        public void Init()
        {
            this.Error("Init Item Error.");
            this.Trace("Trace This FunC.");
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            PELog.InitSettings();
            PELog.Log($"{"sdsdsd"}Hello word");
            PELog.Log(LogCoLor.DarkRed, $"{LogCoLor.DarkRed.ToString()}Hello word");
            PELog.Log(LogCoLor.Green, $"{LogCoLor.Green.ToString()}Hello word");
            PELog.Log(LogCoLor.Blue, $"{LogCoLor.Blue.ToString()}Hello word");
            PELog.Log(LogCoLor.Cyan, $"{LogCoLor.Cyan.ToString()}Hello word");
            PELog.Log(LogCoLor.Magenta, $"{LogCoLor.Magenta.ToString()}Hello word");
            PELog.Log(LogCoLor.DarkYellow, $"{LogCoLor.DarkYellow.ToString()}Hello word");

            Root rt = new Root();
            rt.Init();

            Console.ReadKey();
        }
    }
}
