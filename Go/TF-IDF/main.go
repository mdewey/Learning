package main

import (
	"fmt"
	"math"
	"strings"
)

type Word struct {
	thisWord string
	count    int
	tf_idf   float64
}

var wordList = []Word{}
var cx = "012766819612763970466:kh5frtpar8i"
var apiKey = "AIzaSyAFc9plC-1Vr4d5K8apxueCUFBZ9Asmymc"
var googleUrl = "GET https://www.googleapis.com/customsearch/v1?key=%s&cx=%s&q=%s"

func main() {
	allWords := "This course is about building 'web-intelligence' applications exploiting big data sources arising social media, mobile devices and sensors, using new big-data platforms based on the 'map-reduce' parallel programming paradigm. The course is being offered"

	words := strings.Fields(allWords)

	for i := 0; i < len(words); i++ {
		pos := isWordInSlice(words[i])
		if pos < 0 {
			//new word
			wordList = append(wordList, Word{strings.ToLower(words[i]), 1, 0})
		} else {
			//increase word count
			wordList[pos].count++
		}
	}
	for c, val := range wordList {
		wordList[c].tf_idf = calculateTF_IDF(val)
	}
	//TODO: sort based on count
	fmt.Println(wordList)

}

func isWordInSlice(word string) int {
	for p, v := range wordList {
		if strings.ToLower(v.thisWord) == strings.ToLower(word) {
			return p
		}
	}
	return -1
}

//logbase2(idf) * tf
func calculateTF_IDF(word Word) float64 {
	totalPages := 50000000000.0
	hits := getHits(word.thisWord)
	idf := totalPages / hits
	tf := float64(word.count)
	tf_idf := math.Log2(idf) * tf
	return tf_idf
}

func getHits(word string) float64 {
	url := fmt.Sprintf(googleUrl, apiKey, cx, word)
	//TODO: post to this url and read response
	fmt.Println(url)
	//TODO: return count
	return 2000000000
}
