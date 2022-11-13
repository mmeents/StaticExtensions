using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using StaticExtensions;

namespace TestStaticExtensions {
  [TestClass]
  public class UnitTest1 {

   

    [TestMethod]
    public void TestWorkspace() {
      string test = (HasConsole().Result?"yes":"no");
      Console.WriteLine(test);
    }

    public async Task<Boolean> HasConsole() { 
      return await Task.FromResult(DllExt.HasConsole() );
    }

    [TestMethod]
    public void VerifyToStrTime() {

      Console.WriteLine("MMConLocation: "+DllExt.MMConLocation());
      Console.WriteLine("MMCommonsFolder: "+ DllExt.MMCommonsFolder());

      Console.WriteLine("toStrTime is: "+ DateTime.Now.AsStrTime());
      Console.WriteLine("toStrDateTime is: " + DateTime.Now.AsStrDateTime12H());
      Console.WriteLine("ToStrDateMM is: " + DateTime.Now.AsStrDayHHMM());

      Console.WriteLine("toStrDay is: " + DateTime.Now.AsStrDay());
      Console.WriteLine("ToStrDate is: " + DateTime.Now.AsStrDate());

      Console.WriteLine(" 6 % 2 ="+ (6 % 2).AsString());

      int[] primes = new int[]{ 2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37, 41, 43, 47, 53 };
      long d = 1;
      int c = 1;
      foreach(int p in primes) { 
        d = d * p;
        if (p < 53) { 
          Console.WriteLine(" else if ((n>="+d+") && (n<="+(d*primes[c]-1).AsString()+")) { r = "+c.AsString()+";} ");
        }
        c++;
      }

      Console.WriteLine( string.Format("test{0}", 5));

      List<string> testList = new List<string>();
      testList = null;
      if (!testList.IsNull() && testList.Count > 0) { 
      
        var testError = testList.Count;
      }

      try {
        Console.WriteLine("Try");
      }
      catch { Console.WriteLine("Catch "); } 
      finally { Console.WriteLine("finally"); }
    }


    [TestMethod]
    public void TestBackoff() {
    var waitTimeMS = 25;
    var maxAttempts = 5;
    var attempt = 1;
    var attemptSuccess = false;
    var token = "1";

    while((attempt < maxAttempts)&&!attemptSuccess) {

        try {
          if (attempt < 5) throw new Exception("test except");
          attemptSuccess = !token.IsNull() && token.Length > 0;
        }
        catch { 

        } 
        finally { 
          attempt++;
          if (!attemptSuccess) {
            waitTimeMS = waitTimeMS * 2; // 25, 50, 100, 200, 400
            Thread.Sleep(waitTimeMS);
          }
        }     

    }

  }

  }


}
