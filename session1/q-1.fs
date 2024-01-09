open System


// -------Functions--------------
let double = ((*) 2)
let doubleList = List.map double
let print x = printfn "%A" x

[<EntryPoint>]
let main argv =
    print "Hello World from F#!"
    //------------Tests-----------
    print (doubleList [ 1; 2; 5 ])

    //---------------------------------
    Console.ReadKey() |> ignore // prevents window closing under VS Code
    0 // return an integer exit code


// call with main [||];; after loading in FSI
