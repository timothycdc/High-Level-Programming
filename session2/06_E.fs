let print x = printfn "%A" x
// List functions:
//  length, rev, map, fold, filter, tryFind, collect, indexed, max/min/sum, exists/forall,
// groupBy, partition, countBy, sort/sortDescending, sortBy/sortByDescending,
// zip, unzip, allPairs, pairwise, take, skip, takeWhile, skipWhile

// Q30. (R) Which of these functions are poor style in the sense that they
// can go wrong (and make your program fail with an exception)
// if given invalid inputs? for example: tryFind (returning an Option) can never fail,
// whereas the equivalent find function can fail and crash the whole program.
// Ans: We've had map improved by fold
// Prof Ans: max/min/sum, zip, take/skip


//  Map<'K,'V>
// aka dict or hash
// standard F# map is an immutable binary search tree
// not as fast as hash for lookup
// can create new maps with extra elems with low time/space as tree retains most of structure



// m is a Map (dictionary) value.
// note: Map != map !!!!
let k = "dog"
let m = Map [ "dog", 4; "human", 2 ]
let v = m[k] // m[k] is the value associated with key k in the dictionary

// Maps are immutable, adding returns a new one
let a: Map<string, int> = Map.empty // create an empty Map value
let a' = Map.add "first" 10 a // create an updated Map - NB this does not change a
let a'' = Map.add "second" 20 a'

let b =
    Map.ofList [ "April", 30; "June", 30; "September", 30; "November", 30; "February", 28 ]

// Try the above, then in fsi see what each of: a''["first"], a''["second"], a''[10], a''[""] do.
// Q31. Both a''[""] and a''[10] deliver errors. How are these errors different?
//  no key called "" exists
//  key type error: expected string, got int (10) instead

// Q32.
// function that returns number of days in a month
let months =
    Map.ofList
        [ "January", 31
          "February", 28
          "March", 31
          "April", 30
          "May", 31
          "June", 30
          "July", 31
          "August", 31
          "September", 30
          "October", 31
          "November", 30
          "December", 31 ]

let getDays month =
    match Map.tryFind month months with
    | Some days -> days
    | None -> failwith "Invalid month"

printfn "%A" "###########################################"
printfn "There are %d days in February" (getDays "February")

// Q33 (R) Write a function inverseMap: Map<'K,'V> -> Map<'V,'K>. You may assume that the value it is applied to is a 1-1 mapping, so that every key corresponds to a unique value and therefore the inverse Map is well defined.

let inverseMap map =
    let lst = Map.toList map
    (Map.empty, lst) ||> List.fold (fun acc (key, value) -> Map.add value key acc)

printfn "%A" "###########################################"
print "inverseMap Problem"

let test = Map.ofList [ 22, 1; 99, 2; 43, 3 ]

print test
print <| inverseMap test

// Q34. The m[x] syntax is sugar for a map function. What is this function, and its exact definition?

let sugarlessLookup k m = Map.find k m

print test[22]
print <| sugarlessLookup 22 test



let xm = Map [ "first", "the"; "second", "cat" ]

let ym = Map [ "cat", 7; "the", 3 ]

let zm = Map [ [ 1; 2; 3 ], 5 ]

zm[[ 1; 2; 3 ]] // look up key in zm map (returns 5)

let ym' = Map.add "fox" 5 ym // new map with ("fox",5) added to ym.

// Q35: what is ym[xm["first"]]?
print ""
print "Q35:"
print ym[xm["first"]]
// xm["first"] should give "the"
//  ym["the"] should give 3

// Q36. What type are xm, ym and zm
// val xm: Map<string,string> = map [("first", "the"); ("second", "cat")]
// val ym: Map<string,int> = map [("cat", 7); ("the", 3)]
// val zm: Map<int list,int> = map [([1; 2; 3], 5)]

// Q37: Get a bunch of indexes
let ans37 = m |> Map.toList |> List.map fst
print ans37


// Q38
let data = "The quick brown fox jumps over the lazy dog"

let histogram (data: string) =
    data
    |> Seq.toList // convert string to list of char
    |> List.filter (fun c -> c <> ' ') // remove spaces
    |> List.groupBy id // group by identity function
    |> List.map (fun (c, cs) -> (c, List.length cs)) // map to (char, count)
    |> Map.ofList // convert to map

let hist = histogram data
print hist


// Q39  write pipeline function gap which returns the largest gap between two adjacent numbers
// elements in a list. thus [10;1;3;4] would have e largest gap of 6 (between 4 and 10)

let gap lst =
    lst
    |> List.sort
    |> List.pairwise
    |> List.map (fun (a, b) -> b - a) // NB this is not needed given List.sort!
    |> List.max

print ""
print "gap problem Q39"
print <| gap [ 10; 1; 3; 4 ]
print <| gap [ 1; 212; 322; 41 ]

