using System.Reflection;
using HarmonyLib;
using Verse;

namespace RFFConcrete_Code
{
    [StaticConstructorOnStartup]
    internal static class RFFConcrete_Initializer
    {
        static RFFConcrete_Initializer()
        {
            var harmony = new Harmony("net.rainbeau.rimworld.mod.rffconcrete");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }
    }
}