using System;
using System.IO;
using static System.Console;

public class Lsplines{
	public static double linterp(double[] x, double[] y, double z){
		int i = binsearch(x, z); //Find intervallet hvor z er lokaliseret

		double dx = x[i+1] - x[i]; //eq 6 i noterne
		if (!(dx > 0 )) throw new Exception("ups..."); //tjekker at dx er positiv og ikke lig med 0, meget vigtigt senere

		double dy = y[i+1] - y[i]; //eq 6 i noterne

		return y[i] + dy/dx * (z-x[i]); //ligesom eq 5, men med x = z, og x_i = x[i] ...
	}//double linterp

	public static int binsearch(double[] x, double z){
		if (z < x[0] || z > x[x.Length-1]) throw new Exception ("binary search: bad z");

		int i = 0;
		int j = x.Length-1;
		while (j - i > 1){
			int mid = (i+j)/2;
			if (z > x[mid]) i = mid;
			else j = mid;
		}//while loop
		return i;
	}//int binsearch

	public static double linterpInteg(double[] x, double[] y, double z){
		int i = binsearch(x, z);

		double integral = 0.0;

		//regner integralet fra x[0] til x[i]
		for (int k = 0; k < i; k++){
			double dx = x[k+1] - x[k];
			double dy = y[k+1] - y[k];
			integral += y[k]*dx + 0.5*dy*dx; //udregnet analytisk på papir
		}//for loop

		//regner integralet fra x[i] til z
		//y[z] = y[i] + dy/dx * (z-x[i])
		double dxLast = z-x[i];
		double dyLast = linterp(x, y, z) - y[i];
		integral += y[i]*dxLast + 0.5*dyLast*dxLast;
		return integral;
	}//double linterpinteg
}//Lsplines

public class qspline{
	double[] x, y, b, c;
	
	public int binsearch(double z){
		if (z < x[0] || z > x[x.Length-1]) throw new Exception ("binary search: bad z");

		int i = 0;
		int j = x.Length-1;
		while (j - i > 1){
			int mid = (i+j)/2;
			if (z > x[mid]) i = mid;
			else j = mid;
		}//while loop
		return i;
        }//int binsearch
	
	public qspline(double[] xs, double[] ys){
		int n = xs.Length;
		x = (double[]) xs.Clone();
		y = (double[]) ys.Clone();

		b = new double[n-1]; //Array med n-1 indgange, så lige så stor som x og y
		c = new double[n-1]; //Same

		double[] dx = new double[n-1]; //finde alle x[i+1] - x[i]
		double[] dy = new double[n-1];
		for (int i=0; i<n-1; i++){//VIGTIGT det er n-1 og ikke n
			dx[i] = x[i+1]-x[i];
			dy[i] = y[i+1]-y[i];
		}//for loop

		//Finde koefficienterne b[i] og c[i]
		c[0] = 0;
		for (int i=0; i<n-1; i++){
			if (i<n-2){
				c[i+1] = (dy[i+1]/dx[i+1] - dy[i]/dx[i] - c[i]*dx[i])/dx[i+1];
			}
			b[i] = dy[i]/dx[i] - c[i]*dx[i];
		}
	}//qspline

	public double evaluate(double z){
		int i = binsearch(z);
		double dz = z - x[i];
		return y[i] + b[i]*dz + c[i]*dz*dz;
	}//evaluate

	public double derivative(double z){
		int i = binsearch(z);
		double dz = z - x[i];
		return b[i] + 2*c[i]*dz;
	}//derivative

	public double integral(double z){
		int i = binsearch(z);
		
		double integral = 0.0;
		//regner integralet fra x[0] til x[i]
		for (int k = 0; k < i; k++){
			double dx = x[k+1] - x[k];
			integral += y[k]*dx + 0.5*b[k]*Math.Pow(dx, 2) + c[k]/3*Math.Pow(dx,3); //udregnet analytisk på papir
		}//for loop

		//regner integralet fra x[i] til z
		//y[z] = y[i] + b[i] * (z-x[i]) + c[i]*(z-x[i])^2
		double dxLast = z-x[i];
		integral += y[i]*dxLast + 0.5*b[i]*Math.Pow(dxLast, 2) + c[i]/3*Math.Pow(dxLast,3);
		return integral;
	}//integral
}//Qsplines

