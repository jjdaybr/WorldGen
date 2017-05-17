using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;
using System.IO;

namespace WorldGen
{
    class PNoise
    {
        static Random rand = new Random();
        //static List<float> randomList1 = new List<float>();
        //static List<float> randomList2 = new List<float>();
        //static int listSize = 0;
        static int listSeed = int.MinValue;
        /*
        public static void InitializeRandomList(int seed, int size)
        {
            listSeed = seed;
            listSize = size;
            rand = new Random(seed);
            randomList1 = new List<float>();
            int n;
            for (n = 0; n < size; n++)
            {
                //randomList1.Add(((float)rand.NextDouble() * 2.0f) - 1.0f);
                //randomList2.Add(((float)rand.NextDouble() * 2.0f) - 1.0f);
                randomList1.Add((float)Math.Sin(rand.NextDouble() * Math.PI));
                randomList2.Add((float)Math.Cos(rand.NextDouble() * Math.PI));
            }
            //for (n = 0; n < size; n++)
            //{
            //    Console.WriteLine(randomList[n].ToString());
            //}
        }

        public static float Noise1(int t)
        {
            //Console.WriteLine( (t % listSize) .ToString());
            int i = Math.Abs(t);
            return randomList1[i % listSize];
        }
        public static float Noise2(int t)
        {
            //Console.WriteLine( (t % listSize) .ToString());
            int i = Math.Abs(t);
            return randomList2[i % listSize];
        }
        /*
        public static float Lerp(float t)
        {
            int iC = (int)Math.Ceiling(t);
            int iF = (int)Math.Floor(t);
            if (iC == iF)
            {
                return Noise1(iC);
            }
            //Console.WriteLine( "t: " + t.ToString() + " iC: " + iC.ToString() + " iF: " + iF.ToString());
            float A = Noise1(iF);
            float B = Noise1(iC);
            //return A + (iC -t) * (B - A);
            return A + (t - iF) * (B - A);
        }
        
        public static float GetPoint(float t)
        {
            float iC = (float)Math.Ceiling(t);
            float iF = (float)Math.Floor(t);
            float dt = (t - iF);
            return dt;
        }
        public static float Lerp2(float a0, float a1, float w)
        {
            return (1.0f - w) * a0 + w * a1;
        }

        public static float DotGridGradient(int ix, int iy, float x, float y)
        {
            float dx = x - ix;
            float dy = y - iy;
            return (dx * Noise1(ix * iy) + dy * Noise2(ix * iy));
        }

        public static float Perlin2(float x, float y)
        {
            int x0 = (x > 0.0 ? (int)x : (int)x - 1);
            int x1 = x0 + 1;
            int y0 = (y > 0.0 ? (int)y : (int)y - 1);
            int y1 = y0 + 1;

            float sx = x - x0;
            float sy = y - y0;

            float n0;
            float n1;
            float ix0;
            float ix1;
            n0 = DotGridGradient(x0, y0, x, y);
            n1 = DotGridGradient(x1, y0, x, y);
            ix0 = Lerp2(n0, n1, sx);
            n0 = DotGridGradient(x0, y1, x, y);
            n1 = DotGridGradient(x1, y1, x, y);
            ix1 = Lerp2(n0, n1, sx);
            return Lerp2(ix0, ix1, sy);
        }

        public static int GetPixelValue(float f)
        {
            int retval = 0;
            retval = (int)(((f + 1.0f) / 2.0f) * 255.0f);
            if (retval > 255)
                retval = 255;
            if (retval < 0)
                retval = 0;
            return retval;
        }

        public static void WriteExampleFile()
        {
            int imx = 1000;
            int imy = 1000;
            int sealevel = 128;

            Bitmap image = new Bitmap(imx, imy);
            image.SetPixel(0, 0, Color.White);

            int x;
            int y;
            for (y = 0; y < imy; y++)
            {
                for (x = 0; x < imx; x++)
                {
                    //int c = GetPixelValue(Perlin2(x/30.0f, y/30.0f));
                    int r = GetPixelValue((float)noise(x/50.0f, y/50.0f));
                    int g = GetPixelValue((float)noise(x/60.0f, y/60.0f));
                    int b = GetPixelValue((float)noise(x/70.0f, y/70.0f));
                    if (g > sealevel)
                        g = 255;
                    else
                        g = 0;
                    if (b > sealevel)
                        b = 255;
                    else
                        b = 0;
                    image.SetPixel(x, y, Color.FromArgb(0, g, b));
                }
            }

            
            image.Save("e:\\" + DateTime.Now.Ticks.ToString() + ".bmp", System.Drawing.Imaging.ImageFormat.Bmp);

        }
        */




