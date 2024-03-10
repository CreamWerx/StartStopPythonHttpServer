using Microsoft.VisualBasic;
using System.Diagnostics;
ProcessStartInfo? psi = null;
Process? exe = null;
ConsoleKeyInfo key = new();
string workingDirectory = Environment.GetFolderPath( Environment.SpecialFolder.MyVideos);
if (args.Any())
{
    //path to serve was passed via command line
    workingDirectory = args[0]; 
}

psi = new ProcessStartInfo
{
    FileName = "py",
    Arguments = "-m http.server 8000",
    WorkingDirectory = workingDirectory
};

while (true)
{
    Console.Clear();
    DisplayOptions();
    key = Console.ReadKey();
    Console.WriteLine();
    if (key.Key == ConsoleKey.Spacebar)
    {
        if (psi != null)
        {
            _ = Task.Run(() => Start());
            Console.WriteLine("Running");
        }
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

    Console.WriteLine("Press Space bar to stop server");
    key = Console.ReadKey();
    Console.WriteLine();
    if (key.Key == ConsoleKey.Spacebar)
    {
        Stop();
    }
}

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
}

void DisplayPotentialservedJolder()
{
    Console.Write($"Curently assigned folder to serve : ");
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine(workingDirectory);
    Console.ForegroundColor = ConsoleColor.White;
    Console.WriteLine();
}

void DisplayOptions()
{
    DisplayPotentialservedJolder();
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("Space bar to start server.");
    Console.WriteLine("Esc to exit.");
    Console.WriteLine("Pp to change folder.");
    Console.ForegroundColor = ConsoleColor.White;
    Console.WriteLine();
}




//py -m http.server 8000
