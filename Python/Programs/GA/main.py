from GA_Helpers import GeneticAlgorithm

chromosomeLength = 20
maxGenerations = 100
populationSize = 100

def main():
	myPop = GeneticAlgorithm(chromosomeLength, populationSize, maxGenerations)
	myPop.seePopulationAsString()
	myPop.evolve()
	myPop.seePopulationAsString()
	
#testingChromosomeIsDefined()
main()