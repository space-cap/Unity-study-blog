using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        // 원래 배열 생성
        int[] originalArray = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

        // 원래 배열에서 ArraySegment 생성
        ArraySegment<int> segment = new ArraySegment<int>(originalArray, 2, 5);

        // 세그먼트의 요소에 접근
        Console.WriteLine("세그먼트 요소:");
        for (int i = 0; i < segment.Count; i++)
        {
            Console.WriteLine(segment.Array[segment.Offset + i]);
        }

        // 원래 배열 수정
        originalArray[3] = 42;

        // 원래 배열을 수정하면 세그먼트에도 반영됨
        Console.WriteLine("\n원래 배열 수정 후 세그먼트 요소:");
        foreach (var item in segment)
        {
            Console.WriteLine(item);
        }

        // 세그먼트를 반복하는 또 다른 방법
        Console.WriteLine("\nforeach를 사용한 세그먼트 요소:");
        foreach (var item in segment)
        {
            Console.WriteLine(item);
        }
    }
}