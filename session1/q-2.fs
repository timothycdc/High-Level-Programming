// Pipe-forward operator |>
// The pipe-forward operator |> is syntactic sugar
// used to chain functions together, for chained method calls.

// The result of the expression on the left is passed as the last
// argument to the function on the right.

// This is useful when you want to chain together a series of
// functions that take only one argument.


// It is defined as let (|>) x f = f x

// Example:
let double = ((*) 2)
let doubleList = List.map double
let print x = printfn "%A" x

print [ 1; 2; 5 ]
// is equivalent to
[ 1; 2; 5 ] |> print



print (doubleList [ 1; 2; 5 ])
// is equivalent to
[ 1; 2; 5 ] |> doubleList |> print
// which is also equivalent to
print <| doubleList [ 1; 2; 5 ]
