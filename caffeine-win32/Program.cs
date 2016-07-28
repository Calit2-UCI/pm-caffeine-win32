using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace caffeine_win32
{
    // https://msdn.microsoft.com/en-us/library/windows/desktop/aa373208%28v=vs.85%29.aspx

    public enum EXECUTION_STATE : uint
    {
        ES_AWAYMODE_REQUIRED = 0x00000040,
        ES_CONTINUOUS = 0x80000000,
        ES_SYSTEM_REQUIRED = 0x00000001,
        ES_DISPLAY_REQUIRED = 0x00000002,
    }

    class Program
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern EXECUTION_STATE SetThreadExecutionState(EXECUTION_STATE esFlags);

        public static void failOut()
        {
            Console.WriteLine("Please pass in a single integer argument: minutes");
            Console.Write("Press any key to continue...");
            Console.ReadKey();
        }

        static void Main(string[] args)
        {
            if (args.Length == 1)
            {
                
                string minStr = args[0];
                int mins;
                if (int.TryParse(minStr, out mins))
                {
                    System.Console.WriteLine("Blocking for "+mins+" minutes");
                    SetThreadExecutionState(EXECUTION_STATE.ES_CONTINUOUS | EXECUTION_STATE.ES_DISPLAY_REQUIRED | EXECUTION_STATE.ES_SYSTEM_REQUIRED);
                    var now = DateTime.Now;
                    var resumeDate = now.AddMinutes(mins);
                    var durationUntilResume = resumeDate - now;
                    var t = new System.Threading.Timer(o => {
                        System.Environment.Exit(0);
                    }, null, durationUntilResume, TimeSpan.Zero);
                    object sync = new object();
                    lock (sync)
                    System.Threading.Monitor.Wait(sync);
                }
                else
                {
                    failOut();
                } 
                
            }
            else
            {
                failOut();
            }
        }
    }
}
