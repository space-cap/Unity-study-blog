using System.ComponentModel;

namespace Exercise
{
    class Graph
    {
        private int[,] adj = new int[6, 6]
        {
            {0, 1, 0, 1, 0, 0},
            {1, 0, 1, 1, 0, 0},
            {0, 1, 0, 0, 0, 0},
            {1, 1, 0, 0, 1, 0},
            {0, 0, 0, 1, 0, 1},
            {0, 0, 0, 0, 1, 0},
        };

        private List<int>[] adj2 = new List<int>[]
        {
            new List<int>() {1, 2},
            new List<int>() {0, 2, 3},
            new List<int>() {1},
            new List<int>() {0, 1,4},
            new List<int>() {3,5},
            new List<int>() {4},
        };


        public void BFS(int start)
        {
            bool[] found = new bool[6];
            int[] parent = new int[6];
            int[] distance = new int[6];

            Queue<int> q = new Queue<int>();
            q.Enqueue(start);
            found[start] = true;
            parent[start] = start;
            distance[start] = 0;

            while (q.Count > 0)
            {
                int now = q.Dequeue();
                Console.WriteLine(now);

                for (int next = 0; next < 6; next++)
                {
                    if (adj[now, next] == 0)
                    {
                        continue;
                    }

                    if (found[next])
                    {
                        continue;
                    }

                    q.Enqueue(next);
                    found[next] = true;
                    parent[next] = now;
                    distance[next] = distance[now] + 1;
                }
            }
        }



    }


    public class Program
    {
        static void Main(string[] args)
        {
            // DFS(Depth First Search 깊이 우선 탐색)
            // BFS(Breadth First Search 너비 우선 탐색)

            Graph graph = new Graph();
            graph.BFS(0);
        }
    }
}
