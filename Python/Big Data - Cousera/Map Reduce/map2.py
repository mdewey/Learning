 #!/usr/bin/env python
import mincemeat
import glob
import stopwords
import string
from os import listdir
from os.path import isfile, join

text_files = glob.glob('C:\Users\Mark\Desktop\Big Data - Cousera\hw3data\hw3data\output.txt')		

def file_Contents():
	f = open('output.txt')
	try:
		return f.read()
	finally:
		f.close();

source = dict((file_name, file_Contents()) 
				for file_name in text_files)
		
def mapfn(key, value):
	for line in value.splitlines():
		author = line[:line.find("|")]
		title = line[line.find("|")+3:line.find("']")]
		import lineParse
		keys = CreateKeys(author, title)
		for k in keys:
			yield k, 1

def reducefn(key, value):
	return key, len(value)

s = mincemeat.Server()

# The data source can be any dictionary-like object
s.datasource = source
s.mapfn = mapfn
s.reducefn = reducefn

results = s.run_server(password="changeme")
print results