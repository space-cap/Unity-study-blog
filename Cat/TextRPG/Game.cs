using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    public enum GameMode
    {
        None,
        Lobby,
        Town,
        Field
    }

    class Game
    {
        private GameMode mode = GameMode.Lobby;
        private Player player = null;
        private Monster monster = null;
        Random random = new Random();

        public void Process()
        {
            switch (mode)
            {
                case GameMode.Lobby:
                    ProcessLobby();
                    break;
                case GameMode.Town:
                    ProcessTown();
                    break;
                case GameMode.Field:
                    ProcessField();
                    break;
            }
        }

        private void ProcessLobby()
        {
            Console.WriteLine("직업을 선택하세요.");
            Console.WriteLine("1. 기사");
            Console.WriteLine("2. 궁수");
            Console.WriteLine("3. 법사");

            string input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    player = new Knight();
                    Console.WriteLine("기사를 선택했습니다.");
                    mode = GameMode.Town;
                    break;
                case "2":
                    player = new Archer();
                    Console.WriteLine("궁수를 선택했습니다.");
                    mode = GameMode.Town;
                    break;
                case "3":
                    player = new Mage();
                    Console.WriteLine("법사를 선택했습니다.");
                    mode = GameMode.Town;
                    break;
            }
        }
        private void ProcessTown()
        {
            Console.WriteLine("마을에 입장했습니다!");
            Console.WriteLine("1. 필드로 가기");
            Console.WriteLine("2. 로비로 돌아가기");

            string input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    mode = GameMode.Field;
                    break;
                case "2":
                    mode = GameMode.Lobby;
                    break;
            }
        }
        private void ProcessField()
        {
            Console.WriteLine("필드에 입장했습니다!");
            Console.WriteLine("1. 전투하기");
            Console.WriteLine("2. 마을로 돌아가기");

            CreateRandMonster();

            string input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    ProcessFight();
                    break;
                case "2":
                    mode = GameMode.Town;
                    break;
            }
        }

        private void ProcessFight()
        {
            while (true)
            {
                int damage = player.GetAttack();
                monster.OnDamaged(damage);
                if (monster.IsDead())
                {
                    Console.WriteLine("승리");
                    Console.WriteLine($"남은 체력{player.GetHp()}");
                    mode = GameMode.Lobby;
                    break;
                }

                damage = monster.GetAttack();
                player.OnDamaged(damage);
                if (player.IsDead())
                {
                    Console.WriteLine("패배");
                    mode = GameMode.Lobby;
                    break;
                }
            }
        }

        private void CreateRandMonster()
        {
            int randValue = random.Next(0, 3);
            switch (randValue)
            {
                case 0:
                    monster = new Slime();
                    Console.WriteLine("슬라임 생성");
                    break;
                case 1:
                    monster = new Orc();
                    Console.WriteLine("오크 생성");
                    break;
                case 2:
                    monster = new Skeleton();
                    Console.WriteLine("스켈레톤 생성");
                    break;
            }
        }
    }
}
