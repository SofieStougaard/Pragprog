Out.txt : main.exe 
	mono main.exe input:input.txt -output:$@

main.exe : main.cs
	mcs -target:exe -out:$@ $<

clean :
	rm -f [oO]ut.txt main.exe
