let print x = printfn "%A" x
print "######################################################################################################"

type Names =
    { FirstName: string
      SecondName: string }

let tup1 = "Sean", "Bean"

let rec1 =
    { FirstName = "Sean"
      SecondName = "Bean" }


// Function to print information about a tuple
let printTupleInfo tuple =
    match tuple with
    | fstName, sndName -> sprintf "%s's second name is %s" fstName sndName

// Usage
let result = printTupleInfo tup1
printfn "%s" result


// Matching
let maxOf n m =
    match n > m with
    | true -> n
    | false -> m

let tryGetHead lst =
    match lst with
    | h :: t -> Some h // see 'Pattern matching on lists' below
    | [] -> None

let compute arg =
    match arg with
    | x, [ a; b ] -> x * (a / b) // see 'Matching deeper structure' below
    | x, [ a ] -> x * a
    | x, [] -> x
    | x, _ -> failwithf "Invalid argument {lst} to compute"

//  compute only works for tuples with length 2! match must be for the entire tuple
//  and the second elem of the tuple must be a list
//  otherwise wildcard _ is triggered and exception thrown


// anonymous function pattern match
let foo =
    function
    | Some x -> sprintf "%d" x
    | None -> "Nothing"

// This is equivalent to
let foo' opt =
    match opt with
    | Some x -> sprintf "%d" x
    | None -> "Nothing"


//  HM-type system: need to explicitly label as rec
//  otherwise type inference calls itself to oblivion

let rec map1 f lst =
    match lst with
    | hd :: tl -> (f hd) :: (map1 f tl)
    | [] -> []


// Q12. (R*) Define a recursive function, filter, to filter a list using a Boolean check. filter: ('a->bool) -> 'a list -> 'a list.

let rec filter boolCheckFunction lst =
    match lst with
    | hd :: tl ->
        match boolCheckFunction hd with
        | true -> hd :: (filter boolCheckFunction tl)
        | false -> (filter boolCheckFunction tl)
    | [] -> []


let isEven x =
    match x % 2 with
    | 0 -> true
    | _ -> false

let lst = [ 1; 2; 56; 345; 92; 5; 123; 667; 32 ]


print "\n"

print (filter isEven lst)


// Provided answer uses when(guard statement)
// let rec filter f lst =
//     match lst with
//     | hd::tl when f hd -> hd::(filter f tl)
//     | _::tl -> filter f tl
//     | [] -> []
// let tup = (1066,Some "Hastings")

//     match tup with
//     | year, Some battle when year < 1000 ->
//         printfn "The battle of %s was fought before the 10th century"
//     | year, Some battle when year > 1000 && year < 1100 ->
//         printfn "The battle of %s was fought in the 11th century"
//     | year, Some battle when year > 1100 ->
//         printfn "The battle of %s was fought after the 11th century"
//     | year, None ->
//         printfn "No battle in %d" year
//     | _ -> failwith "Shouldn't happen?"

// Q. Can the wildcard pattern _ in figure 22 ever match?
//  yes, because prof missed out 1000 and 1100

// FOLD
// fold is like reduce but it won't fail on empty lists because you specify a base case
//  let sum = List.fold (+) 0


// Reimplement factorial function using List.fold
let factorial_remastered n = [ 1..n ] |> List.fold (*) 1

print (factorial_remastered 1)
print (factorial_remastered 4)
print (factorial_remastered 9)

//  note List.fold has signature
// val fold:
//    folder: ('State -> 'T -> 'State) ->
//    state : 'State ->
//    list  : list<'T>
//         -> 'State

// compared to reduce
// val reduce:
//    reduction: ('T -> 'T -> 'T) ->
//    list     : list<'T>
//            -> 'T


// for reduce, output is type of initial state, not initial list

// Q14. (R**) Construct a function, using fold, that takes a list and reverses it. Remember that if we build a list using the cons operator, ::, then we are building it from tail to head (reverse order).

let rec reverseList lst =
    // list.fold takes function and init state
    // this function is fed the current iterated list and the current elem
    let backInsertion currList currElem = currElem :: currList
    lst |> List.fold (backInsertion) []

// let rev lst =
//     List.fold (fun lst el -> el::lst) [] lst

// let lst = [ 1; 2; 56; 345; 92; 5; 123; 667; 32 ]
let reversedList = reverseList lst
print reversedList

// Many recursive problems can be solved with fold
//  consider taking list of ints (int list) and transforming it to
// list of lists (int list list) where each sublist is a subset of the original list (power set)

// rule 1: the subsets of the empty set is the empty set
// rule 2: subsets(A u B) where A is singleton are
//  subsets(B) and pairwise union of A with subsets(B)

// Q15. (*) Which is the base case of the above recursive definition?
//  if B is empty, then subsets(B) = [] and since there are no subsets(B) to be union-paired with A, that is also []
//  that's just rule 1

let subsetsWithSingleton subsets singleton =
    List.allPairs subsets [ [ singleton ]; [] ]
    |> List.map (fun (x, y) -> List.append x y)

let subsets lst =
    List.fold subsetsWithSingleton [ [] ] lst


