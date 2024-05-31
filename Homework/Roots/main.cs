using System;
using static System.Console;
using static matrix;

public static class Root{
	public static matrix Jacobian(Func<vector, vector> f, vector x, vector fx = null, vector dx = null){
		int n = x.size;
		if (dx == null){
			dx = x.map(xi => Math.Abs(x[i]) * Math.Pow(2, -26));
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
			if (fx.Norm() < acc) break;

			matrix J = Jacobian(f, x, fx, dx);
			(matrix JQ, matrix JR) = matrix.decomp(J);
			vector Dx = matrix.solve(J, -fx);

			double lambda = 1.0;
			do{
				z = x + lambda *Dx;
				fz = f(z);
				if (fz.Norm() < (1-lambda/2)*fx.Norm()) break;
				if (lambda < 1e-10) break;
				lambda /= 2;
			} while (true);

			x = z;
			fx = fz;
		} while (true);
		return x;
	}//Newton
}//Root


public static class main{
	public static void Main(){
		Func<vector, vector> test = v =>{
			double x = v[0];
			return new vector(new double[]{
					-2*x + 5
					});
		};
		vector start = new vector(1); start[0] = 1;

		vector testRoot = Root.Newton(test, start);
		WriteLine($"Test function root: {testRoot}");
	}//Main
}//main
