import sqlite3	 as lite
import sys
import data

con = None

def Normalize(x, y):
	return x / (x + y)


try: 
	con = lite.connect('myDb.db')
	 
	with con:
		#make tables
		cur = con.cursor()
		
		#make eitherTubOrLung table
		cur.execute("DROP TABLE IF EXISTS eitherTubOrLung")
		cur.execute("CREATE TABLE eitherTubOrLung(e TEXT,l TEXT,t TEXT, p INT)")
		cur.executemany("INSERT INTO eitherTubOrLung VALUES(?,?,?,?)", data.eitherTubOrLung)
		
		#make dyspnoea table
		cur.execute("DROP TABLE IF EXISTS dyspnoea")
		cur.execute("CREATE TABLE dyspnoea(d TEXT,e TEXT, b TEXT, p INT)")
		cur.executemany("INSERT INTO dyspnoea VALUES(?,?,?,?)", data.dyspnoea)
		
		
		#make asia table
		cur.execute("DROP TABLE IF EXISTS asia")
		cur.execute("CREATE TABLE asia(a TEXT, p INT)")
		cur.executemany("INSERT INTO asia VALUES(?,?)", data.asia)
		
		#make smoking table
		cur.execute("DROP TABLE IF EXISTS smoking")
		cur.execute("CREATE TABLE smoking(s TEXT, p INT)")
		cur.executemany("INSERT INTO smoking VALUES(?,?)", data.smoking)
		
		#make tuberculosis table
		cur.execute("DROP TABLE IF EXISTS tuberculosis")
		cur.execute("CREATE TABLE tuberculosis(t TEXT, a TEXT, p INT)")
		cur.executemany("INSERT INTO tuberculosis VALUES(?,?,?)", data.tuberculosis)
		
		#make lungCancer table
		cur.execute("DROP TABLE IF EXISTS lungCancer")
		cur.execute("CREATE TABLE lungCancer(l TEXT, s TEXT, p INT)")
		cur.executemany("INSERT INTO lungCancer VALUES(?,?,?)", data.lungCancer)
		
		#make xRay table
		cur.execute("DROP TABLE IF EXISTS xRay")
		cur.execute("CREATE TABLE xRay(x TEXT, e TEXT, p INT)")
		cur.executemany("INSERT INTO xRay VALUES(?,?,?)", data.xRay)
		
		#make bronchitis table
		cur.execute("DROP TABLE IF EXISTS bronchitis")
		cur.execute("CREATE TABLE bronchitis(b TEXT, s TEXT, p INT)")
		cur.executemany("INSERT INTO bronchitis VALUES(?,?,?)", data.bronchitis)
		
	
	con.commit()
	
	with con:
		cur = con.cursor()
		cur.execute("SELECT tuberculosis.t, SUM(asia.p * tuberculosis.p * smoking.p * lungCancer.p * eitherTubOrLung.p * dyspnoea.p * xRay.p * bronchitis.p ) FROM asia, tuberculosis, smoking, lungCancer, eitherTubOrLung, dyspnoea, xRay, bronchitis WHERE smoking.s = 'n' AND asia.a = 'y' AND dyspnoea.d = 'y' AND xRay.x = 'n' AND asia.a = tuberculosis.a AND tuberculosis.t = eitherTubOrLung.t AND eitherTubOrLung.e = xRay.e AND eitherTubOrLung.l = lungCancer.l AND smoking.s = lungCancer.s AND smoking.s = bronchitis.s AND bronchitis.b = dyspnoea.B AND dyspnoea.e = eitherTubOrLung.e GROUP BY tuberculosis.t")
		rows = cur.fetchall()
		
		for row in rows:
			print row
			
		print "Chance of Tuber:" , Normalize(rows[1][1],rows[0][1])
		
		cur.execute("SELECT lungCancer.l, SUM(asia.p * tuberculosis.p * smoking.p * lungCancer.p * eitherTubOrLung.p * dyspnoea.p * xRay.p * bronchitis.p ) FROM asia, tuberculosis, smoking, lungCancer, eitherTubOrLung, dyspnoea, xRay, bronchitis WHERE smoking.s = 'n' AND asia.a = 'y' AND dyspnoea.d = 'y' AND xRay.x = 'n' AND asia.a = tuberculosis.a AND tuberculosis.t = eitherTubOrLung.t AND eitherTubOrLung.e = xRay.e AND eitherTubOrLung.l = lungCancer.l AND smoking.s = lungCancer.s AND smoking.s = bronchitis.s AND bronchitis.b = dyspnoea.B AND dyspnoea.e = eitherTubOrLung.e GROUP BY lungCancer.l")
		rows = cur.fetchall()
				
		for row in rows:
			print row
		print "Chance of lung cancer:" , Normalize(rows[1][1],rows[0][1])
		
		cur.execute("SELECT bronchitis.b, SUM(asia.p * tuberculosis.p * smoking.p * lungCancer.p * eitherTubOrLung.p * dyspnoea.p * xRay.p * bronchitis.p ) FROM asia, tuberculosis, smoking, lungCancer, eitherTubOrLung, dyspnoea, xRay, bronchitis WHERE smoking.s = 'n' AND asia.a = 'y' AND dyspnoea.d = 'y' AND xRay.x = 'n' AND tuberculosis.t = eitherTubOrLung.t AND eitherTubOrLung.e = xRay.e AND eitherTubOrLung.l = lungCancer.l AND smoking.s = lungCancer.s AND smoking.s = bronchitis.s AND bronchitis.b = dyspnoea.B AND dyspnoea.e = eitherTubOrLung.e GROUP BY bronchitis.b")
		rows = cur.fetchall()
				
		for row in rows:
			print row
		print "Chance of bronchitis:" , Normalize(rows[1][1],rows[0][1])
		
		
except lite.Error, e:
	print "Error %s:" % e.args[0]
	sys.exit(1)
finally:
	if con:
		con.close()
		
