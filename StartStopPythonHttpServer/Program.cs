using System.Diagnostics;

string workingDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyVideos);
SetTitle(false);
ProcessStartInfo? psi = null;
Process? exe = null;
ConsoleKeyInfo key = new();

if (args.Any())
{
    //path to serve was passed via command line
    if (Directory.Exists(args[0]))
    {
        workingDirectory = args[0]; 
    } 
}

while (true)
{
    Console.Clear();
    DisplayOptions();
    key = Console.ReadKey();
    Console.WriteLine();
    if (key.Key == ConsoleKey.Spacebar)
    {
        psi = new ProcessStartInfo
        {
            FileName = "py",
            Arguments = "-m http.server 8000",
            WorkingDirectory = workingDirectory
        };

        _ = Task.Run(() => Start());
        SetTitle(true);
    }
    else if (key.Key == ConsoleKey.Escape)
    {
        Console.Clear();
        break;
    }
    else if (key.Key == ConsoleKey.P)
    {
        ChangeServerFolder();
        continue;
    }
    else
    {
        Console.WriteLine($"{key.Key} has no purpose here.");
        continue;
    }

    
    while (true)
    {
        Console.WriteLine("Press Space bar to stop server");
        key = Console.ReadKey();
        Console.WriteLine();
        if (key.Key == ConsoleKey.Spacebar)
        {
            Stop();
            break;
        }
        Console.Clear();
    }
}

Environment.Exit(0);

void ChangeServerFolder()
{
    Console.WriteLine("Enter folder to serve. wrap in quotes");
    var newFolder = Console.ReadLine()?.Replace("\"", "");
    if(!Directory.Exists(newFolder))
    {
        Console.WriteLine();
        Console.WriteLine(newFolder);
        Console.WriteLine("Folder does nor exist. Nothing was changed.");
        return;
    }
    workingDirectory = newFolder;
    
    DisplayPotentialservedJolder();
}

Environment.Exit(0);

void Start()
{
    exe = Process.Start(psi);
}

void Stop()
{
    exe?.Kill();
    Console.WriteLine("Stopped");
    SetTitle(false);
}

void DisplayPotentialservedJolder()
{
    Console.Write($"Currently assigned folder to serve : ");
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine(workingDirectory);
    Console.ForegroundColor = ConsoleColor.White;
    Console.WriteLine();
}

void DisplayOptions()
{
    DisplayPotentialservedJolder();
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("    Space bar to start server.");
    Console.WriteLine("    Esc to exit.");
    Console.WriteLine("    Pp to change folder.");
    Console.ForegroundColor = ConsoleColor.White;
    Console.WriteLine();
}

void SetTitle(bool running)
{
    if (!running)
    {
        Console.Title = "Stopped"; 
        return;
    }
    Console.Title = $"Serving: {workingDirectory}";
}

//py -m http.server 8000
//yt-dlp -f mp4 http://localhost:8000/Movies/RedNotice/RedNotice.mp4
