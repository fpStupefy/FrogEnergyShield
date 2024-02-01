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
            var config = ModContent.GetInstance<FrogEnergyShieldClientConfig>();
            area = new UIElement();
            area.Left.Set(config.PositionX, 0f); 
            area.Top.Set(config.PositionY, 0f); 
            area.Width.Set(200, 0f); 
            area.Height.Set(60, 0f);

            EnergyBar = new UIImage(ModContent.Request<Texture2D>("FrogEnergyShield/UI/Images/EnergyBox")); 
            EnergyBar.Left.Set(0, 0f);
            EnergyBar.Top.Set(0, 0f);
            EnergyBar.Width.Set(200, 0f);
            EnergyBar.Height.Set(35, 0f);


            textE = new UIText("", 0.8f);
            textE.Width.Set(150, 0f);
            textE.Height.Set(35, 0f);
            textE.Top.Set(40, 0f);
            textE.Left.Set(-10, 0f);

            textCD = new UIText("", 0.8f);
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
            var modPlayer = Main.LocalPlayer.GetModPlayer<FrogEnergyShieldModPlayer>();
            if (modPlayer.ShieldOn == false)
                return;
            var config = ModContent.GetInstance<FrogEnergyShieldClientConfig>();
            if (area.Left.Pixels != config.PositionX)
            {
                area.Left.Pixels = config.PositionX;
            }
            if (area.Top.Pixels != config.PositionY)
            {
                area.Top.Pixels = config.PositionY;
            }

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
            var modPlayer = Main.LocalPlayer.GetModPlayer<FrogEnergyShieldModPlayer>();
            var config = ModContent.GetInstance<FrogEnergyShieldClientConfig>();
            if (modPlayer.ShieldOn == false)
                return;


            if (config.wordshowcase == false)
            {
                textE.SetText("");
                textCD.SetText("");
            }
            else
            {
                textE.SetText(FrogEnergyShieldSystem.EnergyTEXT.Format((int)modPlayer.ShieldEnergy, (int)modPlayer.ShieldEnergyMax));
                textCD.SetText(FrogEnergyShieldSystem.CDTEXT.Format(Math.Round(modPlayer.cooldown / 60d, 2)));
                if (modPlayer.cooldown == 0)
                {
                    textCD.SetText("Ready");
                }
            }
            base.Update(gameTime);
        }
    }

}
