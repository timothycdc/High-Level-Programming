let square x = x * x
let add x y = x + y


// add has form int -> (int -> int)
// note right-associativity
// if you feed in one int, you get a function that
// takes another int and returns an int

// add 3 returns a function that adds 3 to a given int



// explicit currying

let add' x =
    let addx y = // Define a function which adds x.
        x + y

    addx // return the newly created addx function

// λx . λy . (x + y)
