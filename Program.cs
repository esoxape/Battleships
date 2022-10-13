namespace Battleships
{
    internal class Program
    {
        static bool[,] spelarensSkott = new bool[5, 6];
        static bool[,] datornsSkott = new bool[5, 6];

        public static string[,] seeda(string[,] a)
        {
            Random rnd = new Random();
            int skepp = 0;
            for (int i = 0; i < 10000; i++)
            {
                int random1 = rnd.Next(5);
                int random2 = rnd.Next(6);
                if (a[random1, random2] != "S")
                {
                    a[random1, random2] = "S";
                    skepp++;
                    if (skepp == 3) break;
                }
            }

            for (int i = 0; i < a.GetLength(0); i++)
            {
                for (int j = 0; j < a.GetLength(1); j++)
                {
                    if (a[i, j] != "S") a[i, j] = "O";
                }
            }
            return a;

        }
        public static void ritaspelare(string[,] a)
        {
            Console.WriteLine("Spelarens bräde:");
            for (int i = 0; i < a.GetLength(0); i++)
            {
                for (int j = 0; j < a.GetLength(1); j++)
                {
                    if (datornsSkott[i, j] == true && a[i, j] == "S")
                    {
                        Console.BackgroundColor = ConsoleColor.Red;
                        Console.Write(a[i, j]);
                        Console.ResetColor();
                    }
                    else if (datornsSkott[i, j] == true)
                    {
                        Console.BackgroundColor = ConsoleColor.Blue;
                        Console.Write(a[i, j]);
                        Console.ResetColor();
                    }
                    else Console.Write(a[i, j]);
                }
                Console.WriteLine();
            }
        }
        public static void ritadator(string[,] a)
        {
            Console.WriteLine("Fiendens bräde:");
            for (int i = 0; i < a.GetLength(0); i++)
            {
                for (int j = 0; j < a.GetLength(1); j++)
                {
                    if (spelarensSkott[i, j] == true && a[i, j] == "S")
                    {
                        Console.BackgroundColor = ConsoleColor.Red;
                        Console.Write(a[i, j]);
                        Console.ResetColor();
                    }
                    else if (spelarensSkott[i, j] == true)
                    {
                        Console.BackgroundColor = ConsoleColor.Blue;
                        Console.Write(a[i, j]);
                        Console.ResetColor();
                    }
                    else Console.Write("-");
                }
                Console.WriteLine();
            }
        }
        public static void datorskjuta()
        {
            var rand = new Random();
            for (int i = 0; i < 100000; i++)
            {
                int random1 = rand.Next(5);
                int random2 = rand.Next(6);
                if (datornsSkott[random1, random2] == false)
                {
                    datornsSkott[random1, random2] = true;
                    break;
                }
            }
        }
        public static bool vinnare(string[,] a, string[,] b)
        {
            int counter1 = 0;
            int counter2 = 0;

            for (int i = 0; i < a.GetLength(0); i++)
            {
                for (int j = 0; j < a.GetLength(1); j++)
                {
                    if (a[i, j] == "S" && datornsSkott[i, j] == true) counter1++;
                }
            }

            for (int i = 0; i < a.GetLength(0); i++)
            {
                for (int j = 0; j < a.GetLength(1); j++)
                {
                    if (b[i, j] == "S" && spelarensSkott[i, j] == true) counter2++;
                }
            }

            if (counter1 == 3 || counter2 == 3) return true;
            return false;
        }
        static void Main(string[] args)
        {

            string[,] player1 = new string[5, 6];
            string[,] player2 = new string[5, 6];
            int[] coords = new int[2];

            player1 = seeda(player1);
            player2 = seeda(player2);
            for (int i = 0; i < 1000; i++)
            {
            panga:;

                ritaspelare(player1);
                ritadator(player2);
                int panga = 0;
            skott1:;
                Console.WriteLine("Vilka koordinater vill du skjuta på?:(1-5) (X): ");
                panga = Convert.ToInt32(Console.ReadLine()) - 1;
                if (panga + 1 > 0 && panga + 1 < 6) coords[0] = panga;
                else
                {
                    Console.WriteLine("Felaktigt X koordinat, försök igen: ");
                    goto skott1;
                }
            skott2:;
                Console.WriteLine("Vilka koordinater vill du skjuta på?:(1-6) (Y): ");
                panga = Convert.ToInt32(Console.ReadLine()) - 1;
                if (panga + 1 > 0 && panga + 1 < 7) coords[1] = panga;
                else
                {
                    Console.WriteLine("Felaktigt X koordinat, försök igen: ");
                    goto skott2;
                }

                if (spelarensSkott[coords[0], coords[1]] == true)
                {
                    Console.Clear();
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.WriteLine("Du har redan skjutit där, prova igen!");
                    Console.ResetColor();
                    goto panga;
                }
                spelarensSkott[coords[0], coords[1]] = true;
                Console.Clear();
                vinnare(player1, player2);
                if (vinnare(player1, player2))
                {
                    ritaspelare(player1);
                    ritadator(player2);
                    Console.WriteLine("Du vann!!!");
                    break;
                }
                datorskjuta();
                if (vinnare(player1, player2))
                {
                    ritaspelare(player1);
                    ritadator(player2);
                    Console.WriteLine("Du förlorade!!!");
                    break;
                }
            }

        }
    }
}