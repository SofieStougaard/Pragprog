DIR = $(HOME)/Pragprog/Pragprog/Homework/Linear_equations/

Out.txt: main.exe
	mono main.exe > Out.txt

main.exe: main.cs tot.dll matrix.dll
	mcs main.cs -reference:tot.dll -reference:matrix.dll -target:exe -out:main.exe

matrix.dll: $(DIR)vector.cs $(DIR)matrix.cs
	mcs -target:library -out:$@ $^

tot.dll : QRGS.cs matrix.dll
	mcs QRGS.cs -target:library -reference:matrix.dll -out:tot.dll

ThX.gnuplot.svg : fitdata.txt Makefile
	echo '\
		set terminal svg background "white" ;\
		set out "$@" ;\
		set key right top ;\
		set xlabel "time t [days]";\
		set ylabel "Activity Ln(y) (relative units)";\
		set title "Activity of ThX and fits" ;\
		set tics out ;\
		set grid ;\
		plot \
			"data.txt" using 1:2:3 with yerrorbars linestyle 7 title "datapoints" \
			,"bestfit.txt" with lines title "bestfit" \
			,"bestfit_p.txt" with lines title "bestfit + unc" \
			,"bestfit_m.txt" with lines title "bestfit - unc" ;\
	' | tee log.gpi | gnuplot



clean:
	rm -f *.dll *.exe [Oo]ut.txt *.svg bestfit*.txt
