using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

class Program
{
    static void Main()
    {
        int countOfArticles = int.Parse(Console.ReadLine());

        var listOfArticles = new List<Article>();

        
        

        for (int i = 0; i < countOfArticles; i++)
        {
            string[] articleArgs = Console.ReadLine().Split(", ").ToArray();
            string title = articleArgs[0];
            string content = articleArgs[1];
            string author = articleArgs[2];

            listOfArticles.Add(new Article(title, content, author));
        }
        string criteria = Console.ReadLine();
        var orderedListOfArticles = new List<Article>();

        switch (criteria)
        {
            case "title":
                orderedListOfArticles = listOfArticles.OrderBy(c => c.Title).ToList();
                break;
            case "content":
                orderedListOfArticles = listOfArticles.OrderBy(c => c.Content).ToList();
                break;
            case "author":
                orderedListOfArticles = listOfArticles.OrderBy(c => c.Author).ToList();
                break;
        }
        
        

        foreach(var article in orderedListOfArticles)
        {
            Console.WriteLine(article);
        }
    }
}

class Article
{
    public Article(string title, string content, string author)
    {
        this.Title = title;
        this.Content = content;
        this.Author = author;
    }
    public void Edit(string newContent)
    {
        this.Content = newContent;
    }
    public void Change(string newAuthor)
    {
        this.Author = newAuthor;
    }
    public void Rename(string newTitle)
    {
        this.Title = newTitle;
    }
    public override string ToString()
    {
        return $"{this.Title} - {this.Content}: {this.Author}";
    }

    public string Title { get; set; }
    public string Content { get; set; }
    public string Author { get; set; }
}