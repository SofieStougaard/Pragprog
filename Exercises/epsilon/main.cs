using System;
using static System.Console;

class main{
static int Main(){
	int i=1;
	while(i+1>i) {i++;}
	Write($"My max int = {i}, with MaxValue = {int.MaxValue}\n");

	while(i-1<i) {i--;}
	Write($"My min int = {i}, with MinValue = {int.MinValue}\n");
	return 0;
}//end of Main
}//end of main class
