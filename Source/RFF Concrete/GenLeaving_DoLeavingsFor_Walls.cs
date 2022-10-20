using System;
using System.Collections.Generic;
using System.Linq;
using HarmonyLib;
using RimWorld;
using Verse;

namespace RFFConcrete_Code;

[HarmonyPatch(typeof(GenLeaving), "DoLeavingsFor", typeof(Thing), typeof(Map), typeof(DestroyMode),
    typeof(CellRect), typeof(Predicate<IntVec3>), typeof(List<Thing>))]
public static class GenLeaving_DoLeavingsFor_Walls
{
    public static bool Prefix(Thing diedThing, Map map, DestroyMode mode, CellRect leavingsRect,
        Predicate<IntVec3> nearPlaceValidator = null, List<Thing> listOfLeavingsOut = null)
    {
        switch (diedThing.def.defName)
        {
            case "RFFConcreteDoor":
            case "RFFConcreteAutodoor":
            case "RFFPlasticreteDoor":
            case "RFFPlasticreteAutodoor":
            case "RFFConcreteWall":
            case "RFFReinforcedConcreteWall":
            case "RFFPlasticreteWall":
            case "RFFConcreteEmbrasure":
            case "RFFPlasticreteEmbrasure":
            case "RBB_DeepBridge":
            case "RBB_DeepBridge_Stone":
            {
                var list = leavingsRect.Cells.InRandomOrder().ToList();
                var leaving = ThingMaker.MakeThing(ThingDef.Named("CrushedRocks"));
                GenPlace.TryPlaceThing(leaving, list[0], map, ThingPlaceMode.Near);
                if (diedThing.def.defName == "RFFConcreteDoor")
                {
                    var leaving2 = ThingMaker.MakeThing(ThingDefOf.Steel);
                    leaving2.stackCount = Rand.RangeInclusive(2, 4);
                    GenPlace.TryPlaceThing(leaving2, list[0], map, ThingPlaceMode.Near);
                }

                if (diedThing.def.defName == "RFFConcreteAutodoor")
                {
                    var leaving2 = ThingMaker.MakeThing(ThingDefOf.Steel);
                    leaving2.stackCount = Rand.RangeInclusive(2, 4);
                    GenPlace.TryPlaceThing(leaving2, list[0], map, ThingPlaceMode.Near);
                    var leaving3 = ThingMaker.MakeThing(ThingDefOf.ChunkSlagSteel);
                    leaving3.stackCount = Rand.RangeInclusive(2, 3);
                    GenPlace.TryPlaceThing(leaving3, list[0], map, ThingPlaceMode.Near);
                    var leaving4 = ThingMaker.MakeThing(ThingDefOf.ComponentIndustrial);
                    if (Rand.Value < 0.75f)
                    {
                        GenPlace.TryPlaceThing(leaving4, list[0], map, ThingPlaceMode.Near);
                    }
                }

                if (diedThing.def.defName == "RFFPlasticreteDoor")
                {
                    var leaving2 = ThingMaker.MakeThing(ThingDefOf.Steel);
                    leaving2.stackCount = Rand.RangeInclusive(3, 5);
                    GenPlace.TryPlaceThing(leaving2, list[0], map, ThingPlaceMode.Near);
                    var leaving3 = ThingMaker.MakeThing(ThingDefOf.Plasteel);
                    leaving3.stackCount = Rand.RangeInclusive(2, 4);
                    GenPlace.TryPlaceThing(leaving3, list[0], map, ThingPlaceMode.Near);
                }

                if (diedThing.def.defName == "RFFPlasticreteAutodoor")
                {
                    var leaving2 = ThingMaker.MakeThing(ThingDefOf.Steel);
                    leaving2.stackCount = Rand.RangeInclusive(2, 4);
                    GenPlace.TryPlaceThing(leaving2, list[0], map, ThingPlaceMode.Near);
                    var leaving3 = ThingMaker.MakeThing(ThingDefOf.ChunkSlagSteel);
                    leaving3.stackCount = Rand.RangeInclusive(2, 3);
                    GenPlace.TryPlaceThing(leaving3, list[0], map, ThingPlaceMode.Near);
                    var leaving4 = ThingMaker.MakeThing(ThingDefOf.Plasteel);
                    leaving4.stackCount = Rand.RangeInclusive(2, 4);
                    GenPlace.TryPlaceThing(leaving4, list[0], map, ThingPlaceMode.Near);
                    var leaving5 = ThingMaker.MakeThing(ThingDefOf.ComponentIndustrial);
                    if (Rand.Value < 0.75f)
                    {
                        GenPlace.TryPlaceThing(leaving5, list[0], map, ThingPlaceMode.Near);
                    }
                }

                if (diedThing.def.defName is "RFFReinforcedConcreteWall" or "RFFConcreteEmbrasure")
                {
                    var leaving2 = ThingMaker.MakeThing(ThingDefOf.Steel);
                    if (Rand.Value < 0.75f)
                    {
                        GenPlace.TryPlaceThing(leaving2, list[0], map, ThingPlaceMode.Near);
                    }
                }

                if (diedThing.def.defName is "RFFPlasticreteWall" or "RFFPlasticreteEmbrasure")
                {
                    var leaving2 = ThingMaker.MakeThing(ThingDefOf.Steel);
                    leaving2.stackCount = Rand.RangeInclusive(1, 3);
                    GenPlace.TryPlaceThing(leaving2, list[0], map, ThingPlaceMode.Near);
                    var leaving3 = ThingMaker.MakeThing(ThingDefOf.Plasteel);
                    leaving3.stackCount = Rand.RangeInclusive(1, 3);
                    GenPlace.TryPlaceThing(leaving3, list[0], map, ThingPlaceMode.Near);
                }

                if (!diedThing.def.defName.Contains("DeepBridge"))
                {
                    return false;
                }

                var thingCountClasses = diedThing.CostListAdjusted();
                foreach (var thingCountClass in thingCountClasses)
                {
                    if (thingCountClass.thingDef.defName == "RFFConcrete")
                    {
                        continue;
                    }

                    var buildingResourcesLeaveCalculator =
                        GetBuildingResourcesLeaveCalculator(diedThing, mode)(thingCountClass.count);
                    if (buildingResourcesLeaveCalculator <= 0)
                    {
                        continue;
                    }

                    var thing1 = ThingMaker.MakeThing(thingCountClass.thingDef);
                    thing1.stackCount = buildingResourcesLeaveCalculator;
                    GenPlace.TryPlaceThing(thing1, list[0], map, ThingPlaceMode.Near);
                }

                return false;
            }
        }

        return true;
    }

