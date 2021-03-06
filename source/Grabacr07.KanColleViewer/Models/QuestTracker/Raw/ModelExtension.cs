﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Grabacr07.KanColleWrapper.Models.Raw;
using Grabacr07.KanColleViewer.Models.QuestTracker.Model;

namespace Grabacr07.KanColleViewer.Models.QuestTracker.Raw
{
    internal static class ModelExtension
    {
        private static readonly FleetDamages defaultValue = new FleetDamages();

        public static FleetDamages GetEnemyDamages(this kcsapi_data_support_info support)
        {
            if (support == null) return defaultValue;
            if (support.api_support_airatack != null && support.api_support_airatack.api_stage3 != null && support.api_support_airatack.api_stage3.api_edam != null)
                return support.api_support_airatack.api_stage3.api_edam.GetDamages();

            if (support.api_support_hourai != null && support.api_support_hourai.api_damage != null)
                return support.api_support_hourai.api_damage.GetDamages();

            return defaultValue;
        }

        public static FleetDamages GetEnemyDamages(this kcsapi_data_hougeki hougeki)
        {
            return hougeki != null
                ? hougeki.api_damage != null
                    ? hougeki.api_damage.GetEnemyDamages(hougeki.api_df_list)
                    : defaultValue
                : defaultValue;
        }

        public static FleetDamages GetEnemyDamages(this kcsapi_data_midnight_hougeki hougeki)
        {
            return hougeki != null
                ? hougeki.api_damage != null
                    ? hougeki.api_damage.GetEnemyDamages(hougeki.api_df_list)
                    : defaultValue
                : defaultValue;
        }

        public static FleetDamages GetEnemyDamages(this kcsapi_data_kouku kouku)
        {
            return kouku != null
                ? kouku.api_stage3 != null
                    ? kouku.api_stage3.api_edam != null
                        ? kouku.api_stage3.api_edam.GetDamages()
                        : defaultValue
                    : defaultValue
                : defaultValue;
        }

        public static FleetDamages GetEnemyDamages(this kcsapi_data_raigeki raigeki)
        {
            return raigeki != null
                ? raigeki.api_edam != null
                    ? raigeki.api_edam.GetDamages()
                    : defaultValue
                : defaultValue;
        }

        public static IEnumerable<T> GetFriendData<T>(this IEnumerable<T> source, int origin = 1)
        {
            return source.Skip(origin).Take(6);
        }
        public static IEnumerable<T> GetEnemyData<T>(this IEnumerable<T> source, int origin = 1)
        {
            return source.Skip(origin + 6).Take(6);
        }

        public static FleetDamages GetDamages(this decimal[] damages)
        {
            return damages
                .GetFriendData()
                .Select(Convert.ToInt32)
                .ToArray()
                .ToFleetDamages();
        }

        public static FleetDamages GetFriendDamages(this object[] damages, object[] df_list)
        {
            return damages
                .ToIntArray()
                .ToSortedDamages(df_list.ToIntArray())
                .GetFriendData(0)
                .ToFleetDamages();
        }

        public static FleetDamages GetEnemyDamages(this object[] damages, object[] df_list)
        {
            return damages
                .ToIntArray()
                .ToSortedDamages(df_list.ToIntArray())
                .GetEnemyData(0)
                .ToFleetDamages();
        }

        private static int[] ToIntArray(this object[] damages)
        {
            return damages
                .Where(x => x is Array)
                .Select(x => ((Array)x).Cast<object>())
                .SelectMany(x => x.Select(Convert.ToInt32))
                .ToArray();
        }

        private static int[] ToSortedDamages(this int[] damages, int[] dfList)
        {
            var zip = damages.Zip(dfList, (da, df) => new { df, da });
            var ret = new int[12];

            foreach (var d in zip.Where(d => 0 < d.df))
                ret[d.df - 1] += d.da;

            return ret;
        }
    }
}
