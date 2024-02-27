Out.txt : main.exe
	mono main.exe -output:$@ -numbers:2,3,6

main.exe : main.cs
	mcs -target:exe -out:$@ $<

clean :
	rm -f [Oo]ut.txt main.exe
