using System;
using static System.Console;
using System.IO;
using static matrix;
using static QRGS;
using static ODE;


public static class Root{
	public static matrix Jacobian(Func<vector, vector> f, vector x, vector fx = null, vector dx = null){
		int n = x.size;
		if (dx == null){
			dx = x.map(xi => Math.Abs(xi) * Math.Pow(2, -26));
		}

		if (fx == null){
			fx = f(x);
		}

		matrix J = new matrix(x.size);
		for (int j = 0; j<x.size; j++){
			x[j] += dx[j];
			vector df = f(x) - fx;
			for (int i = 0; i<x.size; i++) J[i,j] = df[i]/dx[j];
			x[j] -= dx[j];
		}
		return J;
	}//Jacobian

	public static vector Newton(Func<vector, vector> f, vector start, double acc=1e-2, vector dx =null){
		vector x = start.copy();
		vector fx = f(x);
		vector z, fz;

		do{
			if (fx.norm() < acc) break;

			matrix J = Jacobian(f, x, fx, dx);
			(matrix JQ, matrix JR) = QRGS.decomp(J);
			vector Dx = QRGS.solve(JQ, JR, -fx);

			double lambda = 1.0;
			do{
				z = x + lambda*Dx;
				fz = f(z);
				if (fz.norm() < (1-lambda/2)*fx.norm()) break;
				if (lambda < 1e-10) break;
				lambda /= 2;
			} while (true);

			x = z;
			fx = fz;
		} while (true);
		return x;
	}//Newton
}//Root


public static class Schrodinger{
	public static Func<double, vector, vector> SchrodingerEq(double E){
		return (r, v) => {
			double f = v[0];
			double g = v[1];
			return new vector(new double[]{
					g,
					-2*(E + 1/r)*f
					});
		};
	}//SchrodingerEq

	public static vector Initial(double rmin){
		return new vector(new double[] {
				rmin - rmin*rmin,
				1 - 2*rmin});
	}

	public static double M(Func<double, vector, vector> odeSolver, double E, double rmin, double rmax, double h, double acc, double eps){
		var (xlist, ylist) = ODE.driver(odeSolver, (rmin, rmax), Initial(rmin), h, acc, eps);
		return ylist[ylist.Count - 1][0]; // værdien af f(rmax)
	}//M
}//Schrodinger


public static class main{
	public static void Main(){
		Func<vector, vector> test = v =>{
			double x = v[0];
			return new vector(new double[]{
					-2*x + 5
					});
		};
		vector teststart = new vector(1); teststart[0] = 1;

		vector testRoot = Root.Newton(test, teststart);
		WriteLine($"Test function: -2x+5");
		WriteLine($"Test function root: {testRoot[0]}");

		
		Func<vector, vector> rosenbrockGradient = v => {
			double x = v[0];
			double y = v[1];
			return new vector(new double[]{
					-400*x*(y - x*x) - 2*(1-x),
					200*(y - x*x)
					});
		};

		Func<vector, vector> himmelblauGradient = v => {
			double x = v[0];
			double y = v[1];
			return new vector(new double[]{
					4*x*(x*x + y - 11) + 2*(x + y*y - 7),
					2*(x*x + y -11) + 4*y*(x + y*y -7)
					});
		};

		vector start = new vector(2); start[0]=1.0; start[1]=1.0;

		vector rosenbrockRoot = Root.Newton(rosenbrockGradient, start);
		Console.WriteLine($"Rosenbrock's function root: {rosenbrockRoot[0]}, {rosenbrockRoot[1]}");

		vector himmelblauRoot = Root.Newton(himmelblauGradient, start);
		Console.WriteLine($"Himmelblaus function root: {himmelblauRoot[0]}, {himmelblauRoot[1]}");

		//Exercise B
		Console.WriteLine($"\n\n");

		double rmin = 1e-4;
		double rmax = 8.0;
		double h = 0.01;
		double acc = 1e-5;
		double eps = 1e-5;

		Func<vector, vector> M = E => new vector(new double[] {
				Schrodinger.M(Schrodinger.SchrodingerEq(E[0]), E[0], rmin, rmax, h, acc, eps) });

		vector initialGuess = new vector(new double[] {-0.7});
		vector root = Root.Newton(M, initialGuess);

		double E0 = root[0];
		Console.WriteLine($"Laveste energi E0 : {E0}");

		var (xlist, ylist) = ODE.driver(Schrodinger.SchrodingerEq(E0), (rmin, rmax), Schrodinger.Initial(rmin), h, acc, eps);

		string hydrogen = "Hydrogen.txt";
		using (StreamWriter writer = new StreamWriter(hydrogen)){
			for (int i=0; i<xlist.Count; i++){
				writer.WriteLine($"{xlist[i]} {ylist[i][0]}");
			}}		
	}//Main
}//main
