let lst1 = [ 1..5 ]
let lst2 = [ 6..10 ]

let allThings makePairFunc lsta lstb =
    let makeColumn lst x =
        let pairWithX = makePairFunc x // Partially apply makePairFunc.
        List.map pairWithX lst // Use this to generate a list of pairs with x

    List.map (makeColumn lstb) lsta

let makePair a b = (a, b)

let makeSum a b = a + b

let pairs = allThings makePair lst1 lst2
let sums = allThings makeSum lst1 lst2

// Q24 (R)  On its own, makePair is a function that has a polymorphic type signature. Can you work out what it is?
// val makePair :  'a -> 'b -> 'a * 'b

// Q25. (**) Can you similarly work out the polymorphic type of allThings?
// val allThings :  (makePairFunc: 'a ->'b -> 'c) -> (lsta: 'a list) -> (lstb: 'b list) -> 'c list
