//  Finding all pairwise combos of elems in 2 lists

let print x = printfn "%A" x

let makePair a b = (a, b)

let lst1 = [ 1..5 ]
let lst2 = [ 6..10 ]

let pairWithOne = makePair 1

let combsWithOne = List.map pairWithOne lst2
// Q19
print "Q19"
print combsWithOne


// Q20 define a function makeColumn x that generates a list of pairs of x with each element of lst2
print "Q20"
let makeColumn x = List.map (makePair x) lst2
print (makeColumn 1)

// Q21. (*) Take your answer or the model answer from the previous question and modify it to take a list as input as well (i.e. makeColumn lst x).
print "Q21"
print (List.map makeColumn lst1)
