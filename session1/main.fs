open System // * already in file, don't change

//-------Your functions--------------
let double = ((*) 2)
let doubleList = List.map double
//------------------------------------

[<EntryPoint>] // *
let main argv = // *
    printfn "Hello World from F#!" // *
    //------------Your tests-----------

    printfn "%A" (doubleList [ 1; 2; 5 ])

    //---------------------------------
    Console.ReadKey() |> ignore // prevents window closing under VS Code
    0 // return an integer exit code
