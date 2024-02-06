using System;
using static System.Console;
using static System.Math;
static class main{
	static double x=1.0;
	static double y=2.0;
	static double PI = 3.1415927;
	static double e = 2.718281828459;
	static string hello = $"hello\n";
	static double Sin(double x){ 
		return System.Math.Sin(x); 
	}
	static double times2(double x){
		return x*2;
	}
	static double Sqrt(double x){
		return System.Math.Sqrt(x);
	}
	static double Power(double x, double y){
		return System.Math.Pow(x, y);
	}
	static double Exp(double x){
		return System.Math.Exp(x);
	}
	static int Main(){
		Write($"{hello}");
		Write($"x={x}, x*2={times2(x)}\n");
		Write($"x={x}, Sin(x)={Sin(x)}\n");
		Write($"x={y}, sqrt(x)={Sqrt(y)}, sqrt(x)^2={Sqrt(y)*Sqrt(y)}\n");
		Write($"x={y}, x^(1/5)={Power(2, 0.2)}, x^(1/5)^5={Power(Power(2,0.2), 5)}\n");
		Write($"x=e, e^PI={Exp(PI)}, another way e^PI={Power(e,PI)}\n");
		Write($"x=PI, PI^e={Power(PI,e)}\n");
		double prod=1;
		for(int x=1;x<10;x+=1){
			Write($"fgamma({x})={sfuns.fgamma(x)} {x-1}!={prod}\n");
			prod*=x;
			Write($"lngamma({x})={sfuns.lnfgamma(x)} \n");
			}
	return 0;
	}
}
