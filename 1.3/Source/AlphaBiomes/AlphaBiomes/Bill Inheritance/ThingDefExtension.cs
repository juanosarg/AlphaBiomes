using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Verse;
using RimWorld;

namespace AlphaBiomes
{
    // Token: 0x02000003 RID: 3
    public class ThingDefExtension : DefModExtension
    {

        public static ThingDefExtension Get(Def def)
        {
            return def.GetModExtension<ThingDefExtension>() ?? defaultValues;
        }


        public override IEnumerable<string> ConfigErrors()
        {
            if (inheritRecipesFrom == null)
            {
                yield return "inheritRecipesFrom is null.";
            }
            else
            {
                List<string> nonWorkbenchDefNames = new List<string>();
                List<string> recipelessThingDefNames = new List<string>();
                for (int i = 0; i < inheritRecipesFrom.Count; i++)
                {
                    var thing = inheritRecipesFrom[i];
                    bool flag2 = !thing.IsWorkTable;
                    if (flag2)
                    {
                        nonWorkbenchDefNames.Add(thing.defName);
                    }
                    else
                    {
                        bool flag3 = thing.AllRecipes.NullOrEmpty<RecipeDef>();
                        if (flag3)
                        {
                            recipelessThingDefNames.Add(thing.defName);
                        }
                    }
                }
                if (nonWorkbenchDefNames.Any())
                {
                    yield return "the following ThingDefs in inheritRecipesFrom are not workbenches: " + nonWorkbenchDefNames.ToCommaList(false);
                }
                if (recipelessThingDefNames.Any())
                {
                    yield return "the following workbenches in inheritRecipesFrom do not have any recipes: " + recipelessThingDefNames.ToCommaList(false);
                }
            }
            yield break;
        }

        public bool Allows(RecipeDef recipe)
        {
            ThingDef producedThingDef = recipe.ProducedThingDef;
            return (producedThingDef == null || ((this.allowedProductFilter == null || this.allowedProductFilter.Allows(producedThingDef)) && (this.disallowedProductFilter == null || !this.disallowedProductFilter.Allows(producedThingDef)))) && (this.allowedRecipes == null || this.allowedRecipes.Contains(recipe)) && (this.disallowedRecipes == null || !this.disallowedRecipes.Contains(recipe));
        }

        private static readonly ThingDefExtension defaultValues = new ThingDefExtension();

        public List<ThingDef> inheritRecipesFrom;

        public ThingFilter allowedProductFilter;

        public ThingFilter disallowedProductFilter;

        public List<RecipeDef> allowedRecipes;

        public List<RecipeDef> disallowedRecipes;

    }
}
