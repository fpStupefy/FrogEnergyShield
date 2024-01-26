using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;

namespace FrogEnergyShield.UI
{
    internal class EnergyUI : UIState
    {
        public UIText ShieldEnergy;
        public UIText cooldown;

        public override void OnInitialize()
        {
            ShieldEnergy = new UIText("0", 1.0f);
            ShieldEnergy.Left.Set(925, 0f);
            ShieldEnergy.Top.Set(604, 0f);
            Append(ShieldEnergy);
            cooldown = new UIText("0", 1.0f);
            cooldown.Left.Set(925, 0f);
            cooldown.Top.Set(650, 0f);
            Append(cooldown);
        }

        public override void Update(GameTime gameTime)
        {
            ShieldEnergy.SetText("ENERGY" + "\r\n" + FrogEnergyShieldModPlayer.ShieldEnergy.ToString() + "/" + FrogEnergyShieldModPlayer.ShieldEnergyMax.ToString());

            double a = Math.Round(FrogEnergyShieldModPlayer.cooldown / 60.0d,2);
            cooldown.SetText(a.ToString() + "s");

            base.Update(gameTime);
        }
    }
}
