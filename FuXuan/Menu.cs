using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuXuan
{
    enum MenuMode
    {
        Lobby,
        Select
    }
    class Setting
    {
        // 광추, 세트, 옷, 신발, 구슬, 매듭, 부옵
        public List<string> lightCone = new List<string>() { "그녀는 두 눈을 감았네", "기억의 소재" };
        public List<string> equipment = new List<string>() { "철위대4", "팔라딘4", "철위대2팔라딘2", "철위대2과객2", "철위대2장수2", "철위대2메신저2", "팔라딘2과객2", "팔라딘2장수2", "팔라딘2메신저2", "과객2장수2", "과객2메신저2", "장수2메신저2" };
        public List<string> body = new List<string>() { "방어력%", "HP%", "치유력 보너스" };
        public List<string> feet = new List<string>() { "속도", "HP%", "방어력%" };
        public List<string> sphere = new List<string>() { "방어력%", "HP%" };
        public List<string> rope = new List<string>() { "방어력%", "HP%", "에너지회복효율" };
        public int maxSubStat = 24;

        public Setting CopySetting()
        {
            Setting setting = new Setting();
            setting.lightCone = lightCone;
            setting.equipment = equipment;
            setting.body = body;
            setting.feet = feet;
            setting.sphere = sphere;
            setting.rope = rope;
            setting.maxSubStat = maxSubStat;
            return setting;
        }
    }
    class Result
    {
        public string lightCone;
        public string equipment;
        public string body;
        public string feet;
        public string sphere;
        public string rope;
        public int subDef, subAtk, subSpeed, criticalAttackPoint;
        public void initResult(int c, string l, string e, string b, string f, string s, string r, int subD, int subA, int subS)
        {
            criticalAttackPoint = c;
            lightCone = l;
            equipment = e;
            body = b;
            feet = f;
            sphere = s;
            rope = r;
            subDef = subD;
            subAtk = subA;
            subSpeed = subS;
        }
    }
    class Menu
    {
        public MenuMode mode;
        public List<Result> results = new List<Result>();
        public List<Result> resultsWithDummy = new List<Result>();
        public Menu()
        {
            mode = MenuMode.Lobby;
        }
        public bool Process()
        {
            bool exitFlag = false;
            switch(mode)
            {
                case MenuMode.Lobby:
                    exitFlag = Lobby();
                    break;
                case MenuMode.Select:
                    Setting setting = Select();
                    Iteration(setting);
                    PrintResults();
                    Console.WriteLine();
                    mode = MenuMode.Lobby;
                    break;
            }
            return exitFlag;
        }
        public bool Lobby()
        {
            string? input;

            Console.WriteLine("// 메뉴 입력시 숫자 하나 치고 엔터");
            while (true)
            {
                Console.WriteLine("[1] 세팅별 계산");
                Console.WriteLine("[2] 나가기");
                input = Console.ReadLine();
                if (input == "1" || input == "2") break;
            }
            bool exitFlag = false;
            switch(input)
            {
                case "1":
                    mode = MenuMode.Select;
                    break;
                case "2":
                    exitFlag = true;
                    break;
            }
            return exitFlag;
        }
        public Setting Select()
        {
            Setting setting = new Setting();
            string? input;
            string temp;
            int inputInt;
            
            // 광추
            while (true)
            {
                Console.WriteLine("광추를 선택하세요.");
                for (int i=0;i<setting.lightCone.Count;i++)
                {
                    Console.WriteLine($"[{i+1}] {setting.lightCone[i]}");
                }
                Console.WriteLine($"[{setting.lightCone.Count+1}] 모두 포함");
                input = Console.ReadLine();
                inputInt = Convert.ToInt32( input );
                if(inputInt >=1 && inputInt <= (setting.lightCone.Count + 1) ) break;
            }
            inputInt--;
            if (inputInt < setting.lightCone.Count)
            {
                temp = setting.lightCone[inputInt];
                setting.lightCone.Clear();
                setting.lightCone.Add(temp);
            }
            // 유물
            while (true)
            {
                Console.WriteLine("유물세트를 선택하세요.");
                for (int i = 0; i < setting.equipment.Count; i++)
                {
                    Console.WriteLine($"[{i + 1}] {setting.equipment[i]}");
                }
                Console.WriteLine($"[{setting.equipment.Count + 1}] 모두 포함");
                input = Console.ReadLine();
                inputInt = Convert.ToInt32(input);
                if (inputInt >= 1 && inputInt <= (setting.equipment.Count + 1)) break;
            }
            inputInt--;
            if (inputInt < setting.equipment.Count)
            {
                temp = setting.equipment[inputInt];
                setting.equipment.Clear();
                setting.equipment.Add(temp);
            }
            // 몸통
            while (true)
            {
                Console.WriteLine("몸통의 주옵션를 선택하세요.");
                for (int i = 0; i < setting.body.Count; i++)
                {
                    Console.WriteLine($"[{i+1}] {setting.body[i]}");
                }
                Console.WriteLine($"[{setting.body.Count + 1}] 모두 포함");
                input = Console.ReadLine();
                inputInt = Convert.ToInt32(input);
                if (inputInt >= 1 && inputInt <= (setting.body.Count + 1)) break;
            }
            inputInt--;
            if (inputInt < setting.body.Count)
            {
                temp = setting.body[inputInt];
                setting.body.Clear();
                setting.body.Add(temp);
            }
            // 신발
            while (true)
            {
                Console.WriteLine("신발의 주옵션를 선택하세요.");
                for (int i = 0; i < setting.feet.Count; i++)
                {
                    Console.WriteLine($"[{i + 1}] {setting.feet[i]}");
                }
                Console.WriteLine($"[{setting.feet.Count + 1}] 모두 포함");
                input = Console.ReadLine();
                inputInt = Convert.ToInt32(input);
                if (inputInt >= 1 && inputInt <= (setting.feet.Count + 1)) break;
            }
            inputInt--;
            if (inputInt < setting.feet.Count)
            {
                temp = setting.feet[inputInt];
                setting.feet.Clear();
                setting.feet.Add(temp);
            }
            // 구슬
            while (true)
            {
                Console.WriteLine("구슬의 주옵션를 선택하세요.");
                for (int i = 0; i < setting.sphere.Count; i++)
                {
                    Console.WriteLine($"[{i + 1}] {setting.sphere[i]}");
                }
                Console.WriteLine($"[{setting.sphere.Count + 1}] 모두 포함");
                input = Console.ReadLine();
                inputInt = Convert.ToInt32(input);
                if (inputInt >= 1 && inputInt <= (setting.sphere.Count + 1)) break;
            }
            inputInt--;
            if (inputInt < setting.sphere.Count)
            {
                temp = setting.sphere[inputInt];
                setting.sphere.Clear();
                setting.sphere.Add(temp);
            }
            // 매듭
            while (true)
            {
                Console.WriteLine("매듭의 주옵션를 선택하세요.");
                for (int i = 0; i < setting.rope.Count; i++)
                {
                    Console.WriteLine($"[{i + 1}] {setting.rope[i]}");
                }
                Console.WriteLine($"[{setting.rope.Count + 1}] 모두 포함");
                input = Console.ReadLine();
                inputInt = Convert.ToInt32(input);
                if (inputInt >= 1 && inputInt <= (setting.rope.Count + 1)) break;
            }
            inputInt--;
            if (inputInt < setting.rope.Count)
            {
                temp = setting.rope[inputInt];
                setting.rope.Clear();
                setting.rope.Add(temp);
            }
            // 부옵갯수
            Console.WriteLine("원하는 부옵 갯수를 입력하세요.");
            input = Console.ReadLine();
            inputInt = Convert.ToInt32(input);
            setting.maxSubStat = inputInt;

            results.Clear();
            resultsWithDummy.Clear();

            return setting;
        }
        public void Iteration(Setting setting)
        {
            int dumHp, dumDef;
            while (true)
            {
                Console.WriteLine("부현 외 아군의 HP를 적고 엔터를 눌러주세요.(ex. 2866)");
                string input = Console.ReadLine();
                if (int.TryParse(input, out dumHp))
                {
                    break;
                }
            }
            while (true)
            {
                Console.WriteLine("부현 외 아군의 방어력를 적고 엔터를 눌러주세요.(ex. 991)");
                string input = Console.ReadLine();
                if (int.TryParse(input, out dumDef))
                {
                    break;
                }
            }
            

            Enemy enemy = new Enemy();
            foreach (string l in setting.lightCone)
            {
                foreach (string e in setting.equipment)
                {
                    foreach (string b in setting.body)
                    {
                        foreach (string f in setting.feet)
                        {
                            foreach (string s in setting.sphere)
                            {
                                foreach (string r in setting.rope)
                                {
                                    for (int i = 0; i <= setting.maxSubStat; i++)
                                    {
                                        for (int j = 0; j <= setting.maxSubStat - i; j++)
                                        {
                                            Player fuxuan = new Player();
                                            fuxuan.ApplyLightCone(l);
                                            fuxuan.ApplyEquipment(e);
                                            fuxuan.ApplyBody(b);
                                            fuxuan.ApplyFeet(f);
                                            fuxuan.ApplySphere(s);
                                            fuxuan.ApplyRope(r);
                                            fuxuan.ApplySubstat(i, j, setting.maxSubStat - i - j);
                                            fuxuan.SetFirstState();
                                            int criticalAttackPoint = 0;
                                            int k = 0;
                                            int iV = 1000;
                                            /*
                                            fuxuan.isDummy = false;
                                            
                                            
                                            while(true)
                                            {
                                                // init
                                                fuxuan.initValue();
                                                fuxuan.dummy.initValue(dumHp, dumDef);
                                                enemy.SetAtk(k);
                                                
                                                Battle battle = new Battle();
                                                int leftRound = battle.SustainRound(fuxuan, enemy);
                                                // Console.WriteLine($"버틴 턴 수 : {leftRound}, 공격력 : {k}");
                                                if (leftRound >= 10 && criticalAttackPoint <= k)
                                                {
                                                    criticalAttackPoint = k;
                                                }
                                                if (leftRound < 10)
                                                {
                                                    if (iV == 1) break;
                                                    else
                                                    {
                                                        k -= iV;
                                                        iV /= 10;
                                                    }
                                                }

                                                k += iV;
                                            }
                                            Result result = new Result();
                                            result.initResult(criticalAttackPoint, l, e, b, f, s, r, i, j, setting.maxSubStat - i - j);
                                            results.Add(result);
                                            //Console.WriteLine($"{criticalAttackPoint} / {l} / {e} / 옷:{b} / 신발: {f} / 구체: {s}/매듭: {r}/부옵 : {i}, {j}, {setting.maxSubStat - i - j} (방/HP/속)");
                                            */

                                            fuxuan.isDummy = true;
                                            criticalAttackPoint = 0;

                                            k = 0;
                                            iV = 1000;
                                            while(true)
                                            {
                                                // init
                                                fuxuan.initValue();
                                                fuxuan.dummy.initValue(dumHp, dumDef);
                                                enemy.SetAtk(k);
                                                
                                                Battle battle = new Battle();
                                                int leftRound = battle.SustainRound(fuxuan, enemy);
                                                // Console.WriteLine($"버틴 턴 수 : {leftRound}, 공격력 : {k}");
                                                if (leftRound >= 10 && criticalAttackPoint <= k)
                                                {
                                                    criticalAttackPoint = k;
                                                }
                                                if (leftRound < 10)
                                                {
                                                    if (iV == 1) break;
                                                    else
                                                    {
                                                        k -= iV;
                                                        iV /= 10;
                                                    }
                                                }
                                                k += iV;
                                            }
                                            Result resultWithDummy = new Result();
                                            resultWithDummy.initResult(criticalAttackPoint, l, e, b, f, s, r, i, j, setting.maxSubStat - i - j);
                                            resultsWithDummy.Add(resultWithDummy);
                                            //Console.WriteLine($"{criticalAttackPoint} / {l} / {e} / 옷:{b} / 신발: {f} / 구체: {s}/매듭: {r}/부옵 : {i}, {j}, {setting.maxSubStat - i - j} (방/HP/속)");
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        public void PrintResults()
        {
            int maxCount = 10;
            int i = 0;

            while(true)
            {
                Console.WriteLine("결과가 나왔습니다. 상위 몇번째까지 결과를 표시할까요?");
                string input = Console.ReadLine();
                if(int.TryParse(input, out maxCount))
                {
                    break;
                }
            }
            
            /*
            Console.WriteLine("1인의 경우");
            List<Result> sortedList = results.OrderByDescending(p => p.criticalAttackPoint).ToList();
            foreach(Result r in sortedList)
            {
                if (i >= maxCount) break;
                Console.WriteLine($"{r.criticalAttackPoint} / {r.lightCone} / {r.equipment} / 옷:{r.body} / 신발: {r.feet} / 구체: {r.sphere}/매듭: {r.rope}/부옵 : {r.subDef}, {r.subAtk}, {r.subSpeed} (방/HP/속)");
                i++;
            }
            */

            i = 0;
            Console.WriteLine();
            List<Result> sortedListWithDummy = resultsWithDummy.OrderByDescending(p => p.criticalAttackPoint).ToList();
            foreach (Result r in sortedListWithDummy)
            {
                if (i >= maxCount) break;
                Console.WriteLine($"{r.criticalAttackPoint} / {r.lightCone} / {r.equipment} / 옷:{r.body} / 신발: {r.feet} / 구체: {r.sphere}/매듭: {r.rope}/부옵 : {r.subDef}, {r.subAtk}, {r.subSpeed} (방/HP/속)");
                i++;
            }
        }
    }
}
