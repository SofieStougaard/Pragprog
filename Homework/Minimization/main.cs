using System;
using static System.Console;
using static System.Math;
using System.IO;
using System.Linq;

public static class Minimization{
	public static (vector, int) Newton(
			Func<vector,double> phi, // objective function 
			vector x, //starting point
			double acc = 1e-3,
			int maxSteps = 1000){ //Accuracy goal
		double lambdamin = 1e-8; //minimum step length
		int steps = 0;
		for (int step=0; step < maxSteps; step++){
			steps += 1;
			var nablaPhi = Gradient(phi, x);
			if (nablaPhi.norm() < acc) break;

			var H = Hessian(phi, x);
			(matrix QH, matrix RH) = QRGS.decomp(H);
			var dx = QRGS.solve(QH, RH, -nablaPhi);

			double lambda = 1;
			double phix = phi(x);
			do {
				if (phi(x+lambda*dx) < phix) break; //good step
				if (lambda < lambdamin ) break; //accpet anyway
				lambda /= 2;
			}while(true);
			x += lambda*dx;
		}
		return (x, steps);
	}//Newton 


	public static vector Gradient(Func<vector,double> phi, vector x){
		vector nablaPhi = new vector(x.size);
		double phix = phi(x);
		for (int i=0; i<x.size; i++){
			double dx = Max(Abs(x[i]), 1)*Pow(2,-26);
			x[i] += dx;
			nablaPhi[i] = (phi(x) - phix)/dx;
			x[i] -= dx;
		}
		return nablaPhi;
	}//Gradiens

	public static matrix Hessian(Func<vector,double> phi, vector x){
		matrix H = new matrix(x.size);
		vector nablaPhiX = Gradient(phi, x);
		for (int j=0; j<x.size; j++){
			double dx = Max(Abs(x[j]),1)*Pow(2,-13);
			x[j] += dx;
			vector dnablaPhi = Gradient(phi,x) - nablaPhiX;
			for (int i=0; i<x.size; i++) {
				H[i,j] = dnablaPhi[i]/dx;
			}
			x[j] -= dx;
		}
		return (H+H.T)/2; //Symmetrize the Hessian matrix, which we need for the Newton method
	}
}//Minimization


public static class main{
	public static double Rosenbrock(vector v){
		double x = v[0];
		double y = v[1];
		return (1 - x)*(1 - x) + 100*(y - x*x)*(y - x*x);
	}//Rosenbrock

	public static double Himmelblau(vector v){
		double x = v[0];
		double y = v[1];
		return (x*x + y - 11)*(x*x + y - 11) + (x + y*y - 7)*(x + y*y - 7);
	}//Himmelblau

	static void Main(){
		vector start_Rosen = new vector(0.0, 0.0);
		(vector min_Rosen, int steps_Rosen) = Minimization.Newton(Rosenbrock, start_Rosen);
		WriteLine("Rosenbrock's valley function:");
		WriteLine($"Minimum found at x={min_Rosen[0]} and y={min_Rosen[1]} with {steps_Rosen} steps");
		
		vector start_Himmel = new vector(3.0, 3.0);
		(vector min_Himmel, int steps_Himmel) = Minimization.Newton(Himmelblau, start_Himmel);
		WriteLine("Himmelblau's function:");
		WriteLine($"Minimum found at x={min_Himmel[0]} and y={min_Himmel[1]} with {steps_Himmel} steps");
	}//Main
}//main
