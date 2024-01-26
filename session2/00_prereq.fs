let print x = printfn "%A" x

// new string interp in F#

let x = 2
let name = "Brown"
// old syntax
printfn "x is %d, name is %s" x name
//string interpolation (no type checking of x, name)
printfn $"x is {x}, name is {name}"
// string interpolation with format specifier
printfn $"x is {x}, name is %s{name}"




let tuple1 = 1, 9
let tuple2 = "Bob Smith", 27
let tuple3 = "Bob", "Smith", 27.32
let tuple4 = 36, 6, 142
let tuple5 = "Westminster SW", 1
let tuple6 = 36, true

// Q1. (*) Hover your mouse over each tuple definition in Figure 1
//  and consider their types. Which ones, if any, have the same type?
// Ans: 2 and 5 have the same type (string * int)

// Records
/// record type definition with fields Age, Name, Email
/// line breaks are typical, but could be all on one line
type Person =
    { Age: int
      FirstName: string
      FamilyName: string
      Email: string }

/// constructing a data item of Person type
let tom =
    { Age = 61
      FirstName = "Thomas"
      FamilyName = "Clarke"
      Email = "tom@xx.co.uk" }

/// using copy and update to make a new item
let tomsTwin =
    { tom with
        FirstName = "Bob"
        Email = "bob@yy.co.uk" }

/// access fields of a record
let toTuple p =
    p.Age, p.FirstName, p.FamilyName, p.Email

/// accessing old item in construction of new item
/// note that this creates a new record, it does not chnage the old one
let ageUp p = { p with Age = p.Age + 1 }

/// match on part of a record
let printFamily =
    function
    | { FamilyName = "Clarke" } as p -> printfn $"{p.FirstName} is in the {p.FamilyName} family"
    | { FamilyName = "Smith" } as p -> printfn $"{p.FirstName} is in the {p.FamilyName} family"
    | { FirstName = name
        FamilyName = family } -> printfn $"{name} is in the {family} family"



// Q2. Write a function newEmail: string -> Person -> Person using record type defined above
// that will return a Person record with an altered email.

let newEmail newEmailAddress person = { person with Email = newEmailAddress }


printfn $"Tom's email is {tom.Email}"

let updatedTom = newEmail "newemail@domain.com" tom

printfn $"Tom's email is {updatedTom.Email}"



// Anonymous records
let example (a: int) (b: int) = {| Sum = a + b; Product = a * b |} // anonymous record - no type definition required

// halfway between tuples and records, good for one-off use, no typedef required as
// record fields inferred form usage
// Field names document components
// reduces type safety, unfortunately
// cannot do pattern matching on anonymous record fields

// Pattern matching

let tuple9 = (100, 300)
let x_, y_ = tuple9

print x_
print y_



let name_ = fst ("Bob", 13) // name = "Bob"
let age_ = snd ("Bob", 13) // age = 13

//  pattern matching with records

type Person2 =
    { // record type
      Name: string
      Age: int }


let person = { Name = "Mary"; Age = 21 } // sample record of Person type

let testRecordDeconstruction () =
    // use patterms to deconstruct
    let { Name = x } = person //pattern match to find x
    let { Name = x; Age = y } = person // pattern match to find x and y

    (x, y)




let a_tup1 = 2, 4
let a_tup2 = -1, 4
let a_tup3 = 2, 4

print (a_tup1 = a_tup2) //false
print (a_tup1 <> a_tup2) //true
print (a_tup1 = a_tup3) //true


// Q3. (*) Can we use inequalities like > to compare tuples?
// n fact we can, though it is not obvious. Tuples are ordered naturally using
// lexicographic ordering (as in a dictionary) taking the order in which tuples
// components are written left to right.

print ((2, 10) < (3, 1)) // true
print ((2, 10) < (2, 1)) // false
print ((2, 10) < (2, 20)) // true
