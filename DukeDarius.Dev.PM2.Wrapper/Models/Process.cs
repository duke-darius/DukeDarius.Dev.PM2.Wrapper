using CliWrap;
using CliWrap.Buffered;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DukeDarius.Dev.PM2.Wrapper.Models
{
    public class Process
    {
        [JsonProperty("pid")]
        public long ProcessId { get; set; }

        [JsonProperty("name")]
        public string ProcessName { get; set; }

        [JsonProperty("pm_id")]
        public long ProcessManagerId { get; set; }

        [JsonProperty("monit")] 
        public MonitInformation MonitInformation { get; set; }
        public Action<string> StdOut { get; internal set; }
        public Action<string> StdErr { get; internal set; }

        public async Task Stop()
        {
            var res = await Cli.Wrap("pm2")
                .WithArguments("stop " + ProcessManagerId)
                .ExecuteBufferedAsync();

        }

        public async Task Start()
        {
            var res = await Cli.Wrap("pm2")
                .WithArguments("start " + ProcessManagerId)
                .ExecuteBufferedAsync();

        }

        public async Task Restart()
        {
            var res = await Cli.Wrap("pm2")
                .WithArguments("restart " + ProcessManagerId)
                .ExecuteBufferedAsync();

        }

        public void Listen(CancellationToken token, Action<string> standardOutputReceived, Action<string> standardErrorRecieved)
        {
            StdOut = standardOutputReceived;
            StdErr = standardErrorRecieved;
            Task.Run(async() =>
            {
                var res = await Cli.Wrap("pm2")
                    .WithArguments($"logs {ProcessManagerId} --lines 0")
                    .WithStandardOutputPipe(PipeTarget.ToDelegate(StdOut))
                    .WithStandardErrorPipe(PipeTarget.ToDelegate(StdErr))
                    .WithValidation(CommandResultValidation.None)
                    .ExecuteAsync(token);
            });
        }

        public async Task SendMessageAsync(string message)
        {
            await Cli.Wrap("pm2")
                .WithArguments($"send {ProcessManagerId} {message}")
                .ExecuteAsync();
        }
    }
}
