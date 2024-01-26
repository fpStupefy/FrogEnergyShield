using FrogEnergyShield.UI;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;

namespace FrogEnergyShield
{
    public class FrogEnergyShieldSystem : ModSystem
    {
        internal EnergyUI energyUI;
        internal UserInterface userInterface;

        public override void Load()
        {
            energyUI = new EnergyUI();
            energyUI.Activate();
            userInterface = new UserInterface();
            userInterface.SetState(energyUI);
            base.Load();
        }

        public override void UpdateUI(GameTime gameTime)
        {
            if (FrogEnergyShieldModPlayer.ShieldOn)
            {
                userInterface?.Update(gameTime);
            }
        }

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int mouseTextIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Text"));
            if (mouseTextIndex != -1)
            {
                layers.Insert(mouseTextIndex, new LegacyGameInterfaceLayer(
                    "ENERGY",
                    delegate
                    {
                        if (FrogEnergyShieldModPlayer.ShieldOn)
                        {
                            userInterface.Draw(Main.spriteBatch, new GameTime());
                        }
                        return true;
                    },
                    InterfaceScaleType.UI)
                );
            }
        }
    }
}