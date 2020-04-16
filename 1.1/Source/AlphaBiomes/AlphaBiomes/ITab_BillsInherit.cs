using RimWorld;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Verse;
using System;
using Verse.Sound;


namespace AlphaBiomes

{
   
    public class ITab_BillsInherit : ITab
    {
        private string billsOriginator;

       

        protected Building_WorkTable SelTable
        {
            get
            {
                return (Building_WorkTable)base.SelThing;
            }
        }

        public ITab_BillsInherit()
        {
            this.size = ITab_BillsInherit.WinSize;
            this.labelKey = "TabBills";
            this.tutorTag = "Bills";
        }

        protected override void FillTab()
        {
            if (SelTable.def.defName == "AB_PropaneStove")
            {
                billsOriginator = "FueledStove";
            }
            PlayerKnowledgeDatabase.KnowledgeDemonstrated(ConceptDefOf.BillsTab, KnowledgeAmount.FrameDisplayed);
            Rect rect2 = new Rect(WinSize.x - PasteX, PasteY, PasteSize, PasteSize);
            if (BillUtility.Clipboard == null)
            {
                GUI.color = Color.gray;
                Widgets.DrawTextureFitted(rect2, GraphicsCache.Paste, 1f);
                GUI.color = Color.white;
                TooltipHandler.TipRegionByKey(rect2, "PasteBillTip");
            }
            else if (!SelTable.def.AllRecipes.Contains(BillUtility.Clipboard.recipe) || !BillUtility.Clipboard.recipe.AvailableNow)
            {
                GUI.color = Color.gray;
                Widgets.DrawTextureFitted(rect2, GraphicsCache.Paste, 1f);
                GUI.color = Color.white;
                TooltipHandler.TipRegionByKey(rect2, "ClipboardBillNotAvailableHere");
            }
            else if (SelTable.billStack.Count >= 15)
            {
                GUI.color = Color.gray;
                Widgets.DrawTextureFitted(rect2, GraphicsCache.Paste, 1f);
                GUI.color = Color.white;
                if (Mouse.IsOver(rect2))
                {
                    TooltipHandler.TipRegion(rect2, "PasteBillTip".Translate() + " (" + "PasteBillTip_LimitReached".Translate() + ")");
                }
            }
            else
            {
                if (Widgets.ButtonImageFitted(rect2, GraphicsCache.Paste, Color.white))
                {
                    Bill bill = BillUtility.Clipboard.Clone();
                    bill.InitializeAfterClone();
                    SelTable.billStack.AddBill(bill);
                    SoundDefOf.Tick_Low.PlayOneShotOnCamera();
                }
                TooltipHandler.TipRegionByKey(rect2, "PasteBillTip");
            }
            Rect rect3 = new Rect(0f, 0f, WinSize.x, WinSize.y).ContractedBy(10f);
            Func<List<FloatMenuOption>> recipeOptionsMaker = delegate
            {
                List<FloatMenuOption> list = new List<FloatMenuOption>();
               // ITab_BillsInherit tab_Bills = default(ITab_BillsInherit);
                RecipeDef recipe = default(RecipeDef);
                for (int i = 0; i < ThingDef.Named(billsOriginator).AllRecipes.Count; i++)
                {
                   
                    if (ThingDef.Named(billsOriginator).AllRecipes[i].AvailableNow)
                    {
                        recipe = ThingDef.Named(billsOriginator).AllRecipes[i];
                        list.Add(new FloatMenuOption(recipe.LabelCap, delegate
                        {
                            if (!this.SelTable.Map.mapPawns.FreeColonists.Any((Pawn col) => recipe.PawnSatisfiesSkillRequirements(col)))
                            {
                                Bill.CreateNoPawnsWithSkillDialog(recipe);
                            }
                            Bill bill2 = recipe.MakeNewBill();
                            this.SelTable.billStack.AddBill(bill2);
                            if (recipe.conceptLearned != null)
                            {
                                PlayerKnowledgeDatabase.KnowledgeDemonstrated(recipe.conceptLearned, KnowledgeAmount.Total);
                            }
                            if (TutorSystem.TutorialMode)
                            {
                                TutorSystem.Notify_Event("AddBill-" + recipe.LabelCap.Resolve());
                            }
                        }, recipe.ProducedThingDef, MenuOptionPriority.Default, null, null, 29f, delegate (Rect rect)
                        {
                            if (recipe.products.Count == 1)
                            {
                                ThingDef thingDef = recipe.products[0].thingDef;
                                return Widgets.InfoCardButton(rect.x + 5f, rect.y + (rect.height - 24f) / 2f, thingDef, GenStuff.DefaultStuffFor(thingDef));
                            }
                            return Widgets.InfoCardButton(rect.x + 5f, rect.y + (rect.height - 24f) / 2f, recipe);
                        }));
                    }
                }
                if (!list.Any())
                {
                    list.Add(new FloatMenuOption("NoneBrackets".Translate(), null));
                }
                return list;
            };
            mouseoverBill = SelTable.billStack.DoListing(rect3, recipeOptionsMaker, ref scrollPosition, ref viewHeight);
        }

        public override void TabUpdate()
        {
            if (this.mouseoverBill != null)
            {
                this.mouseoverBill.TryDrawIngredientSearchRadiusOnMap(this.SelTable.Position);
                this.mouseoverBill = null;
            }
        }

        private float viewHeight = 1000f;

        private Vector2 scrollPosition;

        private Bill mouseoverBill;

        private static readonly Vector2 WinSize = new Vector2(420f, 480f);

        [TweakValue("Interface", 0f, 128f)]
        private static float PasteX = 48f;

        [TweakValue("Interface", 0f, 128f)]
        private static float PasteY = 3f;

        [TweakValue("Interface", 0f, 32f)]
        private static float PasteSize = 24f;

       
    }


}


