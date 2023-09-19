using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;

namespace FuXuan
{
    class Enemy : Creature
    {
        public Enemy()
        {
            // atk는 초기값
            atk = 500;
            def = 1000;
            speed = 144;
            maxHp = 0;
            hp = 0;
        }
        public void SetAtk(double atk)
        {
            this.atk = atk;
        }
    }
}
