using CliWrap;
using CliWrap.Buffered;
using DukeDarius.Dev.PM2.Wrapper.Models;
using Newtonsoft.Json;

namespace DukeDarius.Dev.PM2.Wrapper
{
    public class PM2Wrapper
    {
        public async Task<List<PM2Process>> GetPM2Processes()
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

                return JsonConvert.DeserializeObject<List<PM2Process>>(res.StandardOutput) ?? throw new Exception("Deserialized object was null...");

            }catch(Exception e)
            {
                throw;
            }
        }
    }
}