using System;
using static System.Console;
using static matrix;


public static class QRGS{
	public static (matrix,matrix) decomp(matrix A){
		matrix Q = A.copy();
		matrix R = new matrix(A.size2,A.size2);
		/* orthogonalize Q and fill-in R */
		vector u1 = Matrix.Vecs(A)[0];
		vector e1 = u1/u1.norm();
		
		vector[] As = new vector[A.size2];
		As[0] = u1;

		vector[] orthoVecs = new vector[A.size2];
		orthoVecs[0] = e1;

		for (int i=1; i<A.size2; i++){
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
/*	
	public static vector solve(matrix Q, matrix R, vector b){ ... }
	public static double det(matrix R){ ... }
	public static matrix inverse(matrix Q,matrix R){ ... }

	*/
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

public static class main{
	static void Main(){
		matrix I = new matrix(7);
		I = matrix.id(7);	
		matrix A = Matrix.Random(7, 5);
		WriteLine($"Matrix A:\n");
		A.print();
		WriteLine($"\n");

		vector a0 = Matrix.Vecs(A)[0];
		a0.print();
		WriteLine($"\n");
		vector a4 = Matrix.Vecs(A)[4];
		a4.print();
                WriteLine($"\n");
	/*	
		(matrix Q, matrix R) = QRGS.decomp(A);
		matrix a = Q*R;
		WriteLine($"A=Q*R : {a.approx(A)} \n");
		WriteLine($"Q^T*Q=I : {I.approx(Q*Q.transpose())} \n");

		matrix q = Q*Q.transpose();
		q.print();
*/
	}//Main
}//main
