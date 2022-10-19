using System;
using System.Collections.Generic;
using System.Drawing;

class Programm
{
    static bool CheckWord(char[,] table, List<Point> position, string word, int numLetter)
    {
        var x = position.Last().X;
        var y = position.Last().Y;

        var newPosition = new Point(x + 1, y);
        CheckPosition(table, position, newPosition, word, numLetter);

        newPosition = new Point(x, y + 1);
        CheckPosition(table, position, newPosition, word, numLetter);

        newPosition = new Point(x - 1, y);
        CheckPosition(table, position, newPosition, word, numLetter);

        newPosition = new Point(x, y - 1);
        CheckPosition(table, position, newPosition, word, numLetter);

        if (position.Count == word.Length)
            return true;
        else
        {
            position.Remove(position.Last());
            return false;
        }
    }

    static void PrintPath(char[,] table, List<Point> position)
    {
        var tab = (char[,])table.Clone();

        foreach(var point in position)
            tab[point.X, point.Y] = char.ToUpper(tab[point.X, point.Y]);

        for (int i = 0; i < tab.GetLength(0); i++)
        {
            for (int j = 0; j < tab.GetLength(1); j++)
            {
                Console.Write(tab[i, j]);
            }
            Console.WriteLine();
        }
        Console.WriteLine();
    }

    static void CheckPosition(char[,] table, List<Point> position, Point newPosition, string word, int numLetter)
    {
        var x = newPosition.X;
        var y = newPosition.Y;

        // Слово составлено
        if (position.Count >= word.Length)
            return;

        // Уже присутствует в слове
        if (position.IndexOf(newPosition) != -1)
            return;

        // Выход за таблицу
        if (x == table.GetLength(0) || y == table.GetLength(1) || x == -1 || y == -1)
            return;

        // Не совпадение с буквой
        if (word[numLetter] != table[x, y])
            return;

        position.Add(newPosition);
        CheckWord(table, position, word, numLetter + 1);
    }

    static bool FindByFirstSymb(char[,] table, string word)
    {
        for (int i = 0; i < table.GetLength(0); i++)
        {
            for (int j = 0; j < table.GetLength(1); j++)
            {
                if (table[i, j] == word[0])
                {
                    var position = new List<Point>();
                    position.Add(new Point(i, j));

                    if (CheckWord(table, position, word, 1))
                    {
                        PrintPath(table, position);
                        return true;
                    }
                }
            }
        }
        return false;
    }

    public static void Main()
    {
        char[,] table =
        {
            {'a', 'b', 'r', 'a'},
            {'a', 'd', 'a', 'c'},
            {'b', 'a', 'b', 'r'},
            {'a', 'r', 'c', 'a'}
        };

        string word = "ababaaba";

        Console.WriteLine(FindByFirstSymb(table, word));
    }
}