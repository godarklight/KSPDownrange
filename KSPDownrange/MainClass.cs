using System;
using UnityEngine;

namespace KSPDownrange
{
    [KSPAddon(KSPAddon.Startup.Flight, false)]
    public class MainClass : MonoBehaviour
    {
        DownrangeGUI downrangeGui = new DownrangeGUI();

        public void Update()
        {
            downrangeGui.Update();
        }

        public void OnGUI()
        {
            downrangeGui.Draw();
        }
    }
}

