using System;
using System.Collections.Generic;
using Verse;

namespace AlphaBiomes
{
    [StaticConstructorOnStartup]
    public static class StaticConstructorClass
    {
        static StaticConstructorClass()
        {
            // Go through all ThingDefs that are work tables
            for (int i = 0; i < DefDatabase<ThingDef>.AllDefsListForReading.Count; i++)
            {
                var thingDef = DefDatabase<ThingDef>.AllDefsListForReading[i];
                if (thingDef.IsWorkTable)
                {
                    ThingDefExtension thingDefExtension = ThingDefExtension.Get(thingDef);

                    // Sort out recipe inheritance
                    if (thingDefExtension.inheritRecipesFrom != null)
                    {
                        List<RecipeDef> curRecipes = new List<RecipeDef>(thingDef.AllRecipes);
                        NonPublicFields.ThingDef_allRecipesCached.SetValue(thingDef, null);
                        for (int j = 0; j < thingDefExtension.inheritRecipesFrom.Count; j++)
                        {
                            var otherBench = thingDefExtension.inheritRecipesFrom[j];
                            for (int k = 0; k < otherBench.AllRecipes.Count; k++)
                            {
                                var curRecipe = otherBench.AllRecipes[k];
                                if (thingDefExtension.Allows(curRecipe))
                                {
                                    if (thingDef.recipes == null)
                                    {
                                        thingDef.recipes = new List<RecipeDef>();
                                    }

                                    if (!curRecipes.Contains(curRecipe))
                                    {
                                        thingDef.recipes.Add(curRecipe);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
