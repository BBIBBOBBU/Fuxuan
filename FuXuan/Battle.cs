using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuXuan
{
    enum CurrentPlay
    {
        Player,
        Enemy
    }
    class Battle
    {
        CurrentPlay currentPlay;
        // 100라운드
        private double totalActionValue = 10050.0;

        public Battle()
        {
            totalActionValue = 10050.0;
        }

        public int SustainRound(Player player, Enemy enemy)
        {
            int sRound;
            player.ActionGauge = 10000.0;
            enemy.ActionGauge = 10000.0;
            double reducedActionValue;
            while (true)
            {
                if (player.actionValue <= enemy.actionValue)
                {
                    // 아군 먼저
                    currentPlay = CurrentPlay.Player;
                    enemy.ActionGauge -= player.actionValue * enemy.speed;
                    reducedActionValue = player.actionValue;
                    player.ActionGauge = 10000.0;
                }
                else
                {
                    // 적 먼저
                    currentPlay = CurrentPlay.Enemy;
                    player.ActionGauge -= enemy.actionValue * player.speed;
                    reducedActionValue = enemy.actionValue;
                    enemy.ActionGauge = 10000.0;
                }
                bool endFlag = reduceTotalValue(reducedActionValue);
                if (endFlag) { return 100; }
                
                bool isDead = DamageProcess(player, enemy, currentPlay);
                if (isDead)
                {
                    sRound = 100 - (int)Math.Ceiling(totalActionValue / 100.0);
                    break;
                }
                AfterProcess(player, enemy, currentPlay);
                
            }
            return sRound;
        }

        public bool DamageProcess(Player player, Enemy enemy, CurrentPlay currentPlay)
        {
            if (currentPlay == CurrentPlay.Player)
            {
                player.Guard4Check();
                player.UltCheck();
                if(player.currentAction == 0)
                {
                    // 전스
                    if (!player.isSkill)
                    {
                        player.isSkill = true;
                        player.maxHp *= 1.06;
                        player.hp *= 1.06;
                        player.GetEnergy(30);
                        if (player.isDummy)
                        {
                            player.dummy.maxHp *= 1.06;
                            player.dummy.hp *= 1.06;
                        }
                    }
                    else { player.GetEnergy(50); }
                }
                else
                {
                    // 평타
                    player.GetEnergy(20);
                }
                player.currentAction = (player.currentAction + 1) % 3;
            }
            else
            {
                if (player.isDummy)
                    player.GetDamageWithDummy(enemy);
                else
                    player.GetDamage(enemy);

                if (player.hp <= 0)
                {
                    if(player.isDummy)
                    {
                        //Console.WriteLine($"더미 체력 : {player.dummy.hp}/{player.dummy.maxHp}");
                    }
                    return true;
                }
                if (player.isDummy && player.dummy.hp <= 0)
                {
                    //Console.WriteLine("더미로 인한 패배");
                    return true;
                }

                player.TraceCheck();
                player.TexCheck();
            }
            return false;
        }

        public void AfterProcess(Player player, Enemy enemy, CurrentPlay currentPlay)
        {
            player.UltCheck();
            if (currentPlay == CurrentPlay.Player)
            {
                player.BufTurnReduce();
            }
        }

        public bool reduceTotalValue(double actionValue)
        {
            totalActionValue -= actionValue;
            if (totalActionValue <= 0) return true;
            else return false;
        }
    }
}
