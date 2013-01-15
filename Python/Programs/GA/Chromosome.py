from random import randint

class myChromosome:
	def __init__(self, l, empty = False):
		self.fitness = 0
		self.chromo = []
		self.geneLength = l
		if not empty:
			self.initRandomChromosome()
		else:
			for i in range(self.geneLength):
				self.chromo.append(0)

	def initRandomChromosome(self):
		if len(self.chromo) == 0:
			for i in range(self.geneLength):
				self.chromo.append(randint(0, 1))
		else:
			for i in self.chromo:
				self.chromo[i] = randint(0, 1)

	def  SetChromosome(self, X):
		self.chromo = X

	def  printMe(self):
		print "chromo:",
		for item in self.chromo :
			print(item),
		print "|fitness:{0}".format(self.fitness)


