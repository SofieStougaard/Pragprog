using System;
using System.IO;
using static System.Console;
using System.Collections.Generic;
using static matrix;
using static ODE;

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
