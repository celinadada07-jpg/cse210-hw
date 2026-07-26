using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        List<Video> videos = new List<Video>();

        // Video 1
        Video video1 = new Video("Learn C# in 30 Minutes", "Programming Hub", 1800);
        video1.AddComment(new Comment("Alice", "Very helpful tutorial!"));
        video1.AddComment(new Comment("James", "Easy to understand."));
        video1.AddComment(new Comment("Sophia", "Thanks for sharing."));
        videos.Add(video1);

        // Video 2
        Video video2 = new Video("Top 10 Travel Destinations", "Travel World", 900);
        video2.AddComment(new Comment("David", "I want to visit Japan!"));
        video2.AddComment(new Comment("Emma", "Amazing places."));
        video2.AddComment(new Comment("Michael", "Great video."));
        videos.Add(video2);

        // Video 3
        Video video3 = new Video("Best Chocolate Cake Recipe", "Cooking Time", 1200);
        video3.AddComment(new Comment("Sarah", "I tried it and loved it."));
        video3.AddComment(new Comment("Daniel", "Looks delicious."));
        video3.AddComment(new Comment("Grace", "Can't wait to bake this."));
        videos.Add(video3);

        // Video 4
        Video video4 = new Video("Football Skills Compilation", "Sports TV", 600);
        video4.AddComment(new Comment("Chris", "Fantastic skills!"));
        video4.AddComment(new Comment("John", "Awesome highlights."));
        video4.AddComment(new Comment("Mary", "My favorite players."));
        videos.Add(video4);

        foreach (Video video in videos)
        {
            video.DisplayVideo();
        }
    }
}