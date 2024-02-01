using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using tModPorter;

namespace FrogEnergyShield
{
    internal class FrogEnergyShieldModPlayer : ModPlayer
    {
        public int cooldown;
        public int cooldownMax;//仅用于UI的绘制
        public double ShieldEnergy;
        public double ShieldRegen;
        public int ShieldEnergyMax;
        public bool ShieldOn;
        public bool isDodge;


        public override void Initialize()//初始化
        {
            var config = ModContent.GetInstance<FrogEnergyShieldServerConfig>();
            ShieldOn = false;
            cooldown = 0;
            cooldownMax = config.CD;
            ShieldEnergy = 0;
            ShieldRegen = 0.5;
            ShieldEnergyMax = 0;
        }

        public override void OnEnterWorld()//角色进入世界时初始化
        {
            cooldown = 0;
            ShieldEnergy = ShieldEnergyMax;
        }

        public override void OnRespawn()//角色重生时初始化
        {
            cooldown = 0;
            ShieldEnergy = ShieldEnergyMax;
        }

        public override void ResetEffects()//每秒刷新60次
        {
            ShieldOn = false;
            var config = ModContent.GetInstance<FrogEnergyShieldServerConfig>();
            ShieldRegen = config.Regen / 60d;
            cooldownMax = config.CD * 60;
            if (config.MaxShield == "MaxLife")
            { ShieldEnergyMax = Player.statLifeMax; }
            else { ShieldEnergyMax = int.Parse(config.MaxShield); }
            cooldown -= 1;//CD每秒-60

            cooldown = Utils.Clamp(cooldown, 0, cooldownMax);//限制CD范围
            //避免更改设置后引起BUG
            if (cooldown > cooldownMax)
            {
                cooldown = cooldownMax;
            }
            //当CD为0且护盾值低于最大护盾值
            if (cooldown == 0)
            {
                if(ShieldEnergy < ShieldEnergyMax)
                {
                    ShieldEnergy += ShieldRegen;
                    ShieldEnergy = Utils.Clamp(ShieldEnergy, 0, ShieldEnergyMax);//限制护盾值范围
                }
                //避免更改设置后引起BUG
                if (ShieldEnergy > ShieldEnergyMax)
                {
                    ShieldEnergy = ShieldEnergyMax;
                }
            }
        }

        public override bool FreeDodge(Player.HurtInfo info)//返回 true 则玩家无敌
        {
            if(isDodge)
            {
                isDodge = false;
                return true;
            }
            return false;
        }

        public override void ModifyHurt(ref Player.HurtModifiers modifiers)//在玩家收到攻击但未结算伤害前调用
        {
            modifiers.ModifyHurtInfo += _OnHurt;//用于获取Hurtinfo中伤害相关的数据
        }


        private void _OnHurt(ref Player.HurtInfo info)
        {
            if (ShieldOn)
            {
                cooldown = cooldownMax;
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
                        info.Damage = info.SourceDamage - (int)ShieldEnergy;//伤害不能在ModifyHurt方法中进行修改，否则会导致修改作用于下一次受伤，而非本次。
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
