class main{
public static int Main(string[] args){
	foreach (var arg in args){
		if (arg == "erf"){
			for(double x=-5;x<=5;x+=1.0/8){
			System.Console.WriteLine($"{x} {sfuns.erf(x)}");
			}
		}

		else if (arg == "gamma"){
			for(double x=-5+1.0/64;x<=5;x+=1.0/128){
                	System.Console.WriteLine($"{x} {sfuns.gamma(x)}");
        		}
		}
	
		else if (arg == "lngamma"){
			for(double x=-5;x<=5;x+=1.0/8){
                        System.Console.WriteLine($"{x} {sfuns.lngamma(x)}");
			}
		}

		else if (arg == "factorial"){
			double fac = 1;
			for(double x=1;x<=5;x+=1.0){
			System.Console.WriteLine($"{x} {fac}");
			fac *=x;
			}
		}
	}//foreach
	return 0;
	}//Main
}//main
