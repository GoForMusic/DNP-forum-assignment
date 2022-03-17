﻿namespace Domain.Models;

public class Comment
{
    public string? Id { get; set; }
    public string? Body { get; set; }
    public List<Vote>? Votes { get; set; }
    public User? WrittenBy { get; set; }

    public Comment()
    {
        Id = String.Empty;
        Body = String.Empty;
        Votes = new List<Vote>();
        WrittenBy = new User();
    }
}