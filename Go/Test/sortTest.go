package main

import (
	"fmt"
	"sort"
)

type Word struct {
	thisWord string
	count    int
	tf_idf   float64
}

type Words []*Word

func (s Words) Len() int      { return len(s) }
func (s Words) Swap(i, j int) { s[i], s[j] = s[j], s[i] }

// ByName implements sort.Interface by providing Less and using the Len and
// Swap methods of the embedded Organs value.
type ByName struct{ Words }

func (s ByName) Less(i, j int) bool { return s.Words[i].thisWord < s.Words[j].thisWord }

// ByWeight implements sort.Interface by providing Less and using the Len and
// Swap methods of the embedded Organs value.
type ByWeight struct{ Words }

func (s ByWeight) Less(i, j int) bool {
	if s.Words[i] != nil && s.Words[j] != nil {
		return s.Words[i].tf_idf < s.Words[j].tf_idf
	}
	return false
}

func main() {
	s := []*Word{
		{"brain", 1, 1340},
		{"heart", 1, 290},
		{"liver", 1, 1494},
		{"pancreas", 1, 131},
		{"prostate", 1, 62},
		{"spleen", 1, 162},
	}

	s = Append(s, Word{"testing", 1, 0})

	sort.Sort(ByWeight{s})
	fmt.Println("Words by weight:")
	printWords(s)
	s[0].tf_idf = 2000

	sort.Sort(ByWeight{s})
	fmt.Println("Words by weight 2:")
	printWords(s)

}

func Append(slice []*Word, wordToAdd Word) []*Word {
	l := len(slice)
	newSlice := make([]*Word, (l + len(slice)), (l + cap(slice)))
	// The copy function is predeclared and works for any slice type.
	copy(newSlice, slice)
	slice = newSlice
	slice[l] = &wordToAdd

	return slice
}

func printWords(s []*Word) {
	for _, o := range s {
		if o != nil {
			fmt.Printf("%-8s (%v)\n", o.thisWord, o.tf_idf)
		}

	}
}
