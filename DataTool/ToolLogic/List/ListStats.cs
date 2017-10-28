using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using DataTool.Flag;
using OWLib;
using STULib.Types;
using static DataTool.Helper.IO;
using static DataTool.Program;
using static DataTool.Helper.Logger;
using static DataTool.Helper.STUHelper;
using DataTool.DataModels;
using DataTool.Helper;
using STULib.Types.Statistics;

namespace DataTool.ToolLogic.List {
    [Tool("list-stats", Description = "List statistics", TrackTypes = new ushort[] {0x62}, CustomFlags = typeof(ListFlags))]
    public class ListStats : JSONTool, ITool {
        public void IntegrateView(object sender) {
            throw new NotImplementedException();
        }

        public void Parse(ICLIFlags toolFlags) {
            var i = 0;
            var iD = new IndentHelper();

            foreach (var key in TrackedFiles[0x62]) {
                Log($"[{i}]:");
                i++;

                var statistic = GetInstance<STUStatistic>(key);

                var name1 = GetString(statistic.Name1);
                var name2 = GetString(statistic.Name2);

                Log($"{iD+1}  Name1: {name1}");
                Log($"{iD+1}  Name2: {name2}");

                if (statistic.Heroes != null) {
                    var heroesArr = new List<string>();
                    foreach (var guid in statistic.Heroes) {
                        var hero = GetInstance<STUHero>(guid);
                        heroesArr.Add(GetString(hero.Name) ?? "Unknown");
                    }
                    Log($"{iD+1} Heroes: {String.Join(", ", heroesArr)}");
                }
            }
        }
    }
}