// Q16. (*) What happens if the last line of the example is changed to List.fold subsetsWithSingleton [] lst (initial state is now [])?
// Ans: Then no pairs are made with [[singleton];[]], then nothing is produced (empty list returned)

// Q17. (R**) Reimplement the subsets function using recursion (non tail recursive).
let rec subsetsRecursive lst =
    let subsets =
        // for each subset in tl_subsets, create two subsets: the original, and one with hd
        match lst with
        | hd :: tl ->
            let tl_subsets = (subsetsRecursive tl)
            List.append (List.map (fun x -> hd :: x) tl_subsets) tl_subsets
        | [] -> [ [] ]

    subsets

// after reviewing prof's answer, you could've used rule 2
// doing allPairs with [[hd];[]] instead
let lstx = [ 1; 4; 5; 432 ]
print "\n"
print "Subsets Problem"
print lstx
print (subsets lstx)
print (subsetsRecursive lstx)


// Prof's answer:
// let rec subsets lst =
//     match lst with
//     | hd::tl ->
//         List.allPairs [[hd];[]] (subsets tl)
//         |> List.map (fun (x,y) -> List.append x y)
//     | [] -> [[]]

// Q18: What is the base case of  Q17? Ans: empty list given, then subsets are [[]]

// Q19: one iterates bottom-up from empty basecase set [[]], the other one recurses top-down from the full list

// Q20. (R**) Write a tail recursive version of this function.
// That is the recursive function call should be the last call in the function. The function itself should be non-recursive but have a recursive subfunction.

let tail_recursive_subset lst =
    let rec tail_recursive_subset_helper lst subsets =
        match lst with
        | [] -> subsets
        | hd :: tl ->
            List.allPairs subsets [ [ hd ]; [] ]
            |> List.map (fun (x, y) -> List.append y x)
            |> tail_recursive_subset_helper tl

    tail_recursive_subset_helper lst [ [] ]


// SOLUTION
// let subsets lst =
//     let rec subsets' l subs =
//         match l with
//         | hd::tl ->
//             List.allPairs subs [[hd];[]]
//             |> List.map ((<||) List.append)
//             |> subsets' tl
//         | [] -> subs
//     subsets' lst [[]]
// (<||) is backwards pipe operator, applies func to args in reverse order
//  f <|| a <|| b is f b a

// Suppose you have two lists: list1 = [1; 2] and list2 = [3; 4].
// Normally, List.append list1 list2 would result in [1; 2; 3; 4].
// However, with ((<||) List.append), if you pass list1 and list2 to this new function,
// it flips the order of the arguments, so it's like doing List.append list2 list1,
// resulting in [3; 4; 1; 2].


print (tail_recursive_subset lstx)

// Q21. Implement a function using List.fold that returns the length of a list. (Equivalent to List.length).

let ListLength lst =
    (0, lst) ||> List.fold (fun n _ -> n + 1)

print ""
print "ListLength problem"
print (ListLength lstx)

// Q22 Implement function subLists lst using List.fold
//  result is list of all possible sublists of lst
//   A sublist of lst contains a subset of the elements of lst in the same order as in lst

// let subsetsWithSingleton subsets singleton =
//     List.allPairs subsets [ [ singleton ]; [] ]
//     |> List.map (fun (x, y) -> List.append x y)

// let subsets lst =
//     List.fold subsetsWithSingleton [ [] ] lst
let subLists lst =
    // let subListsMap subsets elem =
    //     List.allPairs subsets [ []; [ elem ] ]
    //     |> List.map (fun (x, y) -> List.append x y)
    let subListsMap subsets elem =
        List.collect (fun subset -> [ subset; elem :: subset ]) subsets

    ([ [] ], lst) ||> List.fold subListsMap



let subLists' x =
    /// returns lists of all original lists and the same with each list extended by adding el to head
    let folder lsts el =
        List.collect (fun lst -> [ lst; el :: lst ]) lsts

    ([ [] ], x) ||> List.fold folder |> List.map List.rev // li

print ""
print "subLists"
print (subLists lstx)
print (subLists' lstx)

// Q23 return list of n pseudorandom numbers in linear congruence rng
//  seed 1, recurrence Rn+1 = Rn * 1103515245+12345

let recurrence x _ = x * 1103515245 + 12345

let getRecurrenceList n = (1, [ 1..n ]) ||> List.scan recurrence

let lcg (n: int) =
    (1, [ 1..n ]) ||> List.scan (fun r _n -> r * 1103515245 + 12345)

print ""
print "Recurrent RNG Problem"
print (getRecurrenceList 5)
print (lcg 5)


// Q24 Recursion vs Fold: Ans: Fold, obvious
//  Prof's answer: no answer! There is no answer to this question! In some cases tail recursion is simpler, in some cases List.fold. See Part E for a lot of examples. While List.fold is more functional than tail recursion, it has the problem of often being very obscure and unreadable. However (as we will see in Tick 2), it can be very readable. More philosophically: what is readable depends on practice. Pure functional programmers (which Don Syme is not) who have never been taught loops would say that fold (and its variant scan) are very readable, and find loops difficult.