    private static Func<int, int> GetBuildingResourcesLeaveCalculator(Thing destroyedThing, DestroyMode mode)
    {
        if (!GenLeaving.CanBuildingLeaveResources(destroyedThing, mode))
        {
            return _ => 0;
        }

        if (mode == DestroyMode.Deconstruct && destroyedThing is Frame)
        {
            mode = DestroyMode.Cancel;
        }

        switch (mode)
        {
            case DestroyMode.Vanish:
            {
                return _ => 0;
            }
            case DestroyMode.WillReplace:
            {
                return _ => 0;
            }
            case DestroyMode.KillFinalize:
            {
                return count => GenMath.RoundRandom(count * 0.5f);
            }
            case DestroyMode.Deconstruct:
            {
                return count =>
                    GenMath.RoundRandom(Math.Min(count * destroyedThing.def.resourcesFractionWhenDeconstructed,
                        count - 1));
            }
            case DestroyMode.FailConstruction:
            {
                return count => GenMath.RoundRandom(count * 0.5f);
            }
            case DestroyMode.Cancel:
            {
                return count => GenMath.RoundRandom(count * 1f);
            }
            case DestroyMode.Refund:
            {
                return count => count;
            }
        }

        throw new ArgumentException(string.Concat("Unknown destroy mode ", mode));
    }
}