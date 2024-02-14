using System;
using static System.Console;
using System.Collections.Generic;
using static vec;
//methods:
static class main{
        static int Main(){
	/*	vec a = new vec(2, 0, 0);
		vec b = new vec(0, 1, 0);
		Write($"ax={a.x} ay={a.y} az={a.z}\n");
		Write($"bx={b.x} by={b.y} bz={b.z}\n");
		bool c1 = a.approx(b) ;
		bool c = a.approx(a);
		Write($"{c1} {c}\n");
		vec c2 = cross(a,b);
		Write($"{c2.x} {c2.y} {c2.z}\n"); */
		List<vec> randomVectors = GenerateRandomVectors(5); // Change 5 to the desired number of vectors

        	foreach (var vector in randomVectors)
        	{
			Console.WriteLine($"x={vector.x} y={vector.y} z={vector.z}");
        	}
		return 0;
	}

		static List<vec> GenerateRandomVectors(int numberOfVectors)
    		{
        		List<vec> vectors = new List<vec>();
        		Random random = new Random();

        		for (int i = 0; i < numberOfVectors; i++)
        		{
            			double x = random.NextDouble()*10-5;
            			double y = random.NextDouble()*10-5;
            			double z = random.NextDouble()*10-5;

            			vectors.Add(new vec(x, y, z));
        		}

        	return vectors;
		}
}//main
