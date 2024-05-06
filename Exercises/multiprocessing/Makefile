N = 3e8
TIME = time --portability --append --output $@
Out.txt : main.exe Makefile
	>$@
	$(TIME) mono main.exe -terms:$(N) -threads:1 >> $@
	$(TIME) mono main.exe -terms:$(N) -threads:2 >> $@
	$(TIME) mono main.exe -terms:$(N) -threads:3 >> $@
	$(TIME) mono main.exe -terms:$(N) -threads:4 >> $@

main.exe : main.cs
	mcs -target:exe -out:$@ $<

clean :
	rm -f [oO]ut.txt main.exe

