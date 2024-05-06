Out.txt : main.exe
	mono main.exe input:input.txt -output:$@

main.exe : main.cs genlist.dll
	mcs main.cs -reference:genlist.dll

genlist.dll : genlist.cs
	mcs -target:library -out:genlist.dll genlist.cs

clean:
	rm -f [oO]ut.txt main.exe *.dll

