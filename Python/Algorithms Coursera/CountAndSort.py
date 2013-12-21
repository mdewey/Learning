def CountAndMergeSort(myList):
	if len(myList) <= 1:
		return myList, 0
	if (len(myList)%2 == 0):
		a = myList[0:len(myList) / 2]
		b = myList[len(myList) / 2:]
	else:
		lenA = (len(myList) - 1) / 2 + 1
		lenB = (len(myList)) / 2 
		a = myList[0:lenA]
		b = myList[lenA:]
	aCount = 0		
	bCount = 0
	a, aCount = CountAndMergeSort(a)
	b, bCount = CountAndMergeSort(b)
	#return list
	c = []
	i = 0
	j = 0
	cCount = 0
	#merge arrays
	print "a=", a,aCount ,"b=", b,bCount
	for k in range(len(myList)):
		# check if done with a
		#print i, len(a), j, len(b)
		if i > len(a) - 1:
			# print 'pull from right catch'
			c.append(b[j])
			j += 1
		elif j > len(b) - 1 :
			# print 'pull from left catch'
			c.append(a[i])
			i += 1
		else:
			if a[i] > b[j]:
				# print 'pull from right sorting'
				# print len(b[i:])
				cCount += len(b[i:])
				c.append(b[j])
				j += 1			
			elif a[i] < b[j]:
				# print 'pull from left sorting'
				c.append(a[i])
				i += 1
	print "left", aCount
	print "right", bCount
	print "split", cCount
	rvCount = aCount + bCount + cCount
	return c, rvCount
	
	
# with open('workfile', 'r') as f:
	# read_data = f.read()
# f.closed	
# print read_data
# unsorted = [1, 2,3,8,5,6,7,4]

def merge(left, right):
    result = []
    i, j = 0, 0
    while i < len(left) and j < len(right):
        if left[i] <= right[j]:
            result.append(left[i])
            i += 1
        else:
            result.append(right[j])
            j += 1

    result += left[i:]
    result += right[j:]
    return result


def mergesort(lst):
    if len(lst) <= 1:
        return lst
    middle = int(len(lst) / 2)
    left = mergesort(lst[:middle])
    right = mergesort(lst[middle:])
    return merge(left, right)





# unsorted = [4,3,2,1]
unsorted = [6,5,4,3,2,1]
print CountAndMergeSort(unsorted)
print mergesort([3, 4, 8, 0, 6, 7, 4, 2, 1, 9, 4, 5])
print mergesort(unsorted)
