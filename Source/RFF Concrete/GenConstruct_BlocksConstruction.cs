﻿using HarmonyLib;
using RimWorld;
using Verse;

namespace RFFConcrete_Code
{
    [HarmonyPatch(typeof(GenConstruct), "BlocksConstruction", null)]
    public static class GenConstruct_BlocksConstruction
    {
        public static bool Prefix(Thing constructible, Thing t, ref bool __result)
        {
            ThingDef thingDef;
            if (constructible == null || t == null)
            {
                return true;
            }

            if (!(constructible is Blueprint))
            {
                thingDef = !(constructible is Frame)
                    ? constructible.def.blueprintDef
                    : constructible.def.entityDefToBuild.blueprintDef;
            }
            else
            {
                thingDef = constructible.def;
            }

            if (!(thingDef.entityDefToBuild is ThingDef thingDef1) || thingDef1.building == null ||
                !thingDef1.building.canPlaceOverWall)
            {
                return true;
            }

            if (!t.def.defName.Equals("RFFConcreteWall") && !t.def.defName.Equals("RFFReinforcedConcreteWall") &&
                !t.def.defName.Equals("RFFPlasticreteWall"))
            {
                return true;
            }

            __result = false;
            return false;
        }
    }
}