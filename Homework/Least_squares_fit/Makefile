DIR = $(HOME)/Pragprog/Pragprog/Homework/Linear_equations/

Out.txt: main.exe
	mono main.exe > Out.txt

main.exe: main.cs tot.dll matrix.dll
	mcs main.cs -reference:tot.dll -reference:matrix.dll -target:exe -out:main.exe

matrix.dll: $(DIR)vector.cs $(DIR)matrix.cs
	mcs -target:library -out:$@ $^

tot.dll : QRGS.cs matrix.dll
	mcs QRGS.cs -target:library -reference:matrix.dll -out:tot.dll

clean:
	rm -f *.dll *.exe [Oo]ut.txt
