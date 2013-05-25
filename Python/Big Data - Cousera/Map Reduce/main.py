#!/usr/bin/env python
import mincemeat
import glob
import stopwords
import string
from os import listdir
from os.path import isfile, join

text_files = glob.glob('C:\Users\Mark\Desktop\Big Data - Cousera\hw3data\hw3data\*')		

def file_Contents(file_name):
	f = open(file_name)
	try:
		return f.read()
	finally:
		f.close();

source = dict((file_name, file_Contents(file_name)) 
				for file_name in text_files)
		

def mapfn(key, value):
	import lineParse
	for line in value.splitlines():
		authors, remainingString = lineParse.GetAuthors(line)
		keys = lineParse.CreateKeys(authors, remainingString)
		for k in keys:
			yield k, 1

def reducefn(key, value):
	f = open('output.txt','a')
	f.write(key + "|" + str(len(value)) + '\n')
	return key, len(value)

s = mincemeat.Server()

# The data source can be any dictionary-like object
s.datasource = source
s.mapfn = mapfn
s.reducefn = reducefn

results = s.run_server(password="changeme")
print results