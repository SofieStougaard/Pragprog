cmath.dll : cmath.cs complex.cs
	mcs -target:library -out:./cmath.dll $^

main.exe : main.cs cmath.dll
	mcs -reference:cmath.dll -target:exe -out:main.exe main.cs

out.txt : main.exe
	mono $< > $@

clean :
	rm -f [Oo]ut.txt main.exe *.dll
