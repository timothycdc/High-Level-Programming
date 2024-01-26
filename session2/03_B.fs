let print x = printfn "%A" x

let pickMultiple factor x = if x % factor = 0 then Some x else None

let lst1 = [ 1; -1; 6; 0; -3 ]
let lst2 = [ 2; 5; 65; 3 ]

let firstNegative = List.tryFind ((>) 0) // Why is this not <?

let res1 = firstNegative lst1
let res2 = firstNegative lst2

// Q8 why is it ((>) 0 ) and not ((<)0)?
// Ans: ((>)0) is equiv to fun x -> (>) 0 x which becomes 0 > x


print res1
print res2

// Q9 replace tryFind with find, what is the diff?
// print List.find ((>) 0) lst1
// print List.find ((>) 0) lst2
// Ans: Error, This value is not a function and cannot be applied.
// This is because find throws an exception when it fails, crashing the program

// Q10. Why can't (or shouldn't) we use a list for returning from such a function?
//  We could use the empty list to represent a failure and a singleton list to represent success.
// Ans: this is akin to a null value, which makes it difficult to ascertain errors from type output
//  Other answer: that is not how lists work because empty lists are 'allowed' and should not be
//  used to represent failures, otherwise, use a datatype that allows us to return 0 or 1 value
// Prof's answer: We want the output of our function to be clear to a user. By using an option type you are clearly saying to the user that the function might not return anything. (i.e. an empty list could represent a positive result but None means none)
//          Lists can have more than element. If we only want to return zero or one value we should use a data type which allows only that. This is precisely why types are so important, we want them to allow us to do what we say we are doing and nothing more!

//  Q11. (R***) Define a function, int list -> int, that finds the first negative integer in a list,
//  if no negative number is found then it finds the first positive integer in the list, if no negative or positive numbers are found then it returns zero. (Hint: you will need, List.tryFind, Option.orElse, and Option.defaultValue)


let findFirstNegElsePosElse0 lst =
    // Learn to use pipelines!!!
    // let conditions =
    //     Option.orElse (List.tryFind ((>) 0) lst) (List.tryFind ((<) 0) lst)
    // Option.defaultValue 0 conditions

    List.tryFind ((>) 0) lst
    |> Option.orElse (List.tryFind ((<) 0) lst)
    |> Option.defaultValue 0


// try a thunk
let foo lst =
    List.tryFind ((>) 0) lst
    |> Option.orElseWith (fun () -> List.tryFind ((<) 0) lst)
    |> Option.defaultValue 0

// test foo and findFirstNegElsePosElse0 with sample lists
let lst3 = [ 1; 2; 3; 4; 5 ]
let lst4 = [ -1; -2; -3; -4; -5 ]
let lst5 = [ 0; 1; 2; 3; 4; 5 ]

let lst6 = [ 0; 0 ]

print (findFirstNegElsePosElse0 lst3)
print (findFirstNegElsePosElse0 lst4)
print (findFirstNegElsePosElse0 lst5)
print (findFirstNegElsePosElse0 lst6)
print (foo lst3)
print (foo lst4)
print (foo lst5)
print (foo lst6)
