using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

class Program
{
    static void Main()
    {
        string[] articleArgs = Console.ReadLine().Split(", ").ToArray();
        string title = articleArgs[0];
        string content = articleArgs[1];
        string author = articleArgs[2];

        var article = new Article(title, content, author);
        int countOfCommands = int.Parse(Console.ReadLine());

        for (int i = 0; i < countOfCommands; i++)
        {
            string[] commands = Console.ReadLine()
                .Split(": ");

            string command = commands[0];
            string value = commands[1];

            switch(command)
            {
                case "Edit":
                    article.Edit(value);
                    break;
                case "ChangeAuthor":
                    article.Change(value);
                    break;
                case "Rename":
                    article.Rename(value);
                    break;
            }
        }

        Console.WriteLine(article);
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