let start = "start"
let q0 a b c = [ a, b ], c
let q0' a b c = [ a; b ], c
let q0'' a b c = (a, b), c

let q0''' a b c =
    (a
     b),
    c

let q1 = List.map (List.map (fun n -> n + 1))
// (‘V > ‘W > list<’V> -> list<’W>) (‘T > ‘U > list<’T> -> list<’U>) (‘T -> ‘T)
// (‘V > ‘W > list<’V> -> list<’W>) (list<’T> -> list<’T>)
// (list<’T> > list<’T>  > list<’T>  -> list<’T>)
// (‘T is int)


let q1' = ()
let q2 x y z w = (x y) (z w)
// w : ‘w
// z : ‘w -> ‘z
// (z w) : ‘z

// y : ‘y
// x : ‘y ->‘x
// (x y) :  ‘x

// Unify ‘x |-> (‘z -> ‘x):
// y : ‘y
// x : ‘y ->’z ->‘x
// (x y) :  ‘z ->‘x

// (x y) (z w) : ‘x
//  q2: (‘y -> ‘z ->‘x) -> ‘y -> (‘w -> ‘z ) -> ‘w -> x


let q4 f = id (fun x -> f x)
// id: ‘T -> ‘T
// f: (‘a -> ‘b)
// (fun x -> f x): ‘a -> ‘b
// Id (fun x -> f x): ‘a -> ‘b
// (‘a -> ‘b) -> (‘a -> ‘b)


let rec q5 n f = if n = 0 then id else f (q5 (n - 1) f)
// f: ‘a -> ‘a
// id: ‘T -> ‘T
// q5: int -> (‘a-> ‘a) -> ‘a
// since returning id, unify
// q5: int -> ((‘a-> ‘a) -> ‘a-> ‘a)  -> (‘a-> ‘a)


let q6 x y = x <| x y
// x (x y)

// y: ‘y
// x: ‘y –> ‘x
// x y: ‘x

// how to unify this: ‘y |-> ‘x
// y: ‘x
// x: ‘x –> ‘x
// x y: ‘x
// x: (x y) : ‘x

// so q6: (‘x -> ‘x) -> ‘x -> ‘x


let q7 = [ 1, [] ]
// q7: list<int * list<’a>>

let q8 = id q7
// passing q7 through id implicitly involves the type system
// id is generic and doesn't impose additional type constraints
// however, the way type inf wokrs in F# can lead to generalisations,
// leading to interactions with generic functions and the overall .NET type system
// this includes the obj type, which is the ultimate base type in .NET

// prof's long explanation
// The problem is that F# will not allow generics ('a) to stay generic
//  in that situation when generic data is fed to a any function as data
// (rather than being "bound" in a function signature: 'a -> 'a) because
//  it breaks type inference: the compiler cannot really tell what a function
//  will do with generic data and if it stores in it a mutable that will break
//  type inference if it is used for different data at different times.

// simplified: generic types are messy when not bound in a function signature
// defining q8 is a function that takes in q7 no longer bounds the generic type in q7
// unbound generic data = messy, what if you stored it in a mutable,
// type inference would break

// F# has to ensure that the operation 'id q7' is safe under
//  all possible types that 'a might represent.
//  functions can do all sorts of things with their inputs: storing them
//  in mutable variables, or pass them to other functions with stricter type reqs

let q9' = None
let q9 = 1 + Option.get q9'

let q10' = []

let q10 = id []

let q10'' = q10 @ [ 1 ]

let q11 b x y z = if b then x y else x z

type q12 = unit option option
// unit is a type with only one value, ()
// option is a type with two values, Some and None
// number of distinct values of that type: 3
//  unit, unit option, unit option option

type Q13 = Result<Unit, bool> * Result<bool -> bool -> bool, bool -> Unit>
// Q13 is a tuple of two types
// number of distinct values of that type: 3*(8+2) = 30
// Result<Unit, bool> has 3 values: Ok (), Error true, Error false
// Result<bool -> bool -> bool, bool -> Unit> can be broken down into
// Ok bool -> bool -> bool, Error bool -> Unit
// The first Ok has 8 possible values True or False chained in three, 2^3
// The second Error has 2 possible values, True -> () or False -> ()
// 8+2 = 10, 3*10 = 30


// suppose |'A|=a, |'B| = b
type Q14<'A, 'B> = Result<Result<'A, 'B>, Option<Result<'A, 'B>>> * bool
// innermost Result is either OK 'A or 'B so a + b types
// Since it's wrapped in an option, + 1 for None
// second level: a + b types for Result<'A,'B> and (a+b+1) for Option<Result<'A,'B>>
// outermost Result is a + b + 1 + a + b = 2a + 2b + 1
// since taking a tuple with bool, multiply by 2 = 2(2a + 2b + 1) = 4a + 4b + 2

let q15 = 6 |> (*) 5 |> (+) ((*) 3 4) |> (-) 0

// 42

let q16 =
    [ 0..10 ]
    |> List.partition (fun n -> n % 3 = 0)
    // splits at 3
    |> fun (a, b) -> List.rev a @ b
// [3;2;1;4;5;6;7;8;9;10]

let q17 =
    let thing x =
        function
        | x when x = 1 -> 2
        | _ -> 4

    thing 3 1
// 4
// how this works: the first x is a pattern match, the second x is a guard
// feed in 3,
