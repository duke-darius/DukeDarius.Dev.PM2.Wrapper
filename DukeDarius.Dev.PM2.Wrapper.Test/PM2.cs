using NUnit.Framework;
using System;
using System.Linq;

namespace DukeDarius.Dev.PM2.Wrapper.Test
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {

        }

        

        [Test]
        public async void CanListProcesses()
        {
            var processes = await PM2.GetMonitoredProcesses();
            Assert.IsNotNull(processes);
        }

        [Test]
        public async void CanAddProcess()
        {
            var newProcess = await PM2.StartProcess(new Models.ProcessStartArgs(AppContext.BaseDirectory, "app1.js"));
            Assert.IsNotNull(newProcess);
        }

        [Test]
        public async void CanStopProcess()
        {
            var all = await PM2.GetMonitoredProcesses();
            Assert.IsNotNull(all);

            var first = all.FirstOrDefault();
            Assert.IsNotNull(first);

            await first.Stop();
        }

        [Test]
        public async void CanStartProcess()
        {
            var all = await PM2.GetMonitoredProcesses();
            Assert.IsNotNull(all);

            var first = all.FirstOrDefault();
            Assert.IsNotNull(first);

            await first.Start();
        }
    }
}