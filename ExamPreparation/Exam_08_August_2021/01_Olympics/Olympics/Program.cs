using System;

public class Program
{
    public static void Main(string[] args)
    {
        var olympics = new Olympics();
        var competitor = new Competitor(5, "Ani");

        olympics.AddCompetition(1, "SoftUniada", 500);

        olympics.AddCompetitor(5, "Ani");
        olympics.Compete(5, 1);

        var actual = olympics.Contains(1, competitor);
        Console.WriteLine(actual);
    }
}
