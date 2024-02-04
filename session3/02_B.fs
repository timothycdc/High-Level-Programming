// Q7. What elements does type A1 = | Monday1 of Unit | Tuesday1 of Unit have? In FSI compare it with type A2 = Monday2 | Tuesday2.

type A1 =
    | Monday1 of Unit
    | Tuesday1 of Unit

type A2 =
    | Monday2
    | Tuesday2


// Both A1 and A2 are the equivalent types which have two elements. The difference is in how they are constructed.
//  Monday2 and Tuesday2 are constant values of type A2, as expected. 
// Monday1 and Tuesday1 are constructor functions which when applied to () return the two values of A1. 
// In practice definitions like A1 are never used. We always use the simpler A2.


type MyOrchardDU = | NumAppleTrees of uint32 |  NumPearTrees of uint32 // note unsigned int, can't have negative trees
// can have any number of apple or pear trees but not both

type A = Stick | Hat
type B = Thomas | Peter | John | Emily
type C = Clarke | Smith | Thatcher
type MyDU = | Thing of A | Person of B * C | Weird

// Q8. What is the set equivalent of the Figure B.1 D.U. types?
// MyOrchardDU = Z u Z where Z = {0, 1, 2, 3, ...}
//  Typical set members are NumAppleTrees 3u, NumPearTrees 5u

//  myDU = A u (B x C) u {Weird}
// W = {w:w \in W} is a singleton set corresponding to the single Weird case value
//  Typical set members are Thing Stick, Person (Thomas, Clarke), 


// Q9. MyOrchardDU is not a perfect representation of orchards that can contain either apple or pear trees but not both. 
// One problem is in how an orchard with no trees is represented. Why is there a problem? 
// Think about this before you look at the answer.
// Ans: NumAppleTrees 0 and NumPearTrees 0 are both valid representations of an orchard with with no trees, this 
// redundancy could lead to bugs if one case type is dealt correctly but not the other 
//  Also equality (=) will distinguish between the two cases, although they are the same


// Q10-11. How many distinct values does MyDU have? Enumerate
// Ans: 2 + 4 x 3 + 1 = 15
// Thing Stick
// Thing Hat

// Person(Thomas, Clarke)
// Person(Thomas, Smith)
// Person(Thomas, Thatcher)

// Person(Peter, Clarke)
// Person(Peter, Smith)
// Person(Peter, Thatcher)

// Person(John, Clarke)
// Person(John, Smith)
// Person(John, Thatcher)

// Person(Emily, Clarke)
// Person(Emily, Smith)
// Person(Emily, Thatcher)

// Weird


// DUs are commonly used for enumeration


type DayOfWeek =
    | Monday
    | Tuesday
    | Wednesday
    | Thursday
    | Friday
    | Saturday
    | Sunday


// DUs can be used to make protected versions of normal types

type TPID = | projectID of int
// an int filed may be used as an identifier for a project record
//  such ids are wrapped in a DU so they are not mixed up with normal ints
// let copyProject (pid: int) (count:int) = 
// or...
// let copyProject (pid: TPID) (count:int) = 
// common miastake: calling a function with params swapped. protected types can detect this


// Q12: Consider replacing string types with DUs for tick2
type Course = MEng | BEng | MSc
type Boundary = Fail | Pass | Third | UpperSecond | LowerSecond | First | Merit | Distinction
// Q13: DUs can simplify code by allowing errors to be caught via
//  error checking. for Tick2, which functions markTotal, classify, upliftFunc, if any,
// can have simplier return types if DUs are used?

// upliftFunc only returns an error on wrong course - this is impossible if course value is represented as a D.U. which contains only valid courses. 
// therefore its return type can be simplified to float option. 
// markTotal, classify still need to check out of range marks and return None or Error in that case, however a check on course is no longer required.

// Q14: write a record type definition for the EEE dept db of people
type Role = | EEE | EIE | Staff
type UGYear = | One | Two | Three | Four
type EEEPerson = {
   eeid: int;
   name: string;
   role: Role;
   year: UGYear option;
   cid: int
   }

// Q15 What type does this function have? Why is it not possible to write a safe function for this operation of type int -> int?
 
type TUGYear = One | Two | Three | Four
let incrementUGYear = function
    | One -> Some Two
    | Two -> Some Three
    | Three -> Some Four
    | Four -> None

// Ans: this is because, in int, we could accidentally increment to 5, 6, 7, etc. which are not valid years.
// TUGYear -> Option<TUGYear> or equivalently TUGYear -> TUGYear option

// Tennis Game Problem
// Q16: consider type Player = PlayerOne | PlayerTwo, if DU was not available,
// what would be the alternative type definition for Player? Compare bool and string
// Better to use Bool, since binary states

type MaybeWonTennisGameScore = TennisGameScore OrWin


type 'a OrWin =
    | Win of Player
    | Game of 'a

// type PlayerScore = {Pts: int ; Advantage: bool; Deuce: bool}
// type TennisGameScoreBad = {playerOne: PlayerScore; playerTwo: PlayerScore}
// naive solution above is fine, but has many possible data values
// e.g both players have advantage
// can you do better?

// use state where if neither players have advantage, to show game not in deuce
// use state where both players have advantage, to show game in deuce
// state where one player has advantage is used as normal
type Player = PlayerOne | PlayerTwo
type TennisScore = Love0 | 15 | 30 | 40 | 

// mine: type TennisGameScore = {player1: TennisScore ; player2: TennisScore; player1adv: bool; player2adv: bool; } 
// better: 
type TennisGameScore = 
    | Points of PlayerPoints * PlayerPoints 
    | Advantage of Player // only possible when both Players have 40 points
    | Deuce 


