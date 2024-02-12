using static System.Console;
using static System.Math;
static class main{
	static double x=1.0;
	static double y=2.0;
	static double PI = 3.1415927;
	static double e = 2.718281828459;
	//static string hello = $"hello\n";
	static double Sin(double x){ 
		return System.Math.Sin(x); 
	}//end sin
	static double times2(double x){
		return x*2;
	}//end times2
	static double Sqrt(double x){
		return System.Math.Sqrt(x);
	}//end sqrt
	static double Power(double x, double y){
		return System.Math.Pow(x, y);
	}//end power
	static double Exp(double x){
		return System.Math.Exp(x);
	}//end exp
	static int Main(){
		Write($"Part 1\n");
		Write($"x={x}, x*2={times2(x)}\n");
		Write($"x={x}, Sin(x)={Sin(x)}\n");
		Write($"x={y}, sqrt(x)={Sqrt(y)}, test sqrt(x)^2={Sqrt(y)*Sqrt(y)}\n");
		Write($"x={y}, x^(1/5)={Power(2, 0.2)}, test x^(1/5)^5={Power(Power(2,0.2), 5)}\n");
		Write($"x=e, e^PI={Exp(PI)}, another way e^PI={Power(e,PI)}, should be 23.1406926\n");
		Write($"x=PI, PI^e={Power(PI,e)}, should be 22.459157\n");
		Write($"\nPart 2\n");
		double prod=1;
		for(int x=1;x<10;x+=1){
			Write($"fgamma({x})={sfuns.fgamma(x)}, test {x-1}!={prod}\n");
			prod*=x;
		}//end for-loop
		Write($"\nPart 3\n");
		for(int x=1;x<10;x+=1){
			Write($"lngamma({x})={sfuns.lnfgamma(x)} \n");
			prod*=x;
		}//end for-loop
	return 0;
	}//end Main
}//end class main
