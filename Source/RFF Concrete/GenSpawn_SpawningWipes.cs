using HarmonyLib;
using Verse;

namespace RFFConcrete_Code;

[HarmonyPatch(typeof(GenSpawn), "SpawningWipes", null)]
public static class GenSpawn_SpawningWipes
{
    public static bool Prefix(BuildableDef newEntDef, BuildableDef oldEntDef, ref bool __result)
    {
        if (newEntDef is not ThingDef thingDef || oldEntDef is not ThingDef thingDef1)
        {
            return true;
        }

        var thingDef2 = thingDef.entityDefToBuild as ThingDef;
        if (!thingDef1.IsBlueprint)
        {
            return true;
        }

        if (!thingDef.IsBlueprint)
        {
            return true;
        }

        if (thingDef2?.building == null || !thingDef2.building.canPlaceOverWall)
        {
            return true;
        }

        if (thingDef1.entityDefToBuild is not ThingDef)
        {
            return true;
        }

        if (!thingDef1.entityDefToBuild.defName.Equals("RFFConcreteWall") &&
            !thingDef1.entityDefToBuild.defName.Equals("RFFReinforcedConcreteWall") &&
            !thingDef1.entityDefToBuild.defName.Equals("RFFPlasticreteWall"))
        {
            return true;
        }

        __result = true;
        return false;
    }
}