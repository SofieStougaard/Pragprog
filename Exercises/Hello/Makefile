Out.txt : Hello.exe		 #
	mono Hello.exe > Out.txt #Runs hello.exe, and sends the output to Out.txt

Hello.exe : Hello.cs
	mcs Hello.cs		 #Compiles hello.cs into the hello.exe

clean:
	rm -f Out.txt hello.exe	 #Removes any secondary files