print ([ 10; 1; 3; 4 ] |> List.sort)
print ([ 10; 1; 3; 4 ] |> List.sort |> List.pairwise)
print ([ 10; 1; 3; 4 ] |> List.sort |> List.pairwise |> List.map (fun (a, b) -> b - a))

print (
    [ 10; 1; 3; 4 ]
    |> List.sort
    |> List.pairwise
    |> List.map (fun (a, b) -> b - a)
    |> List.max
)


// Q40: simplify the previous answer using function composition >> instead of pipelines (|>)
let gap' lst =
    (List.sort >> List.pairwise >> List.map (fun (a, b) -> b - a) >> List.max) lst

print <| gap' [ 10; 1; 3; 4 ]


// Q41:
// Write a func mode:  which outputs the modal element of its input list and the number of repetitions. If there is more than one element with an equal number of repetitions return them all.
let modal lst =
    let counter = lst |> List.countBy id
    let max = counter |> List.map snd |> List.max
    let maxes = counter |> List.filter (fun (elem, count) -> count = max)
    maxes |> List.map fst



let ans41 = modal [ 1; 1; 1; 1; 2; 3; 4; 4; 4; 4 ] // breaks on empty list

// prof answer

// let mode lst =
//     let counts = lst |> List.countBy id |> List.sortByDescending snd

//     match counts with
//     | (m, cnt) :: _ ->
//         let modes = List.takeWhile (fun (x, cnt') -> cnt' = cnt) counts
//         List.map fst modes, cnt
//     | [] -> failwithf "Can't find mode of an empty list!"


// Q42

let sequences (a, b) =
    let start_vals = [ a..b ]
    let end_vals start_val = [ start_val..b ]
    List.collect (fun start_val -> List.map (fun end_val -> [ start_val..end_val ]) (end_vals start_val)) start_vals


// Alternates
// let sequences1 (a,b) =
//     List.allPairs [a..b] [a..b]
//     |> List.filter (fun (a,b) -> a <= b)
//     |> List.map (fun (a,b) ->[a..b])

// let sequences2 (a,b) =
//     List.allPairs [a..b] [a..b]
//     |> List.collect (fun (a,b) -> if a <= b then [[a..b]] else [])

let ans42 = sequences (3, 5)

// Q43 (R**) use the list.splitAt function to write functions insertElement and insertList that insert an element or a list into a list at a given index.

let insertElement elem lst splitIndex =
    let first, second = List.splitAt splitIndex lst
    first @ [ elem ] @ second

let insertList sublst lst splitIndex =
    let first, second = List.splitAt splitIndex lst
    first @ sublst @ second


print ""
print "insertElement insertList problem"
print <| insertElement 3 [ 1; 2; 3; 4; 5; 6; 7; 8; 9 ] 3
print <| insertList [ 999; 999; 999; 999 ] [ 1; 2; 3; 4; 5; 6; 7; 8; 9 ] 3

// Q44 rewrite insertElement with list slice operation
let insertElement' elem lst splitIndex =
    let b = splitIndex - 1
    let c = splitIndex
    let d = List.length lst
    (lst[0..b]) @ [ elem ] @ (lst[c..d])

print <| insertElement' 3 [ 1; 2; 3; 4; 5; 6; 7; 8; 9 ] 3

//  Q45

let mergesort lst =
    let rec merge a b =
        match a, b with
        | [], _ -> b
        | _, [] -> a
        | headA :: tailA, headB :: tailB ->
            if headA < headB then
                headA :: merge tailA b
            else
                headB :: merge a tailB
    // prof implementation:
    // | h1 :: t1, h2 :: t2 when h1 <= h2 ->
    //     h1 :: merge t1 l2
    // | x, [] ->
    //     x
    // | _ ->
    //     merge l2 l1


    let rec sort lst =
        match List.length lst with
        | 0
        | 1 -> lst
        | _ ->
            let a, b = List.splitAt (List.length lst / 2) lst // Correct the order of arguments
            merge (sort a) (sort b) // Use 'merge' to combine the lists

    sort lst

print ""
print "mergersort"
print <| mergesort [ 124; 5; 3421; 687; 4; 34; 123; 7; 78; 5; 234 ]



// Q46 Tabulate

//     tabulate [
//         ["aa" ; "abc"]
//         ["abcde"]
//         ["abc"; "a"; "axxx"]
//     ]

//     returns

// [ [ "aa   "; "abc"; "    " ]
//   [ "abcde"; "   "; "    " ]
//   [ "abc  "; "a  "; "axxx" ] ]

