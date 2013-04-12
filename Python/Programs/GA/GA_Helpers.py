from Chromosome import myChromosome
from random import randint
import copy


class GeneticAlgorithm:
	#constructor to set init parameters
	def __init__(self, _cl, _popSize, _numberOfGenerations, _maxMutations = 5, _mutateChance = 5, _maxFitness = 20):
	
		#init constants
		self.thisGeneration = 0
		
		#set values to parameters
		self.chromosomeLength = _cl
		self.maxGenerations = _numberOfGenerations
		self.populationSize = _popSize
		self.maxFitness = _maxFitness
		self.mutateChance = _mutateChance
		self.maxMutations = _maxMutations
				
		#init internal varibles
		self.population = [myChromosome(self.chromosomeLength)] * self.populationSize
		self.newPopulation = []
		
		#finds fitness
		map(self.findFitness, self.population)
		
	#gets the highest fitness population
	def printHighestFitness(self):
		print "The hightest fitness in Gen:{1} is {0}".format(max(myChromosome.fitness for myChromosome in self.population), self.thisGeneration)
		
	#evolves the populations
	def evolve(self):
		for i in range(self.maxGenerations):
			self.newPopulation = []
			self.printHighestFitness()
			self.thisGeneration += 1
			
			#finds fitness
			map(self.findFitness, self.population)
			
			#do selection   BE MORE PYTHONIC HERE!
			for j in range(self.populationSize):
				#mates mom and dad & adds to next gen
				self.newPopulation.append(self.mate(self.selection(), self.selection()))
			
			self.population = []
			self.population = copy.deepcopy(self.newPopulation)
			
			#mutates 5% of the time
			map(self.mutate, self.population)

	#mutates the chromo up to the max *change here to solve a real problem
	def mutate(self, chromosome):
		chance = randint(0,100)
		if (chance <= self.mutateChance):
			#do the mutation
			numOfMutations = randint(0, self.maxMutations)
			for i in range(numOfMutations):
				pos = randint(0, self.chromosomeLength - 1)
				if chromosome.chromo[pos] == 1:
					chromosome.chromo[pos] = 0
				else:
					chromosome.chromo[pos] = 1
		return

	#selects a random chromosome, roulette wheel style
	def selection(self):
		while(True):
			#gets the minFitness for this pass
			minFitness = randint(0, self.maxFitness)
			startingPoint = randint(0, self.populationSize)
			for i in self.population[startingPoint:]:
				if minFitness < i.fitness:
					return i
			for i in self.population[:startingPoint]:
				if minFitness < i.fitness:
					return i
		
	#cross breed 2 chromosome 
	def mate(self, mom, dad):
		#make empty child
		child = myChromosome(self.chromosomeLength, True)
		#select random spot in chromosome
		spot = randint(0,self.chromosomeLength)
		#add the first half mom to the kid and the second half of dad
		child.chromo[0:spot] = mom.chromo[0:spot]
		child.chromo[spot:] = dad.chromo[spot:]
		self.findFitness(child)
		return child
		
	#find the fitness of a chromosome *change here to solve a real problem	
	def findFitness(self, chromoX):
		chromoX.fitness = 0
		for gene in chromoX.chromo:
			chromoX.fitness += gene
	
	#prints a chromosome
	def printChromosome(self, chromosome):
		chromosome.printMe()

	#prints the population into String 
	def seePopulationAsString(self, pop):
		print "Generation {0}".format(self.thisGeneration)
		if len(pop) == 0:
			print "empty!!!"
		map(self.printChromosome, pop)
			
	