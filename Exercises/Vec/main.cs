using System;
using static System.Console;
using System.Collections.Generic;
using static vec;
//methods:
static class main{
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
	
	static int Main(){
		List<vec> VecA = GenerateRandomVectors(5); // Change 5 to the desired number of vectors
		List<vec> VecB = GenerateRandomVectors(5);
		bool allTestsPassed = true;

        	for (int i=0; i<5 ; i++)
                {
			vec c1 = VecA[i] + VecB[i];
			vec c2 = VecB[i] + VecA[i];
			if (!c1.approx(c2)){
				WriteLine($"A+B = B+A not true");
				allTestsPassed = false;}
			
			vec c3 = VecA[i] - VecB[i];
                        vec c4 = VecB[i] - VecA[i];
                        if (!c3.approx(-c4)){
                                WriteLine($"A-B = -(B-A) not true");
                                allTestsPassed = false;}	

			vec c5 = 2*VecA[i];
			vec c6 = VecA[i]+VecA[i];
			if (!c5.approx(c6)){
				WriteLine($"A+A = 2A not true");
				allTestsPassed = false;}

			vec c7 = cross(VecA[i],VecB[i]);
			vec c8 = cross(VecB[i],VecA[i]);
			if (!c7.approx(-c8)){
				WriteLine($"AxB = -BxA not true");
				allTestsPassed = false;}

			double c9 = dot(VecA[i],VecA[i]);
                        double c10 = norm(VecA[i])*norm(VecA[i]);
                        if (!VecA[i].approx_d(c9,c10)){
				WriteLine($"|A|^2 = A*A not true\n");
				allTestsPassed = false;}
                }
		if (allTestsPassed)
        		{
            		WriteLine("All tests passed!");
        		}
        	else
        		{
            		WriteLine("Some tests failed.");
        		}

		return 0;
	}

}//main
