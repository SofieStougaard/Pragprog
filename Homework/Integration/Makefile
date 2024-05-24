DIR = $(HOME)/Pragprog/Pragprog/Homework/Linear_equations/
DIR2 = $(HOME)/Pragprog/Pragprog/Homework/Least_squares_fit/

matrix.dll: $(DIR)vector.cs $(DIR)matrix.cs
	mcs -target:library -out:$@ $^

tot.dll : $(DIR2)QRGS.cs matrix.dll
	mcs -target:library -reference:matrix.dll -out:$@ $(DIR2)QRGS.cs

main.exe : main.cs matrix.dll
	mcs main.cs -reference:matrix.dll -target:exe -out:main.exe

Erf.gnuplot.svg : erf.txt Makefile
	echo '\
		set terminal svg background "white" ;\
		set out "$@" ;\
		set key right top ;\
		set tics out ;\
		set grid ;\
		plot \
			"erf.txt" using 1:2 with lines title "Erf" ;\
	' | tee log.gpi | gnuplot


clean:
	rm -f *.dll *.exe *.txt *.svg bestfit*.txt
