package main

import(
"fmt"
"math/cmplx"
)

func Cbrt(x complex128) (z complex128) {
    z = complex128(1)
    lastZ := complex128(0)
    delta := cmplx.Abs(z - lastZ)
    for delta * 100000 > 0.00001 * 100000 {
        //saving last z
        lastZ = z
        //getting new z
        z = z - (((z*z*z) - x) / (3*(z*z)))
        //figuring out the delta
        delta = cmplx.Abs(z - lastZ)
        }
    return
}

func main() {
    fmt.Println(Cbrt(2))
}
