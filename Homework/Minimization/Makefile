DIR = $(HOME)/Pragprog/Pragprog/Homework/Linear_equations/
DIR2 = $(HOME)/Pragprog/Pragprog/Homework/Least_squares_fit/

matrix.dll: $(DIR)vector.cs $(DIR)matrix.cs
	mcs -target:library -out:$@ $^

tot.dll : $(DIR2)QRGS.cs matrix.dll
	mcs -target:library -reference:matrix.dll -out:$@ $(DIR2)QRGS.cs

Out.txt: main.exe
	mono main.exe > Out.txt

main.exe : main.cs tot.dll matrix.dll 
	mcs main.cs -reference:tot.dll -reference:matrix.dll -target:exe -out:main.exe

clean : 
	rm -f *.txt *.dll *.exe
