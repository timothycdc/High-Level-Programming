// Partial
// In the definition of a function, does the order of parameters
// make a difference to how the function can be used?

// Depends on the function.


// **** This example does not depend on the order of parameters.
let AddTwoNums x y = x + y
print (AddTwoNums 1 2)
print (AddTwoNums 2 1)



// **** Some functions depend on the order of parameters.
let SubtractTwoNums x y = x - y
// a func that subtracts x from 1
let SubtractTwoNums1 = SubtractTwoNums 1
print (SubtractTwoNums1 5)
// to make a func that subtracts 1 from x, we need to swap the parameters
let SubtractTwoNums2' a b = b - a
let Decrement = SubtractTwoNums2' 1
print (Decrement 5)

// we can do a redefinition with swapped parameters
let SubtractTwoNumsSwapped x y = SubtractTwoNums y x
print (SubtractTwoNumsSwapped 1 5)
