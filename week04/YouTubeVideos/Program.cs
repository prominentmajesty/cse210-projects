using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        // Here, i created video list. This list will hold all the videos that are added to it.
        List<Video> videos = new List<Video>();

        // Here, from line 12 to line 35, i Created and populated videos with comments
        Video video1 = new Video("C# Tutorial for Beginners", "By Augustine Ugochukwu", 720);
        video1.AddComment(new Comment("Urchman", "Great tutorial! Very helpful."));
        video1.AddComment(new Comment("Baby Don", "This is Exactly what i have been looking for."));
        video1.AddComment(new Comment("Kaycee", "When will you make video for the interface?"));
        videos.Add(video1);

        Video video2 = new Video("ASP.NET Core video tutorials for biginers", "By Augustine Ugochukwu", 1020);
        video2.AddComment(new Comment("DanvaAI", "This will help so much in my development!"));
        video2.AddComment(new Comment("Affonet", "I like your teaching Bro."));
        video2.AddComment(new Comment("Sunnita.N ET", "Nice One"));
        video2.AddComment(new Comment("Majesty", "Can you create the interface chief. This is nice"));
        videos.Add(video2);

        Video video3 = new Video("Full Stack Web Development with NodeJS and React", "Augustine Ugochukwu", 1560);
        video3.AddComment(new Comment("Majesty", "Thanks man!"));
        video3.AddComment(new Comment("DivPlanet", "When is the next part coming out?"));
        video3.AddComment(new Comment("Affonet", "Can you make video on Redux Saga."));
        videos.Add(video3);

        Video video4 = new Video("Machine Learning for Biginers", "By Augustine Ugochukwu", 890);
        video4.AddComment(new Comment("Malanda", "Thanks man."));
        video4.AddComment(new Comment("GardenPark", "This is so helpful."));
        video4.AddComment(new Comment("Tech-Doctor", "Can you do one deep learning tutorial"));
        videos.Add(video4);

        //Here, i loop through the video list to display information associated with each video
        foreach (var video in videos)
        {
            video.DisplayVideoInfo();
        }
    }
}

// Here, i created a class called Comment to track comment details
public class Comment
{

    public string CommenterName { get; }
    public string CommentText { get; }

    public Comment(string commenterName, string commentText)
    {
        CommenterName = commenterName;
        CommentText = commentText;
    }

    // Method to display comment information
    public string GetCommentInfo()
    {
        return $"{CommenterName}: {CommentText}";
    }
}

// Class that is responsible for tracking the title, author, and length (in seconds) of a video/videos.
public class Video
{
    // Properties
    public string Title { get; }
    public string Author { get; }
    public int LengthInSeconds { get; }
    private List<Comment> Comments { get; } = new List<Comment>();

    public Video(string title, string author, int lengthInSeconds)
    {
        Title = title;
        Author = author;
        LengthInSeconds = lengthInSeconds;
    }

    // adding comment
    public void AddComment(Comment comment)
    {
        Comments.Add(comment);
    }

    // Mgeting the number of comments
    public int GetNumberOfComments()
    {
        return Comments.Count;
    }

    // Method to display all comments
    public void DisplayComments()
    {
        foreach (var comment in Comments)
        {
            Console.WriteLine($" - {comment.GetCommentInfo()}");
        }
    }

    // Showing video information
    public void DisplayVideoInfo()
    {
        Console.WriteLine($"Title: {Title}");
        Console.WriteLine($"Author: {Author}");
        Console.WriteLine($"Length: {LengthInSeconds} seconds");
        Console.WriteLine($"Number of comments: {GetNumberOfComments()}");
        Console.WriteLine("Comments:");
        DisplayComments();
        Console.WriteLine();
    }
}

