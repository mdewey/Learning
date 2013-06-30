from random import randint, choice

def ValidatePattern(pattern):
	patternAsList = map(int, pattern)
	avg = sum(patternAsList) / float(len(patternAsList))
	return isWhole(avg), avg
		
def isWhole(num):
	return(num%1 == 0)
		
def GeneratePattern(len):
	rv = "".join(choice("01234567") for i in range(1, len))
	return rv
		
def GenerateValidRandomPattern():
	isValid = False
	rightPattern = ""
	attempts = 0
	while not isValid:
		rightPattern = GeneratePattern(randint(3,10))
		isValid, numberOfBalls = ValidatePattern(rightPattern)
		attempts += 1
		if (isValid):
			print attempts
			print "pattern",  rightPattern 
			print "balls" , numberOfBalls
			print "beats" , len(rightPattern)
			return rightPattern, numberOfBalls,len(rightPattern)
