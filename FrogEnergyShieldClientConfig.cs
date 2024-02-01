using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader.Config;

namespace FrogEnergyShield
{
    public class FrogEnergyShieldClientConfig : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ClientSide;

        [DefaultValue(true)]
        public bool wordshowcase;

        [DefaultValue(1270)]
        [Range(0, 1720)]
        public int PositionX;

        [DefaultValue(40)]
        [Range(0, 1039)]
        public int PositionY;
    }
}
