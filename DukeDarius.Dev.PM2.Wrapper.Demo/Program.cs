// See https://aka.ms/new-console-template for more information
using DukeDarius.Dev.PM2.Wrapper;
using DukeDarius.Dev.PM2.Wrapper.Models;

Console.WriteLine("Hello, World!");

PM2Wrapper wrapper = new PM2Wrapper();

var t = Task.Run(StartTest);
Task.WaitAll(t);


async Task StartTest()
{
    await GetColdProcessList();
    
};

async Task GetColdProcessList()
{
    var list = await wrapper.GetPM2Processes();
    Console.WriteLine($"Found {list.Count} PM2 Processes");
    await StopStartAndRestartAllProcesses(list);
}

async Task StopStartAndRestartAllProcesses(List<PM2Process> ps)
{
    foreach(var p in ps)
    {
        //await p.Stop();
        await p.Start();
        //await p.Restart();
    }
}