﻿namespace Domain.Models;

public class SubForum
{
    public string? Id { get; set; }
    public User? OwnedBy { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public List<Post>? Posts { get; set; }

    public SubForum()
    {
        Id = String.Empty;
        OwnedBy = new User();
        Title = String.Empty;
        Description = Description;
        Posts = new List<Post>();
    }
}