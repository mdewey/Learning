package main

import "fmt"

// fibonacci is a function that returns
// a function that returns an int.
func fibonacci() func(int) int {
    return func(x int) int {
        previous := 0
        current := 1
        if x == 0 {
            return 0
        } else{
            for i := 0; i < x; i++ {
                newFib := previous + current
                previous = current
                current = newFib
            }
        }
        return current
    }
}

func main() {
	f := fibonacci()
	for i := 0; i <= 10; i++ {
		fmt.Print(f(i), ",")
	}
}
