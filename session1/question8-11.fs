let print x = printfn "%A" x

// ### Q8 Consider:

let add' x =
    let addx y = // Define a function which adds x.
        x + y

    addx // return the newly created addx function


// the body of add' contains a let definition followed by an expression (addx),
// both at the same indentation level.

// In general a block is any number of equally indented let definitions
// followed by a single expression at the same indent level.

// Q: Why does it not normally make any sense to have multiple expressions
// in a block?

// A: in FP a block can only return 1 value so any extra values are discarded
// F# is not purely functional so printfn have a side effect (printing)
// So it is ok to have them as part of a block above a main expression
// Where the main expression returns the value
// printfn expressions are usually discarded
// they have a Unit () type, see Sec7.2 pg 53 of TSFPL DoC Module Notes
// https://www.doc.ic.ac.uk/~svb/TSfPL/notes.pdf


// ### Q9  When the function addx is returned from add it may be used in
// contexts where add has been called again with a different value of x.
// Q: Does this matter?
// A: No, a named value is always a constant, when bound to adx then its
// value remains alive after add' returns, x is no longer in scope
// when add' called again x param is re-def'd, no connection between prev calls


// ### Q10. (R**) How would you write an explicitly Curried function for


let add3 x y z = x + y + z

let add3' x =
    let add3x y = // Define a function which adds x.
        let add3y z = // Define a function which adds x + y.
            x + y + z

        add3y

    add3x // return the newly created addx function



print <| add3 5 10 15
print <| add3' 5 10 15


//  ### Q11
// How would you write the C++ expression f(1+2,3,g(5,h(4))),
// where f,g are translated into equivalent curried functions, in F#,
// using the minimum number of brackets?


// f (1 + 2) 3 (g 5 (h 4))
