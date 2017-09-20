using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Musical_Chairs
{
    class Program
    {
        /*
             *UNSW ProgComp 2014 Grand Final Task 2
             *This question is based on the "Hanoi Towers" problem. We'll use this analogy to solve it
             *to solve a hanoi tower, alternate betwene moving the largest, and smallest disk to the right tower (if N is even)
             *(if N is odd) move disk to the left. Remember you can wrap around the sides!
             *the small "disks" are the ones furthest to the left, the large "disks" are the ones to the right
             *the current tower (1,2 or 3) is represented by the instrument (V,D or S)
             *in this way, this input -> {D V D V}
             *is actually:
             * 1 2 3 4    (size)
             *{2 1 2 1}   (location)
             * 
             *       |        |        |
             *       |        |        |
             *      ###       #        |
             *    #######   #####      |
             *    =======  =======  =======
             */

        static int N;//input
        static int move = 0;//input
        static int?[][] towers;
        static Boolean done = false;
        static void Main(string[] args)
        {
            do {
                initialise();
            } while (1==1);
        }

        static void initialise()
        {
            move = 0;
            Console.Clear();
            Console.Write("N= ");
            string input = Console.ReadLine();
            int n;
            if (int.TryParse(input, out n)) { N = Convert.ToInt32(input); } else { return; };
            towers = new int?[][] { new int?[N], new int?[N], new int?[N] };
            for (int i = 0; i < N; i++)
            {
                towers[0][i] = i;
            };//populate initial values
            Console.Clear();
            render();
            Console.WriteLine("press enter to continue");
            Console.ReadLine();
            MoveTower(N - 1, 0, 2, 1);
            Console.ReadLine();
        }

        static void render() //this function just takes the data from the "towers" variable and renders it on screen (in text)
        {
            string[] lines = new string[N+1];//use an array to hold each line of the image
            for (int e = 0; e < (2 * N - 1); e++) { lines[N] = lines[N] + "="; }; //draw the base of a tower
            lines[N] = lines[N] + " " + lines[N] + " " + lines[N];//multiply that base 3 times (note we're drawing to line "n" which is the last line)
            for (int i = 0; i < 3; i++){Array.Sort(towers[i]);};//sort all the towers so their disks are in the right order
            foreach (int?[] tower in towers)//for each tower, do:
            {
                for(int i = 0; i < N; i++)//for each disk on the relevant tower, do:
                {
                    string echo = "";
                    int? disk = tower[i];
                    if (disk == null) { 
                        echo = new String(' ', (N - 1)) + "|" + new String(' ', N);
                    } else {
                        for (int e = 0; e < (-disk + (N - 1)); e++) { echo = echo + " "; };
                        for (int e = 0; e < disk; e++) { echo = echo + "##"; };
                        echo += "#";
                        for (int e = 0; e < (-disk + (N - 1)); e++) { echo = echo + " "; };
                        echo += " ";
                    };
                    lines[i] = lines[i] + echo;
                };
            };
            foreach (string item in lines) {
                Console.WriteLine(item);
            };
        }

        //BEGIN RECURSIVE PROCESSOR
        static void MoveTower(int disk, int source, int dest, int spare) {
            if (disk == 0) {
                movedisk(disk, source, dest);
            } else {
                MoveTower(disk - 1, source, spare, dest);//this function calls itself
                movedisk(disk, source, dest);         //when you call a function, C# will wait until it is completed before it continues, allowing recursive coding.
                MoveTower(disk - 1, spare, dest, source);
            };
        }
        //END RECURSIVE PROCESSOR
        static void movedisk(int disk, int source, int dest)
        {
            if (Array.IndexOf(towers[2], null) == -1) { done = true; };
            if (!done)
            {
                    towers[source][Array.IndexOf(towers[source], disk)] = null;
                    towers[dest][Array.IndexOf(towers[dest], null)] = disk;
                    Console.Clear();
                    render();
                    move++;
                    Console.WriteLine("(" + move + ") press enter to continue");
                    Console.ReadLine();
            };
        }
    }
}
