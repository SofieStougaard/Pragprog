Out.txt : main.exe
	mono main.exe -output:$@ -numbers:2,3,5,6

main.exe : main.cs
	mcs -target:exe -out:$@ $<

clean :
	rm -f [oO]ut.txt main.exe

