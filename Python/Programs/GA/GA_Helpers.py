from Chromosome import myChromosome
from random import randint
import copy


class GeneticAlgorithm:
	def __init__(self, cl, popSize, numberOfGenerations):
		#init constants
		self.thisGeneration = 0
		self.maxFitness = 20
		self.mutateChance = 5
		self.maxMutations = 5
		self.chromosomeLength = cl
		
		#init local varibles
		self.population = []
		self.newPopulation = []
		self.maxGenerations = numberOfGenerations
		self.populationSize = popSize
	
		#inits population size
		for i in range(popSize):
			temp = myChromosome(self.chromosomeLength)
			self.population.append(temp)
		
		#finds fitness
		map(self.findFitness, self.population)
		
	#evolves the populations
	def evolve(self):
		for i in range(self.maxGenerations):
			self.thisGeneration += 1
			#finds fitness
			map(self.findFitness, self.population)
			#do selection
			for j in range(self.populationSize):
				#finds mum and pa 
				mom = self.selection()
				dad = self.selection()
				#mates mom and date
				child = self.mate(mom, dad)
				
				self.newPopulation.append(child)
			
			self.population = []
			self.population = copy.deepcopy(self.newPopulation)
			self.newPopulation = []
			
			#mutates 5% of the time
			map(self.mutate, self.population)
			
			#finds fitness
			map(self.findFitness, self.population)
		
	
	#mutates the chromo up to the max
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
		
	#find the fitness of a chromosome	
	def findFitness(self, chromoX):
		chromoX.fitness = 0
		for gene in chromoX.chromo:
			chromoX.fitness += gene
	
	#prints the population into String 
	def seePopulationAsString(self):
		print "Generation {0}".format(self.thisGeneration)
		if len(self.population) == 0:
			print "empty!!!"
		for thing in self.population:
			thing.printMe()
			
	#prints the population into String 
	def seeNewPopulationAsString(self):
		print "Next Generation {0}".format(self.thisGeneration)
		if len(self.newPopulation) == 0:
			print "empty!!!"
		for thing in self.newPopulation:
			thing.printMe()