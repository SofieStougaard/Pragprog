using static System.Console;

public class vec{
	public double x,y,z; //Three components of the vector

	//Constructors:
	public vec(){ x=y=z=0;}
	public vec(double x, double y, double z){
		this.x=x;
		this.y=y;
		this.z=z;
	}
	
	//operators:
	public static vec operator*(vec v, double c){return new vec(c*v.x,c*v.y,c*v.z);}
	public static vec operator*(double c, vec v){return v*c;}
	//public static vec operator+(vec u, vec v){/*...*/}
	public static vec operator-(vec u){return new vec(-u.x,-u.y,-u.z);}
	//public static vec operator-(vec u, vec v){/*...*/}

	//methods:
	public void print(string s){Write(s);
		WriteLine($"{x} {y} {z}");}
	public void print(){this.print("");}
}
