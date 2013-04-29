import operator

def file_Contents(file_name):
	f = open(file_name)
	try:
		return f.read()
	finally:
		f.close();
		
		
lines = file_Contents('output.txt').splitlines()
results = {}
for line in lines:
	key = line[:line.find("|")]
	value = line[line.find("|") + 1 :]
	results[key] = value

sorted_x = sorted(results.iteritems(), key=operator.itemgetter(1))

f = open('sorted.txt','a')
for value in sorted_x:
	f.write(str(value) + '\n')
