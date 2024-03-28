using System;
using static System.Console;
using static System.Math;
using static matrix;

public static class Jacobi{
	public static void timesJ(matrix A, int p, int q, double theta){
		double c=Cos(theta),s=Sin(theta);
		for(int i=0;i<A.size1;i++){
			double aip=A[i,p],aiq=A[i,q];
			A[i,p]=c*aip-s*aiq;
			A[i,q]=s*aip+c*aiq;
		}
	}//End of timesJ

	public static void Jtimes(matrix A, int p, int q, double theta){
		double c=Cos(theta),s=Sin(theta);
		for(int j=0;j<A.size1;j++){
			double apj=A[p,j],aqj=A[q,j];
			A[p,j]= c*apj+s*aqj;
			A[q,j]=-s*apj+c*aqj;
		}
	}//End of Jtimes

	/*public static void cyclic(matrix A, vector w, matrix V){
	
	}//End of cyclic*/
}//End of Jacobi

public static class Matrix{
	public static matrix Random(int size1, int size2){
		matrix RandomMatrix = new matrix(size1, size2);
		var rnd = new System.Random(1);
		for (int i=0; i<size1; i++){
			for (int j=i; j<size2; j++){
				RandomMatrix[i, j] = rnd.NextDouble()*10 - 5;
				RandomMatrix[j, i] = RandomMatrix[i, j]; //To make the matrix symmetric
			}
		}
		return RandomMatrix;
	}// Random matrix
}

public static class main{
	static void Main(){
		matrix A = Matrix.Random(3,3);
		A.print();
		//Jacobi.Jtimes(A, 1, 2, 3.14);
		//A.print();
	}//end of Main()
}//end of main
