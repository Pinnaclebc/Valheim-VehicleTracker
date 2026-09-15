using HarmonyLib;
using System.Collections.Generic;
using UnityEngine;

namespace VehicleTracker
{
    public class TrackedVehicle : MonoBehaviour
    {
        public Minimap.PinData pin;
        public string vehicleName;
        public Color glowColor = Color.cyan;
        private Light glowLight;
        private float updateTimer = 0f;
        private ZNetView nview;

        public void Init(string name, Minimap.PinType pinType, Color customColor)
        {
            vehicleName = name;
            glowColor = customColor;

            Minimap.PinData existingSavedPin = FindExistingNearbyPin(name, 10f);

            if (existingSavedPin != null)
            {
                pin = existingSavedPin;
            }
            else
            {
                pin = Minimap.instance.AddPin(transform.position, pinType, name, true, false);
            }

            SetupGlowLight();
        }

        private void SetupGlowLight()
        {
            if (glowLight == null)
            {
                GameObject lightObj = new GameObject("VehicleGlowLight");
                lightObj.transform.SetParent(transform);
                lightObj.transform.localPosition = new Vector3(0, 2f, 0);

                glowLight = lightObj.AddComponent<Light>();
                glowLight.type = LightType.Point;
                glowLight.range = 18f;
                glowLight.intensity = 2.5f;
                glowLight.color = glowColor;
                glowLight.enabled = false;
            }
        }

        void Update()
        {
            if (glowLight != null)
            {
                glowLight.enabled = Plugin.GlowEnabled;
            }

            updateTimer += Time.deltaTime;
            if (updateTimer >= 2f)
            {
                updateTimer = 0f;
                SyncPinPosition();
            }
        }

        private void SyncPinPosition()
        {
            if (!HasActivePin())
            {
                return;
            }

            if (Vector3.Distance(pin.m_pos, transform.position) > 3f)
            {
                pin.m_pos = transform.position;
            }
        }

        public bool HasActivePin()
        {
            if (pin == null || Minimap.instance == null)
            {
                return false;
            }

            var pinsList = Traverse.Create(Minimap.instance).Field("m_pins").GetValue<List<Minimap.PinData>>();
            if (pinsList == null)
            {
                pinsList = Traverse.Create(Minimap.instance).Field("m_customPins").GetValue<List<Minimap.PinData>>();
            }

            return pinsList != null && pinsList.Contains(pin);
        }

        void OnDestroy()
        {

            if (nview == null)
            {
                nview = GetComponent<ZNetView>();
            }


            bool isActuallyDestroyed = false;
            if (nview != null && nview.GetZDO() != null)
            {
                var piece = GetComponent<Piece>();
                var wear = GetComponent<WearNTear>();

                if (wear != null && wear.GetHealthPercentage() <= 0f)
                {
                    isActuallyDestroyed = true;
                }
            }

            if (isActuallyDestroyed && HasActivePin())
            {
                Minimap.instance.RemovePin(pin);
            }
        }

        private Minimap.PinData FindExistingNearbyPin(string name, float searchRadius)
        {
            if (Minimap.instance == null)
            {
                return null;
            }

            var pinsList = Traverse.Create(Minimap.instance).Field("m_pins").GetValue<List<Minimap.PinData>>();
            if (pinsList == null)
            {
                pinsList = Traverse.Create(Minimap.instance).Field("m_customPins").GetValue<List<Minimap.PinData>>();
            }

            if (pinsList == null)
            {
                return null;
            }

            foreach (var p in pinsList)
            {
                if (p.m_name == name && Vector3.Distance(p.m_pos, transform.position) <= searchRadius)
                {
                    return p;
                }
            }

            return null;
        }
    }
}