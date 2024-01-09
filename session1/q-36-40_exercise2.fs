// Calculating a polynomial
//  polynormial order N, P_N(x) at some point x is given by:
// \Sum ^N_{n=0} a_n x^n
let print x = printfn "%A" x

let anLst = [ 1.0; 0.5; 0.0; 0.25 ]

// use List.mapi, which is like List.map but also gives the index of the element
// example usecase: List.mapi (fun i x -> i, x) [ "a"; "b"; "c" ] // [(0, "a"); (1, "b"); (2, "c")]

// Q36. (R***) Using the same approach as in the previous example, define a function, poly x, that calculates the value of the polynomial at x. If you're stuck, the next questions enumerate the steps you could take

let poly x anLst =
    List.mapi (fun i anLstItem -> anLstItem * (x ** float i)) anLst
    |> List.reduce (+)

print (poly 0.0 anLst)
print (poly 1.0 anLst)
print (poly 10.0 anLst)

//  Q37. (R**) Define a function, term x n an that calculates a term from the summation using x, n (n), and an (an). Beware that ** needs floats.

let term x n an = an * (x ** float n)

print (term 1.0 1 0.5)

// Q38 solved in Q36

// Q39. (*) Use poly to calculate the value of the function at a set of discrete points (sample the polynomial).
let interval = 0.1
let linspace = List.map (fun x -> x * interval) [ 0.0 .. 10.0 ]

print (List.map (fun x -> poly x anLst) linspace)


//  Q40. (*) Extend your answer to work for arbitrary polynomials (i.e. make the coefficients an argument to your function poly coeffs x).
// already solved in Q36
