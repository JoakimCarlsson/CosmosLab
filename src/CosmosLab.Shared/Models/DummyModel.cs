namespace CosmosLab.Shared.Models;

public sealed class Video
{
    public Guid Id { get; set; }
    public string HlsPath { get; set; } = default!;
}

public sealed class User
{
    public Guid Id { get; set; }

    public IEnumerable<Challenge> Challenges { get; set; } = default!;
    public IEnumerable<Contribution> Contributions { get; set; } = default!;
    public IEnumerable<Like> Likes { get; set; } = default!;
}

public sealed class Challenge
{
    public Guid Id { get; set; }
    public string Title { get; set; } = default!;

    public Guid VideoId { get; set; }
    public Video Video { get; set; } = default!;
    
    public Guid UserId { get; set; }
    public User User { get; set; } = default!;
    
    public IEnumerable<Contribution> Contributions { get; set; } = default!;
}

public sealed class Contribution
{
    public Guid Id { get; set; }
    public string Title { get; set; } = default!;

    public Guid VideoId { get; set; } = default!;
    public Video Video { get; set; } = default!;
    
    public Guid ChallengeId { get; set; }
    public Challenge Challenge { get; set; } = default!;
    
    public Guid UserId { get; set; }
    public User User { get; set; } = default!;
    
    public IEnumerable<Like> Likes { get; set; } = default!;
}

public sealed class Like
{
    public Guid Id { get; set; }
    
    public Guid ContributionId { get; set; }
    public Contribution Contribution { get; set; } = default!;
    
    public Guid UserId { get; set; }
    public User User { get; set; } = default!;
}
