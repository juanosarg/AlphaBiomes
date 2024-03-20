using System;
using UnityEngine;
using Verse;

namespace AlphaBiomes
{
    [StaticConstructorOnStartup]
    public static class GraphicsCache
    {
       
   
        public static readonly Texture2D Paste = ContentFinder<Texture2D>.Get("UI/Buttons/Paste", true);






    }
}
