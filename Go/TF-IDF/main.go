package main

import (
	"encoding/json"
	"fmt"
	"io/ioutil"
	"math"
	"net/http"
	"sort"
	"strings"

	"strconv"
)

type Word struct {
	thisWord string
	count    int
	tf_idf   float64
}

//sorting stuff
type Words []*Word

func (s Words) Len() int      { return len(s) }
func (s Words) Swap(i, j int) { s[i], s[j] = s[j], s[i] }

type ByIt_idf struct{ Words }

func (s ByIt_idf) Less(i, j int) bool {
	if s.Words[i] != nil && s.Words[j] != nil {
		return s.Words[i].tf_idf > s.Words[j].tf_idf
	}
	return false
}

//end of sorting stuff

//google search api stuff
var cx = "012766819612763970466:kh5frtpar8i"
var apiKey = "AIzaSyAFc9plC-1Vr4d5K8apxueCUFBZ9Asmymc"
var googleUrl = "https://www.googleapis.com/customsearch/v1?key=%s&cx=%s&q=%s"

//consts
var wordList = []*Word{{"this", 0, 0}}
var puncsToRemove = []string{".", ",", "'", "?", "!"}

func main() {
	allWords := "This course is about building 'web-intelligence' course applications exploiting big data sources arising social media, mobile devices and sensors, using new big-data platforms based on the 'map-reduce' parallel programming paradigm. The course is being offered"

	words := strings.Fields(allWords)

	for i := 0; i < len(words); i++ {
		wordToAdd := stripPuncuation(strings.ToLower(words[i]))
		pos := isWordInSlice(wordToAdd)
		if pos < 0 {
			//new word
			wordList = Append(wordList, Word{wordToAdd, 1, 0})
		} else {
			//increase word count
			wordList[pos].count++
		}
	}
	for c, val := range wordList {
		if val != nil {
			wordList[c].tf_idf = calculateTF_IDF(val)
		}
	}

	//sort based on TF_IDF
	sort.Sort(ByIt_idf{wordList})
	printWords(wordList)

}

func stripPuncuation(word string) string {
	rv := word
	for _, val := range puncsToRemove {
		rv = strings.Replace(rv, val, "", 100)
	}
	return rv

}

func isWordInSlice(word string) int {
	for p, v := range wordList {
		if v != nil {
			if strings.ToLower(v.thisWord) == strings.ToLower(word) {
				return p
			}
		}
	}
	return -1
}

func Append(slice []*Word, wordToAdd Word) []*Word {
	l := len(slice)
	newSlice := make([]*Word, (1 + len(slice)), (1 + cap(slice)))
	copy(newSlice, slice)
	slice = newSlice
	slice[l] = &wordToAdd
	return slice
}

//logbase2(idf) * tf
func calculateTF_IDF(word *Word) float64 {
	totalPages := 50000000000.0
	hits := getHits(word.thisWord)
	idf := totalPages / hits
	tf := float64(word.count)
	tf_idf := math.Log2(idf) * tf
	return tf_idf
}

func getHits(word string) float64 {
	url := fmt.Sprintf(googleUrl, apiKey, cx, word)
	//post to this url and read response
	resp, _ := http.Get(url)
	body, _ := ioutil.ReadAll(resp.Body)

	var jsonData interface{}
	err := json.Unmarshal(body, &jsonData)
	if err == nil {
		m := jsonData.(map[string]interface{})
		searchInformation := m["searchInformation"].(map[string]interface{})

		rv, _ := strconv.ParseFloat(searchInformation["totalResults"].(string), 64)
		return rv
	}
	return 0
}

func printWords(s []*Word) {
	for _, o := range s {
		if o != nil {
			fmt.Printf("%-8s [%v](%v)\n", o.thisWord, o.count, o.tf_idf)
		}

	}
}

func printTopWords(s []*Word, numToPrint int) {
	for i := 0; i < numToPrint; i++ {
		if i < len(s) {
			if s[i] != nil {
				fmt.Printf("%-8s [%v](%v)\n", s[i].thisWord, s[i].count, s[i].tf_idf)
			}
		}
	}
}
