using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuXuan
{
    internal class Creature
    {
        public double maxHp { get; set; }
        public double hp { get; set; }
        public double atk { get; set; }
        public double def { get; set; }
        public double speed { get; set; }
        public double actionGauge;
        public double ActionGauge
        {
            get { return actionGauge; }
            set
            {
                actionGauge = value;
                actionValue = actionGauge / speed;
            }
        }
        public double actionValue { get; set; }

        public Creature()
        {
            actionGauge = 0.0;
        }
        public void GetHp(double hp)
        {
            this.hp += hp;
            if (this.hp > maxHp) { this.hp = maxHp; }
        }
    }
}
