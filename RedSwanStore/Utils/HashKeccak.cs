using System;
using System.Collections.Generic;
using System.Linq;


namespace RedSwanStore.Utils
{
    public class HashKeccak
    {
        private static UInt64[] RC =
        {
            0x0000000000000001,
        0x0000000000008082,
        0x800000000000808A,
        0x8000000080008000,
        0x000000000000808B,
        0x0000000080000001,
        0x8000000080008081,
        0x8000000000008009,
        0x000000000000008A,
        0x0000000000000088,
        0x0000000080008009,
        0x000000008000000A,
        0x000000008000808B,
        0x800000000000008B,
        0x8000000000008089,
        0x8000000000008003,
        0x8000000000008002,
        0x8000000000000080,
        0x000000000000800A,
        0x800000008000000A,
        0x8000000080008081,
        0x8000000000008080,
        0x0000000080000001,
        0x8000000080008008
        };

        private static UInt16[,] shift = {
        {0,36,3,41,18},
        {1,44,10,45,2},
        {62,6,43,15,61},
        {28,55,25,21,56},
        {27,20,39,8,14}
        };

        private const UInt16 b = 200; //state string length in bytes
        private const UInt16 w = sizeof(UInt64); //word length in bytes
        private const UInt16 rounds_amount = 24;
        private const UInt16 words_amount = 5; //state string dimension size
        private static UInt16 d; // hash length in bytes
        private static UInt16 r; // sponge rate in bytes
        private static UInt16 c; //sponge capacity in bytes

        private static void Pad(Stack<char> M)
        {
            if (M.Count() % r == r - 1)
                M.Push((char)0x81);
            else
            {
                int zeroes_amount = r - (M.Count() % r) - 2;

                M.Push((char)0x01);

                for (int i = 0; i < zeroes_amount; i++)
                    M.Push((char)0x00);

                M.Push((char)0x80);
            }
        }

        private static void f(ref UInt64[,] S)
        {
            for (int round_number = 0; round_number < rounds_amount; round_number++)
            {
                UInt64[] C = { 0, 0, 0, 0, 0 };
                UInt64[] D = { 0, 0, 0, 0, 0 };
                UInt64[,] B = new UInt64[words_amount, words_amount];
                for (int i = 0; i < words_amount; ++i)
                    for (int j = 0; j < words_amount; ++j)
                        B[i, j] = 0;

                ///THETA STEP///
                for (int i = 0; i < words_amount; ++i)
                    C[i] = S[i, 0] ^ S[i, 1] ^ S[i, 2] ^ S[i, 3] ^ S[i, 4];

                for (int i = 0; i < words_amount; ++i)
                    D[i] = C[(i + 4) % words_amount] ^ cyclicShift(C[(i + 1) % 5], 1);

                for (int i = 0; i < words_amount; ++i)
                    for (int j = 0; j < words_amount; ++j)
                        S[i, j] = S[i, j] ^ D[i];

                ///PI STEP///
                for (int i = 0; i < words_amount; ++i)
                    for (int j = 0; j < words_amount; ++j)
                        B[j, (2 * i + 3 * j) % 5] = cyclicShift(S[i, j], shift[i, j]);

                ///CHI STEP///
                for (int i = 0; i < words_amount; ++i)
                    for (int j = 0; j < words_amount; ++j)
                        S[i, j] = B[i, j] ^ ((~B[(i + 1) % 5, j]) & B[(i + 2) % 5, j]);

                ///IOTA STEP///
                S[0, 0] = S[0, 0] ^ RC[round_number];
            }
        }

        private static UInt64 cyclicShift(UInt64 word, int n)
        {
            n = n % (8 * w);
            return ((word << n) | (word >> (8 * w - n)));
        }

        private static UInt64[][] charMessageToUInt64Words(Stack<char> M)
        {
            int padded_message_length = M.Count();

            //message represented in blocks of b bytes length
            UInt64[][] P = new UInt64[padded_message_length / r][];

            for (int i = 0; i < padded_message_length / r; i++)
            {
                P[i] = new UInt64[b / w];

                for (int j = 0; j < b / w; j++)
                    P[i][j] = 0;

                for (int j = 0; j < r / w; j++)
                {
                    //writing first r bytes to Pi block
                    for (int _byte = 0; _byte < w; _byte++)
                        P[i][j] = (P[i][j] << w) | M.Pop();
                }

                //reversing written bytes order 
                Array.Reverse(P[i], 0, r / w);
            }

            //reversing blocks order 
            Array.Reverse(P);

            return P;
        }

        private static string uInt64ArrayToHexString(UInt64[] result)
        {
            string Z = "";

            for (int i = 0; i < result.Length; i++)
                Z += BitConverter.ToString(BitConverter.GetBytes(result[i]).ToArray()).Replace("-", "");

            return Z;
        }

        public static string createHashOf(string message, UInt16 hash_length)
        {
            switch (hash_length)
            {
                case 128: r = 1344; c = 256; break;
                case 224: r = 1156; c = 448; break;
                case 256: r = 1088; c = 512; break;
                case 384: r = 832; c = 768; break;
                case 512: r = 576; c = 1024; break;
            }

            d = (UInt16)(hash_length / 8);
            r /= 8;
            c /= 8;


            ///PADDING AND INITIALIZATION///
            //writing bytes into stack because of their little-endian representation in the memory
            Stack<char> M = new Stack<char>();
            for (int i = 0; i < message.Length; i++)
                M.Push(message[i]);

            Pad(M); //padding the message to length that's multiple to r

            UInt64[,] S = new UInt64[words_amount, words_amount]; //state string
            for (int i = 0; i < words_amount; i++)
                for (int j = 0; j < words_amount; j++)
                    S[i, j] = 0;


            ///ABSORBING///
            UInt64[][] P = charMessageToUInt64Words(M);
            foreach (UInt64[] Pi in P)
            {
                for (int i = 0; i < words_amount; i++)
                    for (int j = 0; j < words_amount; j++)
                        S[i, j] ^= Pi[i + words_amount * j];

                f(ref S);
            }

            ///SQUEEZING///
            //this is need to be done just because otherwise it does not work properly
            for (int i = 0; i < words_amount; i++)
            {
                for (int j = i; j < words_amount; j++)
                {
                    Utils.Swap(ref S[i, j], ref S[j, i]);
                }
            }

            //resulting first d bytes of state string are hash value
            UInt64[] result = new UInt64[d];
            for (int i = 0; i < words_amount; i++)
            {
                for (int j = 0; j < words_amount; j++)
                {
                    if (words_amount * i + j < result.Length)
                        result[words_amount * i + j] = S[i, j];
                }
            }

            return uInt64ArrayToHexString(result).ToLower().Substring(0, 2 * d);
        }
    }
}