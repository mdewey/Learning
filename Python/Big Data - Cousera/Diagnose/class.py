import sqlite3	 as lite
import sys
import data

con = None

def Normalize(x, y):
	return x / (x + y)

rain = (
	('y', 0.2),
	('n', 0.8))
	
sprinkler = (
	('y', 0.3),
	('n', 0.7))
	
wet = (
	('y','y','y',0.9),
	('y','y','n',0.7),
	('y','n','y',0.8),
	('y','n','n',0.1),
	('n','n','n',0.9),
	('n','n','y',0.2),
	('n','y','n',0.3),
	('n','y','y',0.1)
)
	
	
	
try: 
	con = lite.connect('classExample.db')
	 
	with con:
		#make tables
		cur = con.cursor()
		
		
		#make rain table
		cur.execute("DROP TABLE IF EXISTS rain")
		cur.execute("CREATE TABLE rain(r TEXT, p INT)")
		cur.executemany("INSERT INTO rain VALUES(?,?)", rain)
		
		
		#make sprinkler table
		cur.execute("DROP TABLE IF EXISTS sprinkler")
		cur.execute("CREATE TABLE sprinkler(s TEXT, p INT)")
		cur.executemany("INSERT INTO sprinkler VALUES(?,?)", sprinkler)
		
		#make xRay table
		cur.execute("DROP TABLE IF EXISTS wet")
		cur.execute("CREATE TABLE wet(w TEXT,s TEXT, r TEXT, p INT)")
		cur.executemany("INSERT INTO wet VALUES(?,?,?,?)", wet)		
		
	con.commit()
	
	with con:
		cur = con.cursor()
		cur.execute("SELECT wet.r, SUM(wet.p * rain.p * sprinkler.p) FROM wet, rain, sprinkler WHERE wet.w = ? AND wet.r = rain.r AND wet.s = sprinkler.s GROUP BY wet.r", 'y')
		rows = cur.fetchall()
		
		for row in rows:
			print row
		print Normalize(rows[1][1],rows[0][1])
		
except lite.Error, e:
	print "Error %s:" % e.args[0]
	sys.exit(1)
finally:
	if con:
		con.close()
		