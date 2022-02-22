using CliWrap;
using CliWrap.Buffered;
using CliWrap.EventStream;
using DukeDarius.Dev.PM2.Wrapper.Models;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Process = DukeDarius.Dev.PM2.Wrapper.Models.Process;
using WinProcess = System.Diagnostics.Process;

namespace DukeDarius.Dev.PM2.Wrapper
{
    public static class PM2
    {

        public static async Task<List<Process>> GetMonitoredProcesses()
        {
            try
            {
                var res = await Cli.Wrap("pm2")
                    .WithArguments("jlist")
                    .WithValidation(CommandResultValidation.None)
                    .ExecuteBufferedAsync();

                if (res.ExitCode != 0)
                {
                    // do stuff here
                }

                return JsonConvert.DeserializeObject<List<Process>>(res.StandardOutput) ?? throw new Exception("Deserialized object was null...");

            } catch (Exception e)
            {
                throw;
            }
        }


        public static async Task<Process> StartProcess(ProcessStartArgs args, Action<string> messageRecieved = null)
        {
            try
            {
                Directory.SetCurrentDirectory(args.WorkingDirectory);
                var res = await Cli.Wrap($"pm2")
                    .WithArguments(args.GetStartArguments())
                    .WithValidation(CommandResultValidation.None)
                    .ExecuteBufferedAsync();

                if (res.ExitCode != 0)
                {
                    throw new Exception(res.StandardError);
                }

                foreach (var line in res.StandardOutput.Split('\n'))
                {
                    if (PM2Regex.PM2Message.IsMatch(line))
                    {
                        messageRecieved?.Invoke(line);
                    }
                }

                var processes = await GetMonitoredProcesses();
                return processes.OrderBy(x => x.ProcessManagerId).Last();


            }
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task ReadProcess(Process process, Action<string> StandardOutputRecieved, Action<string> StandardErrorRecieved = null)
        {
            var builder = Cli.Wrap("pm2")
                .WithArguments($"logs {process.ProcessManagerId} --lines 0")
                .WithStandardOutputPipe(PipeTarget.ToDelegate(StandardOutputRecieved))
                .WithValidation(CommandResultValidation.None);
            if (StandardErrorRecieved != null)
                builder.WithStandardErrorPipe(PipeTarget.ToDelegate(StandardErrorRecieved));

            await builder.ExecuteAsync();

        }



    }
}