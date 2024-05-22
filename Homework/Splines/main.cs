using System;
using System.IO;
using static System.Console;

public static class main{
	static void Main(){
		double[] x = {0, 1, 2, 3, 4, 5, 6, 7, 8, 9};
		double[] ly = new double[x.Length];
		double[] qy = new double[x.Length];
		for (int i=0; i< x.Length; i++){ 
			ly[i] = Math.Cos(x[i]);
			qy[i] = Math.Sin(x[i]);
		}
		
		string data = "data.txt";
		using (StreamWriter writer = new StreamWriter(data)){
			for (int i=0; i<x.Length; i++){
				writer.WriteLine($"{x[i]} {ly[i]} {qy[i]}");
			}}

		double stepSize = 0.1;
		int numberOfZs = (int)((x[x.Length-1]-x[0])/stepSize) +1;
		double[] z = new double[numberOfZs];


		double[] lint = new double[numberOfZs];
		double[] lintegral = new double[numberOfZs];

		for (int i=0; i<numberOfZs; i++){
			z[i] = x[0] + i*stepSize;
			lint[i] = Lsplines.linterp(x,ly,z[i]);
			lintegral[i] = Lsplines.linterpInteg(x, ly, z[i]);
		}
			
		string linterpolation = "linterp.txt";
		using (StreamWriter writer = new StreamWriter(linterpolation)){
			for (int i=0; i<z.Length; i++){
				writer.WriteLine($"{z[i]} {lint[i]} {lintegral[i]}");
			}}

		qspline sin = new qspline(x, qy);
		string qinterp = "qinterp.txt";
		using (StreamWriter writer = new StreamWriter(qinterp)){
			for (int i=0; i<z.Length; i++){
				writer.WriteLine($"{z[i]} {sin.evaluate(z[i])} {sin.integral(z[i])}");
		}}


	}//Main
}//class main
