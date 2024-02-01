using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader.Config;

namespace FrogEnergyShield
{
    public class FrogEnergyShieldServerConfig : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ServerSide;

        [DrawTicks]
        [OptionStrings(new string[] { "MaxLife", "100", "200", "300" ,"400","500","600","700","800","900","1000"})]
        [DefaultValue("MaxLife")]
        public string MaxShield;

        [DefaultValue(30)]
        [Increment(10)]
        [Range(10, 100)]
        public int Regen;

        [DefaultValue(6)]
        [Range(1,10)]
        public int CD;


    }
}
