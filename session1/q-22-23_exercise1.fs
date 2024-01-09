let lst1 = [ 1..5 ]
let lst2 = [ 6..10 ]
let print x = printfn "%A" x

let makePair a b = (a, b)

// let makeColumn lst x =
//     let pairWithX = makePair x // Partially apply makePair.
//     List.map pairWithX lst // Use this to generate a
// // list of pairs with x

// let pairs = List.map (makeColumn lst2) lst1


// Q22. (R*) Modify the code above to define a function allPairs lst1 lst2 which generates all the pair-wise combinations of two lists.
let allPairs lst1 lst2 =
    let makeColumn lst x =
        let pairWithX = makePair x // Partially apply makePair.
        List.map pairWithX lst // Use this to generate a list of pairs with x

    List.map (makeColumn lst2) lst1


print (allPairs lst1 lst2)


// Q23. (R) The allPairs code can be made even shorter using anonymous functions.
// In fact, the use of an anonymous function makes the partial application unnecessary (but it is good to have practiced partial application because it will be useful in other contexts). Rewrite allpairs using an anonymous function.

let allPairs' lst1 lst2 =
    List.map (fun x -> List.map (fun y -> (x, y)) lst2) lst1

print (allPairs' lst1 lst2)
