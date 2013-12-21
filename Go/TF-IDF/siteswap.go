package main

import (
    "fmt"
    "strconv"
    "math/rand"
    "time"
)

var maxThrow = 7
var ssLength = 6

func main() {
    rand.Seed(time.Now().Unix())
    isValid := false
    var swap []int
    for !isValid {
            swap = GenerateSiteSwap()
            isValid = ValideSiteSwap(swap)
    }
    fmt.Println(ConvertSiteSwapToString(swap))

}

func ValideSiteSwap(swap []int) bool {
    rv := false
    // make new queue length = highest number in array
    //find highest toss in the array
    //    pos := 0
    highest := -1
    for _,val := range swap {
        if val > highest {
            highest = val
        }
    }
    queue := make([]int, highest)
    for i := 0; i < 3; i++ {

     for _, val := range swap {
             // get toss
         thisToss := val
         // "do toss"
        if queue[thisToss - 1] != 0 {
            return false
         }
         queue[thisToss - 1] = thisToss
         // let gravity happen
         for i, _ :=  range queue {
             if ((i + 1) <= highest - 1) {
                 queue[i] = queue[i + 1]
             } else {
                 queue[i] = 0
             }
         }
         rv = true
     }
    }
    return rv
}

func GenerateSiteSwap() []int {
    length := rand.Intn(ssLength) + 5
    rv := make([]int, length)
    for i,_ := range rv {
        rv[i] = rand.Intn(maxThrow)+ 1
    }
    return rv
}

// change to a print function
func ConvertSiteSwapToString(swap []int) string {
    rv := ""
    for _, v := range swap {
        rv += strconv.Itoa(v)
    }

    return rv
}
