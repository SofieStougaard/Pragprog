DIR = $(HOME)/Pragprog/Pragprog/Homework/Linear_equations/

Out.txt: main.exe
	mono main.exe > Out.txt

main.exe: main.cs splines.dll
	mcs main.cs -reference:splines.dll -target:exe -out:main.exe

splines.dll : splines.cs
	mcs -target:library -out:splines.dll splines.cs

Linterp.gnuplot.svg : data.txt Makefile
	echo '\
		set terminal svg background "white" ;\
		set out "$@" ;\
		set key right top ;\
		set xlabel "x";\
		set ylabel "y";\
		set title "Linear interpolation" ;\
		set tics out ;\
		set grid ;\
		plot \
			"data.txt" using 1:2 linestyle 7 title "datapoints" \
			,"linterp.txt" using 1:2 with lines title "Spline" \
			,"linterp.txt" using 1:3 with lines title "Integral" ;\
	' | tee log.gpi | gnuplot

Qinterp.gnuplot.svg : data.txt Makefile
	echo '\
                set terminal svg background "white" ;\
                set out "$@" ;\
                set key right top ;\
                set xlabel "x";\
                set ylabel "y";\
                set title "Quadratic interpolation" ;\
                set tics out ;\
                set grid ;\
                plot \
                        "data.txt" using 1:3 linestyle 7 title "datapoints" \
                        ,"qinterp.txt" using 1:2 with lines title "Spline" \
                        ,"qinterp.txt" using 1:3 with lines title "Integral" ;\
        ' | tee log.gpi | gnuplot

clean:
	rm -f *.dll *.exe *.txt *.svg *.gpi
