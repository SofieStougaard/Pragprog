using System;
using static System.Console;
using static vec;
//methods:
static class main{
        static int Main(){
                //public void print(string s){Write(s);
                //      WriteLine($"{x} {y} {z}");}
                //public void print(){this.print("");}
		vec a = new vec(2, 0, 0);
		vec b = new vec(0, 1, 0);
		Write($"ax={a.x} ay={a.y} az={a.z}\n");
		Write($"bx={b.x} by={b.y} bz={b.z}\n");
		bool c1 = a.approx(b) ;
		bool c = a.approx(a);
		Write($"{c1} {c}\n");
		vec c2 = cross(a,b);
		Write($"{c2.x} {c2.y} {c2.z}\n");
		return 0;
}//Main
}//main
