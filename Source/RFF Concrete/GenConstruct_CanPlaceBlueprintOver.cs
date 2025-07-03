using HarmonyLib;
using RimWorld;
using Verse;

namespace RFFConcrete_Code;

[HarmonyPatch(typeof(GenConstruct), nameof(GenConstruct.CanPlaceBlueprintOver), null)]
public static class GenConstruct_CanPlaceBlueprintOver
{
    public static bool Prefix(BuildableDef newDef, ThingDef oldDef, ref bool __result)
    {
        if (newDef is not ThingDef thingDef || oldDef == null)
        {
            return true;
        }

        var buildableDef = GenConstruct.BuiltDefOf(oldDef);
        if (thingDef.building is not { canPlaceOverWall: true } ||
            buildableDef is not ThingDef thingDef2)
        {
            return true;
        }

        if (!thingDef2.defName.Equals("RFFConcreteWall") &&
            !thingDef2.defName.Equals("RFFReinforcedConcreteWall") &&
            !thingDef2.defName.Equals("RFFPlasticreteWall"))
        {
            return true;
        }

        __result = true;
        return false;
    }
}