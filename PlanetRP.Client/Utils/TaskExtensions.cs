using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanetRP.Client.Utils
{
    public static class TaskExtensions
    {
        public static void HandleError(this Task task)
        {
            if (!task.IsFaulted) return;
            Console.WriteLine($"Task failed with exception: {task.Exception}");
        }
    }
}