let tabulate data =

    //  ################# HELPFUL VARIABLES #################
    let colsCount = List.length data


    // Initialise a list to keep track of max data lengths for each column
    let textMaxLengths = List.replicate colsCount 0
    printfn $"textMaxLengths:{textMaxLengths}"

    //  ################# HELPER FUNCTIONS #################

    let padRowCell idealLength row = // add empty strings to balance out row lengths
        let shortfall = idealLength - List.length row

        if shortfall > 0 then
            row @ (List.replicate shortfall "")
        else
            row

    let padList idealLength lst padding = // pad a list of ints with zeroes.
        let shortfall = idealLength - List.length lst

        if shortfall > 0 then
            lst @ (List.replicate shortfall padding)
        else
            lst

    let padString idealLength (str: string) = // pad a string with spaces
        let shortfall = idealLength - str.Length

        if shortfall > 0 then
            str + (String.replicate shortfall " ")
        else
            str


    let rowDataToLengths row = // output a list of lengths for a list of strings, and pad for shorter rows.
        // example when colsCount = 3: ["hi";"there";"world"] -> [2, 5, 5]
        // example when colsCount = 4: ["hi";"there";"world"] -> [2, 5, 5, 0]
        row
        |> List.map (fun (s: string) -> s.Length)
        |> (fun lengths -> padList colsCount lengths 0)

    //  ################# FINDING MAX COLUMN LENGTHS #################
    let lengthsMatrix = List.map rowDataToLengths data

    // Max function for two args
    let checkMax currentMax currentElem =
        if currentMax < currentElem then currentElem else currentMax

    // Function that updates columns' max textLengths for a given row
    let updateCurrentTextMaxLengths currentTextMaxLengths row =
        List.map2 checkMax currentTextMaxLengths row

    // For each row, iterate through each string, and check the string length for the corresponding length in currentTextMaxLengths,
    // update accordingly with updateCurrentTextMaxLengths
    let finalTextMaxLengths =
        (textMaxLengths, lengthsMatrix) ||> List.fold updateCurrentTextMaxLengths

    printfn $"finalTextMaxLengths:{finalTextMaxLengths}"

    //  ################# HIGH-LEVEL TABLE OPERATIONS #################

    // Function that pads a row for a given list of finalTextMaxLengths
    let padRowText finalTextMaxLengths row =
        List.map2 padString finalTextMaxLengths row

    // pad data with empty strings as cells
    let dataPadded = List.map (padRowCell colsCount) data
    printfn $"dataPadded:{dataPadded}"

    // pad the texts for the data
    List.map (padRowText finalTextMaxLengths) dataPadded


print ""
print "tabulate problem"
// [ [ "aa"; "abc" ];
//   [ "abcde" ];
//   [ "abc"; "a"; "axxx" ] ]
let ans46 = tabulate [ [ "aa"; "abc" ]; [ "abcde" ]; [ "abc"; "a"; "axxx" ] ]


/// return the values used to display the adjustment slider based on current decade
// let getSliderData (sliderValue: float) =
//     let min,max,step =
//         match sliderValue with
//         |v when (v>=0.000000001 && v<0.00000001) -> 0.000000001,0.0000000099,0.0000000001
//         |v when (v>=0.00000001 && v<0.0000001) -> 0.00000001,0.000000099,0.000000001
//         |v when (v>=0.0000001 && v<0.000001) -> 0.0000001,0.00000099,0.00000001
//         |v when (v>=0.000001 && v<0.00001) -> 0.000001,0.0000099,0.0000001
//         |v when (v>=0.00001 && v<0.0001) -> 0.00001,0.000099,0.000001
//         |v when (v>=0.0001 && v<0.001) -> 0.0001,0.00099,0.00001
//         |v when (v>=0.001 && v<0.01) -> 0.001,0.0099,0.0001
//         |v when (v>=0.01 && v<0.1) -> 0.01,0.009,0.001
//         |v when (v>=0.1 && v<1) -> 0.1,0.99,0.01
//         |v when (v>=1 && v<10) -> 1,9.9,0.1
//         |v when (v>=10 && v<100) -> 10,99,1
//         |v when (v>=100 && v<1000) -> 100,990,10
//         |v when (v>=1000 && v<10000) -> 1000,9900,100
//         |v when (v>=10000 && v<100000) -> 10000,99999,1000
//         |v when (v>=100000 && v<1000000) -> 100000,999000,10000
//         |_ -> 1,10,0.01
//     {| MinVal = min ; MaxVal = max ; Step = step |}

let getSliderData' (sliderValue: float) =
    [ -9.0 .. 5.0 ]
    |> List.tryFind (fun d -> sliderValue >= 10. ** d && sliderValue < 10. ** (d + 1.))
    |> Option.map (fun d -> 10. ** d, 10. ** d * 9.9, 10. ** (d - 1.))
    |> Option.defaultValue (1., 10., 0.01)
    |> (fun (min, max, step) ->
        {| MinVal = min
           MaxVal = max
           Step = step |})


let ans47 = getSliderData' 0.0001
print ans47
