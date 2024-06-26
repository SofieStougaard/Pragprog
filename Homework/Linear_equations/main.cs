using System;
using static System.Console;
using static matrix;


public static class QRGS{
	public static (matrix,matrix) decomp(matrix A){
		matrix Q = new matrix(A.size1, A.size2);
		matrix R = new matrix(A.size2,A.size2);
		
		vector[] As = new vector[A.size2];
		vector[] orthoVecs = new vector[A.size2];
		
		for (int i=0; i<A.size2; i++){
			vector ai = Matrix.Vecs(A)[i];
			vector ui = ai;
			for (int j=0; j<i; j++){
				ui = ui - orthoVecs[j] * ui.dot(orthoVecs[j]);
			}
			vector ei = ui/ui.norm();
			As[i] = ai;
			orthoVecs[i] = ei;
		}
		Q = Matrix.VecsToMatrix(orthoVecs);
		
		for (int i=0; i<A.size2; i++){
			for (int j=0; j<A.size2; j++){
				R[i,j] = As[j].dot(orthoVecs[i]);
			}
		}
		
		return (Q,R);
	}

	public static vector solve(matrix Q, matrix R, vector b){
		int size = b.size;
		vector x = new vector(size);
		matrix Q_trans = Q.T;
		vector QTb = Q_trans*b;

		for (int i=size-1; i>=0; i--){
			double sum = 0;
			for (int j=i; j<size; j++){
				sum += R[i,j]*x[j];
			}
			x[i] = (QTb[i]-sum)/R[i,i];
		}
				
		return x;
	}
	public static double det(matrix R){ 
       		int size = R.size1;
		double det = 1;
		for (int i=0; i<size; i++){
			det *= R[i,i];
		}
		return det;
	}
	public static matrix inverse(matrix Q,matrix R){
		matrix B = new matrix(R.size1, R.size2);
		matrix Q_trans = Q.T;
		vector[] xs = new vector[R.size1];

		for (int i=0; i<R.size1; i++){
			vector ei = new vector(R.size2);
                	ei[i] = 1;
			xs[i] = solve(Q, R, ei);
		}
		B = Matrix.VecsToMatrix(xs);

		return B;
	}

	
}//QRGS

public static class Matrix{
	public static matrix Random(int size1, int size2){
		matrix RandomMatrix = new matrix(size1, size2);
		var rnd = new System.Random(1);
		for (int i=0; i<size1; i++){
			for (int j=0; j<size2; j++){
                        	RandomMatrix[i, j] = rnd.NextDouble()*10 - 5;        
			}
		}
		return RandomMatrix;
	}// Random matrix

	public static vector Random(int size){
                vector RandomVector = new vector(size);
                var rnd = new System.Random(1);
                for (int i=0; i<size; i++){
			RandomVector[i] = rnd.NextDouble()*10 - 5;
                }
                return RandomVector;
        }// Random vector
	
	public static vector[] Vecs(matrix A){
		vector[] columns = new vector[A.size2]; //Matrix of rows=size2 and column=1
    		for (int i = 0; i < A.size2; i++){
			vector e = new vector(A.size1);
			for (int j = 0; j < A.size1; j++){
				e[j] = A[j, i]; // Access each element in column i of matrix A and assign it to e
			}
			columns[i] = e; // Store the extracted column as a vector in the arra
		}
		return columns;
	}//Vecs 

	public static matrix VecsToMatrix(vector[] A){
		int rows = A[0].size;
		int columns = A.Length;
		matrix VTM = new matrix(rows,columns);
		for (int i=0; i<columns; i++){
			for (int j=0; j<rows; j++){
				VTM[j, i] = A[i][j];
			}
		}
		return VTM;
	}//VecsToMatrix
}//Matrix

public static class main{
	static void Main(){	
		matrix A = Matrix.Random(8, 8);
		matrix I = matrix.id(A.size2);
		vector b = Matrix.Random(8);
		
		(matrix Q, matrix R) = QRGS.decomp(A);
		vector x = QRGS.solve(Q,R,b);
		
		WriteLine($"A=Q*R : {A.approx(Q*R)}");
		WriteLine($"Q^T*Q=I : {I.approx(Q.T*Q)}");
		WriteLine($"Q*R*x=b : {b.approx(Q*R*x)}");
		WriteLine($"A*x=b : {b.approx(A*x)}");
		WriteLine($"det(R): {QRGS.det(R)}");
		
		matrix A_inverse = QRGS.inverse(Q,R);
		WriteLine($"A*A^-1=I : {I.approx(A*A_inverse)}");
	}//Main
}//main