        /*
 * https://github.com/SRombauts/SimplexNoise/blob/master/references/SimplexNoise.java
 * 
 * A speed-improved simplex noise algorithm for 2D, 3D and 4D in Java.
 *
 * Based on example code by Stefan Gustavson (stegu@itn.liu.se).
 * Optimisations by Peter Eastman (peastman@drizzle.stanford.edu).
 * Better rank ordering method by Stefan Gustavson in 2012.
 *
 * This could be speeded up even further, but it's useful as it is.
 *
 * Version 2012-03-09
 *
 * This code was placed in the public domain by its original author,
 * Stefan Gustavson. You may use it as you see fit, but
 * attribution is appreciated.
 *
 */
        private static readonly double F2 = 0.5 * (Math.Sqrt(3.0) - 1.0);
        private static readonly double G2 = (3.0 - Math.Sqrt(3.0)) / 6.0;
        private static short[] permMod12 = new short[512];
        private static short[] perm = new short[512];
        private static double dot(Grad g, double x, double y)
        {
            return g.x * x + g.y * y;
        }
        private class Grad
        {
            public double x, y, z, w;

            public Grad(double x, double y, double z)
            {
                this.x = x;
                this.y = y;
                this.z = z;
            }

            public Grad(double x, double y, double z, double w)
            {
                this.x = x;
                this.y = y;
                this.z = z;
                this.w = w;
            }
        }
        private static Grad[] grad3 = {new Grad(1,1,0),new Grad(-1,1,0),new Grad(1,-1,0),new Grad(-1,-1,0),
                                 new Grad(1,0,1),new Grad(-1,0,1),new Grad(1,0,-1),new Grad(-1,0,-1),
                                 new Grad(0,1,1),new Grad(0,-1,1),new Grad(0,1,-1),new Grad(0,-1,-1)};
        private static short[] p = {151,160,137,91,90,15,
            131,13,201,95,96,53,194,233,7,225,140,36,103,30,69,142,8,99,37,240,21,10,23,
            190, 6,148,247,120,234,75,0,26,197,62,94,252,219,203,117,35,11,32,57,177,33,
            88,237,149,56,87,174,20,125,136,171,168, 68,175,74,165,71,134,139,48,27,166,
            77,146,158,231,83,111,229,122,60,211,133,230,220,105,92,41,55,46,245,40,244,
            102,143,54, 65,25,63,161, 1,216,80,73,209,76,132,187,208, 89,18,169,200,196,
            135,130,116,188,159,86,164,100,109,198,173,186, 3,64,52,217,226,250,124,123,
            5,202,38,147,118,126,255,82,85,212,207,206,59,227,47,16,58,17,182,189,28,42,
            223,183,170,213,119,248,152, 2,44,154,163, 70,221,153,101,155,167, 43,172,9,
            129,22,39,253, 19,98,108,110,79,113,224,232,178,185, 112,104,218,246,97,228,
            251,34,242,193,238,210,144,12,191,179,162,241, 81,51,145,235,249,14,239,107,
            49,192,214, 31,181,199,106,157,184, 84,204,176,115,121,50,45,127, 4,150,254,
            138,236,205,93,222,114,67,29,24,72,243,141,128,195,78,66,215,61,156,180};
        private static short[] pDefault = {151,160,137,91,90,15,
            131,13,201,95,96,53,194,233,7,225,140,36,103,30,69,142,8,99,37,240,21,10,23,
            190, 6,148,247,120,234,75,0,26,197,62,94,252,219,203,117,35,11,32,57,177,33,
            88,237,149,56,87,174,20,125,136,171,168, 68,175,74,165,71,134,139,48,27,166,
            77,146,158,231,83,111,229,122,60,211,133,230,220,105,92,41,55,46,245,40,244,
            102,143,54, 65,25,63,161, 1,216,80,73,209,76,132,187,208, 89,18,169,200,196,
            135,130,116,188,159,86,164,100,109,198,173,186, 3,64,52,217,226,250,124,123,
            5,202,38,147,118,126,255,82,85,212,207,206,59,227,47,16,58,17,182,189,28,42,
            223,183,170,213,119,248,152, 2,44,154,163, 70,221,153,101,155,167, 43,172,9,
            129,22,39,253, 19,98,108,110,79,113,224,232,178,185, 112,104,218,246,97,228,
            251,34,242,193,238,210,144,12,191,179,162,241, 81,51,145,235,249,14,239,107,
            49,192,214, 31,181,199,106,157,184, 84,204,176,115,121,50,45,127, 4,150,254,
            138,236,205,93,222,114,67,29,24,72,243,141,128,195,78,66,215,61,156,180};
        public static void SimplexNoise()
        {
            for (int i = 0; i < 512; i++)
            {
                perm[i] = p[i & 255];
                permMod12[i] = (short)(perm[i] % 12);
            }
        }
        public static void SimplexNoise(int seed)
        {
            listSeed = seed;
            rand = new Random(seed);
            int n;
            for (n = 0; n < 256; n++)
            {
                p[n] = pDefault[n];
            }
            for (n = 0; n < 256; n++)
            {
                int pindex = rand.Next(0, 255);
                short p1 = p[n];
                p[n] = p[pindex];
                p[pindex] = p1;                
            }
            for (int i = 0; i < 512; i++)
            {
                perm[i] = p[i & 255];
                permMod12[i] = (short)(perm[i] % 12);
            }
        }
        public static double noise(double xin, double yin)
        {
            double n0, n1, n2; // Noise contributions from the three corners
            // Skew the input space to determine which simplex cell we're in
            double s = (xin + yin) * F2; // Hairy factor for 2D
            //int i = fastfloor(xin + s);
            //int j = fastfloor(yin + s);
            int i = (int)Math.Floor(xin + s);
            int j = (int)Math.Floor(yin + s);
            double t = (i + j) * G2;
            double X0 = i - t; // Unskew the cell origin back to (x,y) space
            double Y0 = j - t;
            double x0 = xin - X0; // The x,y distances from the cell origin
            double y0 = yin - Y0;
            // For the 2D case, the simplex shape is an equilateral triangle.
            // Determine which simplex we are in.
            int i1, j1; // Offsets for second (middle) corner of simplex in (i,j) coords
            if (x0 > y0) { i1 = 1; j1 = 0; } // lower triangle, XY order: (0,0)->(1,0)->(1,1)
            else { i1 = 0; j1 = 1; }      // upper triangle, YX order: (0,0)->(0,1)->(1,1)
            // A step of (1,0) in (i,j) means a step of (1-c,-c) in (x,y), and
            // a step of (0,1) in (i,j) means a step of (-c,1-c) in (x,y), where
            // c = (3-sqrt(3))/6
            double x1 = x0 - i1 + G2; // Offsets for middle corner in (x,y) unskewed coords
            double y1 = y0 - j1 + G2;
            double x2 = x0 - 1.0 + 2.0 * G2; // Offsets for last corner in (x,y) unskewed coords
            double y2 = y0 - 1.0 + 2.0 * G2;
            // Work out the hashed gradient indices of the three simplex corners
            int ii = i & 255;
            int jj = j & 255;
            int gi0 = permMod12[ii + perm[jj]];
            int gi1 = permMod12[ii + i1 + perm[jj + j1]];
            int gi2 = permMod12[ii + 1 + perm[jj + 1]];
            // Calculate the contribution from the three corners
            double t0 = 0.5 - x0 * x0 - y0 * y0;
            if (t0 < 0) n0 = 0.0;
            else
            {
                t0 *= t0;
                n0 = t0 * t0 * dot(grad3[gi0], x0, y0);  // (x,y) of grad3 used for 2D gradient
            }
            double t1 = 0.5 - x1 * x1 - y1 * y1;
            if (t1 < 0) n1 = 0.0;
            else
            {
                t1 *= t1;
                n1 = t1 * t1 * dot(grad3[gi1], x1, y1);
            }
            double t2 = 0.5 - x2 * x2 - y2 * y2;
            if (t2 < 0) n2 = 0.0;
            else
            {
                t2 *= t2;
                n2 = t2 * t2 * dot(grad3[gi2], x2, y2);
            }
            // Add contributions from each corner to get the final noise value.
            // The result is scaled to return values in the interval [-1,1].
            return 70.0 * (n0 + n1 + n2);
        }

