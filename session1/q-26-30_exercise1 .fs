let print x = printfn "%A" x

let add x y = x + y

let sum = List.reduce add

let lst = [ 1..4 ]

let result = sum lst

print result

// Q26: (*) What is the type of sum?
// val sum: (int list -> int)


// Q27. (*) What is the value of result?
// val result: int = 10

// Q28. (*) Modify the code above to calculate the product of integers from 1 to 4.
let mul x y = x * y
let prod = List.reduce mul
let result1 = prod lst
print result1


// alternatively:
let product = List.reduce (*)
let result2 = product lst
print result2

// Q29. (R**) Write a func that finds n!. Remember you can use [a..b] where a or b can take any value
let fac n =
    if n > 0 then
        List.reduce (*) [ 1..n ]
    else if n = 0 then
        1
    else
        printfn "n must be non-negative"
        -1

print (fac 5)
print (fac 0)
print (fac (-1))

// Q30. (R*) List.append is a function that takes two lists and returns the two lists concatenated.
// Construct a function, using List.reduce, that concatenates all lists in a list and use it to concatenate the list
// [ ["Oh";"thoughtless";"mortals! "]; ["ever";"blind"];["to";"fate"] ].

let lst_to_concat =
    [ [ "Oh"; "thoughtless"; "mortals! " ]; [ "ever"; "blind" ]; [ "to"; "fate" ] ]
// to test the case of an empty lists
let empty_lst_to_concat = [ []; [] ]

// provided sol from the exercise
let concatLists lis =
    if lis = [] then [] else List.reduce List.append lis

print (concatLists lst_to_concat)
print (concatLists empty_lst_to_concat)

// another sol from me using List.fold
let concatLists' lis =
    if lis = [] then
        []
    else
        List.fold (fun acc x -> acc @ x) [] lis
// acc is accumulator that holds intermediate result
// x is the current element in the list
// @ is the list concatenation operator
// you could also do List.append acc x instead of acc @ x

print (concatLists' lst_to_concat)
print (concatLists' empty_lst_to_concat)
