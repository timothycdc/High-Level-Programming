// map and reduce
// goal to write expo x n that calculates first n+1 terms of Taylor series expansion for e^x

// expo x n = \sum^n_{i=0} \frac{x^i}{i!}

let print x = printfn "%A" x

let fact n =
    if n = 0 then 1.0 else List.reduce (*) [ 1.0 .. float n ]

// let term x i =
//     (x ** float i) / fact i  // note how brackets are not needed around function application

let expo x n =
    let term i = (x ** float i) / (float (fact i))

    [ 0..n ] |> List.map term |> List.reduce (+)


// In Worksheet 2 you will find the library function that combines map and reduce.
// That would avoid the use of a pipe. Don't use it for now, since these exercises are about different ways to combine functions.

print (expo 1.0 1)
print (expo 1.0 2)
print (expo 1.0 3)
print (expo 1.0 4)
print (expo 1.0 5)
print (expo 1.0 10) //calculate e
