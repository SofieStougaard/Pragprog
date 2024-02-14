using static System.Console;
using static System.Math;

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
	public static vec operator+(vec u, vec v){return new vec(u.x+v.x, u.y+v.y, u.z+v.z);}
	public static vec operator-(vec u){return new vec(-u.x,-u.y,-u.z);}
	public static vec operator-(vec u, vec v){return new vec(u.x-v.x, u.y-v.y, u.z-v.z);}
	//Dot product	
	public double dot(vec other) /* to be called as u.dot(v) */
		{return this.x*other.x+this.y*other.y+this.z*other.z;}
	public static double dot(vec v,vec w) /* to be called as vec.dot(u,v) */
		{return v.x*w.x+v.y*w.y+v.z*w.z;}
	//Cross product
	public static vec cross(vec u, vec v)
		{return new vec(u.y*v.z-u.z*v.y, u.z*v.x-u.x*v.z, u.x*v.y-u.y*v.x);}
	public static double norm(vec u)
		{return Sqrt(Pow(u.x,2)+Pow(u.y,2)+Pow(u.z,2));}

	static bool approx(double a,double b,double acc=1e-9,double eps=1e-9){
		if(Abs(a-b)<acc)return true;
		if(Abs(a-b)<(Abs(a)+Abs(b))*eps)return true;
		return false;
	}

	public bool approx(vec other){
		if(!approx(this.x,other.x))return false;
		if(!approx(this.y,other.y))return false;
		if(!approx(this.z,other.z))return false;
		return true;
	}

	public override string ToString(){ return $"{x} {y} {z}"; }
/*
	//methods:
	static int Main(){
		//public void print(string s){Write(s);
		//	WriteLine($"{x} {y} {z}");}
		//public void print(){this.print("");}
		vec a = new vec(2, 0, 0);
		vec b = new vec(0, 1, 0);
		double k = 2;
		Write($"ax={a.x} ay={a.y} az={a.z}\n");
		Write($"bx={b.x} by={b.y} bz={b.z}\n");
		//vec c1 = a.approx(b) ;
		//Write($"cx={c1.x} cy={c1.y} cz={c1.z}\n");
		bool c2 = a.approx(a) ;
                Write($"{c2}\n");
		return 0;
} */
}
