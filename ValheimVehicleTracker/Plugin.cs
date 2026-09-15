using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using UnityEngine;

namespace VehicleTracker
{
    [BepInPlugin(ModGUID, ModName, ModVersion)]
    public class Plugin : BaseUnityPlugin
    {
        public const string ModGUID = "net.pinnacle.valheim.vehicletracker";
        public const string ModName = "Vehicle Tracker";
        public const string ModVersion = "1.0.0";

        public static BepInEx.Logging.ManualLogSource Log;
        public static bool GlowEnabled = false;


        public static ConfigEntry<KeyboardShortcut> ToggleKey;


        public static ConfigEntry<string> CartPinName;
        public static ConfigEntry<string> RaftPinName;
        public static ConfigEntry<string> KarvePinName;
        public static ConfigEntry<string> LongshipPinName;
        public static ConfigEntry<string> DrakkarPinName;


        public static ConfigEntry<string> ShipGlowColorHex;
        public static ConfigEntry<string> CartGlowColorHex;

        private Harmony harmony;

        void Awake()
        {
            Log = Logger;


            ToggleKey = Config.Bind("General", "ToggleGlowKey", new KeyboardShortcut(KeyCode.H), "Key used to toggle vehicle glow");

            // Names
            CartPinName = Config.Bind("Names", "CartName", "Cart", "Pin name for carts");
            RaftPinName = Config.Bind("Names", "RaftName", "Raft", "Pin name for rafts");
            KarvePinName = Config.Bind("Names", "KarveName", "Karve", "Pin name for the karve");
            LongshipPinName = Config.Bind("Names", "LongshipName", "Longship", "Pin name for the longship");
            DrakkarPinName = Config.Bind("Names", "DrakkarName", "Drakkar", "Pin name for the drakkar");

            // Glow Colors (stored as Hex codes like #00FFFF)
            ShipGlowColorHex = Config.Bind("Colors", "ShipGlowColor", "#00FFFF", "Hex color code for ship glow (Cyan by default)");
            CartGlowColorHex = Config.Bind("Colors", "CartGlowColor", "#FFD700", "Hex color code for cart glow (Gold by default)");

            harmony = new Harmony(ModGUID);
            harmony.PatchAll();
            Log.LogInfo(ModName + " loaded successfully!");
        }

        void Update()
        {
            if (ToggleKey.Value.IsDown())
            {
                GlowEnabled = !GlowEnabled;
                Log.LogInfo("Vehicle glow toggled: " + GlowEnabled);
            }
        }

        void OnDestroy()
        {
            harmony?.UnpatchSelf();
        }
    }
}