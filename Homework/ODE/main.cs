using System;
using System.IO;
using static System.Console;
using System.Collections.Generic;
using static matrix;

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

public static class main{
	public static vector HarmOsci(double x, vector y){
		return new vector(y[1], -y[0]);
	}

	public static vector DampOsci(double x, vector y, double gamma, double omega){
		return new vector(y[1], -2*gamma*y[1] - omega*omega*y[0]);
	}
	
	static void Main(){
		vector ystart = new vector(1.0, 0.0);
		var (xlist_HO, ylist_HO) = ODE.driver(HarmOsci, (0,10), ystart);
		
		string Harm = "HarmOsci.txt";
		using (StreamWriter writer = new StreamWriter(Harm)){
			for (int i=0; i < xlist_HO.Count; i++){
				writer.WriteLine($"{xlist_HO[i]} {ylist_HO[i][0]} {ylist_HO[i][1]}");
		}}
	
		double gamma = 0.1;
		double omega = 2.0;
		Func<double, vector, vector> dampOsci = (x,y) => DampOsci(x, y, gamma, omega);
		var (xlist_DO, ylist_DO) = ODE.driver(dampOsci, (0,10), ystart);

		string Damp = "DampOsci.txt";
		using (StreamWriter writer = new StreamWriter(Damp)){
			for (int i=0; i < xlist_DO.Count; i++){
				writer.WriteLine($"{xlist_DO[i]} {ylist_DO[i][0]} {ylist_DO[i][1]}");
		}}


	}//Main()
}//main
