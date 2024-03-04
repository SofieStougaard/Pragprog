using System;
using static System.Console;
using static System.Math;
using static cmath;

static class main{
	/*
	static bool approx(complex a, complex b, double acc=1e-9, double eps=1e-9){
		if(Abs(b-a) <= acc) return true;
		if(Abs(b-a) <= eps*Max(Abs(a),Abs(b))) return true;
		return false;
	}//end approx
	*/
	static double PI = 3.141592653589793238462643383279502884197169;
	
	static int Main(){
		complex i = I;//How to make the complex number i
		complex a = -1;

		complex sqrti = 1/sqrt(2)+I/sqrt(2);
		complex ei = cos(1)+I*sin(1);
		complex eipi = cos(PI);
		complex ii = cmath.exp(-PI/2);//cmath.exp(i*cmath.log(i));
		complex lni = I*PI/2;
		complex sinipi = I*System.Math.Sinh(PI);
		
		WriteLine($"sqrt(i)={cmath.sqrt(i)} should be equal to {sqrti}: {sqrti.approx(sqrt(i))}");
		WriteLine($"sqrt(-1)={cmath.sqrt(a)} should be equal to {i}: {i.approx(sqrt(a))}");
		WriteLine($"e^i={cmath.exp(i)} should be equal to {ei}: {ei.approx(exp(i))}");
		WriteLine($"e^(i*pi)={cmath.exp(i*PI)} should be equal to {eipi}: {eipi.approx(cmath.exp(i*PI))}");
		WriteLine($"i^i={cmath.pow(i,i)} should be equal to {ii}: {ii.approx(cmath.pow(i,i))}");
		WriteLine($"ln(i)={cmath.log(i)} should be equal to {lni}: {lni.approx(cmath.log(i))}");
		WriteLine($"sin(i*pi)={cmath.sin(i*PI)} should be equal to {sinipi}: {sinipi.approx(cmath.sin(i*PI))}");

	return 0;
	}//end int Main
}//end class main
