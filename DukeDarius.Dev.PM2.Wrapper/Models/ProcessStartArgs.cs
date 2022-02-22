using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DukeDarius.Dev.PM2.Wrapper.Models
{
    public class ProcessStartArgs
    {
        /// <summary>
        /// The location that PM2 will spawn the process from, this is recommended to be an absolute path
        /// </summary>
        public string WorkingDirectory { get; set; } = Directory.GetCurrentDirectory();
        /// <summary>
        /// The path of the executable file, i.e "app1.py", "./SubFolder/app2.js" or "./app3.exe"
        /// </summary>
        public string PathToExecutable { get; set; } = string.Empty;
        /// <summary>
        /// The parameters you wish to pass to the executable
        /// </summary>
        public string? ExecutableParameters { get; set; } = null;

        /// <summary>
        /// If true, PM2 will restart the process if any files in the directory are changed
        /// </summary>
        public bool Watch { get;set; } = false;
        /// <summary>
        /// Prefix all logs with time of occurance
        /// </summary>
        public bool Time { get;set; } = false;
        /// <summary>
        /// Specifies if PM2 should attempt to restart the application should it close for any particular reason
        /// </summary>
        public bool NoAutoRestart { get; set; } = false;
        /// <summary>
        /// Attach to application log of newly spawned process
        /// </summary>
        public bool NoDaemon { get; set; } = false;


        /// <summary>
        /// Specify a maximum Memory Threshold for the newly spawned process, if exceeded, PM2 will restart the process, will be unlimited if null
        /// </summary>
        public string? MaxMemoryRestart { get; set; } = null;
        /// <summary>
        /// Specify the process name for newly spawned process, will default to executable files name if null
        /// </summary>
        public string? Name { get; set; } = null;
        
        /// <summary>
        /// Specify the intended Log path for the newly spawned process, will be default if null
        /// </summary>
        public string? Log { get; set; } = null;

        public ProcessStartArgs(
            string workingDirectory, 
            string pathToExecutable, 
            string? executableParameters = null, 
            bool watch = false, 
            bool time = false, 
            bool noAutoRestart = false, 
            bool noDaemon = false, 
            string? maxMemoryRestart = null,
            string? name = null, 
            string? log = null)
        {
            WorkingDirectory = workingDirectory;
            PathToExecutable = pathToExecutable;
            ExecutableParameters = executableParameters;
            Watch = watch;
            Time = time;
            NoAutoRestart = noAutoRestart;
            NoDaemon = noDaemon;
            MaxMemoryRestart = maxMemoryRestart;
            Name = name;
            Log = log;
        }

        public string GetStartArguments()
        {
            var sb = new StringBuilder();
            sb.Append("start ");
            sb.Append(PathToExecutable);

            if (Watch)
                sb.Append(" --watch");
            if (Time)
                sb.Append(" --time");
            if (NoAutoRestart)
                sb.Append(" --no-auto-restart");
            if (NoDaemon)
                sb.Append(" --no-daemon");
            if (MaxMemoryRestart != null)
                sb.Append($" --max-memory-restart {MaxMemoryRestart}");
            if (Name != null)
                sb.Append($" --name {Name}");
            if(Log != null)
                sb.Append($" --log {Log}");

            if (ExecutableParameters != null)
                sb.Append($" -- {ExecutableParameters}");



            return sb.ToString();

        }
    }
}
