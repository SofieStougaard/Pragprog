Out-stdio.txt : stdio.exe
	mono $< 1> $@ 2> log-stdio.txt

stdio.exe : stdio.cd
	mcs -target:exe -out:$@ $<

