package main

import(
    "tour/wc"
    "strings"
)

func print(s string) {
    fmt.Println(s)
}

func printArray(s []string) {
    fmt.Println(s)
}

func WordCount(s string) (rv map[string]int) {
    //init map
    rv = make(map[string]int)

    //get all words into an array
	words := strings.Split(s, " ")

	//loop through words
	for _, word := range words {
		_, wordExists := rv[word]
		if wordExists {
		    rv[word] += 1
		} else{
		    rv[word] = 1
		}


	}
	return
}

func main() {
    wc.Test(WordCount)
}
