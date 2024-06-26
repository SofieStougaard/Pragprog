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

	public static double corput(int n, int b){
		double q = 0;
		double bk = (double)1/b;
		while (n > 0){
			q += (n % b)*bk;
			n /= b;
			bk /= b;
		}
		return q;
	}//corput 

	public static vector Halton(int n, int b){
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

                double sum_c = 0;
                double sum_h = 0;
                var x_c = new vector(dim);
		var x_h = new vector(dim);
		
                for (int i = 1; i<N; i++){
			var halt = Halton(i, dim);
			for (int k = 0; k < dim; k++){
                                x_h[k] = a[k] + halt[k]*(b[k] - a[k]);
                        }
                        sum_h += f(x_h);
			
                        for (int k = 0; k < dim; k++){
				x_c[k] = a[k] + corput(i+k*N, 19)*(b[k] - a[k]);
			}
			sum_c += f(x_c);
                }

                double mean = sum_h/N;
                double sigma = Abs(sum_h - sum_c)/N;
                var result = (mean*V, sigma*V); //integrale-værdi, estimeret fejl

                return result;
        }//QuasiMC
}//MonteCarlo

public class main{
	public static void Main(){
		Func<vector, double> UnitCircle = z => z[0];

		vector a = new vector(0, 0);
		vector b = new vector(1, 2*PI);

		Func<vector, double> CosIntegral =  z => 1.0 / ((1.0 - Cos(z[0]) * Cos(z[1]) * Cos(z[2])) * PI * PI * PI);

                vector acos = new vector(0, 0, 0);
                vector bcos = new vector(PI, PI, PI);
		
		string plainUnit = "UnitCircle.txt";
		using (StreamWriter writer = new StreamWriter(plainUnit)){
		for (int N=1; N < 10000; N+=200){
			var result = MonteCarlo.plainMC(UnitCircle, a, b, N);
			writer.WriteLine($"{N} {result.Item1} {result.Item2} {1/Sqrt(N)}");
		}}

                string quasiunit = "UnitCirclequasi.txt";
                using (StreamWriter writer = new StreamWriter(quasiunit)){
                for (int N=1; N < 10000; N+=200){
                        var result = MonteCarlo.quasiMC(UnitCircle, a, b, N);
                        writer.WriteLine($"{N} {result.Item1} {result.Item2}");
                }}
		
		int Ncos = 100000;
		var plaincos = MonteCarlo.plainMC(CosIntegral, acos, bcos, Ncos);
		var plainunit = MonteCarlo.plainMC(UnitCircle, a, b, Ncos);
		
		var Quasicos = MonteCarlo.quasiMC(CosIntegral, acos, bcos, Ncos);
		var Quasiunit =  MonteCarlo.quasiMC(UnitCircle, a, b, Ncos);

		Console.WriteLine($"Plain Monte Carlo: Estimated value = {plainunit.Item1}, Estimated error = {plainunit.Item2} actual value {PI}");
		Console.WriteLine($"Plain Monte Carlo: Estimated value = {plaincos.Item1}, Estimated error = {plaincos.Item2} actual value 1.3932039");

		Console.WriteLine($"Quasi Monte Carlo: Estimated value = {Quasiunit.Item1}, Estimated error = {Quasiunit.Item2} actual value {PI}");
		Console.WriteLine($"Quasi Monte Carlo: Estimated value = {Quasicos.Item1}, Estimated error = {Quasicos.Item2} actual value 1.3932039");
	}//Main
}//main
