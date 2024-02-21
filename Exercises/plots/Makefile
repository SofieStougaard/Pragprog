CODE = $(filter %.cs,$^)
LIBS = $(addprefix -reference:,$(filter %.dll,$^))
MKEXE = mcs -target:exe -out:$@ $(LIBS) $(CODE)
MKLIB = mcs -target:library -out:$@ $(LIBS) $(CODE)

.PHONY: all

all: Error.data Gamma.data Lngamma.data Out.erf.gnuplot.svg Factorial.data Out.gamma.gnuplot.svg Out.lngamma.gnuplot.svg

sfuns.dll : sfuns.cs ; $(MKLIB)
main.exe : main.cs sfuns.dll; $(MKEXE)

Error.data : main.exe
	mono $< erf > $@

Gamma.data : main.exe 
	mono $< gamma > $@

Lngamma.data : main.exe
	mono $< lngamma > $@

Factorial.data : main.exe
	mono $< factorial > $@

Out.erf.gnuplot.svg : Error.data Makefile
	echo '\
		set terminal svg background "white" ;\
		set out "$@" ;\
		set key left ;\
		set xlabel "x";\
		set ylabel "erf(x)";\
		set title "Error function" ;\
		set tics out ;\
		set grid ;\
		plot [-5:5]\
			"Error.data" with lines notitle \
			,"erf.data.txt" using 1:2 with points pointtype 6 notitle \
			,"erf.data.txt" using (-($$1)):(-($$2)) with points pointtype 6 notitle ;\
	' | tee log.gpi | gnuplot

Out.gamma.gnuplot.svg : Gamma.data Makefile
	echo '\
                set terminal svg background "white" ;\
                set out "$@" ;\
                set key left ;\
                set xlabel "x";\
                set ylabel "gamma(x)";\
                set title "Gamma function" ;\
                set tics out ;\
                set grid ;\
		set yrange [-5:5] ;\
                plot [-5:5]\
                        "Gamma.data" with lines notitle \
			,"Factorial.data" with points pointtype 6 notitle;\
        ' | tee log.gpi | gnuplot

Out.lngamma.gnuplot.svg : Lngamma.data Makefile
	echo '\
                set terminal svg background "white" ;\
                set out "$@" ;\
                set key left ;\
                set xlabel "x";\
                set ylabel "gamma(x)";\
                set title "Gamma function" ;\
                set tics out ;\
                set grid ;\
                plot [-5:5]\
                        "Lngamma.data" with lines notitle ;\
        ' | tee log.gpi | gnuplot


clean: 
	$(RM) *.exe *.dll [Oo]ut* [Ll]og* *.data
