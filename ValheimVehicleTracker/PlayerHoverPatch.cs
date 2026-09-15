using HarmonyLib;
using UnityEngine;

namespace VehicleTracker
{
    [HarmonyPatch(typeof(Player), "UpdateHover")]
    class PlayerHoverPatch
    {
        private static void Postfix(ref Player __instance)
        {
            if (__instance != Player.m_localPlayer)
            {
                return;
            }

            GameObject hoverObj = __instance.GetHoverObject();
            if (hoverObj == null)
            {
                return;
            }

            Ship ship = hoverObj.GetComponentInParent<Ship>();
            Vagon cart = hoverObj.GetComponentInParent<Vagon>();

            if (ship == null && cart == null)
            {
                return;
            }

            GameObject targetObj = ship != null ? ship.gameObject : cart.gameObject;
            string vehicleName;
            Color chosenColor;

            if (cart != null)
            {
                vehicleName = Plugin.CartPinName.Value;
                chosenColor = ParseColor(Plugin.CartGlowColorHex.Value, Color.yellow);
            }
            else
            {
                vehicleName = ResolveShipName(targetObj.name);
                chosenColor = ParseColor(Plugin.ShipGlowColorHex.Value, Color.cyan);
            }

            Minimap.PinType pinType = Minimap.PinType.Icon3;
            TrackedVehicle tracker = targetObj.GetComponent<TrackedVehicle>();

            if (tracker != null && tracker.HasActivePin())
            {
                return;
            }

            if (tracker != null)
            {
                tracker.Init(vehicleName, pinType, chosenColor);
            }
            else
            {
                tracker = targetObj.AddComponent<TrackedVehicle>();
                tracker.Init(vehicleName, pinType, chosenColor);
            }
        }

        private static string ResolveShipName(string objectName)
        {
            string lower = objectName.ToLower();

            if (lower.Contains("raft"))
            {
                return Plugin.RaftPinName.Value;
            }

            if (lower.Contains("drakkar") || lower.Contains("ashland"))
            {
                return Plugin.DrakkarPinName.Value;
            }

            if (lower.Contains("vikingship") || lower.Contains("longship"))
            {
                return Plugin.LongshipPinName.Value;
            }

            if (lower.Contains("karve"))
            {
                return Plugin.KarvePinName.Value;
            }

            return "Ship";
        }

        private static Color ParseColor(string hexString, Color fallback)
        {
            if (ColorUtility.TryParseHtmlString(hexString, out Color parsedColor))
            {
                return parsedColor;
            }
            return fallback;
        }
    }
}