﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grabacr07.KanColleWrapper.Models;

namespace Grabacr07.KanColleViewer.Models.QuestTracker.Tracker
{
    /// <summary>
    /// 적 동방함대 격멸
    /// </summary>
    internal class Bw6 : ITracker
    {
        private readonly int max_count = 12;
        private int count;

        public event EventHandler ProcessChanged;

        int ITracker.Id => 229;
        public bool IsTracking { get; set; }

        private System.EventArgs emptyEventArgs = new System.EventArgs();

        public void RegisterEvent(TrackManager manager)
        {
            List<string> BossNameList = new List<string>
            {
                "東方派遣艦隊", //4-1
                "東方主力艦隊",
                // 4-3: 東方主力艦隊
                "敵東方中枢艦隊",
                "リランカ島港湾守備隊"
            };

            manager.BattleResultEvent += (sender, args) =>
            {
                if (!IsTracking) return;

                if (args.MapAreaId != 4) return; // 4해역
                if (!BossNameList.Contains(args.EnemyName)) return;
                if (!"SAB".Contains(args.Rank)) return;

                count += count >= max_count ? 0 : 1;
                ProcessChanged?.Invoke(this, emptyEventArgs);
            };
        }

        public void ResetQuest()
        {
            count = 0;
            ProcessChanged?.Invoke(this, emptyEventArgs);
        }

        public double GetProgress()
        {
            return (double)count / max_count * 100;
        }

        public string GetProgressText()
        {
            return count >= max_count ? "완료" : $"4-1 ~ 4-5 보스전 승리 {count} / {max_count}";
        }

        public string SerializeData()
        {
            return $"{count}";
        }

        public void DeserializeData(string data)
        {
            try
            {
                count = int.Parse(data);
            }
            catch
            {
                count = 0;
            }
        }
    }
}
