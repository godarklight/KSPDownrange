using System;
using UnityEngine;

namespace KSPDownrange
{
    public class DownrangeGUI
    {
        private Rect windowRect = new Rect(Screen.width - 350f, 0f, 250f, 0f);
        private bool displayWindow = false;
        private bool closeWindow = false;
        private bool launchPosSet = false;
        private double launchPosLat = 0d;
        private double launchPosLong = 0d;
        private CelestialBody launchBody = null;
        private string downrangeDistance = "";

        public void Update()
        {
            if (closeWindow)
            {
                displayWindow = false;
                closeWindow = false;
            }
            if (FlightGlobals.fetch == null || FlightGlobals.fetch.activeVessel == null)
            {
                return;
            }
            Vessel activeVessel = FlightGlobals.fetch.activeVessel;
            if (FlightGlobals.ready && !launchPosSet)
            {
                launchPosSet = true;
                displayWindow = true;
                launchPosLat = activeVessel.latitude;
                launchPosLong = activeVessel.longitude;
                launchBody = activeVessel.mainBody;
            }
            if (launchBody != activeVessel.mainBody)
            {
                displayWindow = false;
            }
            if (displayWindow)
            {
                CelestialBody currentBody = activeVessel.mainBody;
                Vector3d launchPos = currentBody.GetSurfaceNVector(launchPosLat, launchPosLong);
                Vector3d ourPos = currentBody.GetSurfaceNVector(activeVessel.latitude, activeVessel.longitude);
                double vectorAngle = Vector3d.Angle(launchPos, ourPos);
                double distance = (currentBody.Radius * Math.PI * vectorAngle) / 180000d;
                distance = Math.Round(distance, 2);
                downrangeDistance = "Downrange distance: " + distance + " km.";
            }
        }

        public void Draw()
        {
            if (displayWindow)
            {
                windowRect = GUILayout.Window(238590231, windowRect, DrawWindow, "Downrange");
            }
        }

        private void DrawWindow(int windowID)
        {
            GUI.DragWindow(new Rect(0, 0, float.PositiveInfinity, 20f));
            GUILayout.Label(downrangeDistance);
            if (GUILayout.Button("Reset"))
            {
                launchPosLat = FlightGlobals.fetch.activeVessel.latitude;
                launchPosLong = FlightGlobals.fetch.activeVessel.longitude;
            }
            if (GUILayout.Button("Close"))
            {
                closeWindow = true;
            }
        }
    }
}

