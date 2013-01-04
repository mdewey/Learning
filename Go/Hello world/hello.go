package main

import (
	"fmt"
)

func split(sum int) (x,y int){
	x = sum * 4/9
	y = sum - x
	return
}

var x,y,z int = 1,2,3


func main(){
	c, python, java := true, false, true
	fmt.Println(x, y, z, c, python, java)
}