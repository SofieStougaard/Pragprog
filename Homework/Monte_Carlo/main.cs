using System;
using static System.Console;
using static System.Math;
using System.IO;
using static matrix;

public class MonteCarlo{
	public static (double, double) plainMC(Func<vector, double> f, vector a, vector b, int N){
		int dim = a.size;
		double V = 1;
		for (int i = 0; i < dim; i++){
			V *= b[i] - a[i];
		}

		double sum = 0;
		double sum2 = 0;
		var x = new vector(dim);
		var rnd = new Random();

		for (int i = 0; i<N; i++){
			for (int k = 0; k < dim; k++){
				x[k] = a[k] + rnd.NextDouble()*(b[k] - a[k]);
			}
			double fx = f(x);
			sum += fx;
			sum2 += fx*fx;
		}
		double mean = sum/N;
		double sigma = Sqrt(sum2/N - mean*mean);

		var result = (mean * V, sigma*V/Sqrt(N)); //integrale-værdi, estimeret fejl
		return result;
	}//plainMC
}//MonteCarlo

public class main{
	public static void Main(){
		Func<vector, double> UnitCircle = (vector x) => {
			return (x[0]*x[0] + x[1]*x[1] <= 1) ? 1 : 0;
		};

		vector a = new vector(2); a[0]=-1; a[1]=-1;
		vector b = new vector(2); b[0]=1; b[1]=1;
		
		string data = "UnitCircle.txt";
		using (StreamWriter writer = new StreamWriter(data)){
		for (int N=10; N < 500000; N+=1000){
			var result = MonteCarlo.plainMC(UnitCircle, a, b, N);
			writer.WriteLine($"{N} {result.Item1} {result.Item2} {1/Sqrt(N)}");
		}}
		

		Func<vector, double> CosIntegral = (vector x) => {
			double cosprod = Cos(x[0]) * Cos(x[1]) * Cos(x[2]);
			return 1.0 / (1 - cosprod);
		};

		vector acos = new vector(3); acos[0]=0; acos[1]=0; acos[2]=0;
		vector bcos = new vector(3); bcos[0]=PI; bcos[1]=PI; bcos[2]=PI;

		int Ncos = 500000;
		var resultcos = MonteCarlo.plainMC(CosIntegral, acos, bcos, Ncos);
		Console.WriteLine($"Estimated value = {resultcos.Item1/Pow(PI,3)}, Estimated error = {resultcos.Item2}");
		Console.WriteLine($"Actual value = {1.3932039296856768591842462603255}");
	}//Main
}//main
