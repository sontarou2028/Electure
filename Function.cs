using System.Reflection.Metadata;
using System.Runtime.CompilerServices;

namespace Electure_oldstyle
{
    public class Function
    {
        public List<string> comlist = new List<string>  //   Program list subscribed.
        {                                                                           //   You can drop only "Function" class to add function :D 
            "exit","help","docs","document","multiplex"
        };
        public void PreMain(ref bool[] onstate)             //  Choose a program by onstate from selector in "Program" class.
        {
            Exit(onstate[0]);
            Help(onstate[1]);
            Docs(onstate[2]);
            Document(onstate[3]);
            Multiplex(onstate[4]);
        }
        private void Exit(bool enable)
        {
            if(!enable)return;
            Program.EXIT = false;
            Console.WriteLine(">good bye!");
            Console.ReadKey(true);
        }
        private void Help(bool enable)
        {
            if(!enable)return;
            Console.WriteLine("Help>");
        }
        private void Docs(bool enable)
        {
            if(!enable)return;
            Console.WriteLine("Docs>");
        }
        private void Document(bool enable) 
        {
            if(!enable)return;
            Console.WriteLine("Documet>");
        }
        private void Multiplex(bool enable)
        {
            if(!enable)return;
            Console.WriteLine("Multiplex>");
        }
    }
}
