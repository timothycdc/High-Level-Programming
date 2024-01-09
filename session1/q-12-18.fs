let print x = printfn "%A" x

// ### Q12
// Valid lists
let list0 = []
let list1 = [ 1; 5; 6; 5; -1; 2 ]
let list2 = [ 1.0; 3.12; 0.01; -10.4 ]
let list3 = [ "I've"; "got"; "a"; "little"; "list" ]
let list4 = [ [ 1; 3 ]; [ 1 ]; [ -1; 3; -4 ]; [] ] // nested list
let list5 = [ 1..4 ] // generates a list
// [1;2;3;4]

// Invalid lists
// let list6 = [ 1; 0.1; 30.12; 10; 3 ]
// let list7 = [ "I've"; "got"; "them"; [ "on"; "the"; "list" ] ]

// ### A12: lists must all be same type, cannot mix int and float, etc

// ### Q13 (R*) What is the value and type of result from the Figure 14? (Without using the compiler!)
let square x = //(1)
    x * x

let squareList = List.map square //(2)

let result = squareList [ 1..4 ] //(3)
// ### A13: result = [1;4;9;16] : int list
print result


// ####### Anonymous Functions
let square' x = // This function...
    x * x

fun x -> x * x // Is equivalent to this.

let add x y = // This function...
    x + y

fun x y -> x + y // Is equivalent to this.


let squareList' = List.map (fun x -> x * x)

// ### Q14 (R*) Using an anonymous function, construct a function that increments each element of a list by 1.

let incList = List.map (fun x -> x + 1)
print (incList [ 1..5 ])


// ############
let gr4test x = x > 4

// let elGreaterThanFour = List.map gr4test
// let result = elGreaterThanFour [1;5;2;4]
// // result = [false;true;false;false]


// let elGreaterThanFour = List.map (fun x -> x > 4)
// let result = elGreaterThanFour [1;5;2;4]
// // result = [false;true;false;false]

// Q15(*) What is the type of test and what is the type of greaterThanFour?
// A15: test is a function that takes an int and returns a bool

// Q16. (R**) Construct a function that tests whether elements of a list are equal to 2 (the equality operator in F# is = not == as in C++)?
let equalToTwo = List.map (fun x -> x = 2)
print (equalToTwo [ 1..5 ])


// Q17. (R***) What happens if our mapping function (f in List.map f) is a function of two or more arguments? (Think back to partial application).
// We return a list of partially applied functions

// Polymorphic Types: Q18. (R) What is the type of List.map? Discover the answer and try to work out what this means.
// val map : ('a -> 'b) -> 'a list -> 'b list
