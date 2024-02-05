// For each of the unquoted identifiers qN below state its inferred F# type
let q1 x = (fst x) (snd x)
// ('a -> 'b) * 'a -> 'b

let q2 a b = [ [ a ]; b ]
// 'a -> list<'a> -> list<list<'a>>

let q3 x y z = x z
// ('a -> 'b) -> 'c ->'a -> 'b

let q4 x y z w = x (y w) (z w)
// ('a -> 'b -> 'c) -> ('d -> 'a) -> ('d -> 'b) -> 'd -> 'c

// For each type T below state the number of distinct values of that type |T|.
// For function types with a finite range two functions f, g are distinct if
// there exists x such that f x <> g x

type q5 = Option<Result<(bool option * bool * bool), bool option option>>
// 17

// if |'A| = a, give answer in terms of a
type q6<'A> = Result<'A * 'A, 'A>
// a^2 + a

// if |'A| = a, |'B| = b, give answer in terms of a, b
type q7<'A, 'B> = 'A -> Result<'B, 'A>
// a inputs
// (a + b) outputs
// (a + b)^(a) different functions

// If q8 is a valid function state what is the value of q8 x in the form 'ax + b',
// else say why it is invalid.
let q8 x =
    let y = x
    let x = x + 1
    let z = x + y
    x + y + z
// supposedly invalid because x is redefined, however, it compiles, and we get (x+1) + x + (x+1 + x) = 4x + 2
// what?

let q8Example = q8 5

// For each identifier below state its value
let q9 =
    let f x y =
        let n = y * y

        match x with
        | n -> 10 // n is a variable pattern match, not n = y*y
        | 5 -> 5
        | _ -> 1

    f 5 5 // 10

let q10 =
    let rec f g a b =
        match a with
        | 0 -> b
        | n when n % 2 = 0 -> f g (a - 1) (g b)
        | _ -> f g (a - 1) b

    f (fun n -> n * 2) 10 1
//  32

// NB Q11 & Q12 are significantly more difficult than the other questions.
let q11 =
    let g f x = x |> f |> f |> f // g f x = f^3 x
    let a n m f = n f >> m f
    a (a g g) (fun f x -> f x) (fun x -> x + 1) 0
// 7

let q12 =
    let g f x = x |> f |> f |> f // g f x = f^3 x
    let a n m f = n f >> m f
    let b n m f = m (n f)
    b (a g (b g g)) g (fun x -> x + 10) 0
//  360
