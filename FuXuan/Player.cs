using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace FuXuan
{
    class Dummy
    {
        public double maxHp;
        public double hp;
        public double def;
        public double damageReduction;
        public void initValue(double inHp, double inDef)
        {
            maxHp = inHp;
            hp = maxHp;
            def = inDef;
            damageReduction = 1.0 - 0.18;
        }
        public void GetHp(double hp)
        {
            this.hp += hp;
            if(this.hp>maxHp) this.hp = maxHp;
        }
    }
    class Player : Creature
    {
        public Dummy dummy;
        public double maxEnergy;
        public double energyRegenRate;
        public double energy { get; set; }
        public double shield;
        public int texBuf;
        public int texCool;
        public double baseAtk;
        public double baseDef;
        public double baseMaxHp;
        public double baseSpeed;
        public double damageReduction;
        public double shieldScale;
        public double healingBoost;
        // 피격 후 실드가 없으면 최대hp 32% 실드 획득, 지속2턴 쿨3턴, 실드 보유시 피감 24%
        public bool isTex;
        // 턴 시작시 체력 절반 이하이면 8% 회복, 에너지 5회복; ok
        public bool isGuard4;            
        // 궁관진 : hp 최대치 6% 증가; ok
        public bool isSkill { get; set; }
        public int traceStack;
        public int currentAction { get; set; }
        public double settingHp;
        public bool isDummy;

        public Player()
        {
            // 부현 스탯. 다른 캐릭 구현 안 됨
            atk = 465.7;
            def = 606.37;
            maxHp = 1475.0;
            speed = 100.0;
            baseSpeed = speed;
            shield = 0;
            maxEnergy = 135.0;
            energyRegenRate = 1;
            energy = maxEnergy / 2.0;
            texBuf = 0;
            texCool = 0;
            damageReduction = 1.0-0.18;
            shieldScale = 1.0;
            healingBoost = 1.0;
            isTex = false;
            isGuard4 = false;
            currentAction = 0;
            traceStack = 1;
            dummy = new Dummy();
            isDummy = false;
        }
        public void ApplyLightCone(string l)
        {
            switch(l)
            {
                case "그녀는 두 눈을 감았네":
                    SetBaseStat(423.36, 529.2, 1270.0);
                    maxHp += baseMaxHp * 0.24;
                    energyRegenRate += 0.12;
                    break;
                case "기억의 소재":
                    SetBaseStat(423.36, 529.2, 1058);
                    isTex = true;
                    break;
            }
        }
        public void ApplyEquipment(string e)
        {
            switch (e)
            {
                case "철위대4":
                    damageReduction *= 1 - 0.08;
                    isGuard4 = true;
                    break;
                case "팔라딘4":
                    def += baseDef * 0.15;
                    shieldScale += 0.2;
                    break;
                default:
                    if (e.Contains("철위대"))
                    {
                        damageReduction *= 1 - 0.08;
                    }
                    if (e.Contains("팔라딘"))
                    {
                        def += baseDef * 0.15;
                    }
                    if (e.Contains("과객"))
                    {
                        healingBoost += 0.1;
                    }
                    if (e.Contains("장수"))
                    {
                        maxHp += baseMaxHp * 0.12;
                    }
                    if (e.Contains("메신저"))
                    {
                        speed += baseSpeed * 0.06;
                    }
                    break;
            }
        }
        public void ApplyBody(string b)
        {
            switch (b)
            {
                case "방어력%":
                    def += baseDef * 0.54;
                    break;
                case "HP%":
                    maxHp += baseMaxHp * 0.432;
                    break;
                case "치유력 보너스":
                    healingBoost += 0.3456;
                    break;
            }
        }
        public void ApplyFeet(string f)
        {
            switch (f)
            {
                case "속도":
                    speed += 25.03;
                    break;
                case "방어력%":
                    def += baseDef * 0.54;
                    break;
                case "HP%":
                    maxHp += baseMaxHp * 0.432;
                    break;
            }
        }
        public void ApplySphere(string s)
        {
            switch (s)
            {
                case "방어력%":
                    def += baseDef * 0.54;
                    break;
                case "HP%":
                    maxHp += baseMaxHp * 0.432;
                    break;
            }
        }
        public void ApplyRope(string t)
        {
            switch (t)
            {
                case "방어력%":
                    def += baseDef * 0.54;
                    break;
                case "HP%":
                    maxHp += baseMaxHp * 0.432;
                    break;
                case "에너지회복효율":
                    energyRegenRate += 0.1944;
                    break;
            }
        }
        public void ApplySubstat(int subDef, int subHp, int subSpeed)
        {
            def += subDef * baseDef * 0.0486;
            maxHp += subHp * baseMaxHp * 0.0389;
            speed += subSpeed * 2.3;
        }
        public void SetBaseStat(double atk, double def, double maxHp)
        {
            this.atk += atk;
            this.def += def;
            this.maxHp += maxHp;
            baseAtk = this.atk;
            baseDef = this.def;
            baseMaxHp = this.maxHp;
        }
        public void SetFirstState()
        {
            // 행적 hp%
            maxHp += baseMaxHp * 0.18;
            // 불로인
            maxHp += baseMaxHp * 0.12;
            // 모자
            maxHp += 705.6;
            // 장갑
            atk += 352.8;


            settingHp = maxHp;

            hp = maxHp;
            isSkill = false;
            energy = maxEnergy / 2.0;
        }
        public void GetEnergy(double e)
        {
            energy += e * energyRegenRate;
        }
        public void GetDamage(Enemy enemy)
        {
            double damage = enemy.atk * enemy.def / (enemy.def + def) * damageReduction;
            if (shield >= damage)
                shield -= damage;
            else
            {
                damage -= shield;
                shield = 0;
                hp -= damage;
            }
            GetEnergy(10);
        }
        public void GetDamageWithDummy(Enemy enemy)
        {
            double dummyDamage, playerDamage;
            if (isSkill)
            {
                dummyDamage = 0.35 * enemy.atk * enemy.def / (enemy.def + dummy.def) * dummy.damageReduction;
                playerDamage = 2.95 * enemy.atk * enemy.def / (enemy.def + def) * damageReduction;
            }
            else
            {
                dummyDamage = enemy.atk * enemy.def / (enemy.def + dummy.def) * dummy.damageReduction;
                playerDamage = enemy.atk * enemy.def / (enemy.def + def) * damageReduction;
            }
            
            if (shield >= playerDamage)
                shield -= playerDamage;
            else
            {
                playerDamage -= shield;
                shield = 0;
                //texBuf = 0;
                hp -= playerDamage;
            }

            dummy.hp -= dummyDamage;

            GetEnergy(5);
        }
        public void TexCheck()
        {
            if(isTex && texCool==0 && shield <= 0.0001)
            {
                texBuf = 2;
                texCool = 3;
                shield = maxHp * 0.32 * shieldScale;
            }
            if(isTex && texBuf > 0 && shield <= 0.0001)
            {
                texBuf = 0;
            }
        }
        public void TraceCheck()
        {
            if (hp > 0 && hp <= (maxHp / 2) && traceStack > 0)
            {
                GetHp((maxHp - hp) * 0.9 * healingBoost);
                traceStack--;
            }
        }
        public void Guard4Check()
        {
            if (hp <= (maxHp / 2) && hp > 0 && isGuard4)
            {
                // 철위대4
                GetHp(maxHp * 0.08 * healingBoost);
                GetEnergy(5);
            }
        }
        public void BufTurnReduce()
        {
            if(isTex)
            {
                if (texBuf > 0) texBuf--;
                if (texCool > 0) texCool--;
                if (texBuf == 0) shield = 0;
                if (shield <= 0)
                {
                    texBuf = 0;
                    shield = 0;
                }
            }
        }
        public void UltCheck()
        {
            if(energy >= maxEnergy && traceStack < 2)
            {
                energy = 0;
                GetEnergy(5);
                traceStack = (traceStack + 1) % 3;
                if(isDummy) dummy.GetHp((settingHp * 0.05 + 133)*healingBoost);
                TraceCheck();
            }
        }
        public void initValue()
        {
            energy = maxEnergy / 2.0;
            shield = 0;
            texBuf = 0;
            texCool = 0;
            isSkill = false;
            traceStack = 1;
            currentAction = 0;
            maxHp = settingHp;
            hp = maxHp;
        }
    }
}
