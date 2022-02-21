// See https://aka.ms/new-console-template for more information
using DukeDarius.Dev.PM2.Wrapper;
using DukeDarius.Dev.PM2.Wrapper.Models;

Console.WriteLine("Hello, World!");

PM2Wrapper wrapper = new PM2Wrapper();

var t = Task.Run(StartTest);
Task.WaitAll(t);


async Task StartTest()
{
    //await GetColdProcessList();
    //await StopStartAndRestartAllProcesses();
    //await AddApp1ToPM2();
    //await ReadFirstProgram();

};

async Task GetColdProcessList()
{
    var list = await wrapper.GetMonitoredProcesses();
    Console.WriteLine($"Found {list.Count} PM2 Processes");
}

async Task StopStartAndRestartAllProcesses()
{
    var ps = await wrapper.GetMonitoredProcesses();
    foreach(var p in ps)
    {
        await p.Stop();
        await p.Start();
        await p.Restart();
    }
}

async Task AddApp1ToPM2()
{
    var newProcesses = await wrapper.StartProcess(new ProcessStartArgs(AppContext.BaseDirectory + "Test Applications", "App1.js"), i => Console.WriteLine(i));
}

async Task ReadFirstProgram()
{
    var processes = await wrapper.GetMonitoredProcesses();
    var p = processes.First();

    await wrapper.ReadProcess(p, x => Console.WriteLine("MSG: " + x), x => Console.WriteLine("ERR: " + x));

}

