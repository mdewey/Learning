from sys import argv

script, filename = argv

txt = open(filename)

print "here is your file %r:" % filename
print txt.read()

print "enter file name:"
file2 = raw_input("> ")

txt2 = open(file2)

print txt2.read()