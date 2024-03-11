Out.txt : main.exe
	mono main.exe

main.exe : main.cs
	mcs -target:exe -out:$@ $<

clean :
	rm -f [oO]ut.txt main.exe

