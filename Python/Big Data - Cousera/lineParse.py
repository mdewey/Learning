import stopwords
import string
def GetAuthors(stringToParse):
	stringToParse = stringToParse[stringToParse.find(':::') + 3:]
	rv = []
	while stringToParse.find("::") > 0:
		rv.append(stringToParse[:stringToParse.find("::")])
		stringToParse = stringToParse[stringToParse.find("::") + 2:]
	return rv, stringToParse[1:]
	

def CreateKey(author, word):
	return author + ":" + word

def CleanWord(word):
	#exclude punc
	word = word.replace("-", " ")
	return word.translate(None, string.punctuation).lower()
	
def CreateKeys(authors, remainingString):
	rv = []
	for author in authors:
		for word in remainingString.split():
			if (word not in stopwords.allStopWords):
					if len(word) > 1:
						rv.append(CreateKey(author, CleanWord(word)))
	return rv
	
	
#main program
# testString =  'conf/ep/BertiDM98:::Laure Berti::Jean-Luc Damoiseaux::Elisabeth Murisasco:::Combining the Power of Query Languages and Search Engines for On-line Document and Information Retrieval : The QIRi@D Environment.'
# authors, remainingString =  GetAuthors(testString)
# printMe = CreateKeys(authors, remainingString)
# print printMe
