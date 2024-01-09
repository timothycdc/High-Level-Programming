// Q31. (**) Using your function from the previous question, modify our solution to exercise 1 such that the output of the function is a list of pairs (tuples).
let print x = printfn "%A" x
let lst1 = [ 1..5 ]
let lst2 = [ 6..10 ]

let allpairs lsta lstb =
    let concat lis =
        if lis = [] then [] else List.reduce List.append lis

    let makeColumn lst x = List.map (fun y -> (x, y)) lst
    let lstOfLsts = List.map (makeColumn lstb) lsta
    printfn "[debug]: \nlstofLsts is %A \n[/debug]" lstOfLsts
    concat lstOfLsts

print (allpairs lst1 lst2)

// use dedicated func
let allPairs' lsta lstb =
    let makeColumn lst x = List.map (fun y -> (x, y)) lst
    List.collect (makeColumn lstb) lsta

print (allPairs' lst1 lst2)


// Q32. (*) max is a built-in function in F# that returns the larger of its two arguments.
// Construct a function, using List.reduce, that returns the maximum of a non-empty list and use it to to find the maximum of a list of integers or floats.

let maxList lst = List.reduce max lst
let lst3 = [ 1; 2; -10; 17; 5; -1; -2; -3; 8 ]
print (maxList lst3)


// let lst1 = [1..5]
// let lst2 = [6..10]

// let makeColumn x =
//     List.map (fun y -> (x,y)) lst2
// List.collect makeColumn lst1

// Last line is equiv to
// let lstOfLsts = List.map makeColumn lst1    // Map produces list of lists.
// let lst = List.reduce List.append lstOfLsts // Combine using concatenation.

// Q33. (R**) Construct a function using List.collect to replicate each element of a list 2 times. For example, the list [1;2;3] should become [1;1;2;2;3;3].
let doublePlaceElem lst = List.collect (fun x -> [ x; x ]) lst
let lst = [ 1..5 ]
print (doublePlaceElem lst)

// Q34. (R ***) Abstract your previous answer, i.e. define a two argument function, repl n lst, that replicates elements of a list, lst, n times.
// (Hint: The tricky bit is replicating a single element n times. Try using [a..b] and a List.map to do this).

let repl n lst =
    List.collect (fun x -> [ 1..n ] |> List.map (fun _ -> x)) lst

print (repl 3 lst)


// It is worth noting that there is also a built-in list function called List.replicate that can (and normally should) be used instead of map.

let repl' n lst = List.collect (List.replicate n) lst
print (repl' 3 lst)


// Q35. (R**) Using a conditional statement, construct a function that removes all integers negative integers from a list.
// (Hint: Remember that your mapping function can return an empty list).
let remove_all_negints lst =
    List.collect (fun x -> if x < 0 then [] else [ x ]) lst

print (remove_all_negints [])
print (remove_all_negints lst3)

let remove_all_negints' lst = List.filter (fun x -> x >= 0) lst
print (remove_all_negints' [])
print (remove_all_negints' lst3)
