﻿namespace BattleInfoPlugin.Models.Raw
{
    /// <summary>
    /// 連合艦隊-特殊夜戦
    /// </summary>
    public class combined_battle_sp_midnight
    {
        public int api_deck_id { get; set; }
        public int[] api_ship_ke { get; set; }
        public int[] api_ship_lv { get; set; }
		public decimal[] api_nowhps { get; set; }
		public decimal[] api_maxhps { get; set; }
		public decimal[] api_nowhps_combined { get; set; }
		public decimal[] api_maxhps_combined { get; set; }
        public int[][] api_eSlot { get; set; }
        public int[][] api_eKyouka { get; set; }
        public int[][] api_fParam { get; set; }
        public int[][] api_eParam { get; set; }
        public int[][] api_fParam_combined { get; set; }
        public int[] api_formation { get; set; }
        public int[] api_touch_plane { get; set; }
        public int[] api_flare_pos { get; set; }
        public Midnight_Hougeki api_hougeki { get; set; }
    }
}
