using Microsoft.VisualBasic;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;

namespace Electure_oldstyle
{
    internal class Program                              //////// problem at pushing tab
    { 
        static internal bool EXIT;
        private static Program program = new Program();
        private static Function function = new Function();
        private static bool[] state;

        static void Main(string[] args)             //Main
        {
            Console.Write("Hello, World!");Console.ReadKey(true);
            Console.SetCursorPosition(0,1);
            EXIT = true;
            while(EXIT)
            { 
                Console.WriteLine(">コマンドを入力してください");
                Console.Write(">");
                program.Selector(ref function.comlist);
                function.PreMain(ref state);
            }
        }
        int maxlength = 0;
        bool[] Selector(ref List<string> list)      //  Command input support system.
        {
            List<string> result = new List<string>(list);
            result.Sort();
            ConsoleKeyInfo key;
            char keychar;
            string keystr;              //  about key
            string input = "";
            int chache;
            string str;
            int[] curpos = new int[2];      //cursor`s position(variable)        
            int[] sort = new int[result.Count];
            Program.state = new bool[result.Count];
            int chain = -1;

            for(int i=0;i<result.Count;i++)         //sort and get pattern
            {
                str =result[i];
                sort [i] = list.IndexOf(str);
                chache = str.Length;
                maxlength = maxlength > chache ? maxlength : chache;
                Program.state[i] = false; 
            }
            while(true)         //main function at selector
            {
                curpos[0] = Console.CursorLeft;
                curpos[1] = Console.CursorTop;
                key = Console.ReadKey(true);
                Console.SetCursorPosition(curpos[0],curpos[1]);
                
                if(key.Key == ConsoleKey.Enter)             //enter action
                { 
                    int cursorleft,cursortop;
                    cursorleft = 0;
                    cursortop = Console.CursorTop;
                    Console.SetCursorPosition(cursorleft,cursortop+2);
                    for(int m = 0;m < 13 + input.Length;m++) 
                    {
                        Console.Write("　");
                    } 
                    for(int j = 0;j < list.Count;j++)           //delete recent print
                    {
                        for(int k = 0;k < program.maxlength;k++)
                        {
                            Console.Write("　");
                        }
                        Console.Write("\n");
                    }
                    Console.SetCursorPosition(cursorleft,cursortop);
                    if(chain >= 0)
                        Program.state[sort[chain]] = true;
                    break;
                } 
                keychar = key.KeyChar;
                keystr = keychar.ToString().ToLower();
                if(Convert.ToInt32(keychar) >= 97 && Convert.ToInt32(keychar) <= 122)       //divide input by a~z or else
                {
                     input += keystr;
                }
                else
                {
                     Console.SetCursorPosition(curpos[0],curpos[1]);
                    for(int i = 0;i < 4;i++)
                    {
                        Console.Write("　");
                    }
                    Console.SetCursorPosition(curpos[0],curpos[1]);
                }
                
                if(key.Key == ConsoleKey.Backspace  && curpos[0] > 1 )          //backspace function
                {
                    curpos[0] -= 1;
                    Console.SetCursorPosition(curpos[0],curpos[1]);
                    Console.Write(" ");
                    chache = input.Length;
                    input = input.Substring(0,chache-1);
                    Console.SetCursorPosition(curpos[0],curpos[1]);
                }else if(key.Key == ConsoleKey.Backspace  && curpos[0]<=1){}
                else
                {
                    Console.Write(keystr);
                }
                chain = program.GetOptions(input,Console.CursorLeft,Console.CursorTop,ref result,ref key);
            }
            Console.Write("\n");
            return Program.state;
        }
        int autotype = -1;
        int GetOptions(string written,int X,int Y,ref List<string> list,ref ConsoleKeyInfo key)       //  Sticks to above function "Selector".
        {                                                                                                                           //  Compare between input and command list.
            int a = -1;                                                                                         //  and show recommend options.
            int b = -1;
            int x = 0;
            int length = list.Count;
            int res = -1;
            if(written.Length > 0)
            {
                a = list.FindIndex(element =>  element.Substring(0,element.Length<written.Length?element.Length:written.Length) == (written.Length > element.Length?written.Substring(0,element.Length):written));
                b = list.FindLastIndex(element =>  element.Substring(0,element.Length<written.Length?element.Length:written.Length) == (written.Length > element.Length?written.Substring(0,element.Length):written));
                bool first = false;
                if(key.Key == ConsoleKey.UpArrow || key.Key == ConsoleKey.DownArrow)            //option hilight key function
                {
                    if((autotype < a || autotype > b))
                    {
                        autotype = a;
                        first = true;
                    }
                    if(key.Key == ConsoleKey.UpArrow && autotype >= 0) 
                    { 
                        if(!first) autotype--;
                    } 
                    if(key.Key == ConsoleKey.DownArrow && autotype >= 0 && autotype <= b)
                    {
                        if(!first) autotype++;
                    }
                    if(autotype < a) autotype = b;
                    if(autotype > b) autotype = a;
                }
            }
                
            Console.SetCursorPosition(x,Y+2);
            for(int m = 0;m < 13 + written.Length;m++) 
            {
                Console.Write("　");
            } 
            for(int j = 0;j < list.Count;j++)
            {
                for(int k = 0;k < program.maxlength + written.Length;k++)
                {
                    Console.Write("　");
                }
                Console.Write("\n");
            }
            Console.SetCursorPosition(x,Y+2);
            int i = a;
            if(a < 0)
            {
                Console.WriteLine("  登録されていないコマンド : {0}",written);
                autotype = -1;
            }
            else 
            { 
                do
                {
                    if(i == autotype)           //hilight
                    {
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.White;
                    }
                    Console.Write("  {0}: {1}",i-a+1,list[i]);
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.Write("\n");
                    i++;
                }while(i<=b);
                res = written == list[a] ? a : -1;
            } 
            Console.SetCursorPosition(X,Y);
            return res;
        }
    }
}
