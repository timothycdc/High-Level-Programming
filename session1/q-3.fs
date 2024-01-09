// Partial Application

let print x = printfn "%A" x

let divideByTwo x = (x / 2.0)
let divideSwap y x = x / y
let divideByTwoSwap = divideSwap 2.0


print (divideByTwo 40.0)
print (divideByTwoSwap 40.0)
