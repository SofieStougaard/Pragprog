Out.txt : main.exe
	mono main.exe > Out.txt

main.exe : main.cs
	mcs main.cs -target:exe -out:main.exe

frames_sphere : Positions.txt sphere.txt Makefile
	echo '\
                set terminal pngcairo size 800,600 ;\
                set xrange [-5:5] ;\
                set yrange [-5:5] ;\
                set contour base ;\
                set cntrparam levels discrete 5,10,15,20,25,30,35,40 ;\
                unset surface ;\
                set view map ;\
		do for [i=0:39] {\
		set output sprintf("frame_sphere_%04d.png", i) ;\
			plot \
			"sphere.txt" index 0 using 1:2 with lines title "# Radius 1", \
			"sphere.txt" index 1 using 1:2 with lines title "# Radius 2", \
			"sphere.txt" index 2 using 1:2 with lines title "# Radius 3", \
			"sphere.txt" index 3 using 1:2 with lines title "# Radius 4", \
			"sphere.txt" index 4 using 1:2 with lines title "# Radius 5", \
			"Positions.txt" index i using 1:2 with points title sprintf("# Iteration %d", i) ;\
		}\
	' | gnuplot

plot_sphere.gif : frames_sphere
	convert -delay 15 -loop 1 frame_sphere_*.png plot_sphere.gif

frames_rastrigin : Pos_Rastrigin.txt Makefile
	echo '\
		set terminal pngcairo size 800,600 ;\
		set output "$@" ;\
		set xrange [-5:5] ;\
		set yrange [-5:5] ;\
		set contour base ;\
		set cntrparam levels discrete 5,10,15,20,25,30,35,40 ;\
		unset surface ;\
		set view map ;\
		do for [i=0:39]{\
		set output sprintf("frame_rastrigin_%04d.png", i) ;\
			plot "Pos_Rastrigin.txt" index i using 1:2 with points pt 7 title sprintf("# Iteration %d", i);\
		}\
	' | gnuplot

plot_rastrigin.gif : frames_rastrigin
	convert -delay 15 -loop 1 frame_rastrigin_*.png plot_rastrigin.gif

clean:
	rm -f frame_*.png

clean_all:
	rm -f *.dll *.exe *.txt *.svg *.gpi *.png *.gif

