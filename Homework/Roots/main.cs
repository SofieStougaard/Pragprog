using System;
using static System.Console;
using static matrix;

public static class Root{
	public static matrix Jacobian(Func<vector, vector> f, vector x, vector fx = null, vector dx = null){
		int n = x.size;
		if (dx == null){
			dx = new vector(n);
			for (int i = 0; i < n; i++){
				dx[i] = Math.Abs(x[i]) * Math.Pow(2, -26);
			}}

		if (fx == null){
			fx = f(x);
		}

		matrix J = new matrix(n,n);
		vector 
	}//Jacobian
}//Root
