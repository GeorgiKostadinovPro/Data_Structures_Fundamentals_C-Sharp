using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

public class Board : IBoard
{
    private IDictionary<string, Card> cards;

    public Board()
    {
        this.cards = new Dictionary<string, Card>();
    }

    public bool Contains(string name)
    {
        return this.cards.ContainsKey(name);
    }

    public int Count()
    {
        return this.cards.Count;
    }

    public void Draw(Card card)
    {
        if (this.cards.ContainsKey(card.Name))
        {
            throw new ArgumentException();
        }

        this.cards.Add(card.Name, card);
    }

    public IEnumerable<Card> GetBestInRange(int start, int end)
    {
        return this.cards.Values
            .Where(c => c.Score >= start && c.Score <= end)
            .OrderByDescending(c => c.Level);
    }

    public void Heal(int health)
    {
        Card card = this.cards.Values.OrderBy(c => c.Health).First();
        card.Health += health;
    }

    public IEnumerable<Card> ListCardsByPrefix(string prefix)
    {
        return this.cards.Values.Where(c => c.Name.StartsWith(prefix))
            .OrderBy(c => string.Join("", c.Name.Reverse()))
            .ThenBy(c => c.Level);
    }

    public void Play(string attackerCardName, string attackedCardName)
    {
        if (!this.cards.ContainsKey(attackerCardName))
        {
            throw new ArgumentException();
        }

        if (!this.cards.ContainsKey(attackedCardName))
        {
            throw new ArgumentException();
        }

        Card attackingCard = this.cards[attackerCardName];
        Card attackedCard = this.cards[attackedCardName];

        if (attackingCard.Level != attackedCard.Level)
        {
            throw new ArgumentException();
        }

        if (attackedCard.Health <= 0)
        {
            return;
        }
        
        attackedCard.Health -= attackingCard.Damage;

        if (attackedCard.Health <= 0)
        {
            attackingCard.Score += attackedCard.Level;
        }
    }

    public void Remove(string name)
    {
        if (!this.cards.ContainsKey(name))
        {
            throw new ArgumentException();
        }

        this.cards.Remove(name);
    }

    public void RemoveDeath()
    {
        var carsToRemove = this.cards.Values
            .Where(c => c.Health <= 0).ToList();

        foreach (var card in carsToRemove)
        {
            this.cards.Remove(card.Name);
        }
    }

    public IEnumerable<Card> SearchByLevel(int level)
    {
        return this.cards.Values
            .Where(c => c.Level == level)
            .OrderByDescending(c => c.Score);
    }
}