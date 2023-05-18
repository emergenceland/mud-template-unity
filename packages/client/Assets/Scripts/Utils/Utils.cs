using System.Collections.Generic;
using System.Threading.Tasks;

namespace Utils
{
    public class Utils
    {
        public static IEnumerator<object> AwaitTask(Task task)
        {
            while (!task.IsCompleted)
            {
                yield return null;
            }
	
            if (task.IsFaulted)
            {
                throw task.Exception;
            }
        } 
    }
}
