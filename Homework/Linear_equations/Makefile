Out.txt: main.exe
	mono main.exe

main.exe: main.cs matrix.dll
	mcs main.cs -reference:matrix.dll

matrix.dll: vector.cs matrix.cs
	mcs -target:library -out:matrix.dll $^

clean:
	rm -f *.dll *.exe [Oo]ut.txt

