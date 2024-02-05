Out.txt : main.exe
	mono main.exe > Out.txt

main.exe : main.cs sfuncs.dll
	mcs -target:exe -reference:sfuncs.dll -out:main.exe main.cs

sfuncs.dll : sfuncs.cs
	mcs -target:library -out:sfuncs.dll sfuncs.cs
