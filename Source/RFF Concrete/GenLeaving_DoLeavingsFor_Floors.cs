using HarmonyLib;
using RimWorld;
using Verse;

namespace RFFConcrete_Code
{
    [HarmonyPatch(typeof(GenLeaving), "DoLeavingsFor", typeof(TerrainDef), typeof(IntVec3), typeof(Map))]
    public static class GenLeaving_DoLeavingsFor_Floors
    {
        public static bool Prefix(TerrainDef terrain, IntVec3 cell, Map map)
        {
            if (terrain == TerrainDef.Named("Concrete"))
            {
                return false;
            }

            if (terrain != TerrainDef.Named("PavedTile"))
            {
                return true;
            }

            var leaving = ThingMaker.MakeThing(ThingDef.Named("CrushedRocks"));
            GenPlace.TryPlaceThing(leaving, cell, map, ThingPlaceMode.Near);
            return false;
        }
    }
}