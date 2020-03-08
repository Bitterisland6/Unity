using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

public static class AsyncUtils
{
    public static async Task AwaitNotNull(object obj, int milisecondsRate)
    {
        while(true)
        {
            if (obj != null)
            {
                return;
            }
            await Task.Delay(milisecondsRate);
        }
         
    }
}