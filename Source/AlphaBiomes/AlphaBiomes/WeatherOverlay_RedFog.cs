using System;
using UnityEngine;
using Verse;

namespace AlphaBiomes
{
    [StaticConstructorOnStartup]
    public class WeatherOverlay_RedFog : SkyOverlay
    {
        public WeatherOverlay_RedFog()
        {
            this.worldOverlayMat = RedFogOverlayWorld;
            base.OverlayColor = new Color(0.8f, 0.35f, 0.26f);
            this.worldOverlayPanSpeed1 = 0.005f;
            this.worldOverlayPanSpeed2 = 0.004f;
            this.worldPanDir1 = new Vector2(1f, 1f);
            this.worldPanDir2 = new Vector2(1f, -1f);
        }

        private static readonly Material RedFogOverlayWorld = MatLoader.LoadMat("Weather/FogOverlayWorld", -1);
    }
}