package main

import(
"fmt"
"math"
)

func Sqrt(x float64) (z float64) {
    z = float64(1)
    lastZ := float64(0)
    delta := z - lastZ
    for delta * 100000 > 0.00001 * 100000 {
        //saving last z
        lastZ = z
        //getting new z
        z = z - (((z*z) - x) / (2*z))
        //figuring out the delta
        delta = math.Abs(z - lastZ)
        }
    return
}

func main() {
    fmt.Println(Sqrt(2))
}
