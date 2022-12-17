using System;
using System.Collections.Generic;
using System.Linq;

public class Olympics : IOlympics
{
    private readonly IDictionary<int, Competition> competitions;
    private readonly IDictionary<int, Competitor> competitors;

    public Olympics()
    {
        this.competitions = new Dictionary<int, Competition>();
        this.competitors = new Dictionary<int, Competitor>();
    }   

    public void AddCompetition(int id, string name, int participantsLimit)
    {
        if (this.competitions.ContainsKey(id))
        {
            throw new ArgumentException();
        }

        Competition competition = new Competition(name, id, participantsLimit);
        this.competitions.Add(id, competition);
    }

    public void AddCompetitor(int id, string name)
    {
        if (this.competitors.ContainsKey(id))
        {
            throw new ArgumentException();
        }

        Competitor competitor = new Competitor(id, name);
        this.competitors.Add(id, competitor);
    }

    public void Compete(int competitorId, int competitionId)
    {
        if (!this.competitors.ContainsKey(competitorId))
        {
            throw new ArgumentException();
        }

        if (!this.competitions.ContainsKey(competitionId))
        {
            throw new ArgumentException();
        }

        Competition competition = this.competitions[competitionId];
        Competitor competitor = this.competitors[competitorId];

        competition.Competitors.Add(competitor);
        competitor.TotalScore += competition.Score;
    }

    public int CompetitionsCount()
    {
        return this.competitions.Count;
    }

    public int CompetitorsCount()
    {
        return this.competitors.Count;
    }

    public bool Contains(int competitionId, Competitor comp)
    {
        if (!this.competitions.ContainsKey(competitionId))
        {
            throw new ArgumentException();
        }

        Competition competition = this.competitions[competitionId];

        return competition.Competitors.Any(c => c.Id == comp.Id);
    }

    public void Disqualify(int competitionId, int competitorId)
    {
        if (!this.competitors.ContainsKey(competitorId))
        {
            throw new ArgumentException();
        }

        if (!this.competitions.ContainsKey(competitionId))
        {
            throw new ArgumentException();
        }

        Competition competition = this.competitions[competitionId];
        Competitor competitor = this.competitors[competitorId];

        competition.Competitors.Remove(competitor);
        competitor.TotalScore -= competition.Score;
    }

    public IEnumerable<Competitor> FindCompetitorsInRange(long min, long max)
    {
        return this.competitors.Values.Where(c => c.TotalScore > min && c.TotalScore <= max)
            .OrderBy(c => c.Id);
    }

    public IEnumerable<Competitor> GetByName(string name)
    {
        var competitors = this.competitors.Values
            .Where(c => c.Name == name)
            .OrderBy(c => c.Id);

        if (competitors.Count() == 0)
        {
            throw new ArgumentException();
        }

        return competitors;
    }

    public Competition GetCompetition(int id)
    {
        if (!this.competitions.ContainsKey(id))
        {
            throw new ArgumentException();
        }

        Competition competition = this.competitions[id];

        return competition;
    }

    public IEnumerable<Competitor> SearchWithNameLength(int min, int max)
    {
        return this.competitors.Values
            .Where(c => c.Name.Length >= min && c.Name.Length <= max)
            .OrderBy(c => c.Id);    
    }
}