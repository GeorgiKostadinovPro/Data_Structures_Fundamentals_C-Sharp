﻿using System;
using System.Collections.Generic;
using System.Text;

public class Card
{

    public Card(string name, int damage, int score, int level)
    {
        this.Name = name;
        this.Damage = damage;
        this.Score = score;
        this.Level = level;
        this.Health = 20;
    }
    public string Name { get; set; }

    public int Damage { get; set; }

    public int Score { get; set; }

    public int Health { get; set; }

    public int Level { get; set; }

    public override string ToString()
    {
        return this.Name;
    }

    public override bool Equals(object obj)
    {
        return this.Name == ((Card)obj).Name;
    }
}