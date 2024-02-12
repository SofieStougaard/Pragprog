using System;
using static System.Console;

class main{
	static bool approx(double a, double b, double acc=1e-9, double eps=1e-9){
		if(System.Math.Abs(b-a) <= acc) return true;
		if(System.Math.Abs(b-a) <= eps*System.Math.Max(System.Math.Abs(a),System.Math.Abs(b))) return true;
		return false;
	}//end approx
static int Main(){
	double epsilon = System.Math.Pow(2,-52);
	double tiny = epsilon/2;
	double a = 1 + tiny + tiny;
	double b = tiny + tiny + 1;

	Write($"Part 1\n");
	int i=1;
	while(i+1>i) {i++;}
	Write($"My max int = {i}, with MaxValue = {int.MaxValue}\n");
	while(i-1<i) {i--;}
	Write($"My min int = {i}, with MinValue = {int.MinValue}\n");
	
	Write($"Part 2 - Machine epsilon\n");
	double x=1;
	while(1+x!=1) {x/=2;} 
	x*=2;
	Write($"x = {x}, should be around {epsilon}\n");
	float y=1F;
	while((float)(1F+y) != 1F) {y/=2F;}
	y*=2F;
	Write($"y = {y}, should be around {System.Math.Pow(2,-23)}\n");

	Write($"Part 3\n");
	Write($"a=b ? {a==b}\na>1 ? {a>1}\nb>1 ? {b>1}\n");
	Write($"Difference is due to how floats are represented, so there will might be rounding errors\n");

	Write($"Part 4\n");
	double d1 = 0.1 + 0.1 + 0.1 + 0.1 + 0.1 + 0.1 + 0.1 + 0.1;
	double d2 = 8*0.1;
	Write($"d1=0.1 + 0.1 + 0.1 + 0.1 + 0.1 + 0.1 + 0.1 + 0.1 and d2=8*0.1, so d1=d2 ? {approx(d1,d2)}\n");
	return 0;
}//end of Main
}//end of main class
