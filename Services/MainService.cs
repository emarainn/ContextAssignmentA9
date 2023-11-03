using ContextExample.Data;
using ContextExample.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Threading.Channels;
using static System.Net.Mime.MediaTypeNames;

namespace ContextExample.Services;

/// <summary>
///     You would need to inject your interfaces here to execute the methods in Invoke()
///     See the commented out code as an example
/// </summary>
public class MainService : IMainService
{
    private readonly IContext _context;

    public MainService(IContext context)
    {
        _context = context;
    }

    public void Invoke()
    {
        // menu option for user
        string options;

        do { 
            Console.Write("\nYou have the following options:" +
                "\n\t1. Select By Movie ID" +
                "\n\t2. Select By Title" +
                "\n\t3. Find Movie By Title" +
                "\n\tX. Exit Application" +
                "\nEnter Option Here: ");
            options = Console.ReadLine().ToUpper();

            if (options == "1")
            {
                SelectById();
            }
            else if (options == "2")
            {
                SelectByTitle();
            }
            else if (options == "3")
            {
                FindByTitle();
            }
            else if (options == "X")
            {
                Console.WriteLine("Exiting Application...");
                Environment.Exit(0);
            }

        } while(options != "X");
    }

    public void SelectById()
    {
        Console.WriteLine("\n--------------------------------------" +
                    "\nSelect By Movie ID" +
                    "\n\tThe Following ID's Are Populated: 1 - 4");
        int movieId = 0;

        Console.Write("Enter Movie ID: ");

        while (!int.TryParse(Console.ReadLine(), out movieId) || movieId <= 0 || movieId >= 5)
        {
            Console.WriteLine("\n** Must Enter Number Between 1 and 4 **");
            Console.Write("Enter Movie ID: ");
        }

        var results = _context.GetById(movieId);

        Console.WriteLine($"\nCorresponding Id and Movie:\n\t{results.Id}| {results.Title}"); 
    }

    public void SelectByTitle()
    {
        string movieTitle = "";

        Console.WriteLine("\n--------------------------------------" +
            "\nSelect By Movie Title" +
            "\n\t** Case Sensitive **");

        do
        {
            Console.Write("\nEnter Movie Title: ");
            movieTitle = Console.ReadLine();

            var movie = _context.GetByTitle(movieTitle);

            if (movieTitle == "")
            {
                Console.WriteLine("**Must Enter Movie Title**");
            }else if(movieTitle == movie.Title)
            {
                Console.WriteLine($"\n\nYou Entered:" +
                    $"\n\t{movieTitle}" +
                    $"\nCorresponding Movie ID and Title:" +
                    $"\n\t{movie.Id}| {movie.Title}");
                break;
            }
        } while (movieTitle != "");
    }

    public void FindByTitle()
    {
        Console.WriteLine("\n--------------------------------------" +
            "\nFind By Movie Title" +
            "\n\t** Case Insensitive **");

        Console.Write("Enter Movie Title: ");
        var movieTitle = Console.ReadLine();

        var results = _context.FindMovie(movieTitle);

        Console.WriteLine($"\nYou Entered: " +
            $"\n\t   {movieTitle}" +
            $"\nCorresponding Movie ID and Title: ");

        foreach ( var result in results )
        {
            Console.WriteLine($"\t{result.Id}| {result.Title}");
        }
    }
}