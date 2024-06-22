using System;
using static matrix;
using System.Collections.Generic;

public class ODE{
        public static (vector, vector) rkstep12(Func<double, vector, vector> f, double x, vector y, double h){
                //return type: to vector
                //hedder rkstep12
                //skal bruge en funktion f, variable x og h, samt en vektor y
                vector k0 = f(x,y);
                vector k1 = f(x + h/2, y + k0*(h/2));

                //Estimere y(x+h)
                vector yh = y + k1*h;

                //estimere usikkerheden
                vector dy = (k1-k0)*h;
                return (yh, dy);
        }//rkstep12

        public static (List<double>, List<vector>) driver(Func<double, vector, vector> F, (double, double) interval,
                        vector ystart, double h = 0.125, double acc = 0.01, double eps = 0.01) {
                //return type: to lister, en med vectorer og en med doubles
                //hedder driver
                //skal bruge en function f, et interval, en startvektor for y, og så har den en initial stepsize
                //en absolut præsicions mål og en relativ præcisions mål.
                var (a,b) = interval; //a er starten af intervallet og b er slut punktet
                double x = a;
                vector y = ystart.copy(); //så vi ikke kommer til at ændre i den originale ystart vektor

                var xlist = new List<double>();
                var ylist = new List<vector>();

                xlist.Add(x); //Tilføjer x(start af intervallet)
                ylist.Add(y); //tilføjer start y-værdien

                do{
                        if (x >= b) return (xlist, ylist); //x >= b betyder vi er ved slutningen af intervallet

                        if (x + h > b) h = b - x; //sidste skridt skal slutte ved b

                        var (yh, dy) = rkstep12(F, x, y, h);
                        double tol = (acc + eps*yh.norm()) * Math.Sqrt(h/(b-a));
                        double err = dy.norm();

                        if (err <= tol){
                                x += h;
                                y = yh;
                                xlist.Add(x);
                                ylist.Add(y);
                        }

                        h *= Math.Min(Math.Pow(tol/err, 0.25) * 0.95, 2); //Readjust the stepsize
                } while (true); //Continues the loop as long as x <= b

        }//driver
}
