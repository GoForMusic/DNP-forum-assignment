﻿namespace Domain.Models;

public class SubForum
{
    public int Id { get; set; }
    public User OwnedBy { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
}