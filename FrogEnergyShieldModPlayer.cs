using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace FrogEnergyShield
{
    internal class FrogEnergyShieldModPlayer : ModPlayer
    {
        public static int OverDamage;
        public static int cooldown;
        public static double ShieldEnergy;
        public static double ShieldRegen;
        public static int ShieldEnergyMax;
        public static bool ShieldOn;
        public static bool isDodge;


        public override void Initialize()
        {
            ShieldOn = false;
            cooldown = 0;
            ShieldEnergy = 0;
            ShieldRegen = 0.5;
            ShieldEnergyMax = 0;
        }

        public override void OnEnterWorld()
        {
            cooldown = 0;
            ShieldEnergyMax = Player.statLifeMax;
            ShieldEnergy = ShieldEnergyMax;
        }

        public override void OnRespawn()
        {
            cooldown = 0;
            ShieldEnergyMax = Player.statLifeMax;
            ShieldEnergy = ShieldEnergyMax;
        }

        public override void ResetEffects()
        {
            ShieldOn = false;
            ShieldEnergyMax = Player.statLifeMax;
            cooldown -= 1;
            if (cooldown <= 0)
            {
                cooldown = 0;
            }
            if (cooldown == 0)
            {
                if(ShieldEnergy < ShieldEnergyMax)
                {
                    ShieldEnergy += ShieldRegen;
                    if (ShieldEnergy >= ShieldEnergyMax)
                    {
                        ShieldEnergy = ShieldEnergyMax;
                    }
                }
            }
        }

        public override bool FreeDodge(Player.HurtInfo info)
        {
            if(isDodge)
            {
                isDodge = false;
                return true;
            }
            return false;
        }

        public override void ModifyHurt(ref Player.HurtModifiers modifiers)
        {
            modifiers.ModifyHurtInfo += _OnHurt;
        }


        private void _OnHurt(ref Player.HurtInfo info)
        {
            if (ShieldOn)
            {
                cooldown = 6 * 60;
                if (ShieldEnergy > 0)
                {
                    
                    if (ShieldEnergy >= info.SourceDamage)
                    {
                        Player.immune = true;
                        Player.immuneNoBlink = false;
                        Player.immuneTime = 60;
                        ShieldEnergy -= info.SourceDamage;
                        isDodge = true;
                        return;
                    }
                    else
                    {
                        OverDamage = info.SourceDamage - (int)ShieldEnergy;
                        info.Damage = OverDamage;
                        ShieldEnergy = 0;
                        isDodge = false;
                        return;
                    }
                }
            }
            isDodge = false;
            
        }
    }
}
