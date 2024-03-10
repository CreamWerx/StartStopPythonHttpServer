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

Console.WriteLine(workingDirectory);

while (true)
{
    Console.WriteLine("Press Space bar to start server. Esc to exit.");
    key = Console.ReadKey();
    if (key.Key == ConsoleKey.Spacebar)
    {
        psi = new ProcessStartInfo
        {
            FileName = "py",
            Arguments = "-m http.server 8000",
            WorkingDirectory = workingDirectory
        };
    }
    else if (key.Key == ConsoleKey.Escape)
    {
        Console.Clear();
        break;
    }
    if (psi != null)
    {
        _ = Task.Run(() => Start());
        Console.WriteLine("Running");
    }
    Console.WriteLine("Press Space bar to stop server");
    key = Console.ReadKey();
    if (key.Key == ConsoleKey.Spacebar)
    {
        Stop();
    } 
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




//py -m http.server 8000
