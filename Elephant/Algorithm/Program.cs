﻿namespace Algorithm
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            const int WAIT_TICK = 1000 / 30;

            int lastTick = 0;
            while (true)
            {
                #region 프레임 관리
                // 만약에 경과한 시간이 1/30초보다 작다면
                int currentTick = System.Environment.TickCount;
                if (currentTick - lastTick < WAIT_TICK)
                    continue;
                int deltaTick = currentTick - lastTick;
                lastTick = currentTick;
                #endregion

                // 입력

                // 로직

                // 랜드링
                Console.SetCursorPosition(0, 0);
                Console.WriteLine("Hello, World!");
            }
            
        }
    }
}
