﻿
using FrogEnergyShield;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.UI.Elements;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;

namespace FrogEnergyShields.UI
{
    internal class CDUI : UIState
    {
        private UIElement area;
        private UIImage CDBar;
        private Color CDColor;

        public override void OnInitialize()
        {

            var config = ModContent.GetInstance<FrogEnergyShieldClientConfig>();
            area = new UIElement();
            area.Left.Set(config.PositionX, 0f);
            area.Top.Set(config.PositionY+35, 0f);
            area.Width.Set(155, 0f);
            area.Height.Set(6, 0f);

            CDBar = new UIImage(ModContent.Request<Texture2D>("FrogEnergyShield/UI/Images/CDBox"));
            CDBar.Left.Set(0, 0f);
            CDBar.Top.Set(0, 0f);
            CDBar.Width.Set(155, 0f);
            CDBar.Height.Set(6, 0f);

            CDColor = new Color(255, 235, 59);

            area.Append(CDBar);
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
                area.Top.Pixels = config.PositionY + 35;
            }
            base.Draw(spriteBatch);
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            base.DrawSelf(spriteBatch);

            var modPlayer = Main.LocalPlayer.GetModPlayer<FrogEnergyShieldModPlayer>();
            float CDquotient = (float)modPlayer.cooldown / modPlayer.cooldownMax;
            CDquotient = Utils.Clamp(CDquotient, 0f, 1f);

            Rectangle hitbox = CDBar.GetInnerDimensions().ToRectangle();
            hitbox.X += 3;
            hitbox.Width -= 12;
            hitbox.Y += 0;
            hitbox.Height -= 2;

            int Cleft = hitbox.Left;
            int Cright = hitbox.Right;
            int Csteps = (int)((Cright - Cleft) * CDquotient);
            for (int i = 0; i < Csteps; i += 1)
            {
                spriteBatch.Draw(TextureAssets.MagicPixel.Value, new Rectangle(Cleft + i, hitbox.Y, 1, hitbox.Height), CDColor);
            }
        }

        public override void Update(GameTime gameTime)
        {
            var modPlayer = Main.LocalPlayer.GetModPlayer<FrogEnergyShieldModPlayer>();
            if (modPlayer.ShieldOn == false)
                return;
            base.Update(gameTime);
        }
    }

}
