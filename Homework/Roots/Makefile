DIR = $(HOME)/Pragprog/Pragprog/Homework/Linear_equations/
DIR2 = $(HOME)/Pragprog/Pragprog/Homework/Least_squares_fit/
DIR3 = $(HOME)/Pragprog/Pragprog/Homework/ODE/


matrix.dll: $(DIR)vector.cs $(DIR)matrix.cs
	mcs -target:library -out:$@ $^

tot.dll : $(DIR2)QRGS.cs matrix.dll
	mcs -target:library -reference:matrix.dll -out:$@ $(DIR2)QRGS.cs

ODE.dll : $(DIR3)ODE.cs matrix.dll
	mcs -target:library -reference:matrix.dll -out:$@ $(DIR3)ODE.cs


main.exe : main.cs tot.dll matrix.dll ODE.dll
	mcs main.cs -reference:tot.dll -reference:matrix.dll -reference:ODE.dll -target:exe -out:main.exe

Hydrogen.gnuplot.svg : Hydrogen.txt Makefile
	echo '\
		set terminal svg background "white" ;\
		set out "$@" ;\
		set key right top ;\
		set tics out ;\
		set grid ;\
		set xlabel "r [Bohr radius]" ;\
		set ylabel "f(r) (Radial wave function)" ;\
		plot \
			"Hydrogen.txt" using 1:2 with lines notitle ;\
	' | tee log.gpi | gnuplot

clean : 
	rm -f *.txt *.dll *.exe
