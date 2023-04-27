using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mapping
{
    int xresolution;
    int yresolution;

    public Mapping(int xresolution, int yresolution) {
        this.xresolution = xresolution;
        this.yresolution = yresolution;
    }


    public float ColorHeight(Color[] arrayCol, int index, int x, int y) 
    {
        float red = arrayCol[index][0];
        float green = arrayCol[index][1];
        float blue = arrayCol[index][2];
        float total = 1;
        total -= blue / 48;
        total += (green * Mathf.PerlinNoise(x * 0.07f, y * 0.07f) * 0.5f) / 30;
        total += (red * Mathf.PerlinNoise(x * 0.3f, y * 0.3f) * 0.5f) / 8;
        return total;
    }

    public float[] MapF(Color[] array) 
    {
        float[] vals = new float[(xresolution / 4) * (xresolution / 4)];
        int counter = 0;
        for (int y = yresolution / 4; y < yresolution - yresolution / 4; y++) {
            for (int x = (xresolution / 8) * 5; x > (xresolution / 8) * 3; x--) {
                int i = x + y * xresolution;
                vals[counter] = ColorHeight(array, i, x, y);
                counter++;
            }
        }
        return vals;
    }

    public float[] MapR(Color[] array) 
    {
        float[] vals = new float[(xresolution / 4) * (xresolution / 4)];
        int counter = 0;
        for (int y = yresolution / 4; y < yresolution - yresolution / 4; y++) {
            for (int x = xresolution - xresolution / 8; x > (xresolution / 8) * 5; x--) {
                int i = x + y * xresolution;
                vals[counter] = ColorHeight(array, i, x, y);
                counter++;
            }
        }
        return vals;
    }

    public float[] MapL(Color[] array) 
    { 
        float[] vals = new float[(xresolution / 4) * (xresolution / 4)];
        int counter = 0;
        for (int y = yresolution / 4; y < yresolution - yresolution / 4; y++) {
            for (int x = (xresolution / 8) * 3; x > xresolution / 8; x--) {
                int i = x + y * xresolution;
                vals[counter] = ColorHeight(array, i, x, y);
                counter++;
            }
        }
        return vals;
    }

    public float[] MapB(Color[] array)
    {
        float[] vals = new float[(xresolution / 4) * (xresolution / 4)];
        int counter = 0;
        for (int y = yresolution / 4; y < yresolution - yresolution / 4; y++) {
            for (int x = xresolution / 8; x > 0; x--) {
                int i = x + y * xresolution;
                vals[counter] = ColorHeight(array, i, x, y);
                counter++;
            }
            for (int x = xresolution; x > xresolution - (xresolution / 8); x--) {
                int i = x + y * xresolution;
                vals[counter] = ColorHeight(array, i, x, y);
                counter++;
            }
        }
        return vals;
    }


    public float[] MapU(Color[] array) 
    {
        float[] vals = new float[(xresolution / 4) * (xresolution / 4)];
        // FRONT needs fixing
        int counter = 0;
        int skipmax = -2;
        int rl = -2;
        for (int y = yresolution / 4; y >= 0; y--) { // y
            skipmax += 2;
            int skip = 0;
            int[] skiparray = new int[skipmax];
            for (int d = 0; d < skipmax; d++) {
                skiparray[d] = skip;
                skip += 1;
            }
            rl += 2;
            counter += rl/2;
            for (int x = (xresolution / 8) * 3; x < (xresolution / 8) * 5; x++) { // x
                for (int q = 0; q < skipmax; q++) {
                    if (x == (xresolution / 8) * 3 + skiparray[q]) {x++;}
                }
                int i = x + y * xresolution;
                vals[counter] = ColorHeight(array, i, x, y);
                counter++;
            }
            counter += rl/2;
        }

        // BACK
        counter = vals.Length - 1;
        skipmax = -2;
        rl = -2;
        for (int y = yresolution / 4; y >= 0; y--) {
            skipmax += 2;
            int skip = 0;
            int[] skiparray1 = new int[skipmax/2];
            int[] skiparray2 = new int[skipmax/2];
            for (int d = 0; d < skipmax/2; d++) {
                skiparray1[d] = skip;
                skip += 1;
            }
            skip = 0;
            for (int dd = 0; dd < skipmax/2; dd++) {
                skiparray2[dd] = skip;
                skip += 1;
            }
            rl += 2;    
            counter -= rl/2; 
            for (int x = xresolution - (xresolution / 8); x < xresolution; x++) {
                for (int q = 0; q < skipmax/2; q++) {
                    if (x == xresolution - (xresolution / 8) + skiparray1[q]) {
                        x++;
                    }
                }
                int i = x + y * xresolution;
                vals[counter] = ColorHeight(array, i, x, y);
                counter--;
            }
            for (int x = 0; x < xresolution / 8; x++) {  // start counter at end of vals[]
                for (int q = 0; q < skipmax/2; q++) {
                    if (x == 0 + skiparray2[q]) {
                        x++;
                    }
                }
                int i = x + y * xresolution;
                vals[counter] = ColorHeight(array, i, x, y);
                counter--;
            }
            counter -= rl/2;
        }

        // RIGHT
        counter = 0;
        skipmax = -2;
        rl = -(xresolution/2);
        int re = 0;
        for (int y = yresolution / 4; y >= 0; y--) {
            skipmax += 2;
            int skip = 0;
            int[] skiparray = new int[skipmax];
            for (int d = 0; d < skipmax; d++) {
                skiparray[d] = skip;
                skip += 1;
            }
            rl += xresolution/2;
            counter += rl/2 ;
            for (int x = (xresolution / 8) * 5; x < (xresolution / 8) * 7; x++) {
                for (int q = 0; q < skipmax; q++) {
                    if (x == (xresolution / 8) * 5 + skiparray[q]) {
                        x++;
                    }
                }
                int i = x + y * xresolution;
                vals[counter] = ColorHeight(array, i, x, y);
                counter += xresolution/4;
            }
            re++;
            counter = xresolution/4 - re;
        }

        // LEFT
        counter = 0;
        skipmax = -2;
        rl = -(xresolution/2);
        re = 0;
        for (int y = yresolution / 4; y >= 0; y--) {
            skipmax += 2;
            int skip = 0;
            int[] skiparray = new int[skipmax];
            for (int d = 0; d < skipmax; d++) {
                skiparray[d] = skip;
                skip += 1;
            }
            rl += xresolution/2;
            counter += rl/2 ;
            for (int x = (xresolution / 8) * 3-1; x > (xresolution / 8) * 1; x--) {
                for (int q = 0; q < skipmax; q++) {
                    if (x == (xresolution / 8) * 3 - skiparray[q]) {
                        x--;
                    }
                }
                int i = x + y * xresolution;
                vals[counter] = ColorHeight(array, i, x, y);
                counter += xresolution/4;
            }
            re++;
            counter = re;
        }
        return vals;
    }

    public float[] MapD(Color[] array) {
        
        float[] vals = new float[(xresolution / 4) * (xresolution / 4)];

        // FRONT needs fixing
        int counter = 0;
        int skipmax = -2;
        int rl = -2;
        for (int y = (yresolution - yresolution / 4); y <= yresolution-1; y++) { // y
            skipmax += 2;
            int skip = 0;
            int[] skiparray = new int[skipmax];
            for (int d = 0; d < skipmax; d++) {
                skiparray[d] = skip;
                skip += 1;
            }
            rl += 2;    
            counter += rl/2;
            for (int x = (xresolution / 8) * 5; x > (xresolution / 8) * 3; x--) { // x
                for (int q = 0; q < skipmax; q++) {
                    if (x == (xresolution / 8) * 5 - skiparray[q]) {
                        x--;
                    }
                }
                int i = x + y * xresolution;
                vals[counter] = ColorHeight(array, i, x, y);
                counter++;
            }
            counter += rl/2;
        }

        // BACK
        counter = vals.Length - 1;
        skipmax = -2;
        rl = -2;
        for (int y = (yresolution - yresolution / 4); y <= yresolution-1; y++) {
            skipmax += 2;
            int skip = 0;
            int[] skiparray1 = new int[skipmax/2];
            int[] skiparray2 = new int[skipmax/2];
            for (int d = 0; d < skipmax/2; d++) {
                skiparray1[d] = skip;
                skip += 1;
            }
            skip = 0;
            for (int dd = 0; dd < skipmax/2; dd++) {
                skiparray2[dd] = skip;
                skip += 1;
            }
            rl += 2;    
            counter -= rl/2; 
            for (int x = xresolution / 8; x > 0; x--) {  // start counter at end of vals[]
                for (int q = 0; q < skipmax/2; q++) {
                    if (x == xresolution / 8 - skiparray2[q]) {
                        x--;
                    }
                }
                int i = x + y * xresolution;
                vals[counter] = ColorHeight(array, i, x, y);
                counter--;
            }
            for (int x = xresolution; x > xresolution - (xresolution / 8); x--) {
                for (int q = 0; q < skipmax/2; q++) {
                    if (x == xresolution - skiparray1[q]) {
                        x--;
                    }
                }
                int i = x + y * xresolution;
                vals[counter] = ColorHeight(array, i, x, y);
                counter--;
            }
            counter -= rl/2;
        }

        // RIGHT
        counter = 0;
        skipmax = -2;
        rl = -(xresolution/2);
        int re = 0;
        for (int y = (yresolution - yresolution / 4); y <= yresolution-1; y++) {
            skipmax += 2;
            int skip = 0;
            int[] skiparray = new int[skipmax];
            for (int d = 0; d < skipmax; d++) {
                skiparray[d] = skip;
                skip += 1;
            }
            rl += xresolution/2;
            counter += rl/2 ;
            for (int x = (xresolution / 8) * 3; x > (xresolution / 8) * 1; x--) {
                for (int q = 0; q < skipmax; q++) {
                    if (x == (xresolution / 8) * 3 - skiparray[q]) {
                        x--;
                    }
                }
                int i = x + y * xresolution;
                vals[counter] = ColorHeight(array, i, x, y);
                counter += xresolution/4;
            }
            re++;
            counter = xresolution/4 - re;
        }
        
        
        
        // LEFT
        counter = 0;
        skipmax = -2;
        rl = -(xresolution/2);
        re = 0;
        for (int y = (yresolution - yresolution / 4); y <= yresolution-1; y++) {
            skipmax += 2;
            int skip = 0;
            int[] skiparray = new int[skipmax];
            for (int d = 0; d < skipmax; d++) {
                skiparray[d] = skip;
                skip += 1;
            }
            rl += xresolution/2;
            counter += rl/2 ;
            for (int x = (xresolution / 8) * 5 + 1; x <= (xresolution / 8) * 7; x++) {
                for (int q = 0; q < skipmax; q++) {
                    if (x == (xresolution / 8) * 5 + skiparray[q]) {
                        x++;
                    }
                }
                int i = x + y * xresolution;
                vals[counter] = ColorHeight(array, i, x, y);
                counter += xresolution/4;
            }
            re++;
            counter = re;
        }
        return vals;
    }

}
