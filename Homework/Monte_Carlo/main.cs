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

	static int[] bases = {2,3,5,7,11,13,17,19,23,29,31,37,41,43,47,53,59,61};

	public static double Halton(int n, int b){
		double result = 0;
		double f = 1.0/bases[b];
		while (n > 0){
			result += (n % bases[b]) * f;
			n /= bases[b];
			f /= bases[b];
		}
		return result;
	}//Halton

	public static (double, double) HaltonMC(Func<vector, double> f, vector a, vector b, int N){
                int dim = a.size;
                double V = 1;
                for (int i = 0; i < dim; i++){
                        V *= b[i] - a[i];
                }

                double sum = 0;
                double sum2 = 0;
                var x = new vector(dim);

                for (int i = 0; i<N; i++){
                        for (int k = 0; k < dim; k++){
                                x[k] = a[k] + Halton(i, k)*(b[k] - a[k]);
                        }
                        double fx = f(x);
                        sum += fx;
                        sum2 += fx*fx;
                }
                double mean = sum/N;
                double sigma = Sqrt(sum2/N - mean*mean);

                var result = (mean * V, sigma*V/Sqrt(N)); //integrale-værdi, estimeret fejl
                return result;
        }//HaltonMC
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

                string dataHalton = "UnitCircleHalton.txt";
                using (StreamWriter writer = new StreamWriter(dataHalton)){
                for (int N=10; N < 500000; N+=1000){
                        var result = MonteCarlo.HaltonMC(UnitCircle, a, b, N);
                        writer.WriteLine($"{N} {result.Item1} {result.Item2}");
                }}
		

		Func<vector, double> CosIntegral = (vector x) => {
			double cosprod = Cos(x[0]) * Cos(x[1]) * Cos(x[2]);
			return 1.0 / (1 - cosprod);
		};

		vector acos = new vector(3); acos[0]=0; acos[1]=0; acos[2]=0;
		vector bcos = new vector(3); bcos[0]=PI; bcos[1]=PI; bcos[2]=PI;

		vector acosH = new vector(3); acosH[0]=1e-3; acosH[1]=1e-3; acosH[2]=1e-5;
		vector bcosH = new vector(3); bcosH[0]=3.1415; bcosH[1]=3.1415; bcosH[2]=3.1415;

		int Ncos = 500000;
		var resultcos = MonteCarlo.plainMC(CosIntegral, acos, bcos, Ncos);
		var Haltoncos = MonteCarlo.HaltonMC(CosIntegral, acosH, bcosH, Ncos);
		
		Console.WriteLine($"Plain Monte Carlo: Estimated value = {resultcos.Item1/Pow(PI,3)}, Estimated error = {resultcos.Item2}");
		Console.WriteLine($"Halton Monte Carlo: Estimated value = {Haltoncos.Item1/Pow(PI,3)}, Estimated error = {Haltoncos.Item2}");
		
		Console.WriteLine($"Actual value = {1.3932039296856768591842462603255}");
	}//Main
}//main
