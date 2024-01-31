using FrogEnergyShields.UI;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;

namespace FrogEnergyShield
{

    [Autoload(Side = ModSide.Client)]
    internal class FrogEnergyShieldSystem : ModSystem
    {
        private UserInterface EnergyUserInterface;
        private UserInterface CDUserInterface;

        internal EnergyUI energyUI;
        internal CDUI cdUI;

        public static LocalizedText EnergyTEXT { get; private set; }
        public static LocalizedText CDTEXT { get; private set; }

        public override void Load()
        {
            energyUI = new();
            cdUI = new();
            EnergyUserInterface = new();
            CDUserInterface = new();
            EnergyUserInterface.SetState(energyUI);
            CDUserInterface.SetState(cdUI);

            string category = "UI";
            EnergyTEXT ??= Mod.GetLocalization($"{category}.ShieldEnergy");
            CDTEXT ??= Mod.GetLocalization($"{category}.CD");
        }

        public override void UpdateUI(GameTime gameTime)
        {
            EnergyUserInterface?.Update(gameTime);
            CDUserInterface?.Update(gameTime);
        }

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int resourceBarIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Resource Bars"));
            if (resourceBarIndex != -1)
            {
                layers.Insert(resourceBarIndex, new LegacyGameInterfaceLayer(
                    "ExampleMod: Example Resource Bar",
                    delegate {
                        EnergyUserInterface.Draw(Main.spriteBatch, new GameTime());
                        CDUserInterface.Draw(Main.spriteBatch, new GameTime());
                        return true;
                    },
                    InterfaceScaleType.UI)
                );
            }
        }
    }
}