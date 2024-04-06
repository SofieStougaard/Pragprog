using System;
using static System.Console;

public static class LS{
	public static vector lsfit(Func<double,double>[] fs, vector x, vector y, vector dy){
		int n = x.size;
		int m = fs.Length;
		matrix A = new matrix(n, m);
		vector b = new vector(n);
		for (int i =0; i<n ; i++){
			b[i]=y[i]/dy[i];
			for (int k=0; k<m; k++){
				A[i,k] = fs[k](x[i])/dy[i];
			}
		}
		
		(matrix Q, matrix R) = QRGS.decomp(A);
		//WriteLine("Q size: " +Q.size1+ "x" +Q.size2);
		//WriteLine("R size: " +R.size1+ "x" +R.size2);
		//WriteLine("b size: " +b.size);	
		vector c = QRGS.solve(Q, R, b); // solves | |A∗c−b||−>min
		//matrix AI = QRGS.inverse(Q,R); // calculates pseudoinverse
		//matrix Σ = AI*AI.T;
		return (c);
		//return(A, Q,R);
	}

}//End of LS


public static class main{
        static void Main(){
		vector t = new vector("1 2 3 4 6 9 10 13 15");
		vector yRa = new vector("117 100 88 72 53 29.5 25.2 15.2 11.1");
		vector dy = new vector("6 5 4 4 4 3 3 2 2");
		
		var Decay = new Func<double, double>[2];
		Decay[0] = x => 1;  //Constant function for the ln(a) term
		Decay[1] = x => -Math.Log(x);  //Linear function for the -lamda*x term

		vector yRalog = new vector(yRa.size);
		vector dydiv = new vector(dy.size);
		for (int i = 0; i<yRa.size; i++){
			yRalog[i] = Math.Log(yRa[i]);
			dydiv[i] = dy[i]/yRa[i];
		}

		vector c = LS.lsfit(Decay, t, yRalog, dy);
		//A.print();
		c.print();
		//S.print();

		
        }//End of Main()
}//End of main
