using System;
using System.IO;
using System.Linq;
using static System.Console;

public static class LS{
	public static (vector, matrix) lsfit(Func<double,double>[] fs, vector x, vector y, vector dy){
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
		vector fit = QRGS.solve(Q, R, b); // solves | |A∗c−b||−>min
		
		matrix RTR = R.T*R;
		(matrix pre_cov_Q, matrix pre_cov_R) = QRGS.decomp(RTR);
		matrix RTR_inv = QRGS.inverse(pre_cov_Q, pre_cov_R);
		return (fit, RTR_inv);
		}
}//End of LS


public static class main{
        static void Main(){
		string filename = "data.txt"; //finding the input file called data.txt
		var data = File.ReadAllLines(filename).Select(line => line.Split(' ').Select(double.Parse).ToArray()).ToArray();
		vector t = new vector(data.Select(row => row[0]).ToArray()); //takes the first column in my data file
		vector yRa = new vector(data.Select(row => row[1]).ToArray()); //takes the second column
		vector dy = new vector(data.Select(row => row[2]).ToArray()); //third column
		
		var Decay = new Func<double, double>[2];
		Decay[0] = x => 1;  //Constant function for the ln(a) term
		Decay[1] = x => -x;  //Linear function for the -lamda*x term

		vector yRalog = new vector(yRa.size);
		vector dydiv = new vector(dy.size);
		for (int i = 0; i<yRa.size; i++){
			yRalog[i] = Math.Log(yRa[i]);
			dydiv[i] = dy[i]/yRa[i];
		}

		string logdata = "fitdata.txt";
		using (StreamWriter writer = new StreamWriter(logdata)){
			for (int i=0; i<t.size; i++){
				writer.WriteLine($"{t[i]} {yRalog[i]} {dydiv[i]}");
			}
		}

		(vector c, matrix S) = LS.lsfit(Decay, t, yRalog, dy);
		string bestfit = "bestfit.txt";
		using (StreamWriter writer = new StreamWriter(bestfit)){
			for (double i=t[0]-0.5; i<t[t.size-1]+0.5; i+=1.0/64){
				writer.WriteLine($"{i} {c[0]-c[1]*i}");
			}
		}

		string bestfit_m = "bestfit_m.txt";
                using (StreamWriter writer = new StreamWriter(bestfit_m)){
                        for (double i=t[0]-0.5; i<t[t.size-1]+0.5; i+=1.0/64){
                                writer.WriteLine($"{i} {(c[0]-Math.Sqrt(S[0,0]))-(c[1]+Math.Sqrt(S[1,1]))*i}");
                        }
                }

		string bestfit_p = "bestfit_p.txt";
                using (StreamWriter writer = new StreamWriter(bestfit_p)){
                        for (double i=t[0]-0.5; i<t[t.size-1]+0.5; i+=1.0/64){
                                writer.WriteLine($"{i} {(c[0]+Math.Sqrt(S[0,0]))-(c[1]-Math.Sqrt(S[1,1]))*i}");
                        }
                }

		
		double halflife = Math.Log(2)/c[1];
		double unc = halflife * Math.Sqrt(S[1,1]); //Uncertainty progragation for Z = a*x+b gives dZ = a*dx
		S.print("cov = ");
		WriteLine($"ln(a) = {c[0]:e3}+-{Math.Sqrt(S[0,0]):e3}, lambda = {c[1]:e3}+-{Math.Sqrt(S[1,1]):e3}");
		WriteLine($"Half-life time found by fit is {halflife:e3}+-{unc:e3}, but the real value is 3.6, so it agrees with the modern one");
        }//End of Main()
}//End of main
