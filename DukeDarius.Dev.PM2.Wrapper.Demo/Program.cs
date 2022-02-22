// See https://aka.ms/new-console-template for more information
using DukeDarius.Dev.PM2.Wrapper;
using DukeDarius.Dev.PM2.Wrapper.Models;

Console.WriteLine("Hello, World!");


var t = Task.Run(StartTest);
Task.WaitAll(t);


async Task StartTest()
{
    //await GetColdProcessList();
    //await StopStartAndRestartAllProcesses();
    //await AddApp1ToPM2();
    //await ReadFirstProgram();

    await AttachToProcess();


};

async Task GetColdProcessList()
{
    var list = await PM2.GetMonitoredProcesses();
    Console.WriteLine($"Found {list.Count} PM2 Processes");
}

async Task StopStartAndRestartAllProcesses()
{
    var ps = await PM2.GetMonitoredProcesses();
    foreach(var p in ps)
    {
        await p.Stop();
        await p.Start();
        await p.Restart();
    }
}

async Task AddApp1ToPM2()
{
    var newProcesses = await PM2.StartProcess(new ProcessStartArgs(AppContext.BaseDirectory + "Test Applications", "App1.js"), i => Console.WriteLine(i));
}

async Task ReadFirstProgram()
{
    var processes = await PM2.GetMonitoredProcesses();
    var p = processes.First();

    await PM2.ReadProcess(p, x => Console.WriteLine("MSG: " + x), x => Console.WriteLine("ERR: " + x));

}

async Task AttachToProcess()
{
    var processes = await PM2.GetMonitoredProcesses();
    var p = processes.First();

    CancellationTokenSource cts = new CancellationTokenSource();
    p.Listen(cts.Token, x=>
    {
        Console.WriteLine("MSG: " + x);
    }, x=>
    {
        Console.WriteLine("ERR: " + x);
        cts.Cancel();
    });

    string val;
    while (true)
    {
        val = Console.ReadLine() ?? "";
        if(val == "stop")
        {
            cts.Cancel();
            //break;
        }
        await p.SendMessageAsync(val);
    }
}