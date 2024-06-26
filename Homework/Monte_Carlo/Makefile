DIR = $(HOME)/Pragprog/Pragprog/Homework/Linear_equations/
DIR2 = $(HOME)/Pragprog/Pragprog/Homework/Least_squares_fit/

matrix.dll: $(DIR)vector.cs $(DIR)matrix.cs
	mcs -target:library -out:$@ $^

tot.dll : $(DIR2)QRGS.cs matrix.dll
	mcs -target:library -reference:matrix.dll -out:$@ $(DIR2)QRGS.cs

main.exe : main.cs matrix.dll
	mcs main.cs -reference:matrix.dll -target:exe -out:main.exe

UnitCircle.gnuplot.svg : UnitCircle.txt Makefile
	echo '\
		set terminal svg background "white" ;\
		set out "$@" ;\
		set key right top ;\
		set tics out ;\
		set grid ;\
		set xlabel "N";\
		set ylabel "Error estimate";\
		plot \
			"UnitCircle.txt" using 1:3 with lines title "Plain Monte Carlo"\
			,"UnitCircle.txt" using 1:4 with lines title "1/sqrt(N)" \
		        ,"UnitCirclequasi.txt" using 1:3 with lines title "Quasi Monte Carlo"	;\
	' | tee log.gpi | gnuplot

ValueOfPI.gnuplot.svg : UnitCircle.txt UnitCirclequasi.txt Makefile
	echo '\
                set terminal svg background "white" ;\
                set out "$@" ;\
                set key right top ;\
                set tics out ;\
                set grid ;\
		set yrange [3.0 : 3.3] ;\
                set xlabel "N";\
                set ylabel "Estimate";\
                plot \
			"UnitCircle.txt" using 1:2:3 with yerrorbars linestyle 7 lt rgb "violet" title "Plain Monte Carlo"\
                        ,"UnitCirclequasi.txt" using 1:2:3 with yerrorbars linestyle 7 title "Quasi Monte Carlo" \
			, 3.1415926 lc rgb "black" title "Value of Pi" ;\
	' | tee log.gpi | gnuplot

clean : 
	rm -f *.dll *.exe *.txt *.svg *.gpi
