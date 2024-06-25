using System;
using static System.Console;
using System.IO;
using static System.Math;


public class Particle{
	double a = -5;
	double b = 5;
	double alpha = 0.12; //Typically between 0.1 and 0.5
	double beta = 0.25; //Typically between 0.1 and 0.7
	static Random rnd = new System.Random();
	
	public double[] Position {get; private set;}

	public Particle(int Dimensions){
		Position = new double[Dimensions]; //So we can have the appropiate dimensions for the problem

		for (int i = 0; i < Dimensions; i++){
			Position[i] = a + (b - a)*rnd.NextDouble(); //To get the position to be between [a,b]
		} //No return, due to being a Construter
	}//particle

	public void UpdatedPosition(double[] globalBest){
		int dimensions = Position.Length; //So we can get the same amount of v's as x's
		double[] v = new double[dimensions]; 
		
		for (int i = 0; i < dimensions; i++){
			v[i] = (b - a)*(2*rnd.NextDouble() - 1) ; //Making all the v[i]'s in the interval [-(b-a), (b-a)]
			Position[i] = (1-beta)*Position[i] + beta*globalBest[i] + alpha*v[i];
		}//no return type, as this just updates the position
	}//UpdatedPosition

	public void PrintPos(){
		WriteLine("Particle Position:");
		foreach (var pos in Position){
			WriteLine(pos);
		}			
	}
}//particle


public class APSO{
	Particle[] swarm;
	double[] globalBest;
	int dimensions;
	EvaluationFunction evalFunction;

	public APSO(int dimensions, int swarmSize, EvaluationFunction evalFunction){
		this.dimensions = dimensions;
		this.evalFunction = evalFunction;
		swarm = new Particle[swarmSize]; //making the swarm of N particles

		for (int i = 0; i<swarmSize; i++){
			swarm[i] = new Particle(dimensions); //making each particle in X dimensions
		}

		globalBest = new double[dimensions]; //making the globalBest array as long as the dimensions
		updateGlobalBest(); //updating the global best, so there is values in the globalbest array
	}//APSO, and no return type, since it is a constructor


	public void updateGlobalBest(){
		foreach (var particle in swarm){
			if (evalFunction(particle.Position) < evalFunction(globalBest)){
				Array.Copy(particle.Position, globalBest, dimensions);
				//Copier positions into the globalBest array for all dimensions
			}
		}
	}//updateGlobalBest

	public void Iterate(int iterations, string filename){
		using (StreamWriter writer = new StreamWriter(filename)){
			for (int i=0; i < iterations; i++){
				foreach (var particle in swarm){
					particle.UpdatedPosition(globalBest); //updating all the positions
				}
				updateGlobalBest();
				logPosition(writer, i);
			}}
	}//Iterate


	public void logPosition(StreamWriter writer, int iteration){
		writer.WriteLine($"# Iteration {iteration}");
		foreach (var particle in swarm) {
			writer.WriteLine(string.Join(" ", particle.Position) + " 0");
		}
		writer.WriteLine("\n\n");
	}

	public double[] getPosition(){
		return globalBest; //We want to be able to get the best position
	}//getPosition
}//APSO

public delegate double EvaluationFunction(double[] position);

public class EvaluationFunctions{
	public double sphere(double[] Position){
                double result = 0;
                foreach(var x in Position){
                        result += x*x;
                }
                return result;
        }//Evaluate

        public double Rastrigin(double[] Position){
                double result = 10*Position.Length;
                foreach(var x in Position){
                        result += x*x - 10*Cos(2*PI*x);
                }
                return result;
        }//Rastrigin
}//Ecaluationfunctions

public static class main{
	static void Main(){
		int points = 100;
		string sphere = "sphere.txt";
		using (StreamWriter writer = new StreamWriter(sphere)){
			for (int R = 1; R < 6; R++){
				writer.WriteLine("\n\n");
				writer.WriteLine($"# Radius {R}");
				for (int i = 0; i < points; i++){
					double theta = 2*PI*i/points;

					double x = R * Cos(theta);
					double y = R * Sin(theta);

					writer.WriteLine($"{x} {y}");
				}
				writer.WriteLine($"{R} 0");
			}}
/*
                string ras = "Rastrigin.txt";
                using (StreamWriter writer = new StreamWriter(ras)){
                        for (double x = -5; x < 5; x+=0.5){
                                for (double y = -5; y < 5; y+=0.5){
                                        double result = 20 + x*x + y*y -10*(Cos(2*PI*x) + Cos(2*PI*y));
                                        writer.WriteLine($"{x} {y} {result}");
				}}}
*/

		int dim = 2; //2D for the sphere to test
		int swarm = 100;
		int iterations = 40;
		string filename = "Positions.txt";
		
		EvaluationFunctions evalFunctions = new EvaluationFunctions();		

		APSO apso = new APSO(dim, swarm, evalFunctions.sphere); //dimensions and swarmsize in that way!!
		apso.Iterate(iterations, filename);	
		double[] Position = apso.getPosition();
		WriteLine("Evaluating the global minimum of the 2D sphere with center in (0,0)");	
		WriteLine("Best Position: " + string.Join(" ", Position));

		APSO rastrigin = new APSO(dim, swarm, evalFunctions.Rastrigin);
		string file_ras = "Pos_Rastrigin.txt";
		rastrigin.Iterate(iterations, file_ras);
		double[] Position_ras = rastrigin.getPosition();
		WriteLine("Evaluating the global minimum of the 2D Rastrigin");
		WriteLine("Best Position: " + string.Join(" ", Position_ras));

		int dims = 3;
		string file = "3D sphere.txt";
		APSO threeDSphere = new APSO(dims, swarm, evalFunctions.sphere);
		threeDSphere.Iterate(iterations, file);
		double[] Positions = threeDSphere.getPosition();
		WriteLine("Evaluating the global minimum of the 3D sphere with center in (0,0,0)");
		WriteLine("Best Position: " + string.Join(" ", Positions));


	}//Main
}//main
