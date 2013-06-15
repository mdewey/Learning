import Orange
training = Orange.data.Table("genestrain.tab")
# print "Attributes:", ", ".join(x.name for x in data.domain.features)
print "Class:", training.domain.class_var.name
print "Data instances", len(training)

unknown = Orange.data.Table("genesblind.tab")
print "Class:", unknown.domain.class_var.name
print "Data instances", len(unknown)

learner = Orange.classification.bayes.NaiveLearner()
classifier = learner(training)

for d in unknown:
		result = classifier(d)
		if result == "CEU":
			print 0
		elif  result == "GIH":
			print 1
		elif  result == "JPT":
			print 2
		elif  result == "ASW":
			print 3
		elif  result == "YRI":
			print 4
		else:
			print "Oops!", result
		
		