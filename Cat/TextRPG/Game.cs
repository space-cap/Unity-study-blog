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
        private GameMode _mode = GameMode.Lobby;
        private Player? _player = null;
        private Monster? _monster = null;
        private readonly Random _random = new Random();

        public void Process()
        {
            switch (_mode)
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
                    _player = new Knight();
                    Console.WriteLine("기사를 선택했습니다.");
                    _mode = GameMode.Town;
                    break;
                case "2":
                    _player = new Archer();
                    Console.WriteLine("궁수를 선택했습니다.");
                    _mode = GameMode.Town;
                    break;
                case "3":
                    _player = new Mage();
                    Console.WriteLine("법사를 선택했습니다.");
                    _mode = GameMode.Town;
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
                    _mode = GameMode.Field;
                    break;
                case "2":
                    _mode = GameMode.Lobby;
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
                    _mode = GameMode.Town;
                    break;
            }
        }

        private void ProcessFight()
        {
            if (_player == null || _monster == null)
            {
                _mode = GameMode.Lobby;
                return;
            }

            while (true)
            {
                int damage = _player.GetAttack();
                _monster.OnDamaged(damage);
                if (_monster.IsDead())
                {
                    Console.WriteLine("승리");
                    Console.WriteLine($"남은 체력{_player.GetHp()}");
                    _mode = GameMode.Lobby;
                    break;
                }

                damage = _monster.GetAttack();
                _player.OnDamaged(damage);
                if (_player.IsDead())
                {
                    Console.WriteLine("패배");
                    _mode = GameMode.Lobby;
                    break;
                }
            }
        }

        private void CreateRandMonster()
        {
            int randValue = _random.Next(0, 3);
            switch (randValue)
            {
                case 0:
                    _monster = new Slime();
                    Console.WriteLine("슬라임 생성");
                    break;
                case 1:
                    _monster = new Orc();
                    Console.WriteLine("오크 생성");
                    break;
                case 2:
                    _monster = new Skeleton();
                    Console.WriteLine("스켈레톤 생성");
                    break;
            }
        }
    }
}
