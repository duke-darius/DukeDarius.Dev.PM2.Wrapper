using NUnit.Framework;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DukeDarius.Dev.PM2.Wrapper.Test
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {

        }

        

        [Test]
        public async Task CanListProcesses()
        {
            var processes = await PM2.GetMonitoredProcesses();
            Assert.IsNotNull(processes);
        }

        [Test]
        public async Task CanAddProcess()
        {
            var newProcess = await PM2.StartProcess(new Models.ProcessStartArgs(AppContext.BaseDirectory, "app1.js"));
            Assert.IsNotNull(newProcess);
        }

        [Test]
        public async Task CanStopProcess()
        {
            var all = await PM2.GetMonitoredProcesses();
            Assert.IsNotNull(all);

            var first = all.FirstOrDefault();
            Assert.IsNotNull(first);

            await first.Stop();
        }

        [Test]
        public async Task CanStartProcess()
        {
            var all = await PM2.GetMonitoredProcesses();
            Assert.IsNotNull(all);

            var first = all.FirstOrDefault();
            Assert.IsNotNull(first);

            await first.Start();
        }
    }
}