//  reverse pipeline
// removes need for brackets around RHS expressions!
// using brackets:
// List.takeWhile (function
//     | false, ';' -> false
//     | _ -> true)

// using reverse pipeline:
// List.takeWhile <| function
//     | false, ';' -> false
//     | _ -> true

// using brackets:
// Error(sprintf "can't parse '%A' as an item" lst)

// using reverse pipeline:
// Error <| sprintf "can't parse '%A' as an item" lst

// Q25. Simplify the following code using pipeline or reverse pipeline operators

// let s = Error (sprintf "x in hex is 0x%x, x in decimal is %d" x x)
// let s = Error <| sprintf "x in hex is 0x%x, x in decimal is %d" x x

// Q26. Does List.map (fun x -> Error (sprintf "%x %d" x x)) lst
// translate into lst |> List.map <| fun x -> Error <| sprintf "%x %d" x x?
// No, leftmost operator takes prededence lst |> List.map
// lst |> List.map (fun x -> Error <| sprintf "%x %d" x x)

let test1 lst x =
    List.map (fun x -> Error(sprintf "%x %d" x x)) lst

let test2 lst x =
    lst |> List.map (fun x -> Error <| sprintf "%x %d" x x)




let printPipe x =
    printfn "%A" x
    x

let testPipe x =
    x
    |> List.pairwise
    |> printPipe // A
    |> List.indexed
    |> List.filter (fun (i, _) -> i % 2 <> 0)
    |> printPipe // B
    |> List.map (fun (a, (b, c)) -> a + b + c)
    |> List.sum

// Q27. Consider printPipe in the above figure. Which pipelines can it be inserted into to print intermediate values?
//  Ans: any pipeline

// Q28. (R) In the above figure will the printed data elements from A and B instances
//  of printPipe be interleaved: A[0], B[0], A[1], B[1] or
//  all of A first followed by all of B, or all of B followed by all of A?

// Answer: All of A then all of B
// F# is a strict lang where all of a function's args are evaluated before function
//  only untrue if sequence pipeline (on demand execution order), not in module
// Note: Haskell Lazy Eval is opposite of Strict Eval


// function | or |> are examples of what is called "point-free programming".
// Using functions directly, without named values, to simplify code.
// It can be helpful, but is often over-used by people falling in love with functional style.
//  >> is the function composition operator also often used in F# point-free programming:

let countElements lsts = (List.concat >> List.length) lsts // lsts needed to avoid value restriction (see videos)

let countElements listOfLists = List.concat listOfLists |> List.length

// Q29. Which of these do you think is clearer?
//  Ans: ambigious professor waffle:
// See the official F# style guide section on point-free programming for more information. My view is that those two definitions are about the same, the reduced noise of one is balanced by the additional context of the other. however in a more obscure operation the extra context given by the parameter would be invaluable, in a simpler one the noise reduction would make it easier to see the big picture.

//  My ans: the top is clearer, at least for simpler examples
