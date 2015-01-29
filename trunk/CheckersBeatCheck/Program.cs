using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckersBeatCheck
{
    class Program
    {
        //pozycje pionkow bialych [kolumna, wiersz]
        static int[][] pBiale = {
                                 new int[]{3,1},
                                 new int[]{7,4}
                             };

        //pozycje pionkow czerwonych [kolumna, wiersz]
        static int[][] pCzerwone = {
                                   new int[]{2,2},
                                   new int[]{2,4},
                                   new int[]{4,6},
                                   new int[]{6,6},
                                   new int[]{6,4},
                                   new int[]{4,4},
                               };

        // sprawdz czy da sie wykonac bicie na wskazanym polu
        static bool SprawdzPole(int[] sasiad, int[] ruch, List<int[]> zbite)
        {
            if (Array.Exists(pCzerwone, el => el[0] == sasiad[0] && el[1] == sasiad[1]) == true &&
                Array.Exists(pCzerwone, el => el[0] == ruch[0] && el[1] == ruch[1]) == false &&
                Array.Exists(pBiale, el => el[0] == ruch[0] && el[1] == ruch[1]) == false &&
                ruch[0] > 0 &&
                ruch[0] <= 8 &&
                ruch[1] > 0 &&
                ruch[1] <= 8 &&
                (
                    zbite.Count == 0 ||
                    zbite.Exists(r => r[0] == sasiad[0] && r[1] == sasiad[1]) == false
                )
            )
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // rekurencyjne wyliczenie ilosci mozliwych bic
        static byte Bij(List<int[]> zbite, int[] docPoz, byte ilBic)
        {
            byte biciePg, biciePd, bicieLd, bicieLg;
            biciePg = biciePd = bicieLd = bicieLg = ilBic;

            int[] pgSasiad = new int[] { docPoz[0] + 1, docPoz[1] + 1 };
            int[] pgRuch = new int[] { docPoz[0] + 2, docPoz[1] + 2 };
            int[] pdSasiad = new int[] { docPoz[0] + 1, docPoz[1] - 1 };
            int[] pdRuch = new int[] { docPoz[0] + 2, docPoz[1] - 2 };
            int[] ldSasiad = new int[] { docPoz[0] - 1, docPoz[1] - 1 };
            int[] ldRuch = new int[] { docPoz[0] - 2, docPoz[1] - 2 };
            int[] lgSasiad = new int[] { docPoz[0] - 1, docPoz[1] + 1 };
            int[] lgRuch = new int[] { docPoz[0] - 2, docPoz[1] + 2 };

            if (SprawdzPole(pgSasiad, pgRuch, zbite) == true)
            {
                zbite.Add(pgSasiad);
                biciePg = Bij(zbite, pgRuch, (byte)(ilBic + 1));
            }
            if (SprawdzPole(pdSasiad, pdRuch, zbite) == true)
            {
                zbite.Add(pdSasiad);
                biciePd = Bij(zbite, pdRuch, (byte)(ilBic + 1));
            }
            if (SprawdzPole(ldSasiad, ldRuch, zbite) == true)
            {
                zbite.Add(ldSasiad);
                bicieLd = Bij(zbite, ldRuch, (byte)(ilBic + 1));
            }
            if (SprawdzPole(lgSasiad, lgRuch, zbite) == true)
            {
                zbite.Add(lgSasiad);
                bicieLg = Bij(zbite, lgRuch, (byte)(ilBic + 1));
            }

            byte[] ilosciBic = { biciePg, biciePd, bicieLd, bicieLg };
            Array.Sort(ilosciBic);
            byte maxIloscBic = ilosciBic[ilosciBic.Length - 1];
            return maxIloscBic;
        }

        static void Main(string[] args)
        {
            byte maxIlBic = 0;
            List<int[]> poprzPoz;
            foreach (int[] pBialy in pBiale)
            {
                poprzPoz = new List<int[]>();
                byte ilBic = Program.Bij(poprzPoz, pBialy, 0);
                if (ilBic > maxIlBic)
                {
                    maxIlBic = ilBic;
                }

            }
            Console.WriteLine(maxIlBic);
            Console.ReadKey();
        }
    }
}