        public static Bitmap GetRender(int width, int height,
            int seaLevel, float landStrengthX, float landStrengthY, float landOffsetX, float landOffsetY,
            int treeLevel, float treeStrengthX, float treeStrengthY, float treeOffsetX, float treeOffsetY,
            int mtnLevel, float mtnStrengthX, float mtnStrengthY, float mtnOffsetX, float mtnOffsetY,
            int riverLevel, int riverWindow, float riverStrengthX, float riverStrengthY, float riverOffsetX, float riverOffsetY)
        {
            int imx = width;
            int imy = height;
            Bitmap image = new Bitmap(imx, imy);

            int x;
            int y;
            for (y = 0; y < imy; y++)
            {
                for (x = 0; x < imx; x++)
                {
                    //int c = GetPixelValue(Perlin2(x/30.0f, y/30.0f));
                    int b = 0;
                    int r = 0;
                    int g = 0;
                    int gr = GetPixelValue((float)noise((x + landOffsetX) / landStrengthX, (y + landOffsetY) / landStrengthY));
                    int tl = GetPixelValue((float)noise((x + treeOffsetX) / treeStrengthX, (y + treeOffsetY) / treeStrengthY));
                    int mt = GetPixelValue((float)noise((x + mtnOffsetX) / mtnStrengthX, (y + mtnOffsetY) / mtnStrengthY));
                    int rv = GetPixelValue((float)noise((x + riverOffsetX) / riverStrengthX, (y + riverOffsetY) / riverStrengthY));
                    //int b = GetPixelValue((float)noise(x / 70.0f, y / 70.0f));
                    if (gr > seaLevel)
                    {
                        r = 0xb7;
                        g = 0xe7;
                        b = 0x2e;
                        
                        if (tl > treeLevel)
                        {
                            r = 0x36;
                            g = 0x8b;
                            b = 0x20;
                        }
                        if (rv > (riverLevel - riverWindow) && rv < (riverLevel + riverWindow))
                        {
                            r = 0x4a;
                            g = 0xc7;
                            b = 0xde;
                        }
                        if (mt > mtnLevel)
                        {
                            r = 0x9e;
                            g = 0x9e;
                            b = 0x9e;
                        }
                    }
                    else
                    {
                        r = 0x3b;
                        g = 0x72;
                        b = 0xcf;
                    }
                    //Console.WriteLine("rv: " + rv.ToString() + " min: " + (riverLevel - riverWindow).ToString() + " max: " + (riverLevel + riverWindow).ToString());
                    
                    image.SetPixel(x, y, Color.FromArgb(r, g, b));
                }
            }


            return image;
        }


        public static int GetPixelValue(float f)
        {
            int retval = 0;
            retval = (int)(((f + 1.0f) / 2.0f) * 255.0f);
            if (retval > 255)
                retval = 255;
            if (retval < 0)
                retval = 0;
            return retval;
        }


    }
}
