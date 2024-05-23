DIR = $(HOME)/Pragprog/Pragprog/Homework/Linear_equations/
DIR2 = $(HOME)/Pragprog/Pragprog/Homework/Least_squares_fit/

matrix.dll: $(DIR)vector.cs $(DIR)matrix.cs
	mcs -target:library -out:$@ $^

tot.dll : $(DIR2)QRGS.cs matrix.dll
	mcs -target:library -reference:matrix.dll -out:$@ $(DIR2)QRGS.cs

main.exe : main.cs matrix.dll
	mcs main.cs -reference:matrix.dll -target:exe -out:main.exe


HarmOsci.gnuplot.svg : HarmOsci.txt Makefile
	echo '\
		set terminal svg background "white" ;\
		set out "$@" ;\
		set key right top ;\
		set tics out ;\
		set grid ;\
		plot \
			"HarmOsci.txt" using 1:2 with lines title "u" \
			,"HarmOsci.txt" using 1:3 with lines title "u^{l}" ;\
	' | tee log.gpi | gnuplot

DampOsci.gnuplot.svg : DampOsci.txt Makefile
	echo '\
                set terminal svg background "white" ;\
                set out "$@" ;\
                set key right top ;\
                set tics out ;\
                set grid ;\
                plot \
                        "DampOsci.txt" using 1:2 with lines title "Gamma" \
                        ,"DampOsci.txt" using 1:3 with lines title "Omega" ;\
        ' | tee log.gpi | gnuplot

clean:
	rm -f *.dll *.exe [Oo]ut.txt *.svg bestfit*.txt
