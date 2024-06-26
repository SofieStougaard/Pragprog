using System;
using static System.Console;
using static matrix;
using System.IO;

public class Integration{
	public static double Integrate(Func<double,double> f, double a, double b, double delta=0.001, double epsilon=0.001, double f2 = double.NaN, double f3 = double.NaN){
		double h = b - a; //Finde længden af intervallet

		if (double.IsNaN(f2)){
			f2 = f(a + 2*h/6); 
			f3 = f(a + 4*h/6);
		}

		double f1 = f(a + h/6);
		double f4 = f(a + 5*h/6);

		double Q = (2*f1 + f2 + f3 + 2*f4)/6 * h; //Højere orden reglen
		double q = (f1 + f2 + f3+ f4)/4 * h; //lavere orden reglen
		double err = Math.Abs(Q - q);
		if (err <= delta + epsilon*Math.Abs(Q))
			return Q;
		else
			return Integrate(f, a, (a + b)/2, delta/Math.Sqrt(2), epsilon, f1, f2) + Integrate(f, (a + b)/2, b, delta/Math.Sqrt(2), epsilon, f3, f4);
	}//Integrate

	public static double Erf(double z){
		if (z < 0) return -Erf(-z);
		else if (0 <= z && z <= 1) {
			Func<double, double> integrand = x => Math.Exp(-x * x);
			double integral = Integrate(integrand, 0, z);
			return (2/Math.Sqrt(Math.PI))*integral;
		}
		else {//z>1
			Func<double, double> integrand = t => Math.Exp(-(z+(1-t)/t) * (z+(1-t)/t))/(t*t);
			double integral = Integrate(integrand, 0, 1);
			return 1 - (2/Math.Sqrt(Math.PI))*integral;
		}
	}//erf

	public static (double,int) IntegrateB(Func<double, double> f, double a, double b, double delta = 0.001, double epsilon = 0.001, double f2 = double.NaN, double f3 = double.NaN){
		int evaluationCount = 0;

		Func<double, double> transformedF = theta => {
			double x =  (a + b)/2 + (b - a)/2*Math.Cos(theta);
			 return f(x)*Math.Sin(theta)*(b - a)/ 2;
		};
		double result = AdaptIntegrate(transformedF, 0, Math.PI, delta, epsilon, ref evaluationCount, f2, f3);
		return (result, evaluationCount);
	}//IntegrateB

	public static double AdaptIntegrate(Func<double, double> f, double a, double b, double delta, double epsilon, ref int evaluationCount, double f2, double f3){
		double h = b - a;
		evaluationCount++;

		if (double.IsNaN(f2)){
			f2 = f(a + 2*h/6);
			f3 = f(a + 4*h/6);
		}

		double f1 = f(a + h/6);
		double f4 = f(a + 5*h/6);
				
		double Q = (2*f1 + f2 + f3 + 2*f4)/6*h;
		double q = (f1 + f2 + f3 + f4)/4*h;
		double err = Math.Abs(Q - q);

		if (err <= delta + epsilon*Math.Abs(Q))
			return Q;
		else 
			return AdaptIntegrate(f, a, (a + b) / 2, delta / Math.Sqrt(2), epsilon, ref evaluationCount, f1, f2) + AdaptIntegrate(f, (a + b) / 2, b, delta / Math.Sqrt(2), epsilon, ref evaluationCount, f1, f2);
	}//AdaptIntegrate
}//Integration

public class main{
	public static void Main(){
		Func<double, double> Sqrt = x => Math.Sqrt(x);
		Func<double, double> InvSqrt = x => 1/Math.Sqrt(x);
		Func<double, double> FourSqrt = x => 4*Math.Sqrt(1-x*x);
		Func<double, double> LnInvSqrt = x => Math.Log(x)/Math.Sqrt(x);
		
		double exactSqrt = 2.0/3.0;
		double exactInvSqrt = 2.0;
		double exactFourSqrt = Math.PI;
		double exactLnInvSqrt = -4.0;
		
		WriteLine($"Exercise a)");
		WriteLine($"Integral of sqrt(x) from 0 to 1: {Integration.Integrate(Sqrt, 0, 1)} (Expected: {exactSqrt}, Error: {Math.Abs(Integration.Integrate(Sqrt, 0, 1) - exactSqrt)})");
		WriteLine($"Integral of 1/sqrt(x) from 0 to 1: {Integration.Integrate(InvSqrt, 0, 1)} (Expected: {exactInvSqrt}, Error: {Math.Abs(Integration.Integrate(InvSqrt, 0, 1) - exactInvSqrt)})");
		WriteLine($"Integral of 4*sqrt(1-x^2) from 0 to 1: {Integration.Integrate(FourSqrt, 0, 1)} (Expected: {exactFourSqrt}, Error: {Math.Abs(Integration.Integrate(FourSqrt, 0, 1) - exactFourSqrt)})");
		WriteLine($"Integral of ln(x)/sqrt(x) from 0 to 1: {Integration.Integrate(LnInvSqrt, 0, 1)} (Expected: {exactLnInvSqrt}, Error: {Math.Abs(Integration.Integrate(LnInvSqrt, 0, 1) - exactLnInvSqrt)})");

		var (intInvSqrt, evalInvSqrt) = Integration.IntegrateB(Sqrt, 0, 1);
		var (intLnInvSqrt, evalLnInvSqrt) = Integration.IntegrateB(LnInvSqrt, 0, 1);
		WriteLine($"\n \n Exercise b)");
		WriteLine($"Integral of sqrt(x) from 0 to 1: {intInvSqrt} (Expected: {exactSqrt}, Error: {Math.Abs(intInvSqrt - exactSqrt)}), Evaluations: {evalInvSqrt} and with Python 231");
		WriteLine($"Integral of ln(x)/sqrt(x) from 0 to 1: {intLnInvSqrt} (Expected: {exactLnInvSqrt}, Error: {Math.Abs(intLnInvSqrt - exactLnInvSqrt)}), Evaluations: {evalLnInvSqrt} with Python 315");
		

		string erf = "erf.txt";
		using (StreamWriter writer = new StreamWriter(erf)){
			for (double z = -3; z<=3; z +=0.1){
				writer.WriteLine($"{z} {Integration.Erf(z)}");
		}}
	}//Main
}//main
