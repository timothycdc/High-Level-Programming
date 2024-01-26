let print x = printfn "%A" x
// List functions:
//  length, rev, map, fold, filter, tryFind, collect, indexed, max/min/sum, exists/forall,
// groupBy, partition, countBy, sort/sortDescending, sortBy/sortByDescending,
// zip, unzip, allPairs, pairwise, take, skip, takeWhile, skipWhile

// Q30. (R) Which of these functions are poor style in the sense that they
// can go wrong (and make your program fail with an exception)
// if given invalid inputs? for example: tryFind (returning an Option) can never fail,
// whereas the equivalent find function can fail and crash the whole program.
// Ans: We've had map improved by fold
// Prof Ans: max/min/sum, zip, take/skip


//  Map<'K,'V>
// aka dict or hash
// standard F# map is an immutable binary search tree
// not as fast as hash for lookup
// can create new maps with extra elems with low time/space as tree retains most of structure



// m is a Map (dictionary) value.
// note: Map != map !!!!
let k = "dog"
let m = Map [ "dog", 4; "human", 2 ]
let v = m[k] // m[k] is the value associated with key k in the dictionary

// Maps are immutable, adding returns a new one
let a: Map<string, int> = Map.empty // create an empty Map value
let a' = Map.add "first" 10 a // create an updated Map - NB this does not change a
let a'' = Map.add "second" 20 a'

let b =
    Map.ofList [ "April", 30; "June", 30; "September", 30; "November", 30; "February", 28 ]

// Try the above, then in fsi see what each of: a''["first"], a''["second"], a''[10], a''[""] do.
// Q31. Both a''[""] and a''[10] deliver errors. How are these errors different?
//  no key called "" exists
//  key type error: expected string, got int (10) instead

// Q32.
// function that returns number of days in a month
let months =
    Map.ofList
        [ "January", 31
          "February", 28
          "March", 31
          "April", 30
          "May", 31
          "June", 30
          "July", 31
          "August", 31
          "September", 30
          "October", 31
          "November", 30
          "December", 31 ]

let getDays month =
    match Map.tryFind month months with
    | Some days -> days
    | None -> failwith "Invalid month"

printfn "%A" "###########################################"
printfn "There are %d days in February" (getDays "February")

// Q33 (R) Write a function inverseMap: Map<'K,'V> -> Map<'V,'K>. You may assume that the value it is applied to is a 1-1 mapping, so that every key corresponds to a unique value and therefore the inverse Map is well defined.

let inverseMap map =
    let lst = Map.toList map
    (Map.empty, lst) ||> List.fold (fun acc (key, value) -> Map.add value key acc)

printfn "%A" "###########################################"
print "inverseMap Problem"

let test = Map.ofList [ 22, 1; 99, 2; 43, 3 ]

print test
print <| inverseMap test

// Q34. The m[x] syntax is sugar for a map function. What is this function, and its exact definition?

let sugarlessLookup k m = Map.find k m

print test[22]
print <| sugarlessLookup 22 test
