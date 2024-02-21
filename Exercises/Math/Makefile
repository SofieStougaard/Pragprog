Out.txt : main.exe
	mono main.exe > Out.txt

main.exe : Main.cs sfuns.dll
	mcs -target:exe -out:main.exe -reference:sfuns.dll Main.cs

sfuns.dll : sfuns.cs
	mcs -target:library -out:sfuns.dll sfuns.cs

clean:
	rm -f main.exe Out.txt sfuns.dll
