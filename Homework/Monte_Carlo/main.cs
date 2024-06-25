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

	static double corput(int n, int b){
		double q = 0;
		double bk = 1/b;
		while (n > 0){
			q += (n % b)*bk;
			n /= b;
			bk /= b;
		}
		return q;
	}//corput 

	static vector Halton(int n, int b){
		vector x = new vector(b);
		int[] Base = { 2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37, 41, 43, 47, 53, 59, 61, 67 };
		int b_max = Base.Length;
		if (b > b_max) { throw new Exception("Bad dimension :((");}
		for (int i = 0; i < b; i++){
			x[i] = corput(n, Base[i]);
		}
		return x;
	}//Halton

	public static (double, double) quasiMC(Func<vector, double> f, vector a, vector b, int N){
                int dim = a.size;
                double V = 1;
                for (int i = 0; i < dim; i++){
                        V *= b[i] - a[i];
                }

                double sum = 0;
                double sum2 = 0;
                var x = new vector(dim);

                for (int i = 0; i<N; i++){
			var halt = Halton(i, dim);
			var corp = corput(i, 19);
                        for (int k = 0; k < dim; k++){
                                x[k] = a[k] + halt[k]*(b[k] - a[k]);
                        }
                        sum += f(x);
                        for (int k = 0; k < dim; k++){
				x[k] = a[k] + corp*(b[k] - a[k]);
			}
			sum += f(x);
                }

                double mean = sum/N;
                double sigma = Abs(sum - sum2)/N;

                var result = (mean*V, sigma*V); //integrale-værdi, estimeret fejl
                return result;
        }//HaltonMC
}//MonteCarlo

public class main{
	public static void Main(){
		Func<vector, double> UnitCircle = (vector x) => {
			return (x[0]*x[0] + x[1]*x[1] <= 1) ? 1 : 0;
		};

		vector a = new vector(-1, -1);
		vector b = new vector(1, 1);
		
		string data = "UnitCircle.txt";
		using (StreamWriter writer = new StreamWriter(data)){
		for (int N=10; N < 500000; N+=1000){
			var result = MonteCarlo.plainMC(UnitCircle, a, b, N);
			writer.WriteLine($"{N} {result.Item1} {result.Item2} {1/Sqrt(N)}");
		}}

                string dataquasi = "UnitCirclequasi.txt";
                using (StreamWriter writer = new StreamWriter(dataquasi)){
                for (int N=10; N < 500000; N+=1000){
                        var result = MonteCarlo.quasiMC(UnitCircle, a, b, N);
                        writer.WriteLine($"{N} {result.Item1} {result.Item2}");
                }}
		

		Func<vector, double> CosIntegral = (vector x) => {
			double cosprod = Cos(x[0]) * Cos(x[1]) * Cos(x[2]);
			return 1.0 / (1 - cosprod);
		};

		vector acos = new vector(0, 0, 0);
		vector bcos = new vector(PI, PI, PI);

		int Ncos = 500000;
		var resultcos = MonteCarlo.plainMC(CosIntegral, acos, bcos, Ncos);
		var Haltoncos = MonteCarlo.quasiMC(CosIntegral, acos, bcos, Ncos);
		
		Console.WriteLine($"Plain Monte Carlo: Estimated value = {resultcos.Item1/Pow(PI,3)}, Estimated error = {resultcos.Item2}");
		Console.WriteLine($"Halton Monte Carlo: Estimated value = {Haltoncos.Item1/Pow(PI,3)}, Estimated error = {Haltoncos.Item2}");
		
		Console.WriteLine($"Actual value = {1.3932039296856768591842462603255}");
	}//Main
}//main
