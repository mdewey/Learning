def MergeSort(myList):
	if len(myList) <= 1:
		return myList
	a = myList[0:len(myList) / 2]
	b = myList[len(myList) / 2:]
	a = MergeSort(a)
	b = MergeSort(b)
	#return list
	c = []
	i = 0
	j = 0
	#merge arrays
	print "a=", a,"b=", b
	for k in range(len(myList)):
		# check if done with a
		print i, len(a), j, len(b)
		if i > len(a) - 1:
			print 'pull form b catch'
			c.append(b[j])
			j += 1	
		elif j > len(b) - 1 :
			print 'pull form a catch'
			c.append(a[i])
			i += 1
		else:
			if a[i] > b[j]:
				print 'pull form b sorting'
				c.append(b[j])
				j += 1			
			elif a[i] < b[j]:
				print 'pull from a sorting'
				c.append(a[i])
				i += 1
			
			
	return c
	
	
	
unsorted = [2,1,3,4,5,7,6,8]

print MergeSort(unsorted)
