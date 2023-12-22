using HarmonyLib;
using Kitchen;
using KitchenData;
using KitchenMods;
using System.Reflection;
using UnityEngine;

// Namespace should have "Kitchen" in the beginning
namespace KitchenConfigurableElfHuts
{
    public class Main : IModInitializer
    {
        public const string MOD_GUID = $"IcedMilo.PlateUp.{MOD_NAME}";
        public const string MOD_NAME = "Configurable Elf Huts";
        public const string MOD_VERSION = "0.1.0";

        public Main()
        {
            Harmony harmony = new Harmony(MOD_GUID);
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }

        public void PostActivate(KitchenMods.Mod mod)
        {
            LogWarning($"{MOD_GUID} v{MOD_VERSION} in use!");
        }

        public void PreInject()
        {
            if (GameData.Main.TryGet(-699013948, out Appliance shedOutputPlaceholder))
            {
                for (int i = 0; i < shedOutputPlaceholder.Properties.Count; i++)
                {
                    if (shedOutputPlaceholder.Properties[i] is CChristmasShedPlaceholder christmasShedPlaceholder)
                    {
                        christmasShedPlaceholder.ShedID = -349733673;   // Shed Magic Everything
                        shedOutputPlaceholder.Properties[i] = christmasShedPlaceholder;
                        break;
                    }
                }
            }


            if (GameData.Main.TryGet(-697441390, out Appliance christmasGrabber))
            {
                for (int i = 0; i < christmasGrabber.Properties.Count; i++)
                {
                    if (christmasGrabber.Properties[i] is CFixedRotation)
                    {
                        christmasGrabber.Properties.RemoveAt(i);
                        break;
                    }
                }
            }
        }

        public void PostInject()
        {
        }

        #region Logging
        public static void LogInfo(string _log) { Debug.Log($"[{MOD_NAME}] " + _log); }
        public static void LogWarning(string _log) { Debug.LogWarning($"[{MOD_NAME}] " + _log); }
        public static void LogError(string _log) { Debug.LogError($"[{MOD_NAME}] " + _log); }
        public static void LogInfo(object _log) { LogInfo(_log.ToString()); }
        public static void LogWarning(object _log) { LogWarning(_log.ToString()); }
        public static void LogError(object _log) { LogError(_log.ToString()); }
        #endregion
    }
}
