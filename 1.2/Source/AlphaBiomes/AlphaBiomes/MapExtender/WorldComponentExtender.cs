using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RimWorld;
using Verse;
using Verse.Noise;
using UnityEngine;
using RimWorld.Planet;

namespace AlphaBiomes
{
    public class WorldComponentExtender : WorldComponent
    {

        [Unsaved]
        public ModuleBase noiseRadiation;
        [Unsaved]
        public ModuleBase noiseWeirdness;
        [Unsaved]
        public ModuleBase noiseVolcanic;

        private static readonly FloatRange RadMaxElevation = new FloatRange(650f, 750f);

       
        private static float FreqMultiplier
        {
            get
            {
                return 1f;
            }
        }

        public WorldComponentExtender(World world) : base(world)
        {
            SetupRadiationNoise();
            SetupWeirdnessNoise();
            SetupVolcanicNoise();

        }


        private void SetupRadiationNoise()
        {
            float freqMultiplier = FreqMultiplier;
            ModuleBase moduleBase = new Perlin((double)(0.09f * freqMultiplier), 2.0, 0.40000000596046448, 6, Rand.Range(0, int.MaxValue), QualityMode.High);
            ModuleBase moduleBase2 = new RidgedMultifractal((double)(0.025f * freqMultiplier), 2.0, 6, Rand.Range(0, int.MaxValue), QualityMode.High);
            moduleBase = new ScaleBias(0.5, 0.5, moduleBase);
            moduleBase2 = new ScaleBias(0.5, 0.5, moduleBase2);
            this.noiseRadiation = new Multiply(moduleBase, moduleBase2);
            InverseLerp rhs = new InverseLerp(this.noiseRadiation, RadMaxElevation.max, RadMaxElevation.min);
            this.noiseRadiation = new Multiply(this.noiseRadiation, rhs);
          
            NoiseDebugUI.StorePlanetNoise(this.noiseRadiation, "noiseRadiation");
        }

        private void SetupWeirdnessNoise()
        {
            float freqMultiplier = FreqMultiplier;
            ModuleBase moduleBase = new Perlin((double)(0.09f * freqMultiplier), 2.0, 0.40000000596046448, 6, Rand.Range(0, int.MaxValue), QualityMode.High);
            ModuleBase moduleBase2 = new RidgedMultifractal((double)(0.025f * freqMultiplier), 2.0, 6, Rand.Range(0, int.MaxValue), QualityMode.High);
            moduleBase = new ScaleBias(0.5, 0.5, moduleBase);
            moduleBase2 = new ScaleBias(0.5, 0.5, moduleBase2);
            this.noiseWeirdness = new Multiply(moduleBase, moduleBase2);
            NoiseDebugUI.StorePlanetNoise(this.noiseWeirdness, "noiseWeirdness");
        }

        private void SetupVolcanicNoise()
        {
            float freqMultiplier = FreqMultiplier;
            ModuleBase moduleBase = new Perlin((double)(0.09f * freqMultiplier), 2.0, 0.40000000596046448, 6, Rand.Range(0, int.MaxValue), QualityMode.High);
            ModuleBase moduleBase2 = new RidgedMultifractal((double)(0.005f * freqMultiplier), 2.0, 6, Rand.Range(0, int.MaxValue), QualityMode.High);
            moduleBase = new ScaleBias(0.5, 0.5, moduleBase);
            moduleBase2 = new ScaleBias(0.5, 0.5, moduleBase2);
            this.noiseVolcanic = new Multiply(moduleBase, moduleBase2);
            NoiseDebugUI.StorePlanetNoise(this.noiseVolcanic, "noiseVolcanic");
        }

    }
}
