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
		int m = Q.size2;
		int size = b.size;
		vector x = new vector(size);
		vector QTb = Q.T*b;

		for (int i=m-1; i>=0; i--){
			double sum = 0;
			for (int j=i+1; j<m; j++){
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
		matrix A = Q*R;
		matrix B = A.copy();
		
		for (int i=0; i<A.size2; i++){
			vector ei = new vector(A.size2);
                	ei[i] = 1;
			B[i] = solve(Q, R, ei); 
		}

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

