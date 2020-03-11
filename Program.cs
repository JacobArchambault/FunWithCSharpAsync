using System;
using System.Threading.Tasks;
using static System.Console;
using static System.Threading.Tasks.Task;
using static System.Threading.Thread;
namespace FunWithCSharpAsync
{
    class Program
    {
        static async Task Main()
        {
            WriteLine(" Fun With Async ===>");
            WriteLine(DoWork());
            string message = await DoWorkAsync();
            WriteLine(message);
            WriteLine("Completed");
            await MethodReturningVoidAsync();
            await MultiAwaits();
            await MethodWithTryCatch();
            await ReturnAnInt();
            await MethodWithProblems(7, -5);
            await MethodWithProblemsFixed(7, -5);
            ReadLine();



            //WriteLine("Completed");
            //ReadLine();
        }

        static string DoWork()
        {
            Sleep(5_000);
            return "Done with work!";
        }

        static async Task<string> DoWorkAsync() => await Run(() =>
                                                             {
                                                                 Sleep(5_000);
                                                                 return "Done with work!";
                                                             });

        static async Task MethodReturningVoidAsync()
        {
            await Run(() =>
            {
                /* Do some work here... */
                Sleep(4_000);
            });
            WriteLine("Void method completed");
        }

        static async Task MultiAwaits()
        {
            await Run(() => { Sleep(2_000); });
            WriteLine("Done with first task!");

            await Run(() => { Sleep(2_000); });
            WriteLine("Done with second task!");

            await Run(() => { Sleep(2_000); });
            WriteLine("Done with third task!");
        }

        static async Task<string> MethodWithTryCatch()
        {
            try
            {
                //Do some work
                return "Hello";
            }
            catch (Exception)
            {
                await LogTheErrors();
                throw;
            }
        }

        private static Task LogTheErrors()
        {
            throw new NotImplementedException();
        }

        static async Task MethodWithProblems(int firstParam, int secondParam)
        {
            WriteLine("Enter");
            await Run(() =>
            {
                //Call long running method
                Sleep(4_000);
                WriteLine("First Complete");
                //Call another long running method that fails because
                //the second parameter is out of range
                WriteLine("Something bad happened");
            });
        }

        static async Task MethodWithProblemsFixed(int firstParam, int secondParam)
        {
            WriteLine("Enter");
            if (secondParam < 0)
            {
                WriteLine("Bad data");
                return;
            }

            await actualImplementation();

            async Task actualImplementation()
            {
                await Run(() =>
                {
                    //Call long running method
                    Sleep(4_000);
                    WriteLine("First Complete");
                    //Call another long running method that fails because
                    //the second parameter is out of range
                    WriteLine("Something bad happened");
                });
            }
        }

        static async ValueTask<int> ReturnAnInt()
        {
            await Delay(1_000);
            return 5;
        }
    }
}