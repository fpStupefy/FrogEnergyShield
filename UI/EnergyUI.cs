using FrogEnergyShield;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.UI.Elements;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;

namespace FrogEnergyShields.UI
{
    
    internal class EnergyUI : UIState
    {
        
        private UIText textE;
        private UIText textCD;
        private UIElement area;
        private UIImage EnergyBar;
        private Color EnergyColor;

        public override void OnInitialize()
        {
            area = new UIElement();
            area.Left.Set(-area.Width.Pixels - 650, 1f); 
            area.Top.Set(30, 0f); 
            area.Width.Set(200, 0f); 
            area.Height.Set(60, 0f);

            EnergyBar = new UIImage(ModContent.Request<Texture2D>("FrogEnergyShield/UI/Images/EnergyBox")); 
            EnergyBar.Left.Set(0, 0f);
            EnergyBar.Top.Set(0, 0f);
            EnergyBar.Width.Set(200, 0f);
            EnergyBar.Height.Set(35, 0f);


            textE = new UIText("0/0", 0.8f);
            textE.Width.Set(150, 0f);
            textE.Height.Set(35, 0f);
            textE.Top.Set(40, 0f);
            textE.Left.Set(-10, 0f);

            textCD = new UIText("Ready", 0.8f);
            textCD.Width.Set(30, 0f);
            textCD.Height.Set(35, 0f);
            textCD.Top.Set(40, 0f);
            textCD.Left.Set(140, 0f);

            EnergyColor = new Color(112, 154, 209);

            area.Append(textE);
            area.Append(textCD);
            area.Append(EnergyBar);
            Append(area);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (FrogEnergyShieldModPlayer.ShieldOn == false)
                return;

            base.Draw(spriteBatch);
        }

        
        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            base.DrawSelf(spriteBatch);

            var modPlayer = Main.LocalPlayer.GetModPlayer<FrogEnergyShieldModPlayer>();
            
            float Equotient = (float)modPlayer.ShieldEnergy / modPlayer.ShieldEnergyMax; 
            Equotient = Utils.Clamp(Equotient, 0f, 1f);

            Rectangle hitbox = EnergyBar.GetInnerDimensions().ToRectangle();
            hitbox.X += 46;
            hitbox.Width -= 46;
            hitbox.Y += 7;
            hitbox.Height -= 15;

            int Eleft = hitbox.Left;
            int Eright = hitbox.Right;
            int Esteps = (int)((Eright - Eleft) * Equotient);
            for (int i = 0; i < Esteps; i += 1)
            {
                spriteBatch.Draw(TextureAssets.MagicPixel.Value, new Rectangle(Eleft + i, hitbox.Y, 1, hitbox.Height), EnergyColor);
            }
        }

        public override void Update(GameTime gameTime)
        {
            if (FrogEnergyShieldModPlayer.ShieldOn == false)
                return;

            var modPlayer = Main.LocalPlayer.GetModPlayer<FrogEnergyShieldModPlayer>();
            textE.SetText(FrogEnergyShieldSystem.EnergyTEXT.Format((int)modPlayer.ShieldEnergy, (int)modPlayer.ShieldEnergyMax));
            textCD.SetText(FrogEnergyShieldSystem.CDTEXT.Format(Math.Round(modPlayer.cooldown/60d,2))); 
            if (modPlayer.cooldown == 0)
            {
                textCD.SetText("Ready");
            }
            base.Update(gameTime);
        }
    }

}
